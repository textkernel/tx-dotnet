// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API;
using Sovren.Models.API.Account;
using Sovren.Models.API.BimetricScoring;
using Sovren.Models.API.Geocoding;
using Sovren.Models.API.Indexes;
using Sovren.Models.API.Matching;
using Sovren.Models.API.Matching.Request;
using Sovren.Models.API.Matching.UI;
using Sovren.Models.API.Parsing;
using Sovren.Models.Job;
using Sovren.Models.Matching;
using Sovren.Models.Resume;
using Sovren.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sovren
{
    /// <summary>
    /// The SDK client to perform Sovren API calls. Should be used in conjunction with one of:<br/>
    /// <see cref="Sovren.Services.ParsingService"/><br/>
    /// <see cref="Sovren.Services.BimetricScoringService"/><br/>
    /// <see cref="Sovren.Services.AIMatchingService"/><br/>
    /// <see cref="Sovren.Services.IndexService"/><br/>
    /// </summary>
    public class SovrenClient
    {
        private readonly Credentials _credentials = new Credentials();
        private readonly RestClient _httpClient;
        private readonly DataCenter _dataCenter;
        private readonly ApiEndpoints _endpoints;

        /// <summary>
        /// The <see cref="ApiResponseInfo.CustomerDetails"/> from the most recent request
        /// </summary>
        public AccountInfo LatestCustomerDetails { get; private set; }

        /// <summary>
        /// Set to <see langword="true"/> for debugging API errors. It will show the full JSON request body in <see cref="SovrenException.RequestBody"/>
        /// <br/><b>NOTE: do not set this to <see langword="true"/> in your production system, as it increases the memory footprint</b>
        /// </summary>
        public bool ShowFullRequestBodyInExceptions { get; set; }

        /// <summary>
        /// Create an SDK client to perform Sovren API calls with the account information found at <see href="https://portal.sovren.com/"/>
        /// </summary>
        /// <param name="accountId">The account id for your account</param>
        /// <param name="serviceKey">The service key for your account</param>
        /// <param name="dataCenter">The Data Center for your account. Either <see cref="DataCenter.US"/> or <see cref="DataCenter.EU"/></param>
        public SovrenClient(string accountId, string serviceKey, DataCenter dataCenter)
        {
            if (string.IsNullOrEmpty(accountId))
                throw new ArgumentNullException(nameof(accountId));

            if (string.IsNullOrEmpty(serviceKey))
                throw new ArgumentNullException(nameof(serviceKey));

            //do not validate credentials here, as this could lead to calling GetAccount for every parse call, an AUP violation
            _credentials.AccountId = accountId;
            _credentials.ServiceKey = serviceKey;
            _dataCenter = dataCenter;
            _endpoints = new ApiEndpoints(_dataCenter);

            _httpClient = new RestClient(_dataCenter.Root);
            _httpClient.Headers.Add("Sovren-AccountId", _credentials.AccountId);
            _httpClient.Headers.Add("Sovren-ServiceKey", _credentials.ServiceKey);
        }

        private void ProcessResponse<T>(RestResponse<T> response, string requestBody) where T : ISovrenResponse
        {
            if (response == null || response.Data == null)
            {
                //this should really never happen, but just in case...
                throw new SovrenException(requestBody, response, new ApiResponseInfoLite { Code = "Error", Message = "Unknown API error." }, null);
            }

            if (!response.IsSuccessful)
            {
                throw new SovrenException(requestBody, response, response.Data.Info);
            }

            if (response?.Data?.Info?.CustomerDetails != null)
            {
                LatestCustomerDetails = response.Data.Info.CustomerDetails;
            }

            //TODO: much more error handling here?
        }

        private void ProcessResponse(RestResponse<GenerateUIResponse> response, string requestBody)
        {
            if (!response.IsSuccessful)
            {
                //this is a little bit wonky since the matching ui does not follow the sovren standard API response format
                string transId = "matchui-" + DateTime.Now.ToString();
                throw new SovrenException(requestBody, response, new ApiResponseInfoLite { Code = "Error", Message = response.Content }, transId);
            }
        }

        private string GetBodyIfDebug(RestRequest request)
        {
            if (ShowFullRequestBodyInExceptions)
            {
                return request.GetBody();
            }

            return null;
        }

        private string SerializeJson(object o)
        {
            return JsonSerializer.Serialize(o, SovrenJsonSerialization.DefaultOptions);
        }

        internal async Task<ParseResumeResponse> ParseResume(ParseRequest request)
        {
            RestRequest apiRequest = _endpoints.ParseResume();
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<ParseResumeResponse> response = await _httpClient.ExecuteAsync<ParseResumeResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));

            if (response.Data.Value.ParsingResponse != null && !response.Data.Value.ParsingResponse.IsSuccess)
            {
                throw new SovrenException(GetBodyIfDebug(apiRequest), response, response.Data.Value.ParsingResponse, response.Data.Info.TransactionId);
            }

            if (response.Data.Value.GeocodeResponse != null && !response.Data.Value.GeocodeResponse.IsSuccess)
            {
                throw new SovrenGeocodeResumeException(response, response.Data.Value.GeocodeResponse, response.Data.Info.TransactionId, response.Data);
            }

            if (response.Data.Value.IndexingResponse != null && !response.Data.Value.IndexingResponse.IsSuccess)
            {
                throw new SovrenIndexResumeException(response, response.Data.Value.IndexingResponse, response.Data.Info.TransactionId, response.Data);
            }

            return response.Data;
        }

        internal async Task<ParseJobResponse> ParseJob(ParseRequest request)
        {
            RestRequest apiRequest = _endpoints.ParseJobOrder();
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<ParseJobResponse> response = await _httpClient.ExecuteAsync<ParseJobResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));

            if (response.Data.Value.ParsingResponse != null && !response.Data.Value.ParsingResponse.IsSuccess)
            {
                throw new SovrenException(GetBodyIfDebug(apiRequest), response, response.Data.Value.ParsingResponse, response.Data.Info.TransactionId);
            }

            if (response.Data.Value.GeocodeResponse != null && !response.Data.Value.GeocodeResponse.IsSuccess)
            {
                throw new SovrenGeocodeJobException(response, response.Data.Value.GeocodeResponse, response.Data.Info.TransactionId, response.Data);
            }

            if (response.Data.Value.IndexingResponse != null && !response.Data.Value.IndexingResponse.IsSuccess)
            {
                throw new SovrenIndexJobException(response, response.Data.Value.IndexingResponse, response.Data.Info.TransactionId, response.Data);
            }

            return response.Data;
        }

        internal async Task<GetAccountInfoResponse> GetAccountInfo()
        {
            RestRequest apiRequest = _endpoints.GetAccountInfo();
            RestResponse<GetAccountInfoResponse> response = await _httpClient.ExecuteAsync<GetAccountInfoResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<CreateIndexResponse> CreateIndex(IndexType type, string indexId)
        {
            CreateIndexRequest request = new CreateIndexRequest
            {
                IndexType = type
            };

            RestRequest apiRequest = _endpoints.CreateIndex(indexId);
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<CreateIndexResponse> response = await _httpClient.ExecuteAsync<CreateIndexResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<GetAllIndexesResponse> GetAllIndexes()
        {
            RestRequest apiRequest = _endpoints.GetAllIndexes();
            RestResponse<GetAllIndexesResponse> response = await _httpClient.ExecuteAsync<GetAllIndexesResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<DeleteIndexResponse> DeleteIndex(string indexId)
        {
            RestRequest apiRequest = _endpoints.DeleteIndex(indexId);
            RestResponse<DeleteIndexResponse> response = await _httpClient.ExecuteAsync<DeleteIndexResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<IndexDocumentResponse> AddDocumentToIndex(ParsedResume resume, IndexSingleDocumentInfo options)
        {
            IndexResumeRequest requestBody = new IndexResumeRequest
            {
                ResumeData = resume,
                UserDefinedTags = options.UserDefinedTags
            };

            RestRequest apiRequest = _endpoints.IndexResume(options.IndexId, options.DocumentId);
            apiRequest.AddJsonBody(SerializeJson(requestBody));
            RestResponse<IndexDocumentResponse> response = await _httpClient.ExecuteAsync<IndexDocumentResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<IndexDocumentResponse> AddDocumentToIndex(ParsedJob job, IndexSingleDocumentInfo options)
        {
            IndexJobRequest requestBody = new IndexJobRequest
            {
                JobData = job,
                UserDefinedTags = options.UserDefinedTags
            };
            
            RestRequest apiRequest = _endpoints.IndexJob(options.IndexId, options.DocumentId);
            apiRequest.AddJsonBody(SerializeJson(requestBody));
            RestResponse<IndexDocumentResponse> response = await _httpClient.ExecuteAsync<IndexDocumentResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<IndexMultipleDocumentsResponse> AddMultipleDocumentsToIndex(IEnumerable<IndexResumeInfo> resumes, string indexId)
        {
            IndexMultipleResumesRequest requestBody = new IndexMultipleResumesRequest
            {
                Resumes = resumes.ToList()
            };

            RestRequest apiRequest = _endpoints.IndexMultipleResumes(indexId);
            apiRequest.AddJsonBody(SerializeJson(requestBody));
            RestResponse<IndexMultipleDocumentsResponse> response = await _httpClient.ExecuteAsync<IndexMultipleDocumentsResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<IndexMultipleDocumentsResponse> AddMultipleDocumentsToIndex(IEnumerable<IndexJobInfo> jobs, string indexId)
        {
            IndexMultipleJobsRequest requestBody = new IndexMultipleJobsRequest
            {
                Jobs = jobs.ToList()
            };

            RestRequest apiRequest = _endpoints.IndexMultipleJobs(indexId);
            apiRequest.AddJsonBody(SerializeJson(requestBody));
            RestResponse<IndexMultipleDocumentsResponse> response = await _httpClient.ExecuteAsync<IndexMultipleDocumentsResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<DeleteDocumentResponse> DeleteDocumentFromIndex(string indexId, string documentId)
        {
            RestRequest apiRequest = _endpoints.DeleteDocument(indexId, documentId);
            RestResponse<DeleteDocumentResponse> response = await _httpClient.ExecuteAsync<DeleteDocumentResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<DeleteMultipleDocumentsResponse> DeleteMultipleDocumentsFromIndex(string indexId, IEnumerable<string> documentIds)
        {
            RestRequest apiRequest = _endpoints.DeleteMultipleDocuments(indexId);
            apiRequest.AddJsonBody(SerializeJson(documentIds.ToList()));
            RestResponse<DeleteMultipleDocumentsResponse> response = await _httpClient.ExecuteAsync<DeleteMultipleDocumentsResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<GetResumeResponse> GetResumeFromIndex(string indexId, string documentId)
        {
            RestRequest apiRequest = _endpoints.GetResume(indexId, documentId);
            RestResponse<GetResumeResponse> response = await _httpClient.ExecuteAsync<GetResumeResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<GetJobResponse> GetJobFromIndex(string indexId, string documentId)
        {
            RestRequest apiRequest = _endpoints.GetJob(indexId, documentId);
            RestResponse<GetJobResponse> response = await _httpClient.ExecuteAsync<GetJobResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<UpdateUserDefinedTagsResponse> UpdateResumeUserDefinedTags(string indexId, string documentId, UpdateUserDefinedTagsRequest request)
        {
            RestRequest apiRequest = _endpoints.UpdateResumeUserDefinedTags(indexId, documentId);
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<UpdateUserDefinedTagsResponse> response = await _httpClient.ExecuteAsync<UpdateUserDefinedTagsResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<UpdateUserDefinedTagsResponse> UpdateJobUserDefinedTags(string indexId, string documentId, UpdateUserDefinedTagsRequest request)
        {
            RestRequest apiRequest = _endpoints.UpdateJobUserDefinedTags(indexId, documentId);
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<UpdateUserDefinedTagsResponse> response = await _httpClient.ExecuteAsync<UpdateUserDefinedTagsResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }


        internal async Task<MatchResponse> Match(MatchResumeRequest request)
        {
            RestRequest apiRequest = _endpoints.MatchResume(false);
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<MatchResponse> response = await _httpClient.ExecuteAsync<MatchResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<MatchResponse> Match(MatchJobRequest request)
        {
            RestRequest apiRequest = _endpoints.MatchJob(false);
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<MatchResponse> response = await _httpClient.ExecuteAsync<MatchResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<MatchResponse> Match(string indexId, string documentId, MatchByDocumentIdOptions options)
        {
            RestRequest apiRequest = _endpoints.MatchByDocumentId(indexId, documentId, false);
            apiRequest.AddJsonBody(SerializeJson(options));
            RestResponse<MatchResponse> response = await _httpClient.ExecuteAsync<MatchResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<SearchResponse> Search(SearchRequest request)
        {
            RestRequest apiRequest = _endpoints.Search(false);
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<SearchResponse> response = await _httpClient.ExecuteAsync<SearchResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<BimetricScoreResponse> BimetricScore(BimetricScoreResumeRequest request)
        {
            RestRequest apiRequest = _endpoints.BimetricScoreResume(false);
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<BimetricScoreResponse> response = await _httpClient.ExecuteAsync<BimetricScoreResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<BimetricScoreResponse> BimetricScore(BimetricScoreJobRequest request)
        {
            RestRequest apiRequest = _endpoints.BimetricScoreJob(false);
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<BimetricScoreResponse> response = await _httpClient.ExecuteAsync<BimetricScoreResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<GenerateUIResponse> UISearch(UISearchRequest request)
        {
            RestRequest apiRequest = _endpoints.Search(true);
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<GenerateUIResponse> response = await _httpClient.ExecuteAsync<GenerateUIResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<GenerateUIResponse> UIMatch(string indexId, string documentId, UIMatchByDocumentIdOptions options)
        {
            RestRequest apiRequest = _endpoints.MatchByDocumentId(indexId, documentId, true);
            apiRequest.AddJsonBody(SerializeJson(options));
            RestResponse<GenerateUIResponse> response = await _httpClient.ExecuteAsync<GenerateUIResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<GenerateUIResponse> UIMatch(UIMatchResumeRequest request)
        {
            RestRequest apiRequest = _endpoints.MatchResume(true);
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<GenerateUIResponse> response = await _httpClient.ExecuteAsync<GenerateUIResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<GenerateUIResponse> UIMatch(UIMatchJobRequest request)
        {
            RestRequest apiRequest = _endpoints.MatchJob(true);
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<GenerateUIResponse> response = await _httpClient.ExecuteAsync<GenerateUIResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<GenerateUIResponse> UIBimetricScore(UIBimetricScoreResumeRequest request)
        {
            RestRequest apiRequest = _endpoints.BimetricScoreResume(true);
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<GenerateUIResponse> response = await _httpClient.ExecuteAsync<GenerateUIResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<GenerateUIResponse> UIBimetricScore(UIBimetricScoreJobRequest request)
        {
            RestRequest apiRequest = _endpoints.BimetricScoreJob(true);
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<GenerateUIResponse> response = await _httpClient.ExecuteAsync<GenerateUIResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<GeocodeResumeResponse> Geocode(GeocodeResumeRequest request)
        {
            RestRequest apiRequest = _endpoints.GeocodeResume();
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<GeocodeResumeResponse> response = await _httpClient.ExecuteAsync<GeocodeResumeResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<GeocodeJobResponse> Geocode(GeocodeJobRequest request)
        {
            RestRequest apiRequest = _endpoints.GeocodeJob();
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<GeocodeJobResponse> response = await _httpClient.ExecuteAsync<GeocodeJobResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<GeocodeAndIndexResumeResponse> GeocodeAndIndex(GeocodeAndIndexResumeRequest request)
        {
            RestRequest apiRequest = _endpoints.GeocodeAndIndexResume();
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<GeocodeAndIndexResumeResponse> response = await _httpClient.ExecuteAsync<GeocodeAndIndexResumeResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));

            if (!request.IndexIfGeocodeFails && response.Data.Value.GeocodeResponse != null && !response.Data.Value.GeocodeResponse.IsSuccess)
            {
                throw new SovrenException(GetBodyIfDebug(apiRequest), response, response.Data.Value.GeocodeResponse, response.Data.Info.TransactionId);
            }

            if (response.Data.Value.IndexingResponse != null && !response.Data.Value.IndexingResponse.IsSuccess)
            {
                throw new SovrenException(GetBodyIfDebug(apiRequest), response, response.Data.Value.IndexingResponse, response.Data.Info.TransactionId);
            }

            return response.Data;
        }

        internal async Task<GeocodeAndIndexJobResponse> GeocodeAndIndex(GeocodeAndIndexJobRequest request)
        {
            RestRequest apiRequest = _endpoints.GeocodeAndIndexJob();
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<GeocodeAndIndexJobResponse> response = await _httpClient.ExecuteAsync<GeocodeAndIndexJobResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));

            if (!request.IndexIfGeocodeFails && response.Data.Value.GeocodeResponse != null && !response.Data.Value.GeocodeResponse.IsSuccess)
            {
                throw new SovrenException(GetBodyIfDebug(apiRequest), response, response.Data.Value.GeocodeResponse, response.Data.Info.TransactionId);
            }

            if (response.Data.Value.IndexingResponse != null && !response.Data.Value.IndexingResponse.IsSuccess)
            {
                throw new SovrenException(GetBodyIfDebug(apiRequest), response, response.Data.Value.IndexingResponse, response.Data.Info.TransactionId);
            }

            return response.Data;
        }
    }
}

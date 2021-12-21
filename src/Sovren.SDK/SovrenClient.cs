// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models;
using Sovren.Models.API;
using Sovren.Models.API.Account;
using Sovren.Models.API.BimetricScoring;
using Sovren.Models.API.Formatter;
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
    /// The SDK client to perform Sovren API calls.
    /// </summary>
    public class SovrenClient : ISovrenClient
    {
        private readonly RestClient _httpClient;
        private readonly ApiEndpoints _endpoints;
       
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
        /// <param name="dataCenter">The Data Center for your account. Either <see cref="DataCenter.US"/>, <see cref="DataCenter.EU"/>, or <see cref="DataCenter.AU"/></param>
        /// <param name="trackingTags">Optional tags to use to track API usage for your account</param>
        public SovrenClient(string accountId, string serviceKey, DataCenter dataCenter, params string[] trackingTags)
        {
            if (string.IsNullOrEmpty(accountId))
                throw new ArgumentNullException(nameof(accountId));

            if (string.IsNullOrEmpty(serviceKey))
                throw new ArgumentNullException(nameof(serviceKey));

            if (dataCenter == null)
                throw new ArgumentNullException(nameof(dataCenter));

            _endpoints = new ApiEndpoints(dataCenter);

            //do not validate credentials here, as this could lead to calling GetAccount for every parse call, an AUP violation
            _httpClient = new RestClient(dataCenter.Root);
            _httpClient.Headers.Add("Sovren-AccountId", accountId);
            _httpClient.Headers.Add("Sovren-ServiceKey", serviceKey);

            if (trackingTags != null && trackingTags.Length > 0)
            {
                string tagsHeaderValue = string.Join(", ", trackingTags);
                if (tagsHeaderValue.Length >= 75)//API allows 100, but just to be safe, this should be way more than enough
                {
                    throw new ArgumentException("Too many values or values are too long", nameof(trackingTags));
                }

                _httpClient.Headers.Add("Sovren-TrackingTag", tagsHeaderValue);
            }
        }

        private void ProcessResponse<T>(RestResponse<T> response, string requestBody) where T : ISovrenResponse
        {
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.RequestEntityTooLarge)
            {
                throw new SovrenException(requestBody, response, new ApiResponseInfoLite { Code = "Error", Message = "Request body was too large." }, null);
            }

            if (response != null && response.Data == null)
            {
                //this happens when its a non-Sovren 404 or a 500-level error
                string message = $"{(int)response.StatusCode} - {response.StatusDescription}";
                throw new SovrenException(requestBody, response, new ApiResponseInfoLite { Code = "Error", Message = message }, null);
            }

            if (response == null || response.Data == null)
            {
                //this should really never happen, but just in case...
                throw new SovrenException(requestBody, response, new ApiResponseInfoLite { Code = "Error", Message = "Unknown API error." }, null);
            }

            if (!response.IsSuccessful)
            {
                throw new SovrenException(requestBody, response, response.Data.Info);
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

        /// <summary>
        /// Get the account info (remaining credits, max concurrency, etc).
        /// </summary>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<GetAccountInfoResponse> GetAccountInfo()
        {
            RestRequest apiRequest = _endpoints.GetAccountInfo();
            RestResponse<GetAccountInfoResponse> response = await _httpClient.ExecuteAsync<GetAccountInfoResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        /// <summary>
        /// Format a parsed resume into a standardized/templated resume
        /// </summary>
        /// <param name="request">The request body</param>
        /// <returns>The formatted resume document</returns>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        public async Task<FormatResumeResponse> FormatResume(FormatResumeRequest request)
        {
            RestRequest apiRequest = _endpoints.FormatResume();
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<FormatResumeResponse> response = await _httpClient.ExecuteAsync<FormatResumeResponse>(apiRequest);

            return response.Data;
        }

        #region Parsing

        /// <summary>
        /// Parse a resume
        /// </summary>
        /// <param name="request">The request body</param>
        /// <returns>The parse result and any metadata</returns>
        /// <exception cref="SovrenException">Thrown when a parsing or API error occurred</exception>
        /// <exception cref="SovrenGeocodeResumeException">Thrown when parsing was successful, but an error occurred during geocoding</exception>
        /// <exception cref="SovrenIndexResumeException">Thrown when parsing was successful, but an error occurred during indexing</exception>
        public async Task<ParseResumeResponse> ParseResume(ParseRequest request)
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


        /// <summary>
        /// Parse a job
        /// </summary>
        /// <param name="request">The request body</param>
        /// <returns>The parse result and any metadata</returns>
        /// <exception cref="SovrenException">Thrown when a parsing or API error occurred</exception>
        /// <exception cref="SovrenGeocodeJobException">Thrown when parsing was successful, but an error occurred during geocoding</exception>
        /// <exception cref="SovrenIndexJobException">Thrown when parsing was successful, but an error occurred during indexing</exception>
        public async Task<ParseJobResponse> ParseJob(ParseRequest request)
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

        #endregion

        #region Indexes

        /// <summary>
        /// Create a new index
        /// </summary>
        /// <param name="type">The type of documents stored in this index. Either 'Resume' or 'Job'</param>
        /// <param name="indexId">
        /// The ID to assign to the new index. This is restricted to alphanumeric with dashes 
        /// and underscores. All values will be converted to lower-case.
        /// </param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<CreateIndexResponse> CreateIndex(IndexType type, string indexId)
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

        /// <summary>
        /// Get all existing indexes
        /// </summary>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<GetAllIndexesResponse> GetAllIndexes()
        {
            RestRequest apiRequest = _endpoints.GetAllIndexes();
            RestResponse<GetAllIndexesResponse> response = await _httpClient.ExecuteAsync<GetAllIndexesResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        /// <summary>
        /// Delete an existing index. Note that this is a destructive action and 
        /// cannot be undone. All the documents in this index will be deleted.
        /// </summary>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<DeleteIndexResponse> DeleteIndex(string indexId)
        {
            RestRequest apiRequest = _endpoints.DeleteIndex(indexId);
            RestResponse<DeleteIndexResponse> response = await _httpClient.ExecuteAsync<DeleteIndexResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        #endregion

        #region Documents

        /// <summary>
        /// Add a resume to an existing index
        /// </summary>
        /// <param name="resume">A resume generated by the Sovren Resume Parser</param>
        /// <param name="indexId">The index the document should be added into (case-insensitive).</param>
        /// <param name="documentId">
        /// The ID to assign to the new document. This is restricted to alphanumeric 
        /// with dashes and underscores. All values will be converted to lower-case.
        /// </param>
        /// <param name="userDefinedTags">The user-defined tags that the resume should have</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<IndexDocumentResponse> IndexDocument(ParsedResume resume, string indexId, string documentId, IEnumerable<string> userDefinedTags = null)
        {
            IndexResumeRequest requestBody = new IndexResumeRequest
            {
                ResumeData = resume,
                UserDefinedTags = userDefinedTags?.ToList()
            };

            RestRequest apiRequest = _endpoints.IndexResume(indexId, documentId);
            apiRequest.AddJsonBody(SerializeJson(requestBody));
            RestResponse<IndexDocumentResponse> response = await _httpClient.ExecuteAsync<IndexDocumentResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        /// <summary>
        /// Add a job to an existing index
        /// </summary>
        /// <param name="job">A job generated by the Sovren Job Parser</param>
        /// <param name="indexId">The index the document should be added into (case-insensitive).</param>
        /// <param name="documentId">
        /// The ID to assign to the new document. This is restricted to alphanumeric 
        /// with dashes and underscores. All values will be converted to lower-case.
        /// </param>
        /// <param name="userDefinedTags">The user-defined tags that the job should have</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<IndexDocumentResponse> IndexDocument(ParsedJob job, string indexId, string documentId, IEnumerable<string> userDefinedTags = null)
        {
            IndexJobRequest requestBody = new IndexJobRequest
            {
                JobData = job,
                UserDefinedTags = userDefinedTags?.ToList()
            };
            
            RestRequest apiRequest = _endpoints.IndexJob(indexId, documentId);
            apiRequest.AddJsonBody(SerializeJson(requestBody));
            RestResponse<IndexDocumentResponse> response = await _httpClient.ExecuteAsync<IndexDocumentResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        /// <summary>
        /// Add several resumes to an existing index
        /// </summary>
        /// <param name="resumes">The resumes generated by the Sovren Resume Parser paired with their DocumentIds</param>
        /// <param name="indexId">The index the resumes should be added into (case-insensitive).</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<IndexMultipleDocumentsResponse> IndexMultipleDocuments(IEnumerable<IndexResumeInfo> resumes, string indexId)
        {
            IndexMultipleResumesRequest requestBody = new IndexMultipleResumesRequest
            {
                Resumes = resumes?.ToList()
            };

            RestRequest apiRequest = _endpoints.IndexMultipleResumes(indexId);
            apiRequest.AddJsonBody(SerializeJson(requestBody));
            RestResponse<IndexMultipleDocumentsResponse> response = await _httpClient.ExecuteAsync<IndexMultipleDocumentsResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        /// <summary>
        /// Add several jobs to an existing index
        /// </summary>
        /// <param name="jobs">The jobs generated by the Sovren Job Parser paired with their DocumentIds</param>
        /// <param name="indexId">The index the jobs should be added into (case-insensitive).</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<IndexMultipleDocumentsResponse> IndexMultipleDocuments(IEnumerable<IndexJobInfo> jobs, string indexId)
        {
            IndexMultipleJobsRequest requestBody = new IndexMultipleJobsRequest
            {
                Jobs = jobs?.ToList()
            };

            RestRequest apiRequest = _endpoints.IndexMultipleJobs(indexId);
            apiRequest.AddJsonBody(SerializeJson(requestBody));
            RestResponse<IndexMultipleDocumentsResponse> response = await _httpClient.ExecuteAsync<IndexMultipleDocumentsResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        /// <summary>
        /// Delete an existing document from an index
        /// </summary>
        /// <param name="indexId">The index containing the document</param>
        /// <param name="documentId">The ID of the document to delete</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<DeleteDocumentResponse> DeleteDocument(string indexId, string documentId)
        {
            RestRequest apiRequest = _endpoints.DeleteDocument(indexId, documentId);
            RestResponse<DeleteDocumentResponse> response = await _httpClient.ExecuteAsync<DeleteDocumentResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        /// <summary>
        /// Delete a group of existing documents from an index
        /// </summary>
        /// <param name="indexId">The index containing the documents</param>
        /// <param name="documentIds">The IDs of the documents to delete</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<DeleteMultipleDocumentsResponse> DeleteMultipleDocuments(string indexId, IEnumerable<string> documentIds)
        {
            RestRequest apiRequest = _endpoints.DeleteMultipleDocuments(indexId);
            apiRequest.AddJsonBody(SerializeJson(new { DocumentIds = documentIds?.ToList() }));
            RestResponse<DeleteMultipleDocumentsResponse> response = await _httpClient.ExecuteAsync<DeleteMultipleDocumentsResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        /// <summary>
        /// Retrieve an existing resume from an index
        /// </summary>
        /// <param name="indexId">The index containing the resume</param>
        /// <param name="documentId">The ID of the resume to retrieve</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<GetResumeResponse> GetResume(string indexId, string documentId)
        {
            RestRequest apiRequest = _endpoints.GetResume(indexId, documentId);
            RestResponse<GetResumeResponse> response = await _httpClient.ExecuteAsync<GetResumeResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        /// <summary>
        /// Retrieve an existing job from an index
        /// </summary>
        /// <param name="indexId">The index containing the job</param>
        /// <param name="documentId">The ID of the job to retrieve</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<GetJobResponse> GetJob(string indexId, string documentId)
        {
            RestRequest apiRequest = _endpoints.GetJob(indexId, documentId);
            RestResponse<GetJobResponse> response = await _httpClient.ExecuteAsync<GetJobResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        /// <summary>
        /// Updates the user-defined tags for a resume
        /// </summary>
        /// <param name="indexId">The index containing the resume</param>
        /// <param name="documentId">The ID of the resume to update</param>
        /// <param name="userDefinedTags">The user-defined tags to add/delete/etc</param>
        /// <param name="method">Which method to use for the specified user-defined tags</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<UpdateUserDefinedTagsResponse> UpdateResumeUserDefinedTags(
            string indexId,
            string documentId,
            IEnumerable<string> userDefinedTags,
            UserDefinedTagsMethod method)
        {
            UpdateUserDefinedTagsRequest requestBody = new UpdateUserDefinedTagsRequest
            {
                UserDefinedTags = userDefinedTags?.ToList(),
                Method = method
            };

            RestRequest apiRequest = _endpoints.UpdateResumeUserDefinedTags(indexId, documentId);
            apiRequest.AddJsonBody(SerializeJson(requestBody));
            RestResponse<UpdateUserDefinedTagsResponse> response = await _httpClient.ExecuteAsync<UpdateUserDefinedTagsResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }


        /// <summary>
        /// Updates the user-defined tags for a job
        /// </summary>
        /// <param name="indexId">The index containing the job</param>
        /// <param name="documentId">The ID of the job to update</param>
        /// <param name="userDefinedTags">The user-defined tags to add/delete/etc</param>
        /// <param name="method">Which method to use for the specified user-defined tags</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<UpdateUserDefinedTagsResponse> UpdateJobUserDefinedTags(
            string indexId,
            string documentId,
            IEnumerable<string> userDefinedTags,
            UserDefinedTagsMethod method)
        {
            UpdateUserDefinedTagsRequest requestBody = new UpdateUserDefinedTagsRequest
            {
                UserDefinedTags = userDefinedTags?.ToList(),
                Method = method
            };

            RestRequest apiRequest = _endpoints.UpdateJobUserDefinedTags(indexId, documentId);
            apiRequest.AddJsonBody(SerializeJson(requestBody));
            RestResponse<UpdateUserDefinedTagsResponse> response = await _httpClient.ExecuteAsync<UpdateUserDefinedTagsResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        #endregion

        #region Matching

        /// <summary>
        /// Find matches for a non-indexed resume.
        /// </summary>
        /// <param name="resume">The resume (generated by the Sovren Resume Parser) to use as the source for a match query</param>
        /// <param name="indexesToQuery">The indexes to find results in. These must all be of the same type (resumes or jobs)</param>
        /// <param name="preferredWeights">
        /// The preferred category weights for scoring the results. If none are provided,
        /// Sovren will determine the best values based on the source resume
        /// </param>
        /// <param name="filters">Any filters to apply prior to the match (a result must satisfy all the filters)</param>
        /// <param name="settings">Settings for this match</param>
        /// <param name="numResults">The number of results to show. If not specified, the default will be used.</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<MatchResponse> Match(
            ParsedResume resume,
            IEnumerable<string> indexesToQuery,
            CategoryWeights preferredWeights = null,
            FilterCriteria filters = null,
            SearchMatchSettings settings = null,
            int numResults = 0)
        {
            MatchResumeRequest requestBody = CreateRequest(resume, indexesToQuery, preferredWeights, filters, settings, numResults);

            RestRequest apiRequest = _endpoints.MatchResume(false);
            apiRequest.AddJsonBody(SerializeJson(requestBody));
            RestResponse<MatchResponse> response = await _httpClient.ExecuteAsync<MatchResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal MatchResumeRequest CreateRequest(
            ParsedResume resume,
            IEnumerable<string> indexesToQuery,
            CategoryWeights preferredWeights,
            FilterCriteria filters,
            SearchMatchSettings settings,
            int numResults
            )
        {
            return new MatchResumeRequest()
            {
                ResumeData = resume,
                IndexIdsToSearchInto = indexesToQuery?.ToList(),
                PreferredCategoryWeights = preferredWeights,
                FilterCriteria = filters,
                Settings = settings,
                Take = numResults
            };
        }

        /// <summary>
        /// Find matches for a non-indexed job.
        /// </summary>
        /// <param name="job">The job (generated by the Sovren Job Parser) to use as the source for a match query</param>
        /// <param name="indexesToQuery">The indexes to find results in. These must all be of the same type (resumes or jobs)</param>
        /// <param name="preferredWeights">
        /// The preferred category weights for scoring the results. If none are provided,
        /// Sovren will determine the best values based on the source job
        /// </param>
        /// <param name="filters">Any filters to apply prior to the match (a result must satisfy all the filters)</param>
        /// <param name="settings">Settings for this match</param>
        /// <param name="numResults">The number of results to show. If not specified, the default will be used.</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<MatchResponse> Match(
            ParsedJob job,
            IEnumerable<string> indexesToQuery,
            CategoryWeights preferredWeights = null,
            FilterCriteria filters = null,
            SearchMatchSettings settings = null,
            int numResults = 0)
        {
            MatchJobRequest requestBody = CreateRequest(job, indexesToQuery, preferredWeights, filters, settings, numResults);

            RestRequest apiRequest = _endpoints.MatchJob(false);
            apiRequest.AddJsonBody(SerializeJson(requestBody));
            RestResponse<MatchResponse> response = await _httpClient.ExecuteAsync<MatchResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal MatchJobRequest CreateRequest(
            ParsedJob job,
            IEnumerable<string> indexesToQuery,
            CategoryWeights preferredWeights,
            FilterCriteria filters,
            SearchMatchSettings settings,
            int numResults)
        {
            return new MatchJobRequest()
            {
                JobData = job,
                IndexIdsToSearchInto = indexesToQuery?.ToList(),
                PreferredCategoryWeights = preferredWeights,
                FilterCriteria = filters,
                Settings = settings,
                Take = numResults
            };
        }

        /// <summary>
        /// Find matches for a resume or job that is already indexed
        /// </summary>
        /// <param name="indexId">The index containing the document you want to match</param>
        /// <param name="documentId">The ID of the document to match</param>
        /// <param name="indexesToQuery">The indexes to find results in. These must all be of the same type (resumes or jobs)</param>
        /// <param name="preferredWeights">
        /// The preferred category weights for scoring the results. If none are provided,
        /// Sovren will determine the best values based on the source resume/job
        /// </param>
        /// <param name="filters">Any filters to apply prior to the match (a result must satisfy all the filters)</param>
        /// <param name="settings">Settings for this match</param>
        /// <param name="numResults">The number of results to show. If not specified, the default will be used.</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<MatchResponse> Match(
            string indexId,
            string documentId,
            IEnumerable<string> indexesToQuery,
            CategoryWeights preferredWeights = null,
            FilterCriteria filters = null,
            SearchMatchSettings settings = null,
            int numResults = 0)
        {
            MatchByDocumentIdOptions requestBody = CreateRequest(indexesToQuery, preferredWeights, filters, settings, numResults);

            RestRequest apiRequest = _endpoints.MatchByDocumentId(indexId, documentId, false);
            apiRequest.AddJsonBody(SerializeJson(requestBody));
            RestResponse<MatchResponse> response = await _httpClient.ExecuteAsync<MatchResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal MatchByDocumentIdOptions CreateRequest(
            IEnumerable<string> indexesToQuery,
            CategoryWeights preferredWeights,
            FilterCriteria filters,
            SearchMatchSettings settings,
            int numResults)
        {
            return new MatchByDocumentIdOptions()
            {
                IndexIdsToSearchInto = indexesToQuery?.ToList(),
                PreferredCategoryWeights = preferredWeights,
                FilterCriteria = filters,
                Settings = settings,
                Take = numResults
            };
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

        #endregion

        #region Searching

        /// <summary>
        /// Search for resumes or jobs that meet specific criteria
        /// </summary>
        /// <param name="indexesToQuery">The indexes to find results in. These must all be of the same type (resumes or jobs)</param>
        /// <param name="query">The search query. A result must satisfy all of these criteria</param>
        /// <param name="settings">The settings for this search request</param>
        /// <param name="pagination">Pagination settings. If not specified the default will be used</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<SearchResponse> Search(
            IEnumerable<string> indexesToQuery,
            FilterCriteria query,
            SearchMatchSettings settings = null,
            PaginationSettings pagination = null)
        {
            SearchRequest requestBody = CreateRequest(indexesToQuery, query, settings, pagination);

            RestRequest apiRequest = _endpoints.Search(false);
            apiRequest.AddJsonBody(SerializeJson(requestBody));
            RestResponse<SearchResponse> response = await _httpClient.ExecuteAsync<SearchResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal SearchRequest CreateRequest(
            IEnumerable<string> indexesToQuery,
            FilterCriteria query,
            SearchMatchSettings settings,
            PaginationSettings pagination)
        {
            return new SearchRequest()
            {
                IndexIdsToSearchInto = indexesToQuery?.ToList(),
                FilterCriteria = query,
                Settings = settings,
                PaginationSettings = pagination
            };
        }

        internal async Task<GenerateUIResponse> UISearch(UISearchRequest request)
        {
            RestRequest apiRequest = _endpoints.Search(true);
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<GenerateUIResponse> response = await _httpClient.ExecuteAsync<GenerateUIResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        #endregion

        #region Bimetric Scoring

        /// <summary>
        /// Score one or more target documents against a source resume
        /// </summary>
        /// <param name="sourceResume">The source resume</param>
        /// <param name="targetDocuments">The target resumes/jobs</param>
        /// <param name="preferredWeights">
        /// The preferred category weights for scoring the results. If none are provided,
        /// Sovren will determine the best values based on the source resume
        /// </param>
        /// <param name="settings">Settings to be used for this scoring request</param>
        /// <typeparam name="TTarget">Either <see cref="ParsedResumeWithId"/> or <see cref="ParsedJobWithId"/></typeparam>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<BimetricScoreResponse> BimetricScore<TTarget>(
            ParsedResumeWithId sourceResume,
            List<TTarget> targetDocuments,
            CategoryWeights preferredWeights = null,
            SearchMatchSettings settings = null) where TTarget : IParsedDocWithId
        {
            BimetricScoreResumeRequest requestBody = CreateRequest(sourceResume, targetDocuments, preferredWeights, settings);

            RestRequest apiRequest = _endpoints.BimetricScoreResume(false);
            apiRequest.AddJsonBody(SerializeJson(requestBody));
            RestResponse<BimetricScoreResponse> response = await _httpClient.ExecuteAsync<BimetricScoreResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal BimetricScoreResumeRequest CreateRequest<TTarget>(
            ParsedResumeWithId sourceResume,
            List<TTarget> targetDocuments,
            CategoryWeights preferredWeights,
            SearchMatchSettings settings) where TTarget : IParsedDocWithId
        {
            return new BimetricScoreResumeRequest()
            {
                PreferredCategoryWeights = preferredWeights,
                Settings = settings,
                SourceResume = sourceResume,
                TargetResumes = targetDocuments as List<ParsedResumeWithId>,
                TargetJobs = targetDocuments as List<ParsedJobWithId>
            };
        }

        /// <summary>
        /// Score one or more target documents against a source job
        /// </summary>
        /// <param name="sourceJob">The source job</param>
        /// <param name="targetDocuments">The target resumes/jobs</param>
        /// <param name="preferredWeights">
        /// The preferred category weights for scoring the results. If none are provided,
        /// Sovren will determine the best values based on the source job
        /// </param>
        /// <param name="settings">Settings to be used for this scoring request</param>
        /// <typeparam name="TTarget">Either <see cref="ParsedResumeWithId"/> or <see cref="ParsedJobWithId"/></typeparam>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<BimetricScoreResponse> BimetricScore<TTarget>(
            ParsedJobWithId sourceJob,
            List<TTarget> targetDocuments,
            CategoryWeights preferredWeights = null,
            SearchMatchSettings settings = null) where TTarget : IParsedDocWithId
        {
            BimetricScoreJobRequest requestBody = CreateRequest(sourceJob, targetDocuments, preferredWeights, settings);

            RestRequest apiRequest = _endpoints.BimetricScoreJob(false);
            apiRequest.AddJsonBody(SerializeJson(requestBody));
            RestResponse<BimetricScoreResponse> response = await _httpClient.ExecuteAsync<BimetricScoreResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal BimetricScoreJobRequest CreateRequest<TTarget>(
            ParsedJobWithId sourceJob,
            List<TTarget> targetDocuments,
            CategoryWeights preferredWeights,
            SearchMatchSettings settings) where TTarget : IParsedDocWithId
        {
            return new BimetricScoreJobRequest()
            {
                PreferredCategoryWeights = preferredWeights,
                Settings = settings,
                SourceJob = sourceJob,
                TargetResumes = targetDocuments as List<ParsedResumeWithId>,
                TargetJobs = targetDocuments as List<ParsedJobWithId>
            };
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

        internal async Task<GenerateUIResponse> UIViewDetails(UIBimetricScoreResumeDetailsRequest request)
        {
            RestRequest apiRequest = _endpoints.ViewDetailsResume();
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<GenerateUIResponse> response = await _httpClient.ExecuteAsync<GenerateUIResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<GenerateUIResponse> UIViewDetails(UIBimetricScoreJobDetailsRequest request)
        {
            RestRequest apiRequest = _endpoints.ViewDetailsJob();
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<GenerateUIResponse> response = await _httpClient.ExecuteAsync<GenerateUIResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        internal async Task<GenerateUIResponse> UIViewDetails(UIMatchDetailsRequest request)
        {
            RestRequest apiRequest = _endpoints.ViewDetailsIndexed();
            apiRequest.AddJsonBody(SerializeJson(request));
            RestResponse<GenerateUIResponse> response = await _httpClient.ExecuteAsync<GenerateUIResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        #endregion

        #region Geocoding

        private async Task<GeocodeResumeResponse> InternalGeocode(ParsedResume resume, GeocodeCredentials geocodeCredentials, Address address = null)
        {
            GeocodeResumeRequest requestBody = new GeocodeResumeRequest
            {
                ResumeData = resume,
                Provider = geocodeCredentials?.Provider ?? GeocodeProvider.Google,
                ProviderKey = geocodeCredentials?.ProviderKey,
                PostalAddress = address
            };

            RestRequest apiRequest = _endpoints.GeocodeResume();
            apiRequest.AddJsonBody(SerializeJson(requestBody));
            RestResponse<GeocodeResumeResponse> response = await _httpClient.ExecuteAsync<GeocodeResumeResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        private async Task<GeocodeJobResponse> InternalGeocode(ParsedJob job, GeocodeCredentials geocodeCredentials, Address address = null)
        {
            GeocodeJobRequest requestBody = new GeocodeJobRequest
            {
                JobData = job,
                Provider = geocodeCredentials?.Provider ?? GeocodeProvider.Google,
                ProviderKey = geocodeCredentials?.ProviderKey,
                PostalAddress = address
            };

            RestRequest apiRequest = _endpoints.GeocodeJob();
            apiRequest.AddJsonBody(SerializeJson(requestBody));
            RestResponse<GeocodeJobResponse> response = await _httpClient.ExecuteAsync<GeocodeJobResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));
            return response.Data;
        }

        /// <summary>
        /// Uses the address in the resume (if present) to look up geocoordinates and add them into the ParsedResume object.
        /// These coordinates are used by the AI Searching/Matching engine.
        /// </summary>
        /// <param name="resume">The resume to geocode</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        public async Task<GeocodeResumeResponse> Geocode(ParsedResume resume, GeocodeCredentials geocodeCredentials = null)
        {
            return await InternalGeocode(resume, geocodeCredentials);
        }

        /// <summary>
        /// Use this if you would like to provide an address for geocoding instead of using the one in the parsed
        /// resume. The address included in the parsed resume (if present) will not be modified.
        /// </summary>
        /// <param name="resume">The resume to insert the geocoordinates (from the address) into</param>
        /// <param name="address">The address to use to retrieve geocoordinates</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        public async Task<GeocodeResumeResponse> Geocode(ParsedResume resume, Address address, GeocodeCredentials geocodeCredentials = null)
        {
            return await InternalGeocode(resume, geocodeCredentials, address: address);
        }

        /// <summary>
        /// Uses the address in the job (if present) to look up geocoordinates and add them into the ParsedJob object.
        /// These coordinates are used by the AI Searching/Matching engine.
        /// </summary>
        /// <param name="job">The job to geocode</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        public async Task<GeocodeJobResponse> Geocode(ParsedJob job, GeocodeCredentials geocodeCredentials = null)
        {
            return await InternalGeocode(job, geocodeCredentials);
        }

        /// <summary>
        /// Use this if you would like to provide an address for geocoding instead of using the one in the parsed
        /// job. The address included in the parsed job (if present) will not be modified.
        /// </summary>
        /// <param name="job">The job to insert the geocoordinates (from the address) into</param>
        /// <param name="address">The address to use to retrieve geocoordinates</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        public async Task<GeocodeJobResponse> Geocode(ParsedJob job, Address address, GeocodeCredentials geocodeCredentials = null)
        {
            return await InternalGeocode(job, geocodeCredentials, address: address);
        }

        private async Task<GeocodeAndIndexResumeResponse> InternalGeocodeAndIndex(ParsedResume resume, GeocodeCredentials geocodeCredentials, IndexSingleDocumentInfo indexingOptions, bool indexIfGeocodeFails, Address address = null, GeoCoordinates coordinates = null)
        {
            GeocodeAndIndexResumeRequest requestBody = new GeocodeAndIndexResumeRequest
            {
                ResumeData = resume,
                GeocodeOptions = new GeocodeOptionsBase
                {
                    Provider = geocodeCredentials?.Provider ?? GeocodeProvider.Google,
                    ProviderKey = geocodeCredentials?.ProviderKey,
                    PostalAddress = address,
                    GeoCoordinates = coordinates
                },
                IndexingOptions = indexingOptions,
                IndexIfGeocodeFails = indexIfGeocodeFails
            };

            RestRequest apiRequest = _endpoints.GeocodeAndIndexResume();
            apiRequest.AddJsonBody(SerializeJson(requestBody));
            RestResponse<GeocodeAndIndexResumeResponse> response = await _httpClient.ExecuteAsync<GeocodeAndIndexResumeResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));

            if (!requestBody.IndexIfGeocodeFails && response.Data.Value.GeocodeResponse != null && !response.Data.Value.GeocodeResponse.IsSuccess)
            {
                throw new SovrenException(GetBodyIfDebug(apiRequest), response, response.Data.Value.GeocodeResponse, response.Data.Info.TransactionId);
            }

            if (response.Data.Value.IndexingResponse != null && !response.Data.Value.IndexingResponse.IsSuccess)
            {
                throw new SovrenException(GetBodyIfDebug(apiRequest), response, response.Data.Value.IndexingResponse, response.Data.Info.TransactionId);
            }

            return response.Data;
        }

        private async Task<GeocodeAndIndexJobResponse> InternalGeocodeAndIndex(ParsedJob job, GeocodeCredentials geocodeCredentials, IndexSingleDocumentInfo indexingOptions, bool indexIfGeocodeFails, Address address = null, GeoCoordinates coordinates = null)
        {
            GeocodeAndIndexJobRequest requestBody = new GeocodeAndIndexJobRequest
            {
                JobData = job,
                GeocodeOptions = new GeocodeOptionsBase
                {
                    Provider = geocodeCredentials?.Provider ?? GeocodeProvider.Google,
                    ProviderKey = geocodeCredentials?.ProviderKey,
                    PostalAddress = address,
                    GeoCoordinates = coordinates
                },
                IndexingOptions = indexingOptions,
                IndexIfGeocodeFails = indexIfGeocodeFails
            };

            RestRequest apiRequest = _endpoints.GeocodeAndIndexJob();
            apiRequest.AddJsonBody(SerializeJson(requestBody));
            RestResponse<GeocodeAndIndexJobResponse> response = await _httpClient.ExecuteAsync<GeocodeAndIndexJobResponse>(apiRequest);
            ProcessResponse(response, GetBodyIfDebug(apiRequest));

            if (!requestBody.IndexIfGeocodeFails && response.Data.Value.GeocodeResponse != null && !response.Data.Value.GeocodeResponse.IsSuccess)
            {
                throw new SovrenException(GetBodyIfDebug(apiRequest), response, response.Data.Value.GeocodeResponse, response.Data.Info.TransactionId);
            }

            if (response.Data.Value.IndexingResponse != null && !response.Data.Value.IndexingResponse.IsSuccess)
            {
                throw new SovrenException(GetBodyIfDebug(apiRequest), response, response.Data.Value.IndexingResponse, response.Data.Info.TransactionId);
            }

            return response.Data;
        }

        /// <summary>
        /// Uses the address in the resume (if present) to look up geocoordinates and add them into the ParsedResume object.
        /// These coordinates are used by the AI Searching/Matching engine.
        /// </summary>
        /// <param name="resume">The resume to geocode</param>
        /// <param name="indexingOptions">What index/document id to use to index the document after geocoding</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <param name="indexIfGeocodeFails">Indicates whether or not the document should still be added to the index if the geocode request fails. Default is false.</param>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        public async Task<GeocodeAndIndexResumeResponse> GeocodeAndIndex(
            ParsedResume resume,
            IndexSingleDocumentInfo indexingOptions,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false)
        {
            return await InternalGeocodeAndIndex(resume, geocodeCredentials, indexingOptions, indexIfGeocodeFails);
        }

        /// <summary>
        /// Use this if you would like to provide an address for geocoding instead of using the one in the parsed
        /// resume. The address included in the parsed resume (if present) will not be modified.
        /// </summary>
        /// <param name="resume">The resume to insert the geocoordinates (from the address) into</param>
        /// <param name="indexingOptions">What index/document id to use to index the document after geocoding</param>
        /// <param name="address">The address to use to retrieve geocoordinates</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <param name="indexIfGeocodeFails">Indicates whether or not the document should still be added to the index if the geocode request fails. Default is false.</param>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        public async Task<GeocodeAndIndexResumeResponse> GeocodeAndIndex(
            ParsedResume resume,
            IndexSingleDocumentInfo indexingOptions,
            Address address,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false)
        {
            return await InternalGeocodeAndIndex(resume, geocodeCredentials, indexingOptions, indexIfGeocodeFails, address: address);
        }

        /// <summary>
        /// Use this if you already have latitude/longitude coordinates and simply wish to add them to your parsed resume.
        /// The coordinates will be inserted into your parsed resume, and the address included in the 
        /// parsed resume (if present) will not be modified.
        /// </summary>
        /// <param name="resume">The resume to insert the geocoordinates into</param>
        /// <param name="indexingOptions">What index/document id to use to index the document after geocoding</param>
        /// <param name="coordinates">The geocoordinates to use</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <param name="indexIfGeocodeFails">Indicates whether or not the document should still be added to the index if the geocode request fails. Default is false.</param>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        public async Task<GeocodeAndIndexResumeResponse> GeocodeAndIndex(
            ParsedResume resume,
            IndexSingleDocumentInfo indexingOptions,
            GeoCoordinates coordinates,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false)
        {
            return await InternalGeocodeAndIndex(resume, geocodeCredentials, indexingOptions, indexIfGeocodeFails, coordinates: coordinates);
        }

        /// <summary>
        /// Use this if you already have latitude/longitude coordinates AND a known address and want to add/override them in your parsed resume.
        /// The coordinates will be inserted into your parsed resume, and the address in the 
        /// parsed resume will not be set/modified with what you specify.
        /// </summary>
        /// <param name="resume">The resume to insert the geocoordinates into</param>
        /// <param name="indexingOptions">What index/document id to use to index the document after geocoding</param>
        /// <param name="coordinates">The geocoordinates to use</param>
        /// <param name="address">The address to set/override in the parsed resume prior to indexing</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <param name="indexIfGeocodeFails">Indicates whether or not the document should still be added to the index if the geocode request fails. Default is false.</param>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        public async Task<GeocodeAndIndexResumeResponse> GeocodeAndIndex(
            ParsedResume resume,
            IndexSingleDocumentInfo indexingOptions,
            GeoCoordinates coordinates,
            Address address,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false)
        {
            return await InternalGeocodeAndIndex(resume, geocodeCredentials, indexingOptions, indexIfGeocodeFails, coordinates: coordinates, address: address);
        }

        /// <summary>
        /// Uses the address in the job (if present) to look up geocoordinates and add them into the ParsedJob object.
        /// These coordinates are used by the AI Searching/Matching engine.
        /// </summary>
        /// <param name="job">The job to geocode</param>
        /// <param name="indexingOptions">What index/document id to use to index the document after geocoding</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <param name="indexIfGeocodeFails">Indicates whether or not the document should still be added to the index if the geocode request fails. Default is false.</param>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        public async Task<GeocodeAndIndexJobResponse> GeocodeAndIndex(
            ParsedJob job,
            IndexSingleDocumentInfo indexingOptions,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false)
        {
            return await InternalGeocodeAndIndex(job, geocodeCredentials, indexingOptions, indexIfGeocodeFails);
        }

        /// <summary>
        /// Use this if you would like to provide an address for geocoding instead of using the one in the parsed
        /// job. The address included in the parsed job (if present) will not be modified.
        /// </summary>
        /// <param name="job">The job to insert the geocoordinates (from the address) into</param>
        /// <param name="indexingOptions">What index/document id to use to index the document after geocoding</param>
        /// <param name="address">The address to use to retrieve geocoordinates</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <param name="indexIfGeocodeFails">Indicates whether or not the document should still be added to the index if the geocode request fails. Default is false.</param>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        public async Task<GeocodeAndIndexJobResponse> GeocodeAndIndex(
            ParsedJob job,
            IndexSingleDocumentInfo indexingOptions,
            Address address,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false)
        {
            return await InternalGeocodeAndIndex(job, geocodeCredentials, indexingOptions, indexIfGeocodeFails, address: address);
        }

        /// <summary>
        /// Use this if you already have latitude/longitude coordinates and simply wish to add them to your parsed job.
        /// The coordinates will be inserted into your parsed job, and the address included in the 
        /// parsed job (if present) will not be modified.
        /// </summary>
        /// <param name="job">The job to insert the geocoordinates into</param>
        /// <param name="indexingOptions">What index/document id to use to index the document after geocoding</param>
        /// <param name="coordinates">The geocoordinates to use</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <param name="indexIfGeocodeFails">Indicates whether or not the document should still be added to the index if the geocode request fails. Default is false.</param>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        public async Task<GeocodeAndIndexJobResponse> GeocodeAndIndex(
            ParsedJob job,
            IndexSingleDocumentInfo indexingOptions,
            GeoCoordinates coordinates,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false)
        {
            return await InternalGeocodeAndIndex(job, geocodeCredentials, indexingOptions, indexIfGeocodeFails, coordinates: coordinates);
        }

        /// <summary>
        /// Use this if you already have latitude/longitude coordinates AND a known address and want to add/override them in your parsed job.
        /// The coordinates will be inserted into your parsed job, and the address in the 
        /// parsed job will not be set/modified with what you specify.
        /// </summary>
        /// <param name="job">The job to insert the geocoordinates into</param>
        /// <param name="indexingOptions">What index/document id to use to index the document after geocoding</param>
        /// <param name="coordinates">The geocoordinates to use</param>
        /// <param name="address">The address to set/override in the parsed job prior to indexing</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <param name="indexIfGeocodeFails">Indicates whether or not the document should still be added to the index if the geocode request fails. Default is false.</param>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        public async Task<GeocodeAndIndexJobResponse> GeocodeAndIndex(
            ParsedJob job,
            IndexSingleDocumentInfo indexingOptions,
            GeoCoordinates coordinates,
            Address address,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false)
        {
            return await InternalGeocodeAndIndex(job, geocodeCredentials, indexingOptions, indexIfGeocodeFails, coordinates: coordinates, address: address);
        }

        #endregion
    }
}

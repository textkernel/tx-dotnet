// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models;
using Textkernel.Tx.Models.API;
using Textkernel.Tx.Models.API.Account;
using Textkernel.Tx.Models.API.BimetricScoring;
using Textkernel.Tx.Models.API.DataEnrichment;
using Textkernel.Tx.Models.API.DataEnrichment.Ontology.Request;
using Textkernel.Tx.Models.API.DataEnrichment.Ontology.Response;
using Textkernel.Tx.Models.API.DataEnrichment.Professions.Request;
using Textkernel.Tx.Models.API.DataEnrichment.Professions.Response;
using Textkernel.Tx.Models.API.DataEnrichment.Skills.Request;
using Textkernel.Tx.Models.API.DataEnrichment.Skills.Response;
using Textkernel.Tx.Models.API.Formatter;
using Textkernel.Tx.Models.API.Geocoding;
using Textkernel.Tx.Models.API.Indexes;
using Textkernel.Tx.Models.API.Matching;
using Textkernel.Tx.Models.API.Matching.Request;
using Textkernel.Tx.Models.API.Matching.UI;
using Textkernel.Tx.Models.API.Parsing;
using Textkernel.Tx.Models.Job;
using Textkernel.Tx.Models.Matching;
using Textkernel.Tx.Models.Resume;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using Textkernel.Tx.Models.DataEnrichment;
using Textkernel.Tx.Models.API.JobDescription;

namespace Textkernel.Tx
{
    /// <summary>
    /// Settings for a TxClient (used when configuring the TxClient with dependency injection)
    /// </summary>
    public class TxClientSettings
    {
        /// <summary>
        /// The Account ID for your account
        /// </summary>
        public string AccountId { get; set; }
        /// <summary>
        /// The Service Key for your account
        /// </summary>
        public string ServiceKey { get; set; }
        /// <summary>
        /// The Data Center for your account. Either <see cref="DataCenter.US"/>, <see cref="DataCenter.EU"/>, or <see cref="DataCenter.AU"/>
        /// </summary>
        public DataCenter DataCenter { get; set; }
        /// <summary>
        /// Optional tags to use to track API usage for your account
        /// </summary>
        public IEnumerable<string> TrackingTags { get; set; }
    }

    //public static class TxClientExtensions
    //{
    //    public static IServiceCollection AddTxClient(this IServiceCollection services, Action<TxClientSettings> setupAction)
    //    {
    //        services.AddOptions<TxClientSettings>().Configure(setupAction);
    //        services.AddHttpClient<ITxClient, TxClient>();
    //        return services;
    //    }
    //}

    internal static class HttpRequestExtensions
    {
        internal static void AddJsonBody<T>(this HttpRequestMessage request, T requestBody)
        {
            string json = JsonSerializer.Serialize(requestBody, TxJsonSerialization.DefaultOptions);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }
    }

    /// <summary>
    /// The SDK client to perform Tx API calls.
    /// </summary>
    public class TxClient : ITxClient, IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly ApiEndpoints _endpoints;

        private static readonly string _sdkVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        /// Disposes this object and the contained HttpClient
        /// </summary>
        public void Dispose() => _httpClient?.Dispose();

        /// <summary>
        /// Set to <see langword="true"/> for debugging API errors. It will show the full JSON request body in <see cref="TxException.RequestBody"/>
        /// <br/><b>NOTE: do not set this to <see langword="true"/> in your production system, as it increases the memory footprint</b>
        /// </summary>
        public bool ShowFullRequestBodyInExceptions { get; set; }

        /// <param name="accountId">The account id for your account</param>
        /// <param name="serviceKey">The service key for your account</param>
        /// <param name="dataCenter">The Data Center for your account. Either <see cref="DataCenter.US"/>, <see cref="DataCenter.EU"/>, or <see cref="DataCenter.AU"/></param>
        /// <param name="trackingTags">Optional tags to use to track API usage for your account</param>
        /// <remarks>
        /// IMPORTANT: if you are using DI or would like to pass in your own HttpClient, use <see cref="TxClient(HttpClient, TxClientSettings)"/>
        /// </remarks>
        public TxClient(string accountId, string serviceKey, DataCenter dataCenter, IEnumerable<string> trackingTags = null)
            : this(accountId, serviceKey, dataCenter, trackingTags, null)
        { }

        /// <summary>
        /// This constructor allows the user to specify the HttpClient to use. For best practices,
        /// see <see href="https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines">here</see>.
        /// <br/>Here is an example of how to inject a TxClient with DI:
        /// <code>
        /// var builder = WebApplication.CreateBuilder(args);
        /// builder.Services.AddSingleton(_ => new TxClientSettings
        /// {
        ///     AccountId = "12345678",
        ///     ...
        /// });
        /// //requires Microsoft.Extensions.Http package
        /// builder.Services.AddHttpClient&lt;ITxClient, TxClient&gt;();
        /// 
        /// //now you can retrieve your injected client from the service provider
        /// TxClient client = builder.Services.GetRequiredService&lt;TxClient&gt;();
        /// </code>
        /// </summary>
        /// <param name="httpClient">The HttpClient to use</param>
        /// <param name="settings">The settings for this client</param>
        public TxClient(HttpClient httpClient, TxClientSettings settings)
            : this(settings?.AccountId, settings?.ServiceKey, settings?.DataCenter, settings?.TrackingTags, httpClient)
        { }

        private TxClient(string accountId, string serviceKey, DataCenter dataCenter, IEnumerable<string> trackingTags, HttpClient httpClient)
        {
            if (string.IsNullOrEmpty(accountId))
                throw new ArgumentNullException(nameof(accountId));

            if (string.IsNullOrEmpty(serviceKey))
                throw new ArgumentNullException(nameof(serviceKey));

            if (dataCenter == null)
                throw new ArgumentNullException(nameof(DataCenter));

            _httpClient = httpClient ?? new HttpClient();
            _endpoints = new ApiEndpoints(dataCenter);

            //do not validate credentials here, as this could lead to calling GetAccount for every parse call, an AUP violation
            _httpClient.BaseAddress = new Uri(dataCenter.Root + (dataCenter.Root.EndsWith("/") ? "" : "/"));
            _httpClient.DefaultRequestHeaders.Add("Tx-AccountId", accountId);
            _httpClient.DefaultRequestHeaders.Add("Tx-ServiceKey", serviceKey);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", $"tx-dotnet-{_sdkVersion}");

            if (trackingTags?.Any() ?? false)
            {
                string tagsHeaderValue = string.Join(", ", trackingTags);
                if (tagsHeaderValue.Length >= 75)//API allows 100, but just to be safe, this should be way more than enough
                {
                    throw new ArgumentException("Too many values or values are too long", nameof(trackingTags));
                }

                _httpClient.DefaultRequestHeaders.Add("Tx-TrackingTag", tagsHeaderValue);
            }
        }

        private async Task<T> ProcessResponse<T>(HttpResponseMessage response, string requestBody) where T : ITxResponse
        {
            if (response != null && response.StatusCode == System.Net.HttpStatusCode.RequestEntityTooLarge)
            {
                throw new TxException(requestBody, response, new ApiResponseInfoLite { Code = "Error", Message = "Request body was too large." }, null);
            }

            T data = await DeserializeBody<T>(response);

            if (response != null && data == null)
            {
                //this happens when its a non-Tx 404 or a 500-level error
                string message = $"{(int)response.StatusCode} - {response.ReasonPhrase}";
                throw new TxException(requestBody, response, new ApiResponseInfoLite { Code = "Error", Message = message }, null);
            }

            if (response == null || data == null)
            {
                //this should really never happen, but just in case...
                throw new TxException(requestBody, response, new ApiResponseInfoLite { Code = "Error", Message = "Unknown API error." }, null);
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new TxException(requestBody, response, data.Info);
            }

            return data;
            //TODO: much more error handling here?
        }

        private async Task<GenerateUIResponse> ProcessUIResponse(HttpResponseMessage response, string requestBody)
        {
            if (!response.IsSuccessStatusCode)
            {
                //this is a little bit wonky since the matching ui does not follow the standard API response format
                string transId = "matchui-" + DateTime.Now.ToString();
                throw new TxException(requestBody, response, new ApiResponseInfoLite { Code = "Error", Message = await response.Content.ReadAsStringAsync() }, transId);
            }

            return await DeserializeBody<GenerateUIResponse>(response);
        }

        private async Task<string> GetBodyIfDebug(HttpRequestMessage request)
        {
            if (ShowFullRequestBodyInExceptions)
            {
                return await request.Content.ReadAsStringAsync();
            }

            return null;
        }

        private async Task<T> DeserializeBody<T>(HttpResponseMessage response)
        {
            try
            {
                return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync());
            }
            catch
            {
                //non-200 responses will not have a body that we can deserialize, so swallow that error
                //but for a 200 response we should always be able to deserialize
                if (response.IsSuccessStatusCode) throw;

                return default(T);
            }
        }

        /// <summary>
        /// Get the account info (remaining credits, max concurrency, etc).
        /// </summary>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public async Task<GetAccountInfoResponse> GetAccountInfo()
        {
            HttpRequestMessage apiRequest = _endpoints.GetAccountInfo();
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<GetAccountInfoResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        #region Formatter

        /// <inheritdoc />
        public async Task<FormatResumeResponse> FormatResume(FormatResumeRequest request)
        {
            HttpRequestMessage apiRequest = _endpoints.FormatResume();
            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);

            return await DeserializeBody<FormatResumeResponse>(response);
        }

        /// <inheritdoc />
        public async Task<FormatResumeResponse> FormatResumeWithTemplate(FormatResumeWithTemplateRequest request)
        {
            HttpRequestMessage apiRequest = _endpoints.FormatResumeWithTemplate();
            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);

            return await DeserializeBody<FormatResumeResponse>(response);
        }

        #endregion

        #region Parsing

        /// <summary>
        /// Parse a resume
        /// </summary>
        /// <param name="request">The request body</param>
        /// <returns>The parse result and any metadata</returns>
        /// <exception cref="TxException">Thrown when a parsing or API error occurred</exception>
        /// <exception cref="TxGeocodeResumeException">Thrown when parsing was successful, but an error occurred during geocoding</exception>
        /// <exception cref="TxIndexResumeException">Thrown when parsing was successful, but an error occurred during indexing</exception>
        /// <exception cref="TxProfessionNormalizationResumeException">Thrown when parsing was successful, but an error occurred during profession normalization</exception>
        public async Task<ParseResumeResponse> ParseResume(ParseRequest request)
        {
            HttpRequestMessage apiRequest = _endpoints.ParseResume();
            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            var data = await ProcessResponse<ParseResumeResponse>(response, await GetBodyIfDebug(apiRequest));

            if (data.Value.ParsingResponse != null && !data.Value.ParsingResponse.IsSuccess)
            {
                throw new TxException(await GetBodyIfDebug(apiRequest), response, data.Value.ParsingResponse, data.Info.TransactionId);
            }

            if (data.Value.GeocodeResponse != null && !data.Value.GeocodeResponse.IsSuccess)
            {
                throw new TxGeocodeResumeException(response, data.Value.GeocodeResponse, data.Info.TransactionId, data);
            }

            if (data.Value.IndexingResponse != null && !data.Value.IndexingResponse.IsSuccess)
            {
                throw new TxIndexResumeException(response, data.Value.IndexingResponse, data.Info.TransactionId, data);
            }

            if (data.Value.ProfessionNormalizationResponse != null && !data.Value.ProfessionNormalizationResponse.IsSuccess)
            {
                throw new TxProfessionNormalizationResumeException(response, data.Value.ProfessionNormalizationResponse, data.Info.TransactionId, data);
            }

            return data;
        }


        /// <summary>
        /// Parse a job
        /// </summary>
        /// <param name="request">The request body</param>
        /// <returns>The parse result and any metadata</returns>
        /// <exception cref="TxException">Thrown when a parsing or API error occurred</exception>
        /// <exception cref="TxGeocodeJobException">Thrown when parsing was successful, but an error occurred during geocoding</exception>
        /// <exception cref="TxIndexJobException">Thrown when parsing was successful, but an error occurred during indexing</exception>
        /// <exception cref="TxProfessionNormalizationJobException">Thrown when parsing was successful, but an error occurred during profession normalization</exception>
        public async Task<ParseJobResponse> ParseJob(ParseRequest request)
        {
            HttpRequestMessage apiRequest = _endpoints.ParseJobOrder();
            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            var data = await ProcessResponse<ParseJobResponse>(response, await GetBodyIfDebug(apiRequest));

            if (data.Value.ParsingResponse != null && !data.Value.ParsingResponse.IsSuccess)
            {
                throw new TxException(await GetBodyIfDebug(apiRequest), response, data.Value.ParsingResponse, data.Info.TransactionId);
            }

            if (data.Value.GeocodeResponse != null && !data.Value.GeocodeResponse.IsSuccess)
            {
                throw new TxGeocodeJobException(response, data.Value.GeocodeResponse, data.Info.TransactionId, data);
            }

            if (data.Value.IndexingResponse != null && !data.Value.IndexingResponse.IsSuccess)
            {
                throw new TxIndexJobException(response, data.Value.IndexingResponse, data.Info.TransactionId, data);
            }

            if (data.Value.ProfessionNormalizationResponse != null && !data.Value.ProfessionNormalizationResponse.IsSuccess)
            {
                throw new TxProfessionNormalizationJobException(response, data.Value.IndexingResponse, data.Info.TransactionId, data);
            }

            return data;
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
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public async Task<CreateIndexResponse> CreateIndex(IndexType type, string indexId)
        {
            CreateIndexRequest request = new CreateIndexRequest
            {
                IndexType = type
            };

            HttpRequestMessage apiRequest = _endpoints.CreateIndex(indexId);
            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<CreateIndexResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <summary>
        /// Get all existing indexes
        /// </summary>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public async Task<GetAllIndexesResponse> GetAllIndexes()
        {
            HttpRequestMessage apiRequest = _endpoints.GetAllIndexes();
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<GetAllIndexesResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <summary>
        /// Delete an existing index. Note that this is a destructive action and 
        /// cannot be undone. All the documents in this index will be deleted.
        /// </summary>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public async Task<DeleteIndexResponse> DeleteIndex(string indexId)
        {
            HttpRequestMessage apiRequest = _endpoints.DeleteIndex(indexId);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<DeleteIndexResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        #endregion

        #region Documents

        /// <summary>
        /// Add a resume to an existing index
        /// </summary>
        /// <param name="resume">A resume generated by the Resume Parser</param>
        /// <param name="indexId">The index the document should be added into (case-insensitive).</param>
        /// <param name="documentId">
        /// The ID to assign to the new document. This is restricted to alphanumeric 
        /// with dashes and underscores. All values will be converted to lower-case.
        /// </param>
        /// <param name="userDefinedTags">The user-defined tags that the resume should have</param>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public async Task<IndexDocumentResponse> IndexDocument(ParsedResume resume, string indexId, string documentId, IEnumerable<string> userDefinedTags = null)
        {
            IndexResumeRequest requestBody = new IndexResumeRequest
            {
                ResumeData = resume,
                UserDefinedTags = userDefinedTags?.ToList()
            };

            HttpRequestMessage apiRequest = _endpoints.IndexResume(indexId, documentId);
            apiRequest.AddJsonBody(requestBody);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<IndexDocumentResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <summary>
        /// Add a job to an existing index
        /// </summary>
        /// <param name="job">A job generated by the Job Parser</param>
        /// <param name="indexId">The index the document should be added into (case-insensitive).</param>
        /// <param name="documentId">
        /// The ID to assign to the new document. This is restricted to alphanumeric 
        /// with dashes and underscores. All values will be converted to lower-case.
        /// </param>
        /// <param name="userDefinedTags">The user-defined tags that the job should have</param>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public async Task<IndexDocumentResponse> IndexDocument(ParsedJob job, string indexId, string documentId, IEnumerable<string> userDefinedTags = null)
        {
            IndexJobRequest requestBody = new IndexJobRequest
            {
                JobData = job,
                UserDefinedTags = userDefinedTags?.ToList()
            };
            
            HttpRequestMessage apiRequest = _endpoints.IndexJob(indexId, documentId);
            apiRequest.AddJsonBody(requestBody);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<IndexDocumentResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <summary>
        /// Add several resumes to an existing index
        /// </summary>
        /// <param name="resumes">The resumes generated by the Resume Parser paired with their DocumentIds</param>
        /// <param name="indexId">The index the resumes should be added into (case-insensitive).</param>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public async Task<IndexMultipleDocumentsResponse> IndexMultipleDocuments(IEnumerable<IndexResumeInfo> resumes, string indexId)
        {
            IndexMultipleResumesRequest requestBody = new IndexMultipleResumesRequest
            {
                Resumes = resumes?.ToList()
            };

            HttpRequestMessage apiRequest = _endpoints.IndexMultipleResumes(indexId);
            apiRequest.AddJsonBody(requestBody);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<IndexMultipleDocumentsResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <summary>
        /// Add several jobs to an existing index
        /// </summary>
        /// <param name="jobs">The jobs generated by the Job Parser paired with their DocumentIds</param>
        /// <param name="indexId">The index the jobs should be added into (case-insensitive).</param>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public async Task<IndexMultipleDocumentsResponse> IndexMultipleDocuments(IEnumerable<IndexJobInfo> jobs, string indexId)
        {
            IndexMultipleJobsRequest requestBody = new IndexMultipleJobsRequest
            {
                Jobs = jobs?.ToList()
            };

            HttpRequestMessage apiRequest = _endpoints.IndexMultipleJobs(indexId);
            apiRequest.AddJsonBody(requestBody);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<IndexMultipleDocumentsResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <summary>
        /// Delete an existing document from an index
        /// </summary>
        /// <param name="indexId">The index containing the document</param>
        /// <param name="documentId">The ID of the document to delete</param>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public async Task<DeleteDocumentResponse> DeleteDocument(string indexId, string documentId)
        {
            HttpRequestMessage apiRequest = _endpoints.DeleteDocument(indexId, documentId);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<DeleteDocumentResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <summary>
        /// Delete a group of existing documents from an index
        /// </summary>
        /// <param name="indexId">The index containing the documents</param>
        /// <param name="documentIds">The IDs of the documents to delete</param>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public async Task<DeleteMultipleDocumentsResponse> DeleteMultipleDocuments(string indexId, IEnumerable<string> documentIds)
        {
            HttpRequestMessage apiRequest = _endpoints.DeleteMultipleDocuments(indexId);
            apiRequest.AddJsonBody(new { DocumentIds = documentIds?.ToList() });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<DeleteMultipleDocumentsResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <summary>
        /// Retrieve an existing resume from an index
        /// </summary>
        /// <param name="indexId">The index containing the resume</param>
        /// <param name="documentId">The ID of the resume to retrieve</param>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public async Task<GetResumeResponse> GetResume(string indexId, string documentId)
        {
            HttpRequestMessage apiRequest = _endpoints.GetResume(indexId, documentId);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<GetResumeResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <summary>
        /// Retrieve an existing job from an index
        /// </summary>
        /// <param name="indexId">The index containing the job</param>
        /// <param name="documentId">The ID of the job to retrieve</param>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public async Task<GetJobResponse> GetJob(string indexId, string documentId)
        {
            HttpRequestMessage apiRequest = _endpoints.GetJob(indexId, documentId);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<GetJobResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <summary>
        /// Updates the user-defined tags for a resume
        /// </summary>
        /// <param name="indexId">The index containing the resume</param>
        /// <param name="documentId">The ID of the resume to update</param>
        /// <param name="userDefinedTags">The user-defined tags to add/delete/etc</param>
        /// <param name="method">Which method to use for the specified user-defined tags</param>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
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

            HttpRequestMessage apiRequest = _endpoints.UpdateResumeUserDefinedTags(indexId, documentId);
            apiRequest.AddJsonBody(requestBody);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<UpdateUserDefinedTagsResponse>(response, await GetBodyIfDebug(apiRequest));
        }


        /// <summary>
        /// Updates the user-defined tags for a job
        /// </summary>
        /// <param name="indexId">The index containing the job</param>
        /// <param name="documentId">The ID of the job to update</param>
        /// <param name="userDefinedTags">The user-defined tags to add/delete/etc</param>
        /// <param name="method">Which method to use for the specified user-defined tags</param>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
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

            HttpRequestMessage apiRequest = _endpoints.UpdateJobUserDefinedTags(indexId, documentId);
            apiRequest.AddJsonBody(requestBody);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<UpdateUserDefinedTagsResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        #endregion

        #region Matching

        /// <summary>
        /// Find matches for a non-indexed resume.
        /// </summary>
        /// <param name="resume">The resume (generated by the Resume Parser) to use as the source for a match query</param>
        /// <param name="indexesToQuery">The indexes to find results in. These must all be of the same type (resumes or jobs)</param>
        /// <param name="preferredWeights">
        /// The preferred category weights for scoring the results. If none are provided,
        /// The best values will be determined based on the source resume
        /// </param>
        /// <param name="filters">Any filters to apply prior to the match (a result must satisfy all the filters)</param>
        /// <param name="settings">Settings for this match</param>
        /// <param name="numResults">The number of results to show. If not specified, the default will be used.</param>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public async Task<MatchResponse> Match(
            ParsedResume resume,
            IEnumerable<string> indexesToQuery,
            CategoryWeights preferredWeights = null,
            FilterCriteria filters = null,
            SearchMatchSettings settings = null,
            int numResults = 0)
        {
            MatchResumeRequest requestBody = CreateRequest(resume, indexesToQuery, preferredWeights, filters, settings, numResults);

            HttpRequestMessage apiRequest = _endpoints.MatchResume(false);
            apiRequest.AddJsonBody(requestBody);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<MatchResponse>(response, await GetBodyIfDebug(apiRequest));
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
        /// <param name="job">The job (generated by the Job Parser) to use as the source for a match query</param>
        /// <param name="indexesToQuery">The indexes to find results in. These must all be of the same type (resumes or jobs)</param>
        /// <param name="preferredWeights">
        /// The preferred category weights for scoring the results. If none are provided,
        /// The best values will be determined based on the source job
        /// </param>
        /// <param name="filters">Any filters to apply prior to the match (a result must satisfy all the filters)</param>
        /// <param name="settings">Settings for this match</param>
        /// <param name="numResults">The number of results to show. If not specified, the default will be used.</param>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public async Task<MatchResponse> Match(
            ParsedJob job,
            IEnumerable<string> indexesToQuery,
            CategoryWeights preferredWeights = null,
            FilterCriteria filters = null,
            SearchMatchSettings settings = null,
            int numResults = 0)
        {
            MatchJobRequest requestBody = CreateRequest(job, indexesToQuery, preferredWeights, filters, settings, numResults);

            HttpRequestMessage apiRequest = _endpoints.MatchJob(false);
            apiRequest.AddJsonBody(requestBody);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<MatchResponse>(response, await GetBodyIfDebug(apiRequest));
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
        /// The best values will be determined based on the source resume/job
        /// </param>
        /// <param name="filters">Any filters to apply prior to the match (a result must satisfy all the filters)</param>
        /// <param name="settings">Settings for this match</param>
        /// <param name="numResults">The number of results to show. If not specified, the default will be used.</param>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
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

            HttpRequestMessage apiRequest = _endpoints.MatchByDocumentId(indexId, documentId, false);
            apiRequest.AddJsonBody(requestBody);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<MatchResponse>(response, await GetBodyIfDebug(apiRequest));
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
            HttpRequestMessage apiRequest = _endpoints.MatchByDocumentId(indexId, documentId, true);
            apiRequest.AddJsonBody(options);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessUIResponse(response, await GetBodyIfDebug(apiRequest));
        }

        internal async Task<GenerateUIResponse> UIMatch(UIMatchResumeRequest request)
        {
            HttpRequestMessage apiRequest = _endpoints.MatchResume(true);
            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessUIResponse(response, await GetBodyIfDebug(apiRequest));
        }

        internal async Task<GenerateUIResponse> UIMatch(UIMatchJobRequest request)
        {
            HttpRequestMessage apiRequest = _endpoints.MatchJob(true);
            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessUIResponse(response, await GetBodyIfDebug(apiRequest));
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
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public async Task<SearchResponse> Search(
            IEnumerable<string> indexesToQuery,
            FilterCriteria query,
            SearchMatchSettings settings = null,
            PaginationSettings pagination = null)
        {
            SearchRequest requestBody = CreateRequest(indexesToQuery, query, settings, pagination);

            HttpRequestMessage apiRequest = _endpoints.Search(false);
            apiRequest.AddJsonBody(requestBody);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<SearchResponse>(response, await GetBodyIfDebug(apiRequest));
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
            HttpRequestMessage apiRequest = _endpoints.Search(true);
            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessUIResponse(response, await GetBodyIfDebug(apiRequest));
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
        /// The best values will be determined based on the source resume
        /// </param>
        /// <param name="settings">Settings to be used for this scoring request</param>
        /// <typeparam name="TTarget">Either <see cref="ParsedResumeWithId"/> or <see cref="ParsedJobWithId"/></typeparam>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public async Task<BimetricScoreResponse> BimetricScore<TTarget>(
            ParsedResumeWithId sourceResume,
            List<TTarget> targetDocuments,
            CategoryWeights preferredWeights = null,
            SearchMatchSettings settings = null) where TTarget : IParsedDocWithId
        {
            BimetricScoreResumeRequest requestBody = CreateRequest(sourceResume, targetDocuments, preferredWeights, settings);

            HttpRequestMessage apiRequest = _endpoints.BimetricScoreResume(false);
            apiRequest.AddJsonBody(requestBody);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<BimetricScoreResponse>(response, await GetBodyIfDebug(apiRequest));
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
        /// The best values will be determined based on the source job
        /// </param>
        /// <param name="settings">Settings to be used for this scoring request</param>
        /// <typeparam name="TTarget">Either <see cref="ParsedResumeWithId"/> or <see cref="ParsedJobWithId"/></typeparam>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public async Task<BimetricScoreResponse> BimetricScore<TTarget>(
            ParsedJobWithId sourceJob,
            List<TTarget> targetDocuments,
            CategoryWeights preferredWeights = null,
            SearchMatchSettings settings = null) where TTarget : IParsedDocWithId
        {
            BimetricScoreJobRequest requestBody = CreateRequest(sourceJob, targetDocuments, preferredWeights, settings);

            HttpRequestMessage apiRequest = _endpoints.BimetricScoreJob(false);
            apiRequest.AddJsonBody(requestBody);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<BimetricScoreResponse>(response, await GetBodyIfDebug(apiRequest));
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
            HttpRequestMessage apiRequest = _endpoints.BimetricScoreResume(true);
            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessUIResponse(response, await GetBodyIfDebug(apiRequest));
        }

        internal async Task<GenerateUIResponse> UIBimetricScore(UIBimetricScoreJobRequest request)
        {
            HttpRequestMessage apiRequest = _endpoints.BimetricScoreJob(true);
            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessUIResponse(response, await GetBodyIfDebug(apiRequest));
        }

        internal async Task<GenerateUIResponse> UIViewDetails(UIBimetricScoreResumeDetailsRequest request)
        {
            HttpRequestMessage apiRequest = _endpoints.ViewDetailsResume();
            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessUIResponse(response, await GetBodyIfDebug(apiRequest));
        }

        internal async Task<GenerateUIResponse> UIViewDetails(UIBimetricScoreJobDetailsRequest request)
        {
            HttpRequestMessage apiRequest = _endpoints.ViewDetailsJob();
            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessUIResponse(response, await GetBodyIfDebug(apiRequest));
        }

        internal async Task<GenerateUIResponse> UIViewDetails(UIMatchDetailsRequest request)
        {
            HttpRequestMessage apiRequest = _endpoints.ViewDetailsIndexed();
            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessUIResponse(response, await GetBodyIfDebug(apiRequest));
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

            HttpRequestMessage apiRequest = _endpoints.GeocodeResume();
            apiRequest.AddJsonBody(requestBody);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<GeocodeResumeResponse>(response, await GetBodyIfDebug(apiRequest));
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

            HttpRequestMessage apiRequest = _endpoints.GeocodeJob();
            apiRequest.AddJsonBody(requestBody);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<GeocodeJobResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <summary>
        /// Uses the address in the resume (if present) to look up geocoordinates and add them into the ParsedResume object.
        /// These coordinates are used by the AI Searching/Matching engine.
        /// </summary>
        /// <param name="resume">The resume to geocode</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
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
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
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
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
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
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
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

            HttpRequestMessage apiRequest = _endpoints.GeocodeAndIndexResume();
            apiRequest.AddJsonBody(requestBody);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            var data = await ProcessResponse<GeocodeAndIndexResumeResponse>(response, await GetBodyIfDebug(apiRequest));

            if (!requestBody.IndexIfGeocodeFails && data.Value.GeocodeResponse != null && !data.Value.GeocodeResponse.IsSuccess)
            {
                throw new TxException(await GetBodyIfDebug(apiRequest), response, data.Value.GeocodeResponse, data.Info.TransactionId);
            }

            if (data.Value.IndexingResponse != null && !data.Value.IndexingResponse.IsSuccess)
            {
                throw new TxException(await GetBodyIfDebug(apiRequest), response, data.Value.IndexingResponse, data.Info.TransactionId);
            }

            return data;
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

            HttpRequestMessage apiRequest = _endpoints.GeocodeAndIndexJob();
            apiRequest.AddJsonBody(requestBody);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            var data = await ProcessResponse<GeocodeAndIndexJobResponse>(response, await GetBodyIfDebug(apiRequest));

            if (!requestBody.IndexIfGeocodeFails && data.Value.GeocodeResponse != null && !data.Value.GeocodeResponse.IsSuccess)
            {
                throw new TxException(await GetBodyIfDebug(apiRequest), response, data.Value.GeocodeResponse, data.Info.TransactionId);
            }

            if (data.Value.IndexingResponse != null && !data.Value.IndexingResponse.IsSuccess)
            {
                throw new TxException(await GetBodyIfDebug(apiRequest), response, data.Value.IndexingResponse, data.Info.TransactionId);
            }

            return data;
        }

        /// <summary>
        /// Uses the address in the resume (if present) to look up geocoordinates and add them into the ParsedResume object.
        /// These coordinates are used by the AI Searching/Matching engine.
        /// </summary>
        /// <param name="resume">The resume to geocode</param>
        /// <param name="indexingOptions">What index/document id to use to index the document after geocoding</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <param name="indexIfGeocodeFails">Indicates whether or not the document should still be added to the index if the geocode request fails. Default is false.</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
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
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
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
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
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
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
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
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
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
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
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
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
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
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
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

        #region DES - Skills

        /// <inheritdoc />
        public async Task<GetSkillsTaxonomyResponse> GetSkillsTaxonomy(TaxonomyFormat format = TaxonomyFormat.json)
        {
            HttpRequestMessage apiRequest = _endpoints.DESSkillsGetTaxonomy(format);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<GetSkillsTaxonomyResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <inheritdoc />
        public async Task<GetMetadataResponse> GetSkillsTaxonomyMetadata()
        {
            HttpRequestMessage apiRequest = _endpoints.DESGetProfessionsMetadata();
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<GetMetadataResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <inheritdoc />
        public async Task<AutoCompleteSkillsResponse> AutocompleteSkill(string prefix, IEnumerable<string> languages = null,
            string outputLanguage = null, IEnumerable<string> types = null, int limit = 10)
        {
            HttpRequestMessage apiRequest = _endpoints.DESSkillsAutoComplete();
            apiRequest.AddJsonBody(new SkillsAutoCompleteRequest
            {
                Prefix = prefix,
                Languages = languages?.ToList(),
                OutputLanguage = outputLanguage,
                Types = types?.ToList(),
                Limit = limit
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<AutoCompleteSkillsResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <inheritdoc />
        public async Task<LookupSkillCodesResponse> LookupSkills(IEnumerable<string> skillIds, string outputLanguage = null)
        {
            HttpRequestMessage apiRequest = _endpoints.DESSkillsLookup();
            apiRequest.AddJsonBody(new LookupSkillsRequest
            {
                SkillIds = skillIds.ToList(),
                OutputLanguage = outputLanguage
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<LookupSkillCodesResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <inheritdoc />
        public async Task<NormalizeSkillsResponse> NormalizeSkills(IEnumerable<string> skills, string language = "en", string outputLanguage = null)
        {
            HttpRequestMessage apiRequest = _endpoints.DESSkillsNormalize();
            apiRequest.AddJsonBody(new NormalizeSkillsRequest
            {
                Skills = skills.ToList(),
                Language = language,
                OutputLanguage = outputLanguage
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<NormalizeSkillsResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <inheritdoc />
        public async Task<ExtractSkillsResponse> ExtractSkills(string text, string language = "en", string outputLanguage = null, float threshold = 0.5f)
        {
            HttpRequestMessage apiRequest = _endpoints.DESSkillsExtract();
            apiRequest.AddJsonBody(new ExtractSkillsRequest
            {
                Text = text,
                Language = language,
                OutputLanguage = outputLanguage,
                Threshold = threshold
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<ExtractSkillsResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        #endregion

        #region DES - Professions

        /// <inheritdoc />
        public async Task<AutoCompleteProfessionsResponse> AutocompleteProfession(string prefix, IEnumerable<string> languages = null, string outputLanguage = null, int limit = 10)
        {
            HttpRequestMessage apiRequest = _endpoints.DESProfessionsAutoComplete();
            apiRequest.AddJsonBody(new AutocompleteRequest
            {
                Prefix = prefix,
                Languages = languages?.ToList(),
                OutputLanguage = outputLanguage,
                Limit = limit
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<AutoCompleteProfessionsResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <inheritdoc />
        public async Task<GetProfessionsTaxonomyResponse> GetProfessionsTaxonomy(string language = null, TaxonomyFormat format = TaxonomyFormat.json)
        {
            HttpRequestMessage apiRequest = _endpoints.DESProfessionsGetTaxonomy(format, language);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<GetProfessionsTaxonomyResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <inheritdoc />
        public async Task<GetMetadataResponse> GetProfessionsTaxonomyMetadata()
        {
            HttpRequestMessage apiRequest = _endpoints.DESGetProfessionsMetadata();
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<GetMetadataResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <inheritdoc />
        public async Task<LookupProfessionCodesResponse> LookupProfessions(IEnumerable<int> codeIds, string outputLanguage = null)
        {
            HttpRequestMessage apiRequest = _endpoints.DESProfessionsLookup();
            apiRequest.AddJsonBody(new LookupProfessionCodesRequest
            {
                CodeIds = codeIds.ToList(),
                OutputLanguage = outputLanguage
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<LookupProfessionCodesResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <inheritdoc />
        public async Task<NormalizeProfessionsResponse> NormalizeProfessions(IEnumerable<string> jobTitles, string language = null, string outputLanguage = null)
        {
            HttpRequestMessage apiRequest = _endpoints.DESProfessionsNormalize();
            apiRequest.AddJsonBody(new NormalizeProfessionsRequest
            {
                JobTitles = jobTitles.ToList(),
                Language = language,
                OutputLanguage = outputLanguage
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<NormalizeProfessionsResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        #endregion

        #region DES - Ontology

        /// <inheritdoc />
        public async Task<CompareProfessionsResponse> CompareProfessions(int profession1, int profession2, string outputLanguage = null)
        {
            HttpRequestMessage apiRequest = _endpoints.DESOntologyCompareProfessions();
            apiRequest.AddJsonBody(new CompareProfessionsRequest
            {
                ProfessionACodeId = profession1,
                ProfessionBCodeId = profession2,
                OutputLanguage = outputLanguage
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<CompareProfessionsResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <inheritdoc />
        public async Task<CompareSkillsToProfessionResponse> CompareSkillsToProfession(int professionCodeId, string outputLanguage = null, params SkillScore[] skills)
        {
            HttpRequestMessage apiRequest = _endpoints.DESOntologyCompareSkillsToProfessions();
            apiRequest.AddJsonBody(new CompareSkillsToProfessionRequest
            {
                ProfessionCodeId = professionCodeId,
                Skills = new List<SkillScore>(skills),
                OutputLanguage = outputLanguage
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<CompareSkillsToProfessionResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <inheritdoc />
        public async Task<CompareSkillsToProfessionResponse> CompareSkillsToProfession(
            ParsedResume resume,
            int professionCodeId,
            string outputLanguage = null,
            bool weightSkillsByExperience = true)
        {
            if (!(resume?.Skills?.Normalized?.Any() ?? false))
            {
                throw new ArgumentException("The resume must be parsed with V2 skills selected, and with skills normalization enabled", nameof(resume));
            }

            return await CompareSkillsToProfession(professionCodeId, outputLanguage, GetNormalizedSkillsFromResume(resume, weightSkillsByExperience).ToArray());
        }

        private IEnumerable<SkillScore> GetNormalizedSkillsFromResume(ParsedResume resume, bool weightSkillsByExperience)
        {
            if (!(resume?.Skills?.Normalized?.Any() ?? false))
            {
                throw new ArgumentException("The resume must be parsed with V2 skills selected, and with skills normalization enabled", nameof(resume));
            }

            float maxExperience = resume.Skills.Normalized.Max(s => s.MonthsExperience?.Value ?? 0);
            return resume.Skills.Normalized
                .OrderByDescending(s => s.MonthsExperience?.Value ?? 0)
                .Take(50)
                .Select(s => new SkillScore
                {
                    Id = s.Id,
                    Score = (weightSkillsByExperience && maxExperience > 0) ? ((s.MonthsExperience?.Value ?? 0) / maxExperience) : 1
                });
        }

        /// <inheritdoc />
        public async Task<SuggestSkillsResponse> SuggestSkillsFromProfessions(ParsedResume resume, int limit = 10, string outputLanguage = null)
        {
            if (!(resume?.EmploymentHistory?.Positions?.Any(p => p.NormalizedProfession?.Profession?.CodeId != null) ?? false))
            {
                throw new ArgumentException("No professions were found in the resume, or the resume was parsed without professions normalization enabled", nameof(resume));
            }

            List<int> normalizedProfs = new List<int>();
            foreach (var position in resume.EmploymentHistory.Positions)
            {
                if (position?.NormalizedProfession?.Profession?.CodeId != null)
                {
                    normalizedProfs.Add(position.NormalizedProfession.Profession.CodeId);
                }
            }

            return await SuggestSkillsFromProfessions(normalizedProfs, limit, outputLanguage);
        }

        /// <inheritdoc />
        public async Task<SuggestSkillsResponse> SuggestSkillsFromProfessions(ParsedJob job, int limit = 10, string outputLanguage = null)
        {
            if (job?.JobTitles?.NormalizedProfession?.Profession?.CodeId == null)
            {
                throw new ArgumentException("No professions were found in the job, or the job was parsed without professions normalization enabled", nameof(job));
            }

            return await SuggestSkillsFromProfessions(new int[]{ job.JobTitles.NormalizedProfession.Profession.CodeId }, limit, outputLanguage);
        }

        /// <inheritdoc />
        public async Task<SuggestSkillsResponse> SuggestSkillsFromProfessions(IEnumerable<int> professionCodeIDs, int limit = 10, string outputLanguage = null)
        {
            HttpRequestMessage apiRequest = _endpoints.DESOntologySuggestSkillsFromProfessions();
            apiRequest.AddJsonBody(new SuggestSkillsFromProfessionsRequest
            {
                Limit = limit,
                ProfessionCodeIds = professionCodeIDs.ToList(),
                OutputLanguage = outputLanguage
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<SuggestSkillsResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <inheritdoc />
        public async Task<SuggestProfessionsResponse> SuggestProfessionsFromSkills(
            ParsedResume resume,
            int limit = 10,
            bool returnMissingSkills = false,
            string outputLanguage = null,
            bool weightSkillsByExperience = true)
        {
            if (!(resume?.Skills?.Normalized?.Any() ?? false))
            {
                throw new ArgumentException("The resume must be parsed with V2 skills selected, and with skills normalization enabled", nameof(resume));
            }
            
            return await SuggestProfessionsFromSkills(GetNormalizedSkillsFromResume(resume, weightSkillsByExperience), limit, returnMissingSkills, outputLanguage);
        }

        /// <inheritdoc />
        public async Task<SuggestProfessionsResponse> SuggestProfessionsFromSkills(ParsedJob job, int limit = 10, bool returnMissingSkills = false, string outputLanguage = null)
        {
            if (!(job?.Skills?.Normalized?.Any() ?? false))
            {
                throw new ArgumentException("The job must be parsed with V2 skills selected, and with skills normalization enabled", nameof(job));
            }

            return await SuggestProfessionsFromSkills(job.Skills.Normalized.Take(50).Select(s => new SkillScore { Id = s.Id }), limit, returnMissingSkills, outputLanguage);
        }

        /// <inheritdoc />
        public async Task<SuggestProfessionsResponse> SuggestProfessionsFromSkills(
            IEnumerable<SkillScore> skills,
            int limit = 10,
            bool returnMissingSkills = false,
            string outputLanguage = null)
        {
            HttpRequestMessage apiRequest = _endpoints.DESOntologySuggestProfessions();
            apiRequest.AddJsonBody(new SuggestProfessionsRequest
            {
                Skills = skills.ToList(),
                Limit = limit,
                ReturnMissingSkills = returnMissingSkills,
                OutputLanguage = outputLanguage
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<SuggestProfessionsResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <inheritdoc />
        public async Task<SuggestSkillsResponse> SuggestSkillsFromSkills(ParsedResume resume, int limit = 10, string outputLanguage = null, bool weightSkillsByExperience = true)
        {
            if (!(resume?.Skills?.Normalized?.Any() ?? false))
            {
                throw new ArgumentException("The resume must be parsed with V2 skills selected, and with skills normalization enabled", nameof(resume));
            }

            return await SuggestSkillsFromSkills(GetNormalizedSkillsFromResume(resume, weightSkillsByExperience), limit, outputLanguage);
        }

        /// <inheritdoc />
        public async Task<SuggestSkillsResponse> SuggestSkillsFromSkills(ParsedJob job, int limit = 10, string outputLanguage = null)
        {
            if (!(job?.Skills?.Normalized?.Any() ?? false))
            {
                throw new ArgumentException("The job must be parsed with V2 skills selected, and with skills normalization enabled", nameof(job));
            }

            return await SuggestSkillsFromSkills(job.Skills.Normalized.Take(50).Select(s => new SkillScore { Id = s.Id }), limit, outputLanguage);
        }

        /// <inheritdoc />
        public async Task<SuggestSkillsResponse> SuggestSkillsFromSkills(IEnumerable<SkillScore> skills, int limit = 25, string outputLanguage = null)
        {
            HttpRequestMessage apiRequest = _endpoints.DESOntologySuggestSkillsFromSkills();
            apiRequest.AddJsonBody(new SuggestSkillsFromSkillsRequest
            {
                Limit = limit,
                Skills = skills.ToList(),
                OutputLanguage = outputLanguage
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<SuggestSkillsResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <inheritdoc />
        public async Task<SkillsSimilarityScoreResponse> SkillsSimilarityScore(IEnumerable<SkillScore> skillSetA, IEnumerable<SkillScore> skillSetB)
        {
            HttpRequestMessage apiRequest = _endpoints.DESOntologySkillsSimilarityScore();
            apiRequest.AddJsonBody(new SkillsSimilarityScoreRequest
            {
                SkillsA = skillSetA.ToList(),
                SkillsB = skillSetB.ToList()
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<SkillsSimilarityScoreResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        #endregion

        #region Job Description API

        /// <inheritdoc />
        public async Task<GenerateJobResponse> GenerateJobDescription(GenerateJobRequest request)
        {
            HttpRequestMessage apiRequest = _endpoints.JobDescriptionGenerate();
            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<GenerateJobResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        /// <inheritdoc />
        public async Task<SuggestSkillsFromJobTitleResponse> SuggestSkillsFromJobTitle(string jobTitle, string language = "en", int? limit = null)
        {
            HttpRequestMessage apiRequest = _endpoints.JobDescriptionSuggestSkills();
            apiRequest.AddJsonBody(new SuggestSkillsFromJobTitleRequest
            {
                JobTitle = jobTitle,
                Language = language,
                Limit = limit
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<SuggestSkillsFromJobTitleResponse>(response, await GetBodyIfDebug(apiRequest));
        }

        #endregion
    }
}

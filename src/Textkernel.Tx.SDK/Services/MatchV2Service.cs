﻿// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Textkernel.Tx.Models.API;
using Textkernel.Tx.Models.API.MatchV2.Response;
using Textkernel.Tx.Models.Job;
using Textkernel.Tx.Models.Resume;
using Textkernel.Tx.Models.API.MatchV2.Request;
using System.Text.Json.Serialization;

namespace Textkernel.Tx.Services
{
    /// <summary>
    /// The target environment for Search &amp; Match V2
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MatchV2Environment
    {
        /// <summary>
        /// Acceptance
        /// </summary>
        ACC,
        /// <summary>
        /// Production
        /// </summary>
        PROD
    }

    /// <summary>
    /// Use <see cref="TxClient.SearchMatchV2"/>
    /// </summary>
    internal class MatchV2Service : ServiceBase, IMatchV2Service
    {
        internal MatchV2Service(HttpClient httpClient, EnvironmentSettings settings)
            : base(httpClient, settings)
        {
        }

        /// <inheritdoc />
        public async Task<ApiResponse<object>> AddCandidate(string documentId, ParsedResume candidate, IEnumerable<string> roles = null, bool anonymize = false, Dictionary<string, string> customFields = null)
        {
            var request = new AddCandidateRequest
            {
                Anonymize = anonymize,
                ResumeData = candidate,
                Roles = roles,
                SearchAndMatchEnvironment = _settings.MatchV2Environment,
                CustomFields = customFields
            };

            HttpRequestMessage apiRequest = ApiEndpoints.MatchV2CandidatesAddDocument(documentId);
            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);

            return await ProcessResponse<ApiResponse<object>>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<ApiResponse<object>> AddJob(string documentId, ParsedJob job, IEnumerable<string> roles = null, Dictionary<string, string> customFields = null)
        {
            var request = new AddJobRequest
            {
                JobData = job,
                Roles = roles,
                SearchAndMatchEnvironment = _settings.MatchV2Environment,
                CustomFields = customFields
            };

            HttpRequestMessage apiRequest = ApiEndpoints.MatchV2JobsAddDocument(documentId);
            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);

            return await ProcessResponse<ApiResponse<object>>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<DeleteDocumentsResponse> DeleteCandidates(IEnumerable<string> documentIds)
        {
            if (documentIds == null || documentIds.Count() == 0) throw new ArgumentException("No document IDs were specified", nameof(documentIds));

            HttpRequestMessage apiRequest = ApiEndpoints.MatchV2CandidatesDeleteDocuments(documentIds, _settings.MatchV2Environment.ToString());
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);

            return await ProcessResponse<DeleteDocumentsResponse>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<DeleteDocumentsResponse> DeleteJobs(IEnumerable<string> documentIds)
        {
            if (documentIds == null || documentIds.Count() == 0) throw new ArgumentException("No document IDs were specified", nameof(documentIds));

            HttpRequestMessage apiRequest = ApiEndpoints.MatchV2JobsDeleteDocuments(documentIds, _settings.MatchV2Environment.ToString());
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);

            return await ProcessResponse<DeleteDocumentsResponse>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<SearchResponse> MatchCandidates(DocumentSource sourceDocument, Options options, SearchQuery query = null)
        {
            return await MatchInternal(options, query, sourceDocument, ApiEndpoints.MatchV2CandidatesMatchDocument());
        }

        /// <inheritdoc />
        public async Task<SearchResponse> MatchJobs(DocumentSource sourceDocument, Options options, SearchQuery query = null)
        {
            return await MatchInternal(options, query, sourceDocument, ApiEndpoints.MatchV2JobsMatchDocument());
        }

        /// <inheritdoc />
        public async Task<SearchResponse> SearchCandidates(SearchQuery query, Options options)
        {
            return await SearchInternal(query, options, ApiEndpoints.MatchV2CandidatesSearch());
        }

        /// <inheritdoc />
        public async Task<SearchResponse> SearchJobs(SearchQuery query, Options options)
        {
            return await SearchInternal(query, options, ApiEndpoints.MatchV2JobsSearch());
        }

        private async Task<SearchResponse> MatchInternal(Options options, SearchQuery query, DocumentSource sourceDocument, HttpRequestMessage apiRequest)
        {
            var request = new MatchRequest
            {
                Options = options,
                SearchAndMatchEnvironment = _settings.MatchV2Environment,
                Query = query,
                SourceDocument = sourceDocument,
            };

            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);

            return await ProcessResponse<SearchResponse>(response, apiRequest);
        }

        private async Task<SearchResponse> SearchInternal(SearchQuery query, Options options, HttpRequestMessage apiRequest)
        {
            var request = new Models.API.MatchV2.Request.MatchRequest
            {
                Options = options,
                Query = query,
                SearchAndMatchEnvironment = _settings.MatchV2Environment
            };

            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);

            return await ProcessResponse<SearchResponse>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<AutocompleteResponse> AutocompleteCandidates(AutocompleteCandidatesField field, string input, params string[] languages)
        {
            return await AutocompleteInternal(ApiEndpoints.MatchV2CandidatesAutocomplete(), field, input, languages);
        }
        
        /// <inheritdoc />
        public async Task<AutocompleteResponse> AutocompleteJobs(AutocompleteJobsField field, string input, params string[] languages)
        {
            return await AutocompleteInternal(ApiEndpoints.MatchV2JobsAutocomplete(), field, input, languages);
        }

        private async Task<AutocompleteResponse> AutocompleteInternal<T>(HttpRequestMessage apiRequest, T field, string input, params string[] languages)
        {
            var request = new
            {
                Field = field,
                Input = input,
                SearchAndMatchEnvironment = _settings.MatchV2Environment,
                Language = string.Join(",", languages)
            };

            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);

            return await ProcessResponse<AutocompleteResponse>(response, apiRequest);
        }
    }
}

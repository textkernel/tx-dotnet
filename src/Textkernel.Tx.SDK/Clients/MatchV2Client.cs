// Copyright © 2023 Textkernel BV. All rights reserved.
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

namespace Textkernel.Tx.Clients
{
    /// <summary>
    /// Use <see cref="TxClient.MatchV2"/>
    /// </summary>
    internal class MatchV2Client : ClientBase, IMatchV2Client
    {
        internal MatchV2Client(HttpClient httpClient) : base(httpClient) { }


        /// <inheritdoc />
        public async Task<ApiResponse<object>> AddCandidate(string documentId, ParsedResume candidate, IEnumerable<string> roles, bool anonymize = false)
        {
            var request = new AddCandidateRequest
            {
                Anonymize = anonymize,
                ResumeData = candidate,
                Roles = roles
            };

            HttpRequestMessage apiRequest = ApiEndpoints.MatchV2CandidatesAddDocument(documentId);
            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);

            return await ProcessResponse<ApiResponse<object>>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<ApiResponse<object>> AddVacancy(string documentId, ParsedJob vacancy, IEnumerable<string> roles)
        {
            var request = new AddVacancyRequest
            {
                JobData = vacancy,
                Roles = roles
            };

            HttpRequestMessage apiRequest = ApiEndpoints.MatchV2VacanciesAddDocument(documentId);
            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);

            return await ProcessResponse<ApiResponse<object>>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<DeleteDocumentsResponse> DeleteCandidates(IEnumerable<string> documentIds)
        {
            if (documentIds == null || documentIds.Count() == 0) throw new ArgumentException("No document IDs were specified", nameof(documentIds));

            HttpRequestMessage apiRequest = ApiEndpoints.MatchV2CandidatesDeleteDocuments(documentIds);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);

            return await ProcessResponse<DeleteDocumentsResponse>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<DeleteDocumentsResponse> DeleteVacancies(IEnumerable<string> documentIds)
        {
            if (documentIds == null || documentIds.Count() == 0) throw new ArgumentException("No document IDs were specified", nameof(documentIds));

            HttpRequestMessage apiRequest = ApiEndpoints.MatchV2VacanciesDeleteDocuments(documentIds);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);

            return await ProcessResponse<DeleteDocumentsResponse>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<SearchResponse> MatchCandidates(string documentId, IEnumerable<string> roles, MatchOptions options)
        {
            return await MatchInternal(roles, options, ApiEndpoints.MatchV2CandidatesMatchDocument(documentId));
        }

        /// <inheritdoc />
        public async Task<SearchResponse> MatchVacancies(string documentId, IEnumerable<string> roles, MatchOptions options)
        {
            return await MatchInternal(roles, options, ApiEndpoints.MatchV2VacanciesMatchDocument(documentId));
        }

        /// <inheritdoc />
        public async Task<SearchResponse> SearchCandidates(SearchQuery query, IEnumerable<string> roles, SearchOptions options)
        {
            return await SearchInternal(query, roles, options, ApiEndpoints.MatchV2CandidatesSearch());
        }

        /// <inheritdoc />
        public async Task<SearchResponse> SearchVacancies(SearchQuery query, IEnumerable<string> roles, SearchOptions options)
        {
            return await SearchInternal(query, roles, options, ApiEndpoints.MatchV2VacanciesSearch());
        }

        private async Task<SearchResponse> MatchInternal(IEnumerable<string> roles, MatchOptions options, HttpRequestMessage apiRequest)
        {
            var request = new MatchRequest
            {
                Roles = roles,
                Options = options
            };

            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);

            return await ProcessResponse<SearchResponse>(response, apiRequest);
        }

        private async Task<SearchResponse> SearchInternal(SearchQuery query, IEnumerable<string> roles, SearchOptions options, HttpRequestMessage apiRequest)
        {
            var request = new SearchRequest
            {
                Roles = roles,
                Options = options,
                Query = query
            };

            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);

            return await ProcessResponse<SearchResponse>(response, apiRequest);
        }

    }
}

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


        #region Candidates

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
        public async Task<DeleteDocumentsResponse> DeleteCandidates(IEnumerable<string> ids)
        {
            if (ids == null || ids.Count() == 0) throw new ArgumentException("No document IDs were specified", nameof(ids));

            HttpRequestMessage apiRequest = ApiEndpoints.MatchV2CandidatesDeleteDocuments(ids);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);

            return await ProcessResponse<DeleteDocumentsResponse>(response, apiRequest);
        }

        ///// <inheritdoc />
        //public async Task<object> Search(object stuff)
        //{

        //}

        ///// <inheritdoc />
        //public async Task<object> MatchDocument(object stuff)
        //{

        //}

        #endregion

        #region Vacancies

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
        public async Task<DeleteDocumentsResponse> DeleteVacancies(IEnumerable<string> ids)
        {
            if (ids == null || ids.Count() == 0) throw new ArgumentException("No document IDs were specified", nameof(ids));

            HttpRequestMessage apiRequest = ApiEndpoints.MatchV2VacanciesDeleteDocuments(ids);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);

            return await ProcessResponse<DeleteDocumentsResponse>(response, apiRequest);
        }

        ///// <inheritdoc />
        //public async Task<object> Search(object stuff)
        //{

        //}

        ///// <inheritdoc />
        //public async Task<object> MatchDocument(object stuff)
        //{

        //}

        #endregion

    }
}

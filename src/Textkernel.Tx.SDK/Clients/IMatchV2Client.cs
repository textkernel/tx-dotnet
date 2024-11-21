// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Textkernel.Tx.Models.API;
using Textkernel.Tx.Models.API.MatchV2.Request;
using Textkernel.Tx.Models.API.MatchV2.Response;
using Textkernel.Tx.Models.Job;
using Textkernel.Tx.Models.Resume;

namespace Textkernel.Tx.Clients
{
    /// <summary>
    /// Use <see cref="TxClient.MatchV2"/>
    /// </summary>
    public interface IMatchV2Client
    {

        #region Candidates

        Task<ApiResponse<object>> AddCandidate(string documentId, ParsedResume candidate, IEnumerable<string> roles, bool anonymize = false);

        /// <summary>
        /// Delete candidate documents from environment
        /// </summary>
        /// <param name="documentIds">The document IDs to delete</param>
        /// <returns>Document IDs that succeeded/failed to delete</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<DeleteDocumentsResponse> DeleteCandidates(IEnumerable<string> documentIds);

        Task<SearchResponse> MatchCandidates(string documentId, IEnumerable<string> roles, Options options);

        Task<SearchResponse> SearchCandidates(SearchQuery query, IEnumerable<string> roles, Options options);

        #endregion

        #region Vacancies

        Task<ApiResponse<object>> AddVacancy(string documentId, ParsedJob vacancy, IEnumerable<string> roles);

        /// <summary>
        /// Delete vacancy documents from environment
        /// </summary>
        /// <param name="documentIds">The document IDs to delete</param>
        /// <returns>Document IDs that succeeded/failed to delete</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<DeleteDocumentsResponse> DeleteVacancies(IEnumerable<string> documentIds);

        Task<SearchResponse> MatchVacancies(string documentId, IEnumerable<string> roles, Options options);

        Task<SearchResponse> SearchVacancies(SearchQuery query, IEnumerable<string> roles, Options options);

        #endregion
    }
}

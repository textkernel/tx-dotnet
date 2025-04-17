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

        /// <summary>
        /// Upload a candidates CV to the search and match V2 environment.
        /// </summary>
        /// <param name="resume">Parsed output from the Textkernel CV/Resume Parser</param>
        /// <param name="roles">The roles associated with the request. Defaults to ["All"] if none are provided.</param>
        /// <param name="anonymize">A boolean flag to strip PII data out of the resume before indexing.</param>
        /// <param name="customFields">A collection of custom fields represented as key-value pairs.</param>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        Task<ApiResponse<object>> AddCandidate(string documentId, ParsedResume resume, IEnumerable<string> roles = null, bool anonymize = false, Dictionary<string, string> customFields = null);

        /// <summary>
        /// Delete candidate documents from environment
        /// </summary>
        /// <param name="documentIds">The document IDs to delete</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<DeleteDocumentsResponse> DeleteCandidates(IEnumerable<string> documentIds);

        /// <summary>
        /// Match an existing candidate document with filters provided.
        /// </summary>
        /// <param name="documentId">The document id that the user would like to run a match on.</param>
        /// <param name="options">Options for the Match request</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<SearchResponse> MatchCandidates(string documentId, Options options);

        /// <summary>
        /// Search for a candidate based on the query provided.
        /// </summary>
        /// <param name="query">The query object that will drive the search.</param>
        /// <param name="options">Options for the search request</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<SearchResponse> SearchCandidates(SearchQuery query, Options options);

        #endregion

        #region Vacancies

        /// <summary>
        /// Upload a vacancy to the search and match V2 environment.
        /// </summary>
        /// <param name="vacancy">Parsed output from the Textkernel Job Parser</param>
        /// <param name="roles">The roles associated with the request. Defaults to ["All"] if none are provided.</param>
        /// <param name="customFields">A collection of custom fields represented as key-value pairs.</param>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        Task<ApiResponse<object>> AddVacancy(string documentId, ParsedJob vacancy, IEnumerable<string> roles = null, Dictionary<string, string> customFields = null);

        /// <summary>
        /// Delete vacancy documents from environment
        /// </summary>
        /// <param name="documentIds">The document IDs to delete</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<DeleteDocumentsResponse> DeleteVacancies(IEnumerable<string> documentIds);

        /// <summary>
        /// Match an existing vacancy document with filters provided.
        /// </summary>
        /// <param name="documentId">The document id that the user would like to run a match on.</param>
        /// <param name="options">Options for the Match request</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<SearchResponse> MatchVacancies(string documentId, Options options);

        /// <summary>
        /// Search for a vacancy based on the query provided.
        /// </summary>
        /// <param name="query">The query object that will drive the search.</param>
        /// <param name="options">Options for the search request</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<SearchResponse> SearchVacancies(SearchQuery query, Options options);

        #endregion
    }
}

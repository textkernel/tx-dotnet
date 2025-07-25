﻿// Copyright © 2023 Textkernel BV. All rights reserved.
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

namespace Textkernel.Tx.Services
{
    /// <summary>
    /// Use <see cref="TxClient.SearchMatchV2"/>
    /// </summary>
    public interface IMatchV2Service
    {

        #region Candidates

        /// <summary>
        /// Upload a candidates CV to the search and match V2 environment.
        /// </summary>
        /// <param name="documentId">The id to use for the document</param>
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
        /// <param name="query">The query object that will be combined with the match query to drive the search</param>
        /// <param name="options">Options for the Match request</param>
        /// <param name="sourceDocument">The document to generate the search query from</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<SearchResponse> MatchCandidates(DocumentSource sourceDocument, Options options, SearchQuery query = null);

        /// <summary>
        /// Search for a candidate based on the query provided.
        /// </summary>
        /// <param name="query">The query object that will drive the search.</param>
        /// <param name="options">Options for the search request</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<SearchResponse> SearchCandidates(SearchQuery query, Options options);

        /// <summary>
        /// Returns a list of suggested Completions. This endpoint is used to give a user instant
        /// feedback while typing a query. If the given field is the FULLTEXT field, the service
        /// returns suggestions from all configured dictionaries that are not explicitly excluded from full-text suggestions.
        /// </summary>
        /// <param name="field">Which field to use to retrieve completions</param>
        /// <param name="input">The user-typed input string.</param>
        /// <param name="languages">
        /// Optional 2-letter ISO-639-1 language codes. The first language is used for field label translations.
        /// All languages are used to retrieve completions when the environment doesn't have default languages set.
        /// </param>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        Task<AutocompleteResponse> AutocompleteCandidates(AutocompleteCandidatesField field, string input, params string[] languages);

        #endregion

        #region Jobs

        /// <summary>
        /// Upload a job to the search and match V2 environment.
        /// </summary>
        /// <param name="documentId">The id to use for the document</param>
        /// <param name="job">Parsed output from the Textkernel Job Parser</param>
        /// <param name="roles">The roles associated with the request. Defaults to ["All"] if none are provided.</param>
        /// <param name="customFields">A collection of custom fields represented as key-value pairs.</param>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        Task<ApiResponse<object>> AddJob(string documentId, ParsedJob job, IEnumerable<string> roles = null, Dictionary<string, string> customFields = null);

        /// <summary>
        /// Delete job documents from environment
        /// </summary>
        /// <param name="documentIds">The document IDs to delete</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<DeleteDocumentsResponse> DeleteJobs(IEnumerable<string> documentIds);

        /// <summary>
        /// Match an existing job document with filters provided.
        /// </summary>
        /// <param name="query">The query object that will be combined with the match query to drive the search</param>
        /// <param name="options">Options for the Match request</param>
        /// <param name="sourceDocument">The document to generate the search query from</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<SearchResponse> MatchJobs(DocumentSource sourceDocument, Options options, SearchQuery query = null);

        /// <summary>
        /// Search for a job based on the query provided.
        /// </summary>
        /// <param name="query">The query object that will drive the search.</param>
        /// <param name="options">Options for the search request</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<SearchResponse> SearchJobs(SearchQuery query, Options options);

        /// <summary>
        /// Returns a list of suggested Completions. This endpoint is used to give a user instant
        /// feedback while typing a query. If the given field is the FULLTEXT field, the service
        /// returns suggestions from all configured dictionaries that are not explicitly excluded from full-text suggestions.
        /// </summary>
        /// <param name="field">Which field to use to retrieve completions</param>
        /// <param name="input">The user-typed input string.</param>
        /// <param name="languages">
        /// Optional 2-letter ISO-639-1 language codes. The first language is used for field label translations.
        /// All languages are used to retrieve completions when the environment doesn't have default languages set.
        /// </param>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        Task<AutocompleteResponse> AutocompleteJobs(AutocompleteJobsField field, string input, params string[] languages);

        #endregion
    }
}

// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models;
using Sovren.Models.API.Account;
using Sovren.Models.API.BimetricScoring;
using Sovren.Models.API.Formatter;
using Sovren.Models.API.Geocoding;
using Sovren.Models.API.Indexes;
using Sovren.Models.API.Matching;
using Sovren.Models.API.Matching.Request;
using Sovren.Models.API.Parsing;
using Sovren.Models.Job;
using Sovren.Models.Matching;
using Sovren.Models.Resume;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sovren
{
    /// <summary>
    /// The SDK client to perform Sovren API calls.
    /// </summary>
    public interface ISovrenClient
    {
        /// <summary>
        /// Get the account info (remaining credits, max concurrency, etc).
        /// </summary>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        Task<GetAccountInfoResponse> GetAccountInfo();

        /// <summary>
        /// Format a parsed resume into a standardized/templated resume
        /// </summary>
        /// <param name="request">The request body</param>
        /// <returns>The formatted resume document</returns>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        Task<FormatResumeResponse> FormatResume(FormatResumeRequest request);

        #region Parsing

        /// <summary>
        /// Parse a resume
        /// </summary>
        /// <param name="request">The request body</param>
        /// <returns>The parse result and any metadata</returns>
        /// <exception cref="SovrenException">Thrown when a parsing or API error occurred</exception>
        /// <exception cref="SovrenGeocodeResumeException">Thrown when parsing was successful, but an error occurred during geocoding</exception>
        /// <exception cref="SovrenIndexResumeException">Thrown when parsing was successful, but an error occurred during indexing</exception>
        Task<ParseResumeResponse> ParseResume(ParseRequest request);


        /// <summary>
        /// Parse a job
        /// </summary>
        /// <param name="request">The request body</param>
        /// <returns>The parse result and any metadata</returns>
        /// <exception cref="SovrenException">Thrown when a parsing or API error occurred</exception>
        /// <exception cref="SovrenGeocodeJobException">Thrown when parsing was successful, but an error occurred during geocoding</exception>
        /// <exception cref="SovrenIndexJobException">Thrown when parsing was successful, but an error occurred during indexing</exception>
        Task<ParseJobResponse> ParseJob(ParseRequest request);

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
        Task<CreateIndexResponse> CreateIndex(IndexType type, string indexId);

        /// <summary>
        /// Get all existing indexes
        /// </summary>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        Task<GetAllIndexesResponse> GetAllIndexes();

        /// <summary>
        /// Delete an existing index. Note that this is a destructive action and 
        /// cannot be undone. All the documents in this index will be deleted.
        /// </summary>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        Task<DeleteIndexResponse> DeleteIndex(string indexId);

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
        Task<IndexDocumentResponse> IndexDocument(ParsedResume resume, string indexId, string documentId, IEnumerable<string> userDefinedTags = null);

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
        Task<IndexDocumentResponse> IndexDocument(ParsedJob job, string indexId, string documentId, IEnumerable<string> userDefinedTags = null);

        /// <summary>
        /// Add several resumes to an existing index
        /// </summary>
        /// <param name="resumes">The resumes generated by the Sovren Resume Parser paired with their DocumentIds</param>
        /// <param name="indexId">The index the resumes should be added into (case-insensitive).</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        Task<IndexMultipleDocumentsResponse> IndexMultipleDocuments(IEnumerable<IndexResumeInfo> resumes, string indexId);

        /// <summary>
        /// Add several jobs to an existing index
        /// </summary>
        /// <param name="jobs">The jobs generated by the Sovren Job Parser paired with their DocumentIds</param>
        /// <param name="indexId">The index the jobs should be added into (case-insensitive).</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        Task<IndexMultipleDocumentsResponse> IndexMultipleDocuments(IEnumerable<IndexJobInfo> jobs, string indexId);

        /// <summary>
        /// Delete an existing document from an index
        /// </summary>
        /// <param name="indexId">The index containing the document</param>
        /// <param name="documentId">The ID of the document to delete</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        Task<DeleteDocumentResponse> DeleteDocument(string indexId, string documentId);

        /// <summary>
        /// Delete a group of existing documents from an index
        /// </summary>
        /// <param name="indexId">The index containing the documents</param>
        /// <param name="documentIds">The IDs of the documents to delete</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        Task<DeleteMultipleDocumentsResponse> DeleteMultipleDocuments(string indexId, IEnumerable<string> documentIds);

        /// <summary>
        /// Retrieve an existing resume from an index
        /// </summary>
        /// <param name="indexId">The index containing the resume</param>
        /// <param name="documentId">The ID of the resume to retrieve</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        Task<GetResumeResponse> GetResume(string indexId, string documentId);

        /// <summary>
        /// Retrieve an existing job from an index
        /// </summary>
        /// <param name="indexId">The index containing the job</param>
        /// <param name="documentId">The ID of the job to retrieve</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        Task<GetJobResponse> GetJob(string indexId, string documentId);

        /// <summary>
        /// Updates the user-defined tags for a resume
        /// </summary>
        /// <param name="indexId">The index containing the resume</param>
        /// <param name="documentId">The ID of the resume to update</param>
        /// <param name="userDefinedTags">The user-defined tags to add/delete/etc</param>
        /// <param name="method">Which method to use for the specified user-defined tags</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        Task<UpdateUserDefinedTagsResponse> UpdateResumeUserDefinedTags(
            string indexId,
            string documentId,
            IEnumerable<string> userDefinedTags,
            UserDefinedTagsMethod method);


        /// <summary>
        /// Updates the user-defined tags for a job
        /// </summary>
        /// <param name="indexId">The index containing the job</param>
        /// <param name="documentId">The ID of the job to update</param>
        /// <param name="userDefinedTags">The user-defined tags to add/delete/etc</param>
        /// <param name="method">Which method to use for the specified user-defined tags</param>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        Task<UpdateUserDefinedTagsResponse> UpdateJobUserDefinedTags(
            string indexId,
            string documentId,
            IEnumerable<string> userDefinedTags,
            UserDefinedTagsMethod method);

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
        Task<MatchResponse> Match(
            ParsedResume resume,
            IEnumerable<string> indexesToQuery,
            CategoryWeights preferredWeights = null,
            FilterCriteria filters = null,
            SearchMatchSettings settings = null,
            int numResults = 0);

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
        Task<MatchResponse> Match(
            ParsedJob job,
            IEnumerable<string> indexesToQuery,
            CategoryWeights preferredWeights = null,
            FilterCriteria filters = null,
            SearchMatchSettings settings = null,
            int numResults = 0);

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
        Task<MatchResponse> Match(
            string indexId,
            string documentId,
            IEnumerable<string> indexesToQuery,
            CategoryWeights preferredWeights = null,
            FilterCriteria filters = null,
            SearchMatchSettings settings = null,
            int numResults = 0);

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
        Task<SearchResponse> Search(
            IEnumerable<string> indexesToQuery,
            FilterCriteria query,
            SearchMatchSettings settings = null,
            PaginationSettings pagination = null);

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
        Task<BimetricScoreResponse> BimetricScore<TTarget>(
            ParsedResumeWithId sourceResume,
            List<TTarget> targetDocuments,
            CategoryWeights preferredWeights = null,
            SearchMatchSettings settings = null) where TTarget : IParsedDocWithId;


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
        Task<BimetricScoreResponse> BimetricScore<TTarget>(
            ParsedJobWithId sourceJob,
            List<TTarget> targetDocuments,
            CategoryWeights preferredWeights = null,
            SearchMatchSettings settings = null) where TTarget : IParsedDocWithId;

        #endregion

        #region Geocoding

        /// <summary>
        /// Uses the address in the resume (if present) to look up geocoordinates and add them into the ParsedResume object.
        /// These coordinates are used by the AI Searching/Matching engine.
        /// </summary>
        /// <param name="resume">The resume to geocode</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        Task<GeocodeResumeResponse> Geocode(ParsedResume resume, GeocodeCredentials geocodeCredentials = null);

        /// <summary>
        /// Use this if you would like to provide an address for geocoding instead of using the one in the parsed
        /// resume. The address included in the parsed resume (if present) will not be modified.
        /// </summary>
        /// <param name="resume">The resume to insert the geocoordinates (from the address) into</param>
        /// <param name="address">The address to use to retrieve geocoordinates</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        Task<GeocodeResumeResponse> Geocode(ParsedResume resume, Address address, GeocodeCredentials geocodeCredentials = null);

        /// <summary>
        /// Uses the address in the job (if present) to look up geocoordinates and add them into the ParsedJob object.
        /// These coordinates are used by the AI Searching/Matching engine.
        /// </summary>
        /// <param name="job">The job to geocode</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        Task<GeocodeJobResponse> Geocode(ParsedJob job, GeocodeCredentials geocodeCredentials = null);

        /// <summary>
        /// Use this if you would like to provide an address for geocoding instead of using the one in the parsed
        /// job. The address included in the parsed job (if present) will not be modified.
        /// </summary>
        /// <param name="job">The job to insert the geocoordinates (from the address) into</param>
        /// <param name="address">The address to use to retrieve geocoordinates</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        Task<GeocodeJobResponse> Geocode(ParsedJob job, Address address, GeocodeCredentials geocodeCredentials = null);

        /// <summary>
        /// Uses the address in the resume (if present) to look up geocoordinates and add them into the ParsedResume object.
        /// These coordinates are used by the AI Searching/Matching engine.
        /// </summary>
        /// <param name="resume">The resume to geocode</param>
        /// <param name="indexingOptions">What index/document id to use to index the document after geocoding</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <param name="indexIfGeocodeFails">Indicates whether or not the document should still be added to the index if the geocode request fails. Default is false.</param>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        Task<GeocodeAndIndexResumeResponse> GeocodeAndIndex(
            ParsedResume resume,
            IndexSingleDocumentInfo indexingOptions,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false);

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
        Task<GeocodeAndIndexResumeResponse> GeocodeAndIndex(
            ParsedResume resume,
            IndexSingleDocumentInfo indexingOptions,
            Address address,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false);

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
        Task<GeocodeAndIndexResumeResponse> GeocodeAndIndex(
            ParsedResume resume,
            IndexSingleDocumentInfo indexingOptions,
            GeoCoordinates coordinates,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false);

        /// <summary>
        /// Uses the address in the job (if present) to look up geocoordinates and add them into the ParsedJob object.
        /// These coordinates are used by the AI Searching/Matching engine.
        /// </summary>
        /// <param name="job">The job to geocode</param>
        /// <param name="indexingOptions">What index/document id to use to index the document after geocoding</param>
        /// <param name="geocodeCredentials">The credentials used for geocoding</param>
        /// <param name="indexIfGeocodeFails">Indicates whether or not the document should still be added to the index if the geocode request fails. Default is false.</param>
        /// <exception cref="SovrenException">Thrown when an API error occurred</exception>
        Task<GeocodeAndIndexJobResponse> GeocodeAndIndex(
            ParsedJob job,
            IndexSingleDocumentInfo indexingOptions,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false);

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
        Task<GeocodeAndIndexJobResponse> GeocodeAndIndex(
            ParsedJob job,
            IndexSingleDocumentInfo indexingOptions,
            Address address,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false);

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
        Task<GeocodeAndIndexJobResponse> GeocodeAndIndex(
            ParsedJob job,
            IndexSingleDocumentInfo indexingOptions,
            GeoCoordinates coordinates,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false);

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
        Task<GeocodeAndIndexResumeResponse> GeocodeAndIndex(
            ParsedResume resume,
            IndexSingleDocumentInfo indexingOptions,
            GeoCoordinates coordinates,
            Address address,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false);

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
        Task<GeocodeAndIndexJobResponse> GeocodeAndIndex(
            ParsedJob job,
            IndexSingleDocumentInfo indexingOptions,
            GeoCoordinates coordinates,
            Address address,
            GeocodeCredentials geocodeCredentials = null,
            bool indexIfGeocodeFails = false);

        #endregion
    }
}

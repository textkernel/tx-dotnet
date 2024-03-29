// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.API.BimetricScoring;
using Textkernel.Tx.Models.API.Matching;
using Textkernel.Tx.Models.API.Matching.Request;
using Textkernel.Tx.Models.API.Matching.UI;
using Textkernel.Tx.Models.Job;
using Textkernel.Tx.Models.Resume;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Textkernel.Tx
{
    /// <summary/>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public class TxClientWithUI
    {
        internal MatchUISettings UISessionOptions { get; set; }
        internal TxClient InternalClient { get; set; }
        internal TxClientWithUI(TxClient client, MatchUISettings uiSessionOptions)
        {
            InternalClient = client;
            UISessionOptions = uiSessionOptions;
        }
    }

    /// <summary/>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public static class TxClientExtensions
    {
        /// <summary>
        /// Access methods for generating Matching UI sessions. For example: <code>txClient.UI(options).Search(...)</code>
        /// </summary>
        /// <param name="client">the internal TxClient to make the API calls</param>
        /// <param name="uiSessionOptions">
        /// Options/settings for the Matching UI.
        /// <br/>NOTE: if you do not provide a <see cref="BasicUIOptions.Username"/> (in <see cref="MatchUISettings.UIOptions"/>),
        /// the user will be prompted to login as soon as the Matching UI session is loaded
        /// </param>
        public static TxClientWithUI UI(this TxClient client, MatchUISettings uiSessionOptions = null)
        {
            return new TxClientWithUI(client, uiSessionOptions);
        }

        /// <summary>
        /// Create a Matching UI session to find matches for a resume or job that is already indexed
        /// </summary>
        /// <param name="txClient">The TxClient</param>
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
        public static async Task<GenerateUIResponse> Match(
            this TxClientWithUI txClient,
            string indexId,
            string documentId,
            IEnumerable<string> indexesToQuery,
            CategoryWeights preferredWeights = null,
            FilterCriteria filters = null,
            SearchMatchSettings settings = null,
            int numResults = 0)
        {
            MatchByDocumentIdOptions options = txClient.InternalClient.CreateRequest(indexesToQuery, preferredWeights, filters, settings, numResults);
            UIMatchByDocumentIdOptions uiOptions = new UIMatchByDocumentIdOptions(options, txClient.UISessionOptions);
            return await txClient.InternalClient.UIMatch(indexId, documentId, uiOptions);
        }

        /// <summary>
        /// Create a Matching UI session to search for resumes or jobs that meet specific criteria
        /// </summary>
        /// <param name="txClient">The TxClient</param>
        /// <param name="indexesToQuery">The indexes to find results in. These must all be of the same type (resumes or jobs)</param>
        /// <param name="query">The search query. A result must satisfy all of these criteria</param>
        /// <param name="settings">The settings for this search request</param>
        /// <param name="pagination">Pagination settings. If not specified the default will be used</param>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public static async Task<GenerateUIResponse> Search(
            this TxClientWithUI txClient,
            IEnumerable<string> indexesToQuery,
            FilterCriteria query,
            SearchMatchSettings settings = null,
            PaginationSettings pagination = null)
        {
            SearchRequest request = txClient.InternalClient.CreateRequest(indexesToQuery, query, settings, pagination);
            UISearchRequest uiRequest = new UISearchRequest(request, txClient.UISessionOptions);
            return await txClient.InternalClient.UISearch(uiRequest);
        }

        /// <summary>
        /// Create a Matching UI session to find matches for a non-indexed resume
        /// </summary>
        /// <param name="txClient">The TxClient</param>
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
        public static async Task<GenerateUIResponse> Match(
            this TxClientWithUI txClient,
            ParsedResume resume,
            IEnumerable<string> indexesToQuery,
            CategoryWeights preferredWeights = null,
            FilterCriteria filters = null,
            SearchMatchSettings settings = null,
            int numResults = 0)
        {
            MatchResumeRequest request = txClient.InternalClient.CreateRequest(resume, indexesToQuery, preferredWeights, filters, settings, numResults);
            UIMatchResumeRequest uiRequest = new UIMatchResumeRequest(request, txClient.UISessionOptions);
            return await txClient.InternalClient.UIMatch(uiRequest);
        }

        /// <summary>
        /// Create a Matching UI session to find matches for a non-indexed job
        /// </summary>
        /// <param name="txClient">The TxClient</param>
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
        public static async Task<GenerateUIResponse> Match(
            this TxClientWithUI txClient,
            ParsedJob job,
            IEnumerable<string> indexesToQuery,
            CategoryWeights preferredWeights = null,
            FilterCriteria filters = null,
            SearchMatchSettings settings = null,
            int numResults = 0)
        {
            MatchJobRequest request = txClient.InternalClient.CreateRequest(job, indexesToQuery, preferredWeights, filters, settings, numResults);
            UIMatchJobRequest uiRequest = new UIMatchJobRequest(request, txClient.UISessionOptions);
            return await txClient.InternalClient.UIMatch(uiRequest);
        }

        /// <summary>
        /// Create a Matching UI session to score one or more target documents against a source resume
        /// </summary>
        /// <param name="txClient">The TxClient</param>
        /// <param name="sourceResume">The source resume</param>
        /// <param name="targetDocuments">The target resumes/jobs</param>
        /// <param name="preferredWeights">
        /// The preferred category weights for scoring the results. If none are provided,
        /// The best values will be determined based on the source resume
        /// </param>
        /// <param name="settings">Settings to be used for this scoring request</param>
        /// <typeparam name="TTarget">Either <see cref="ParsedResumeWithId"/> or <see cref="ParsedJobWithId"/></typeparam>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public static async Task<GenerateUIResponse> BimetricScore<TTarget>(
            this TxClientWithUI txClient,
            ParsedResumeWithId sourceResume,
            List<TTarget> targetDocuments,
            CategoryWeights preferredWeights = null,
            SearchMatchSettings settings = null) where TTarget : IParsedDocWithId
        {
            BimetricScoreResumeRequest request = txClient.InternalClient.CreateRequest(sourceResume, targetDocuments, preferredWeights, settings);
            UIBimetricScoreResumeRequest uiRequest = new UIBimetricScoreResumeRequest(request, txClient.UISessionOptions);
            return await txClient.InternalClient.UIBimetricScore(uiRequest);
        }

        /// <summary>
        /// Create a Matching UI session to score one or more target documents against a source job
        /// </summary>
        /// <param name="txClient">The TxClient</param>
        /// <param name="sourceJob">The source job</param>
        /// <param name="targetDocuments">The target resumes/jobs</param>
        /// <param name="preferredWeights">
        /// The preferred category weights for scoring the results. If none are provided,
        /// The best values will be determined based on the source job
        /// </param>
        /// <param name="settings">Settings to be used for this scoring request</param>
        /// <typeparam name="TTarget">Either <see cref="ParsedResumeWithId"/> or <see cref="ParsedJobWithId"/></typeparam>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public static async Task<GenerateUIResponse> BimetricScore<TTarget>(
            this TxClientWithUI txClient,
            ParsedJobWithId sourceJob,
            List<TTarget> targetDocuments,
            CategoryWeights preferredWeights = null,
            SearchMatchSettings settings = null) where TTarget : IParsedDocWithId
        {
            BimetricScoreJobRequest request = txClient.InternalClient.CreateRequest(sourceJob, targetDocuments, preferredWeights, settings);
            UIBimetricScoreJobRequest uiRequest = new UIBimetricScoreJobRequest(request, txClient.UISessionOptions);
            return await txClient.InternalClient.UIBimetricScore(uiRequest);
        }

        /// <summary>
        /// Create a Matching UI session to view a single resume from a Bimetric Score API response
        /// </summary>
        /// <param name="txClient">The TxClient</param>
        /// <param name="bimetricResponse">The Bimetric Score API response containing the result you want to view</param>
        /// <param name="resume">The specific resume/id in the result set that you want to view</param>
        /// <param name="sourceDocType">The type of document this result was scored against</param>
        /// <param name="htmlResume">Optionally, the HTML resume to display in the details view</param>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public static async Task<GenerateUIResponse> ViewDetails(
            this TxClientWithUI txClient,
            BimetricScoreResponseValue bimetricResponse,
            ParsedResumeWithId resume,
            Models.Matching.IndexType sourceDocType,
            string htmlResume = null)
        {
            BimetricScoreResumeDetails details = new BimetricScoreResumeDetails
            {
                Result = bimetricResponse.Matches.Single(m => m.Id == resume.Id),
                AppliedCategoryWeights = bimetricResponse.AppliedCategoryWeights,
                ResumeData = resume.ResumeData,
                SourceDocumentType = sourceDocType,
                HtmlDocument = htmlResume
            };

            UIBimetricScoreResumeDetailsRequest uiRequest = new UIBimetricScoreResumeDetailsRequest(details, txClient.UISessionOptions?.UIOptions);
            return await txClient.InternalClient.UIViewDetails(uiRequest);
        }

        /// <summary>
        /// Create a Matching UI session to view a single job from a Bimetric Score API response
        /// </summary>
        /// <param name="txClient">The TxClient</param>
        /// <param name="bimetricResponse">The Bimetric Score API response containing the result you want to view</param>
        /// <param name="job">The specific job/id in the result set that you want to view</param>
        /// <param name="sourceDocType">The type of document this result was scored against</param>
        /// <param name="htmlJob">Optionally, the HTML job to display in the details view</param>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public static async Task<GenerateUIResponse> ViewDetails(
            this TxClientWithUI txClient,
            BimetricScoreResponseValue bimetricResponse,
            ParsedJobWithId job,
            Models.Matching.IndexType sourceDocType,
            string htmlJob = null)
        {
            BimetricScoreJobDetails details = new BimetricScoreJobDetails
            {
                Result = bimetricResponse.Matches.Single(m => m.Id == job.Id),
                AppliedCategoryWeights = bimetricResponse.AppliedCategoryWeights,
                JobData = job.JobData,
                SourceDocumentType = sourceDocType,
                HtmlDocument = htmlJob
            };

            UIBimetricScoreJobDetailsRequest uiRequest = new UIBimetricScoreJobDetailsRequest(details, txClient.UISessionOptions?.UIOptions);
            return await txClient.InternalClient.UIViewDetails(uiRequest);
        }

        /// <summary>
        /// Create a Matching UI session to view a single result from an AI Matching API response
        /// </summary>
        /// <param name="txClient">The TxClient</param>
        /// <param name="matchResponse">The AI Matching API response containing the result you want to view</param>
        /// <param name="matchId">The id of the specific result in the result set that you want to view</param>
        /// <param name="sourceDocType">The type of document this result was scored against</param>
        /// <param name="htmlDocument">Optionally, the HTML resume/job to display in the details view</param>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        public static async Task<GenerateUIResponse> ViewDetails(
            this TxClientWithUI txClient,
            MatchResponseValue matchResponse,
            string matchId,
            Models.Matching.IndexType sourceDocType,
            string htmlDocument = null)
        {
            AIMatchDetails details = new AIMatchDetails
            {
                Result = matchResponse.Matches.Single(m => m.Id == matchId),
                AppliedCategoryWeights = matchResponse.AppliedCategoryWeights,
                SourceDocumentType = sourceDocType,
                HtmlDocument = htmlDocument
            };

            UIMatchDetailsRequest uiRequest = new UIMatchDetailsRequest(details, txClient.UISessionOptions?.UIOptions);
            return await txClient.InternalClient.UIViewDetails(uiRequest);
        }
    }
}

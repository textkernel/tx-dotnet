// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API.BimetricScoring;
using Sovren.Models.API.Indexes;
using Sovren.Models.API.Matching;
using Sovren.Models.API.Matching.Request;
using Sovren.Models.API.Matching.UI;
using Sovren.Models.Job;
using Sovren.Models.Resume;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sovren.Services
{
    /// <summary/>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public class AIMatchingServiceWithUI
    {
        internal MatchUISettings UISessionOptions { get; set; }
        internal AIMatchingService InternalService { get; set; }
        internal AIMatchingServiceWithUI(AIMatchingService service, MatchUISettings uiSessionOptions)
        {
            InternalService = service;
            UISessionOptions = uiSessionOptions;
        }
    }

    /// <summary/>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public static class AIMatchingServiceExtensions
    {
        /// <summary>
        /// Access methods for generating Sovren Matching UI sessions. For example: <code>aiMatchingService.UI(options).Search(...)</code>
        /// </summary>
        /// <param name="service">the internal AIM service</param>
        /// <param name="uiSessionOptions">
        /// Options/settings for the Matching UI.
        /// <br/>NOTE: if you do not provide a <see cref="UIOptions.Username"/> (in <see cref="MatchUISettings.UIOptions"/>),
        /// the user will be prompted to login as soon as the Matching UI session is loaded
        /// </param>
        public static AIMatchingServiceWithUI UI(this AIMatchingService service, MatchUISettings uiSessionOptions = null)
        {
            return new AIMatchingServiceWithUI(service, uiSessionOptions);
        }

        /// <summary>
        /// Create a Matching UI session to find matches for a resume or job that is already indexed
        /// </summary>
        /// <param name="aimSvc">The AI Matching service</param>
        /// <param name="indexId">The index containing the document you want to match</param>
        /// <param name="documentId">The ID of the document to match</param>
        /// <param name="indexesToQuery">The indexes to find results in. These must all be of the same type (resumes or jobs)</param>
        /// <param name="preferredWeights">
        /// The preferred category weights for scoring the results. If none are provided,
        /// Sovren will determine the best values based on the source resume/job
        /// </param>
        /// <param name="filters">Any filters to apply prior to the match (a result must satisfy all the filters)</param>
        /// <returns>A <see cref="GenerateUIResponse"/> with a URL for the Matching UI session</returns>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public static async Task<GenerateUIResponse> MatchIndexedDocument(this AIMatchingServiceWithUI aimSvc, string indexId, string documentId,
            List<string> indexesToQuery, CategoryWeights preferredWeights = null, FilterCriteria filters = null)
        {
            MatchByDocumentIdOptions options = aimSvc.InternalService.CreateRequest(indexesToQuery, preferredWeights, filters);
            UIMatchByDocumentIdOptions uiOptions = new UIMatchByDocumentIdOptions(options, aimSvc.UISessionOptions);
            return await aimSvc.InternalService.Client.UIMatch(indexId, documentId, uiOptions);
        }

        /// <summary>
        /// Create a Matching UI session to search for resumes or jobs that meet specific criteria
        /// </summary>
        /// <param name="aimSvc">The AI Matching service</param>
        /// <param name="indexesToQuery">The indexes to find results in. These must all be of the same type (resumes or jobs)</param>
        /// <param name="query">The search query. A result must satisfy all of these criteria</param>
        /// <param name="skip">For pagination, the number of results to skip</param>
        /// <returns>A <see cref="GenerateUIResponse"/> with a URL for the Matching UI session</returns>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public static async Task<GenerateUIResponse> Search(this AIMatchingServiceWithUI aimSvc, List<string> indexesToQuery,
            FilterCriteria query, uint skip = 0)
        {
            SearchRequest request = aimSvc.InternalService.CreateRequest(indexesToQuery, query, skip);
            UISearchRequest uiRequest = new UISearchRequest(request, aimSvc.UISessionOptions);
            return await aimSvc.InternalService.Client.UISearch(uiRequest);
        }

        /// <summary>
        /// Create a Matching UI session to find matches for a non-indexed resume
        /// </summary>
        /// <param name="aimSvc">The AI Matching service</param>
        /// <param name="resume">The resume (generated by the Sovren Resume Parser) to use as the source for a match query</param>
        /// <param name="indexesToQuery">The indexes to find results in. These must all be of the same type (resumes or jobs)</param>
        /// <param name="preferredWeights">
        /// The preferred category weights for scoring the results. If none are provided,
        /// Sovren will determine the best values based on the source resume
        /// </param>
        /// <param name="filters">Any filters to apply prior to the match (a result must satisfy all the filters)</param>
        /// <returns>A <see cref="GenerateUIResponse"/> with a URL for the Matching UI session</returns>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public static async Task<GenerateUIResponse> MatchResume(this AIMatchingServiceWithUI aimSvc, ParsedResume resume,
            List<string> indexesToQuery, CategoryWeights preferredWeights = null, FilterCriteria filters = null)
        {
            MatchResumeRequest request = aimSvc.InternalService.CreateRequest(resume, indexesToQuery, preferredWeights, filters);
            UIMatchResumeRequest uiRequest = new UIMatchResumeRequest(request, aimSvc.UISessionOptions);
            return await aimSvc.InternalService.Client.UIMatch(uiRequest);
        }

        /// <summary>
        /// Create a Matching UI session to find matches for a non-indexed job
        /// </summary>
        /// <param name="aimSvc">The AI Matching service</param>
        /// <param name="job">The job (generated by the Sovren Job Parser) to use as the source for a match query</param>
        /// <param name="indexesToQuery">The indexes to find results in. These must all be of the same type (resumes or jobs)</param>
        /// <param name="preferredWeights">
        /// The preferred category weights for scoring the results. If none are provided,
        /// Sovren will determine the best values based on the source job
        /// </param>
        /// <param name="filters">Any filters to apply prior to the match (a result must satisfy all the filters)</param>
        /// <returns>A <see cref="GenerateUIResponse"/> with a URL for the Matching UI session</returns>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public static async Task<GenerateUIResponse> MatchJob(this AIMatchingServiceWithUI aimSvc, ParsedJob job,
            List<string> indexesToQuery, CategoryWeights preferredWeights = null, FilterCriteria filters = null)
        {
            MatchJobRequest request = aimSvc.InternalService.CreateRequest(job, indexesToQuery, preferredWeights, filters);
            UIMatchJobRequest uiRequest = new UIMatchJobRequest(request, aimSvc.UISessionOptions);
            return await aimSvc.InternalService.Client.UIMatch(uiRequest);
        }
    }

    /// <summary/>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public class BimetricScoringServiceWithUI
    {
        internal MatchUISettings UISessionOptions { get; set; }
        internal BimetricScoringService InternalService { get; set; }
        internal BimetricScoringServiceWithUI(BimetricScoringService service, MatchUISettings uiSessionOptions)
        {
            InternalService = service;
            UISessionOptions = uiSessionOptions;
        }
    }

    /// <summary/>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public static class BimetricScoringServiceExtensions
    {
        /// <summary>
        /// Access methods for generating Sovren Matching UI sessions. For example: <code>scoringService.UI(options).BimetricScore(...)</code>
        /// </summary>
        /// <param name="service">the internal bimetric scoring service</param>
        /// <param name="uiSessionOptions">
        /// Options/settings for the Matching UI.
        /// <br/>NOTE: if you do not provide a <see cref="UIOptions.Username"/> (in <see cref="MatchUISettings.UIOptions"/>),
        /// the user will be prompted to login as soon as the Matching UI session is loaded
        /// </param>
        public static BimetricScoringServiceWithUI UI(this BimetricScoringService service, MatchUISettings uiSessionOptions = null)
        {
            return new BimetricScoringServiceWithUI(service, uiSessionOptions);
        }

        /// <summary>
        /// Create a Matching UI session to score one or more target documents against a source resume
        /// </summary>
        /// <param name="scoreSvc">The Bimetric scoring service</param>
        /// <param name="sourceResume">The source resume</param>
        /// <param name="targetDocuments">The target resumes/jobs</param>
        /// <param name="preferredWeights">
        /// The preferred category weights for scoring the results. If none are provided,
        /// Sovren will determine the best values based on the source resume
        /// </param>
        /// <typeparam name="TTarget">Either <see cref="ParsedResumeWithId"/> or <see cref="ParsedJobWithId"/></typeparam>
        /// <returns>A <see cref="GenerateUIResponse"/> with a URL for the Matching UI session</returns>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public static async Task<GenerateUIResponse> BimetricScore<TTarget>(this BimetricScoringServiceWithUI scoreSvc, ParsedResumeWithId sourceResume, List<TTarget> targetDocuments,
            CategoryWeights preferredWeights = null) where TTarget : IParsedDocWithId
        {
            BimetricScoreResumeRequest request = scoreSvc.InternalService.CreateRequest(sourceResume, targetDocuments, preferredWeights);
            UIBimetricScoreResumeRequest uiRequest = new UIBimetricScoreResumeRequest(request, scoreSvc.UISessionOptions);
            return await scoreSvc.InternalService.Client.UIBimetricScore(uiRequest);
        }

        /// <summary>
        /// Create a Matching UI session to score one or more target documents against a source job
        /// </summary>
        /// <param name="scoreSvc">The Bimetric scoring service</param>
        /// <param name="sourceJob">The source job</param>
        /// <param name="targetDocuments">The target resumes/jobs</param>
        /// <param name="preferredWeights">
        /// The preferred category weights for scoring the results. If none are provided,
        /// Sovren will determine the best values based on the source job
        /// </param>
        /// <typeparam name="TTarget">Either <see cref="ParsedResumeWithId"/> or <see cref="ParsedJobWithId"/></typeparam>
        /// <returns>A <see cref="GenerateUIResponse"/> with a URL for the Matching UI session</returns>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public static async Task<GenerateUIResponse> BimetricScore<TTarget>(this BimetricScoringServiceWithUI scoreSvc, ParsedJobWithId sourceJob, List<TTarget> targetDocuments,
            CategoryWeights preferredWeights = null) where TTarget : IParsedDocWithId
        {
            BimetricScoreJobRequest request = scoreSvc.InternalService.CreateRequest(sourceJob, targetDocuments, preferredWeights);
            UIBimetricScoreJobRequest uiRequest = new UIBimetricScoreJobRequest(request, scoreSvc.UISessionOptions);
            return await scoreSvc.InternalService.Client.UIBimetricScore(uiRequest);
        }
    }
}

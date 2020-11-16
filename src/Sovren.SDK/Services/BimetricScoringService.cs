// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API.BimetricScoring;
using Sovren.Models.API.Matching;
using Sovren.Models.API.Matching.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sovren.Services
{
    /// <inheritdoc/>
    public class BimetricScoringService : SovrenService
    {
        /// <summary>
        /// Settings for all bimetric scores
        /// </summary>
        public SearchMatchSettings Settings { get; set; }

        /// <summary>
        /// Create a service to Bimetric score documents. If you are using Sovren's AI Matching, you probably should use <see cref="AIMatchingService"/>.
        /// <br/>
        /// <strong>
        /// Note that this class is not thread-safe, therefore you should never share a 
        /// service across multiple threads. Instead, use a single service per thread.
        /// </strong>
        /// </summary>
        /// <param name="client">The SovrenClient that will make the low-level API calls</param>
        /// <param name="settings">Settings to be used for all scoring transactions. These can be changed on-the-fly via <see cref="Settings"/></param>
        public BimetricScoringService(SovrenClient client, SearchMatchSettings settings = null)
            : base(client)
        {
            Settings = settings;
        }

        /// <summary>
        /// Score one or more target documents against a source resume
        /// </summary>
        /// <param name="sourceResume">The source resume</param>
        /// <param name="targetDocuments">The target resumes/jobs</param>
        /// <param name="preferredWeights">
        /// The preferred category weights for scoring the results. If none are provided,
        /// Sovren will determine the best values based on the source resume
        /// </param>
        /// <typeparam name="TTarget">Either <see cref="ParsedResumeWithId"/> or <see cref="ParsedJobWithId"/></typeparam>
        /// <returns>A <see cref="BimetricScoreResponseValue"/> containing results and any metadata</returns>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<BimetricScoreResponseValue> BimetricScore<TTarget>(ParsedResumeWithId sourceResume, List<TTarget> targetDocuments,
            CategoryWeights preferredWeights = null) where TTarget : IParsedDocWithId
        {
            BimetricScoreResumeRequest request = CreateRequest(sourceResume, targetDocuments, preferredWeights);
            BimetricScoreResponse response = await Client.BimetricScore(request);
            return response?.Value;
        }

        internal BimetricScoreResumeRequest CreateRequest<TTarget>(ParsedResumeWithId sourceResume, List<TTarget> targetDocuments,
            CategoryWeights preferredWeights = null) where TTarget : IParsedDocWithId
        {
            return new BimetricScoreResumeRequest()
            {
                PreferredCategoryWeights = preferredWeights,
                Settings = Settings,
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
        /// Sovren will determine the best values based on the source job
        /// </param>
        /// <typeparam name="TTarget">Either <see cref="ParsedResumeWithId"/> or <see cref="ParsedJobWithId"/></typeparam>
        /// <returns>A <see cref="BimetricScoreResponseValue"/> containing results and any metadata</returns>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<BimetricScoreResponseValue> BimetricScore<TTarget>(ParsedJobWithId sourceJob, List<TTarget> targetDocuments,
            CategoryWeights preferredWeights = null) where TTarget : IParsedDocWithId
        {
            BimetricScoreJobRequest request = CreateRequest(sourceJob, targetDocuments, preferredWeights);
            BimetricScoreResponse response = await Client.BimetricScore(request);
            return response?.Value;
        }

        internal BimetricScoreJobRequest CreateRequest<TTarget>(ParsedJobWithId sourceJob, List<TTarget> targetDocuments,
            CategoryWeights preferredWeights = null) where TTarget : IParsedDocWithId
        {
            return new BimetricScoreJobRequest()
            {
                PreferredCategoryWeights = preferredWeights,
                Settings = Settings,
                SourceJob = sourceJob,
                TargetResumes = targetDocuments as List<ParsedResumeWithId>,
                TargetJobs = targetDocuments as List<ParsedJobWithId>
            };
        }
    }
}

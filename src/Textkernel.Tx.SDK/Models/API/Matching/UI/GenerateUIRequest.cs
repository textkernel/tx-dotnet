// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.API.BimetricScoring;
using Textkernel.Tx.Models.API.Geocoding;
using Textkernel.Tx.Models.API.Matching.Request;
using Textkernel.Tx.Models.API.Parsing;

namespace Textkernel.Tx.Models.API.Matching.UI
{
    /// <inheritdoc/>
    public class UISearchRequest : GenerateUIRequest<SearchRequest>
    {
        internal UISearchRequest(SearchRequest request, MatchUISettings settings)
            : base(request, settings) { }
    }

    /// <inheritdoc/>
    public class UIMatchResumeRequest : GenerateUIRequest<MatchResumeRequest>
    {
        internal UIMatchResumeRequest(MatchResumeRequest request, MatchUISettings settings)
            : base(request, settings) { }
    }

    /// <inheritdoc/>
    public class UIMatchJobRequest : GenerateUIRequest<MatchJobRequest>
    {
        internal UIMatchJobRequest(MatchJobRequest request, MatchUISettings settings)
            : base(request, settings) { }
    }

    /// <inheritdoc/>
    public class UIMatchByDocumentIdOptions : GenerateUIRequest<MatchByDocumentIdOptions> 
    {
        internal UIMatchByDocumentIdOptions(MatchByDocumentIdOptions options, MatchUISettings settings)
            : base(options, settings) { }
    }

    /// <inheritdoc/>
    public class UIBimetricScoreResumeRequest : GenerateUIRequest<BimetricScoreResumeRequest>
    {
        internal UIBimetricScoreResumeRequest(BimetricScoreResumeRequest request, MatchUISettings settings)
            : base(request, settings) { }
    }

    /// <inheritdoc/>
    public class UIBimetricScoreJobRequest : GenerateUIRequest<BimetricScoreJobRequest>
    {
        internal UIBimetricScoreJobRequest(BimetricScoreJobRequest request, MatchUISettings settings)
            : base(request, settings) { }
    }

    /// <summary>
    /// Settings for generating a Matching UI session
    /// </summary>
    public class MatchUISettings
    {
        /// <summary>
        /// Various options for the Matching UI user experience
        /// </summary>
        public UIOptions UIOptions { get; set; }

        /// <summary>
        /// Options for parsing documents from external sources such as job boards
        /// and custom web sourcing. You only need to use this if you are using Sourcing
        /// </summary>
        public BasicParseOptions ParseOptions { get; set; }

        /// <summary>
        /// Settings for geocoding within the Matching UI. This is used
        /// when you allow your users to perform radius filtering.
        /// </summary>
        public GeocodeOptions GeocodeOptions { get; set; }

        internal void CopyFrom(MatchUISettings other)
        {
            UIOptions = other?.UIOptions;
            ParseOptions = other?.ParseOptions;
            GeocodeOptions = other?.GeocodeOptions;
        }
    }

    /// <summary>
    /// The request body for generating a Matching UI session
    /// </summary>
    /// <typeparam name="T">The type of search/match to be performed in the session</typeparam>
    public class GenerateUIRequest<T> : MatchUISettings
    {
        /// <summary>
        /// The SaaS request that defines the match/search.
        /// </summary>
        public T SaasRequest { get; set; }

        internal GenerateUIRequest(T saasRequest, MatchUISettings settings)
        {
            SaasRequest = saasRequest;
            CopyFrom(settings);
        }
    }
}

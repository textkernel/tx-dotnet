// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models;
using Textkernel.Tx.Models.API.Account;
using Textkernel.Tx.Models.API.BimetricScoring;
using Textkernel.Tx.Models.API.DataEnrichment;
using Textkernel.Tx.Models.API.DataEnrichment.Ontology.Response;
using Textkernel.Tx.Models.API.DataEnrichment.Professions.Response;
using Textkernel.Tx.Models.API.DataEnrichment.Skills.Response;
using Textkernel.Tx.Models.API.Formatter;
using Textkernel.Tx.Models.API.Geocoding;
using Textkernel.Tx.Models.API.Indexes;
using Textkernel.Tx.Models.API.Matching;
using Textkernel.Tx.Models.API.Matching.Request;
using Textkernel.Tx.Models.API.Parsing;
using Textkernel.Tx.Models.DataEnrichment;
using Textkernel.Tx.Models.Job;
using Textkernel.Tx.Models.Matching;
using Textkernel.Tx.Models.Resume;
using System.Collections.Generic;
using System.Threading.Tasks;
using Textkernel.Tx.Models.API.JobDescription;
using Textkernel.Tx.Clients;

namespace Textkernel.Tx
{
    /// <summary>
    /// The SDK client to perform Tx API calls.
    /// </summary>
    public interface ITxClient
    {
        /// <summary>
        /// Get the account info (remaining credits, max concurrency, etc).
        /// </summary>
        /// <exception cref="TxException">Thrown when an API error occurs</exception>
        Task<GetAccountInfoResponse> GetAccountInfo();

        /// <summary>
        /// Contains all endpoints/methods for the Resume Formatter
        /// </summary>
        IFormatterClient Formatter { get; }

        /// <summary>
        /// Contains all endpoints/methods for the Resume &amp; Job Parsers
        /// </summary>
        IParserClient Parser { get; }

        /// <summary>
        /// Contains all endpoints/methods for the Geocoder
        /// </summary>
        IGeocoderClient Geocoder { get; }

        /// <summary>
        /// Contains all endpoints/methods for Search &amp; Match
        /// </summary>
        ISearchMatchClient SearchMatch { get; }

        /// <summary>
        /// Contains all endpoints/methods for Skills Intelligence
        /// </summary>
        ISkillsIntelligenceClient SkillsIntelligence { get; }

        /// <summary>
        /// Contains all endpoints/methods for Match V2
        /// </summary>
        IMatchV2Client MatchV2 { get; }
    }
}

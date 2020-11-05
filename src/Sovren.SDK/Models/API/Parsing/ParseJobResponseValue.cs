// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.Job;

namespace Sovren.Models.API.Parsing
{
    /// <inheritdoc/>
    public class ParseJobResponseValue : BaseParseResponseValue
    {
        /// <summary>
        /// The main output from the Sovren Job Parser
        /// </summary>
        public ParsedJob JobData { get; set; }
    }
}

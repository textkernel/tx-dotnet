// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.Job;

namespace Textkernel.Tx.Models.API.Parsing
{
    /// <inheritdoc/>
    public class ParseJobResponseValue : BaseParseResponseValue
    {
        /// <summary>
        /// The main output from the Job Parser
        /// </summary>
        public ParsedJob JobData { get; set; }
    }
}

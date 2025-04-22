// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Text;
using Textkernel.Tx.Models.Job;

namespace Textkernel.Tx.Models.API.MatchV2.Request
{
    /// <summary>
    /// Request body for AddJob request
    /// </summary>
    public class AddJobRequest : AddDocumentRequestBase
    {
        /// <summary>
        /// Parsed output from the Job Parser
        /// </summary>
        public ParsedJob JobData { get; set; }

        /// <summary>
        /// Optional fields that match up to the custom fields set on an Account for Search &amp; Match V2
        /// </summary>
        public Dictionary<string, string> CustomFields { get; set; }
    }
}

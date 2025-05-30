﻿// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Textkernel.Tx.Models.API.MatchV2.Response
{
    /// <inheritdoc/>
    public class AutocompleteResponse : ApiResponse<AutocompleteResponseValue> { }

    /// <summary>
    /// response body for a candidates/jobs autocomplete
    /// </summary>
    public class AutocompleteResponseValue
    {
        /// <summary>
        /// A list of suggested completions
        /// </summary>
        public List<Completion> Return { get; set; }
    }
}

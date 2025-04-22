// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Text;
using Textkernel.Tx.Services;

namespace Textkernel.Tx.Models.API.MatchV2.Request
{
    /// <summary>
    /// Most SearchMatchV2 requests have Roles
    /// </summary>
    public class AddDocumentRequestBase
    {
        /// <summary>
        /// The roles associated with the request. Defaults to ["All"] if none are provided.
        /// </summary>
        public IEnumerable<string> Roles { get; set; }

        /// <summary>
        /// The target environment
        /// </summary>
        public MatchV2Environment SearchAndMatchEnvironment { get; set; }
    }
}

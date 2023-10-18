﻿// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Textkernel.Tx.Models.DataEnrichment;
using System.Collections.Generic;

namespace Textkernel.Tx.Models.API.DataEnrichment.Professions.Response
{
    /// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'GetProfessionsTaxonomy' response
	/// </summary>
    public class GetTaxonomyProfessionsResponseValue : Taxonomy
    {
        /// <summary>
        /// A list of returned professions.
        /// </summary>
        public List<ProfessionMultipleDescriptions> Professions { get; set; }
    }
}

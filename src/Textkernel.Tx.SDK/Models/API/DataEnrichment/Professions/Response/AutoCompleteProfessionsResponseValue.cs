// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Textkernel.Tx.Models.DataEnrichment;
using System.Collections.Generic;

namespace Textkernel.Tx.Models.API.DataEnrichment.Professions.Response
{
    /// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'AutocompleteProfessions' response.
	/// </summary>
    public class AutoCompleteProfessionsResponseValue
    {
        /// <summary>
        /// A list of professions that match the given Prefix.
        /// </summary>
        public List<BasicProfession> Professions { get; set; }
    }
}

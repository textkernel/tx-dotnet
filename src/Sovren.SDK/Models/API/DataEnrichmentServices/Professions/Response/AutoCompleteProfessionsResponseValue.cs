﻿// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Sovren.Models.API.DataEnrichmentServices.Professions.Response
{
    /// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'AutocompleteProfessions' response.
	/// </summary>
    public class AutoCompleteProfessionsResponseValue
    {
        /// <summary>
        /// A list of professions that match the given Prefix.
        /// </summary>
        public List<AutoCompleteProfession> Professions { get; set; }
    }

    /// <summary>
    /// A profession object that matched the given Prefix.
    /// </summary>
    public class AutoCompleteProfession
    {
        /// <summary>
        /// The description of the found profession in the requested language.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The code ID of this profession in the <see href="https://sovren.com/technical-specs/latest/rest-api/data-enrichment-services/overview/#professions-taxonomies">Sovren Professions Taxonomy</see>.
        /// </summary>
        public int CodeId { get; set; }
    }
}
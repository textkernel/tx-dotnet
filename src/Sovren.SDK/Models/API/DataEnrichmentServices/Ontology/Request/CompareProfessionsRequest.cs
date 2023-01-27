﻿// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.DataEnrichmentServices.Ontology.Request
{
    /// <summary>
    /// Request body for a 'CompareProfessions' request
    /// </summary>
    public class CompareProfessionsRequest
    {
        /// <summary>
        /// The two profession code IDs from the <see href="https://sovren.com/technical-specs/latest/rest-api/data-enrichment-services/overview/#professions-taxonomies">Sovren Professions Taxonomy</see> to compare. This list must have 2 values.
        /// </summary>
        public List<int> ProfessionCodeIds { get; set; }
    }
}
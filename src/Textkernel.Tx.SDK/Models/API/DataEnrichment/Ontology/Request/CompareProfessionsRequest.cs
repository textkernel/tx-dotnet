﻿// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Textkernel.Tx.Models.API.DataEnrichment.Ontology.Request
{
    /// <summary>
    /// Request body for a 'CompareProfessions' request
    /// </summary>
    public class CompareProfessionsRequest
    {
        /// <summary>
        /// A profession code ID from the <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-taxonomies">Professions Taxonomy</see> to compare.
        /// </summary>
        public int ProfessionACodeId { get; set; }
        /// <summary>
        /// A profession code ID from the <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-taxonomies">Professions Taxonomy</see> to compare.
        /// </summary>
        public int ProfessionBCodeId { get; set; }

        /// <summary>
        /// The language to use for the returned descriptions.
        /// </summary>
        public string OutputLanguage { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Textkernel.Tx.Models.API.DataEnrichment.Professions.Request
{
    /// <summary>
    /// Request body for a 'LookupProfessions' request
    /// </summary>
    public class LookupProfessionCodesRequest
    {
        /// <summary>
        /// The profession code IDs to get details about from the <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-taxonomies">Professions Taxonomy</see>.
        /// </summary>
        public List<int> CodeIds { get; set; }
        /// <summary>
        /// The language to use for professions descriptions (default is en). Must be an allowed <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO code</see>.
        /// </summary>
        public string OutputLanguage { get; set; } = "en";
    }
}

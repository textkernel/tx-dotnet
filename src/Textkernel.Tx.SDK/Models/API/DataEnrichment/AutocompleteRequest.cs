// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Textkernel.Tx.Models.API.DataEnrichment
{
    /// <summary>
    /// Request body for a 'Autocomplete' request
    /// </summary>
    public class AutocompleteRequest
    {
        /// <summary>
        /// The job title prefix to be completed. Must contain at least 1 character.
        /// </summary>
        public string Prefix { get; set; }
        /// <summary>
        /// The maximum number of returned professions. The default is 10 and the maximum is 100.
        /// </summary>
        public int Limit { get; set; } = 10;
        /// <summary>
        /// The language(s) used to search for matching professions (the language of the provided Prefix). A maximum of 5 languages can be provided. Must be one of the supported <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// </summary>
        public List<string> Languages { get; set; }
        /// <summary>
        /// The language to ouput the found professions in (default is 'en'). Must be one of the supported <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// </summary>
        public string OutputLanguage { get; set; } = "en";
    }
}

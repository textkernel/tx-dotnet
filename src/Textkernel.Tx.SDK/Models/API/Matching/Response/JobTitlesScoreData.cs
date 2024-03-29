// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System.Collections.Generic;

namespace Textkernel.Tx.Models.API.Matching.Response
{
    /// <inheritdoc/>
    public class JobTitlesScoreData : CategoryScoreData
    {
        /// <summary>
        /// List of terms requested, but not found.
        /// </summary>
        public List<string> NotFound { get; set; }

        /// <summary>
        /// List of job titles found in both documents.
        /// </summary>
        public List<FoundJobTitle> Found { get; set; }
    }

    /// <summary>
    /// Information about a job title match
    /// </summary>
    public class FoundJobTitle
    {
        /// <summary>
        /// Exact term found.
        /// </summary>
        public string RawTerm { get; set; }

        /// <summary>
        /// Original term that the variation was derived from (or <see langword="null"/> if the <see cref="RawTerm"/> was an exact match)
        /// </summary>
        public string VariationOf { get; set; }

        /// <summary>
        /// <see langword="true"/> when the job title found is in the current time-frame.
        /// </summary>
        public bool IsCurrent { get; set; }
    }
}

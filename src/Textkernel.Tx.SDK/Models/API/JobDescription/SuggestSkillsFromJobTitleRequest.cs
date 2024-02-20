// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.API.JobDescription
{
    /// <summary>
    /// Request body for 'Suggest Skills for Job Title' request
    /// </summary>
    public class SuggestSkillsFromJobTitleRequest
    {
        /// <summary>
        /// The title of the job for which skills are being suggested.
        /// </summary>
        public string JobTitle { get; set; }
        /// <summary>
        /// Language of the suggested skills in ISO 639-1 code format.
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// Maximum number of skills to suggest. Must be within [1 - 50]. Default is 10.
        /// </summary>
        public int? Limit { get; set; }
    }
}

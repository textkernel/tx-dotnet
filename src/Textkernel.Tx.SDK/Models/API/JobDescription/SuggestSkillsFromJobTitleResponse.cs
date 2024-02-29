// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System.Collections.Generic;

namespace Textkernel.Tx.Models.API.JobDescription
{
    /// <inheritdoc/>
    public class SuggestSkillsFromJobTitleResponse : ApiResponse<SuggestSkillsFromJobTitleResponseValue> { }

    /// <summary>
    /// The <see cref="ApiResponse{T}.Value"/> from a 'Suggest Skills for Job' response
    /// </summary>
    public class SuggestSkillsFromJobTitleResponseValue
    {
        /// <summary>
        /// List of skills suggested for the job title.
        /// </summary>
        public List<string> SuggestedSkills { get; set; }
    }
}

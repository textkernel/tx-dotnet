// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System.Collections.Generic;
using Textkernel.Tx.Models.Resume.Employment;

namespace Textkernel.Tx.Models.Job
{
    /// <summary>
    /// Job titles found in a job description
    /// </summary>
    public class JobTitles
    {
        /// <summary>
        /// The main/overall job title
        /// </summary>
        public string MainJobTitle { get; set; }

        /// <summary>
        /// All job titles found in the job description
        /// </summary>
        public List<string> JobTitle { get; set; }

        /// <summary>
        /// Normalized profession for the main job title.
        /// </summary>
        public ParsingNormalizedProfession NormalizedProfession {get;set;}
    }
}

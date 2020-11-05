// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Collections.Generic;

namespace Sovren.Models.Job
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
    }
}

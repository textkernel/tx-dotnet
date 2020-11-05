// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Collections.Generic;

namespace Sovren.Models.Resume.Employment
{
    /// <summary>
    /// A job title found in a resume
    /// </summary>
    public class JobTitle
    {
        /// <summary>
        /// The raw text as it was found in the resume
        /// </summary>
        public string Raw { get; set; }

        /// <summary>
        /// The normalized job title
        /// </summary>
        public string Normalized { get; set; }

        /// <summary>
        /// The degree of certainty that the job title value is accurate. One of:
        /// <br/>VeryUnlikely - recommend discarding
        /// <br/>Unlikely - recommend discarding
        /// <br/>Probable - recommend review
        /// <br/>Confident - no action needed
        /// </summary>
        public string Probability { get; set; }

        /// <summary>
        /// Any variations of this job title that might be useful for matching
        /// </summary>
        public List<string> Variations { get; set; }
    }
}

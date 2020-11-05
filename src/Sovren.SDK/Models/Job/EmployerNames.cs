// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Collections.Generic;

namespace Sovren.Models.Job
{
    /// <summary>
    /// Names of employers found in a job description
    /// </summary>
    public class EmployerNames
    {
        /// <summary>
        /// The main/overall employer name
        /// </summary>
        public string MainEmployerName { get; set; }

        /// <summary>
        /// All employer names found in a job description
        /// </summary>
        public List<string> EmployerName { get; set; }
    }
}

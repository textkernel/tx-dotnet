// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System.Collections.Generic;

namespace Textkernel.Tx.Models.Job
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

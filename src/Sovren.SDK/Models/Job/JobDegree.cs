// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.Job
{
    /// <summary>
    /// A preferred/required educational degree found in a job
    /// </summary>
    public class JobDegree
    {
        /// <summary>
        /// The name of the educational degree
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of the educational degree
        /// </summary>
        public string Type { get; set; }
    }
}

// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.Resume.Education
{
    /// <summary>
    /// An educational degree
    /// </summary>
    public class Degree
    {
        /// <summary>
        /// The name of the degree
        /// </summary>
        public NormalizedString Name { get; set; }

        /// <summary>
        /// The type of degree
        /// </summary>
        public string Type { get; set; }
    }
}

// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Collections.Generic;

namespace Sovren.Models.Resume.Skills
{
    /// <summary>
    /// A container for skills taxonomies found in a resume
    /// </summary>
    public class ResumeTaxonomyRoot
    {
        /// <summary>
        /// The name of the skills list that these taxonomies belong to
        /// </summary>
        public string Root { get; set; }

        /// <summary>
        /// The skills taxonomies found in a resume
        /// </summary>
        public List<ResumeTaxonomy> Taxonomies { get; set; }
    }
}

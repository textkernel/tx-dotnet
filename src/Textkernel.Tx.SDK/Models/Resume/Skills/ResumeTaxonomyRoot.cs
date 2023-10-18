// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System.Collections.Generic;

namespace Textkernel.Tx.Models.Resume.Skills
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

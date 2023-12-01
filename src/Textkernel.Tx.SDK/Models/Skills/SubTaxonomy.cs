// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.Skills
{
    /// <summary>
    /// A subtaxonomy to group similar skills
    /// </summary>
    public class SubTaxonomy
    {
        /// <summary>
        /// The id of this subtaxonomy
        /// </summary>
        public string SubTaxonomyId { get; set; }

        /// <summary>
        /// The human-readable name of this subtaxonomy
        /// </summary>
        public string SubTaxonomyName { get; set; }
    }

    /// <inheritdoc/>
    public abstract class FoundSubTaxonomy : SubTaxonomy
    {
        /// <summary>
        /// The percent (0-100) of skills found in this subtaxonomy compared to all subtaxonomies
        /// </summary>
        public int PercentOfOverall { get; set; }

        /// <summary>
        /// The percent (0-100) of skills found in this subtaxonomy compared to other subtaxonomies in the parent taxonomy
        /// </summary>
        public int PercentOfParent { get; set; }
    }
}

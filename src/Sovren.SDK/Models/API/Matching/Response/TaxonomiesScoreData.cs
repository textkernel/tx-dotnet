// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.API.Matching.Response
{
    /// <inheritdoc/>
    public class TaxonomiesScoreData : CategoryScoreData
    {
        /// <summary>
        /// Taxonomies/industries found.
        /// </summary>
        public DocumentTaxonomies ActualTaxonomies { get; set; }

        /// <summary>
        /// Taxonomies/industries requested.
        /// </summary>
        public DocumentTaxonomies DesiredTaxonomies { get; set; }
    }

    /// <summary>
    /// Primary and secondary taxonomy (industry)
    /// </summary>
    public class DocumentTaxonomies
    {
        /// <summary>
        /// Best fit taxonomy (industry) evidence.
        /// </summary>
        public TaxonomyEvidence Primary { get; set; }

        /// <summary>
        /// Second best fit taxonomy (industry) evidence.
        /// </summary>
        public TaxonomyEvidence Secondary { get; set; }
    }

    /// <summary>
    /// A taxonomy/subtaxonomy (industry/specialization) pair
    /// </summary>
    public class TaxonomyEvidence
    {
        /// <summary>
        /// Parent taxonomy (industry)
        /// </summary>
        public TaxonomyInfo Taxonomy { get; set; }

        /// <summary>
        /// Child subtaxonomy (specialization)
        /// </summary>
        public TaxonomyInfo Subtaxonomy { get; set; }
    }

    /// <summary>
    /// Evidence for a specific taxonomy/subtaxonomy
    /// </summary>
    public class TaxonomyInfo
    {
        /// <summary>
        /// Taxonomy/subtaxonomy name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id for the taxonomy/subtaxonomy
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// <see langword="true"/> when this taxonomy/subtaxonomy is found in both source and target documents
        /// </summary>
        public bool Matched { get; set; }
    }
}

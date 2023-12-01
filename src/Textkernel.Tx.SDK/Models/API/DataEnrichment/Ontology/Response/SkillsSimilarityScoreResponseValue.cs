// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.API.DataEnrichment.Ontology.Response
{
    /// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'Skills Similarity Score' response.
	/// </summary>
    public class SkillsSimilarityScoreResponseValue
    {
        /// <summary>
        /// A value from [0 - 1] representing how closely related skill set A and skill set B are, based on the relations between skills.
        /// </summary>
        public float SimilarityScore { get; set; }
    }
}

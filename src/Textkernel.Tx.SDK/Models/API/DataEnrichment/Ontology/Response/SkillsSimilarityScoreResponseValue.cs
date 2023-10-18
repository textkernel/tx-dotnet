﻿// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

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

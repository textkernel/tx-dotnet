// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.Skills;
using System;
using System.Collections.Generic;
using System.Text;

namespace Textkernel.Tx.Models.API.DataEnrichment.Ontology.Response
{
    /// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'CompareSkillsToProfessions' response.
	/// </summary>
    public class CompareSkillsToProfessionResponseValue
    {
        /// <summary>
        /// A value from[0 - 1] indicating the similarity of the skill set and profession.
        /// </summary>
        public float SimilarityScore { get; set; }
        /// <summary>
        /// A list of common skills between skill set and profession.
        /// </summary>
        public List<SkillScore> CommonSkills { get; set; }
        /// <summary>
        /// The list of given skill IDs that are not associated to the given profession.
        /// </summary>
        public List<string> InputSkillsNotInProfession { get; set; }
        /// <summary>
        /// A list of skills associated with the profession but missing from list of provided skills.
        /// </summary>
        public List<SkillScore> MissingSkillsFoundInProfession { get; set; }
    }
}

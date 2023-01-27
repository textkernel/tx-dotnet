// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.Skills;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.DataEnrichmentServices.Ontology.Response
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

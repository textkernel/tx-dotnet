// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.DataEnrichment.Ontology.Response
{
    /// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'CompareProfessions' response.
	/// </summary>
    public class CompareProfessionsResponseValue
    {
        /// <summary>
        /// A value from [0 - 1] indicating the similarity between the two professions.
        /// </summary>
        public float SimilarityScore { get; set; }
        /// <summary>
        /// A list of common skills for both professions.
        /// </summary>
        public List<SkillScore> CommonSkills { get; set; }
        /// <summary>
        /// A list of exclusive skills per profession. This list will have at most 2 entries (one for each profession you provided).
        /// </summary>
        public List<ProfessionExclusiveSkills> ExclusiveSkillsByProfession { get; set; }
    }

    /// <summary>
    /// An exclusive skill per profession.
    /// </summary>
    public class ProfessionExclusiveSkills
    {
        /// <summary>
        /// The code ID of the profession in the <see href="https://sovren.com/technical-specs/latest/rest-api/data-enrichment/overview/#professions-taxonomies">Professions Taxonomy</see>.
        /// </summary>
        public int ProfessionCodeId { get; set; }
        /// <summary>
        /// A list of skills that are relative to this profession, but not the other.
        /// </summary>
        public List<SkillScore> SkillsFoundOnlyInThisProfession { get; set; }
    }
}

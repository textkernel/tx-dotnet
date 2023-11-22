// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.API.DataEnrichment.Ontology.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Textkernel.Tx.Models.API.DataEnrichment.Ontology.Response
{
    /// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'SuggestProfessions' response.
	/// </summary>
    public class SuggestProfessionsResponseValue
    {
        /// <summary>
        /// A list of professions most relevant to the given skills.
        /// </summary>
        public List<SuggestedProfession> SuggestedProfessions { get; set; }

        /// <summary>
        /// Any warnings when attempting to suggest professions from the given skills.
        /// </summary>
        public SuggestProfessionsWarnings Warnings { get; set; }
    }

    /// <summary>
    /// A profession that was most relevant to the given skill.
    /// </summary>
    public class SuggestedProfession
    {
        /// <summary>
        /// The list of skills relevant to this profession but missing from the given list of skills in the request. This will only be returned if the <see cref="SuggestProfessionsRequest.ReturnMissingSkills"/> flag is set to true.
        /// </summary>
        public List<SkillScore> MissingSkills { get; set; }
        /// <summary>
        /// A value from [0 - 1] indicating how relative the given skills are to this profession.
        /// </summary>
        public float Score { get; set; }
        /// <summary>
        /// The code ID of the profession in the <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-taxonomies">Professions Taxonomy</see>.
        /// </summary>
        public int CodeId { get; set; }

        /// <summary>
        /// The description of the profession in the Professions Taxonomy.
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// Warnings when trying to suggest professions from skills
    /// </summary>
    public class SuggestProfessionsWarnings
    {
        /// <summary>
        /// A list of warnings about provided skills that do not have a profession relation.
        /// </summary>
        public List<string> SkillsWithoutProfessionRelation { get; set; }
        /// <summary>
        /// A list of warnings about provided skills that are invalid.
        /// </summary>
        public List<string> InvalidSkills { get; set; }
    }
}

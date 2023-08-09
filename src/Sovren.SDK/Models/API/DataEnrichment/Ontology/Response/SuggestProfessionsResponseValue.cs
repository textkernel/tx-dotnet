// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API.DataEnrichment.Ontology.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.DataEnrichment.Ontology.Response
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
        /// The code ID of the profession in the <see href="https://sovren.com/technical-specs/latest/rest-api/data-enrichment/overview/#professions-taxonomies">Professions Taxonomy</see>.
        /// </summary>
        public int CodeId { get; set; }
    }
}

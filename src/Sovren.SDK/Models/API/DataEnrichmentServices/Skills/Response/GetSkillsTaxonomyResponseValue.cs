// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.DataEnrichment;
using System.Collections.Generic;

namespace Sovren.Models.API.DataEnrichmentServices.Skills.Response
{
    /// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'GetSkillsTaxonomy' response
	/// </summary>
    public class GetSkillsTaxonomyResponseValue : Taxonomy
    {
        /// <summary>
        /// A list of skills objects.
        /// </summary>
        public List<Code> Skills { get; set; }
    }

    /// <summary>
    /// A skill object.
    /// </summary>
    public class Code
    {
        /// <summary>
        /// The ID for the skill in the taxonomy.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// A list of descriptions of the skill in all supported/requested languages.
        /// </summary>
        public List<LangDescription> Descriptions { get; set; }
        /// <summary>
        /// Type of skill. Possible values are Professional, IT, Language, or Soft.
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// The language ISO 639-1 code. This will only appear for language skills (Type = Language).
        /// </summary>
        public string IsoCode { get; set; }
    }

    /// <summary>
    /// The description in the particular language.
    /// </summary>
    public class LangDescription
    {
        /// <summary>
        /// The <see href="https://sovren.com/technical-specs/latest/rest-api/data-enrichment-services/overview/#professions-languages">ISO code</see> for the language of the description.
        /// </summary>
        public string IsoCode { get; set; }
        /// <summary>
        /// The description in the particular language.
        /// </summary>
        public string Description { get; set; }
    }
}

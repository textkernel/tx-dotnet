// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Sovren.Models.API.DataEnrichmentServices.Skills.Response
{
    /// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'ExtractSkills' response
	/// </summary>
    public class ExtractSkillsResponseValue
    {
        /// <summary>
        /// Whether the input text was truncated or not due to length.
        /// </summary>
        public bool Truncated { get; set; }
        /// <summary>
        /// A list of extracted skills.
        /// </summary>
        public List<ExtractedSkill> Skills { get; set; }
    }

    /// <inheritdoc/>
    public class ExtractedSkill : BaseSkill
    {
        /// <summary>
        /// A list of matches where this skill was found in the text.
        /// </summary>
        public List<SkillMatch> Matches { get; set; }
    }

    /// <summary>
    /// A skill from the skill taxonomy.
    /// </summary>
    public class BaseSkill
    {
        /// <summary>
        /// Type of skill. Possible values are Professional, IT, Language, or Soft.
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// The ID for the skill in the skills taxonomy.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Overall confidence that the skill was normalized to the correct skill.
        /// </summary>
        public float Confidence { get; set; }
        /// <summary>
        /// The description of the normalized skill concept in the requested language.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The language ISO 639-1 code. This will only appear for language skills (Type = Language).
        /// </summary>
        public string IsoCode { get; set; }
    }

    /// <summary>
    /// A matched skill that was found in the text provided.
    /// </summary>
    public class SkillMatch
    {
        /// <summary>
        /// The index of the first character of the match (0-based)
        /// </summary>
        public int BeginSpan { get; set; }
        /// <summary>
        /// The index of the last character of the match (0-based).
        /// </summary>
        public int EndSpan { get; set; }
        /// <summary>
        /// Likelihood that the matched term actually refers to a skill in the context of the text.
        /// </summary>
        public float Likelihood { get; set; }
        /// <summary>
        /// The actual term that was found as evidence of this skill (the substring from BeginSpan to EndSpan).
        /// </summary>
        public string RawText { get; set; }
    }
}

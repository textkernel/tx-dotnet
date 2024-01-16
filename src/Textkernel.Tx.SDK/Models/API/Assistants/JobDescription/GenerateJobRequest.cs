// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Textkernel.Tx.Models.API.Assistants.JobDescription
{
    /// <summary>
    /// Request body for a 'Generate Job' request
    /// </summary>
    public class GenerateJobRequest
    {
        /// <summary>
        /// The title of the job.
        /// </summary>
        public string JobTitle { get; set; }
        /// <summary>
        /// List of skill requirements for the job.
        /// </summary>
        public List<GenerateJobSkill> Skills { get; set; }
        /// <summary>
        /// The tone of the job description.
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public JobTone Tone { get; set; }
        /// <summary>
        /// Language of the job description, in ISO 639-1 code format.
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// Location of the job.
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// The organization offering the job.
        /// </summary>
        public string Organization { get; set; }
    }

    /// <summary>
    /// A skill used when generating a job via assistants
    /// </summary>
    public class GenerateJobSkill
    {
        /// <summary>
        /// The name of the skill.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Priority of the skill.
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SkillPriority Priority { get; set; }
    }

    /// <summary>
    /// Priority of skills for generating job descriptions
    /// </summary>
    public enum SkillPriority
    {
        /// <summary>
        /// Skill is not required
        /// </summary>
        NiceToHave,
        /// <summary>
        /// Skill is required
        /// </summary>
        MustHave
    }

    /// <summary>
    /// Tone to use when generating job descriptions
    /// </summary>
    public enum JobTone
    {
        /// <summary>
        /// A professional tone (typical)
        /// </summary>
        Professional,
        /// <summary>
        /// A casual tone
        /// </summary>
        Casual,
        /// <summary>
        /// A funny tone
        /// </summary>
        Funny
    }
}

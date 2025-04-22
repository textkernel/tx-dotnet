using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using Textkernel.Tx.Models.API.DataEnrichment.Professions;

namespace Textkernel.Tx.Models.API.MatchV2.Request
{
    /// <summary>
    /// Which field should be used to generate completions for an autocomplete request
    /// </summary>
    [JsonConverter(typeof(EnumMemberConverter<AutocompleteJobsField>))]
    public enum AutocompleteJobsField
    {
        /// <summary>
        /// Generate completions from multiple dictionaries
        /// </summary>
        [EnumMember(Value = "FULLTEXT")]
        FullText,
        /// <summary>
        /// Generate completions from IT skills in the index
        /// </summary>
        [EnumMember(Value = "compskills")]
        ITSkills,
        /// <summary>
        /// Generate completions from language skills in the index
        /// </summary>
        [EnumMember(Value = "langskills")]
        LanguageSkills,
        /// <summary>
        /// Generate completions from all job titles in the index
        /// </summary>
        [EnumMember(Value = "job_title")]
        JobTitle,
        /// <summary>
        /// Generate completions from vacancy locations (addresses) in the index
        /// </summary>
        [EnumMember(Value = "location")]
        Location,
        /// <summary>
        /// Generate completions from profession groups in the index
        /// </summary>
        [EnumMember(Value = "work_field.profession_group")]
        ProfessionGroup,
        /// <summary>
        /// Generate completions from normalized international education levels in the index
        /// </summary>
        [EnumMember(Value = "education_level_international")]
        EducationLevel
    }

    /// <summary>
    /// Which field should be used to generate completions for an autocomplete request
    /// </summary>
    [JsonConverter(typeof(EnumMemberConverter<AutocompleteCandidatesField>))]
    public enum AutocompleteCandidatesField
    {
        /// <summary>
        /// Generate completions from multiple dictionaries
        /// </summary>
        [EnumMember(Value = "FULLTEXT")]
        FullText,
        /// <summary>
        /// Generate completions from IT skills in the index
        /// </summary>
        [EnumMember(Value = "compskills")]
        ITSkills,
        /// <summary>
        /// Generate completions from professional skills in the index
        /// </summary>
        [EnumMember(Value = "profskills")]
        ProfessionalSkills,
        /// <summary>
        /// Generate completions from language skills in the index
        /// </summary>
        [EnumMember(Value = "langskills.name")]
        LanguageSkills,
        /// <summary>
        /// Generate completions from recent job titles in the index
        /// </summary>
        [EnumMember(Value = "recent_job_titles")]
        RecentJobTitles,
        /// <summary>
        /// Generate completions from all job titles in the index
        /// </summary>
        [EnumMember(Value = "job_titles")]
        AllJobTitles,
        /// <summary>
        /// Generate completions from last job titles in the index
        /// </summary>
        [EnumMember(Value = "last_job_title")]
        LastJobTitle,
        /// <summary>
        /// Generate completions from degree names in the index
        /// </summary>
        [EnumMember(Value = "degrees.name")]
        DegreeNames,
        /// <summary>
        /// Generate completions from candidate locations (addresses) in the index
        /// </summary>
        [EnumMember(Value = "location")]
        Location,
        /// <summary>
        /// Generate completions from profession groups in the index
        /// </summary>
        [EnumMember(Value = "work_field.profession_group")]
        ProfessionGroup,
        /// <summary>
        /// Generate completions from normalized international education levels in the index
        /// </summary>
        [EnumMember(Value = "education_level_international")]
        EducationLevel
    }
}

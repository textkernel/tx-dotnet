using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace Textkernel.Tx.Models.API.MatchV2.Request
{
    internal static class AutocompleteEnumHelper
    {
        //https://stackoverflow.com/a/59061296

        internal static string AsString(this AutocompleteJobsField field)
        {
            switch (field)
            {
                case AutocompleteJobsField.FullText:
                    return "FULLTEXT";
                case AutocompleteJobsField.ITSkills:
                    return "compskills";
                case AutocompleteJobsField.LanguageSkills:
                    return "langskills";
                case AutocompleteJobsField.JobTitle:
                    return "job_title";
                case AutocompleteJobsField.Location:
                    return "location";
                case AutocompleteJobsField.ProfessionGroup:
                    return "work_field.profession_group";
                case AutocompleteJobsField.EducationLevel:
                    return "education_level_international";
                default:
                    throw new NotImplementedException();
            }
        }

        internal static string AsString(this AutocompleteCandidatesField field)
        {
            switch (field)
            {
                case AutocompleteCandidatesField.FullText:
                    return "FULLTEXT";
                case AutocompleteCandidatesField.ITSkills:
                    return "compskills";
                case AutocompleteCandidatesField.ProfessionalSkills:
                    return "profskills";
                case AutocompleteCandidatesField.LanguageSkills:
                    return "langskills.name";
                case AutocompleteCandidatesField.RecentJobTitles:
                    return "recent_job_titles";
                case AutocompleteCandidatesField.AllJobTitles:
                    return "job_titles";
                case AutocompleteCandidatesField.LastJobTitle:
                    return "last_job_title";
                case AutocompleteCandidatesField.DegreeNames:
                    return "degrees.name";
                case AutocompleteCandidatesField.Location:
                    return "location";
                case AutocompleteCandidatesField.ProfessionGroup:
                    return "work_field.profession_group";
                case AutocompleteCandidatesField.EducationLevel:
                    return "education_level_international";
                default:
                    throw new NotImplementedException();
            }
        }
    }
    /// <summary>
    /// Which field should be used to generate completions for an autocomplete request
    /// </summary>
    public enum AutocompleteJobsField
    {
        /// <summary>
        /// Generate completions from multiple dictionaries
        /// </summary>
        FullText,
        /// <summary>
        /// Generate completions from IT skills in the index
        /// </summary>
        ITSkills,
        /// <summary>
        /// Generate completions from language skills in the index
        /// </summary>
        LanguageSkills,
        /// <summary>
        /// Generate completions from all job titles in the index
        /// </summary>
        JobTitle,
        /// <summary>
        /// Generate completions from vacancy locations (addresses) in the index
        /// </summary>
        Location,
        /// <summary>
        /// Generate completions from profession groups in the index
        /// </summary>
        ProfessionGroup,
        /// <summary>
        /// Generate completions from normalized international education levels in the index
        /// </summary>
        EducationLevel
    }

    /// <summary>
    /// Which field should be used to generate completions for an autocomplete request
    /// </summary>
    public enum AutocompleteCandidatesField
    {
        /// <summary>
        /// Generate completions from multiple dictionaries
        /// </summary>
        FullText,
        /// <summary>
        /// Generate completions from IT skills in the index
        /// </summary>
        ITSkills,
        /// <summary>
        /// Generate completions from professional skills in the index
        /// </summary>
        ProfessionalSkills,
        /// <summary>
        /// Generate completions from language skills in the index
        /// </summary>
        LanguageSkills,
        /// <summary>
        /// Generate completions from recent job titles in the index
        /// </summary>
        RecentJobTitles,
        /// <summary>
        /// Generate completions from all job titles in the index
        /// </summary>
        AllJobTitles,
        /// <summary>
        /// Generate completions from last job titles in the index
        /// </summary>
        LastJobTitle,
        /// <summary>
        /// Generate completions from degree names in the index
        /// </summary>
        DegreeNames,
        /// <summary>
        /// Generate completions from candidate locations (addresses) in the index
        /// </summary>
        Location,
        /// <summary>
        /// Generate completions from profession groups in the index
        /// </summary>
        ProfessionGroup,
        /// <summary>
        /// Generate completions from normalized international education levels in the index
        /// </summary>
        EducationLevel
    }
}

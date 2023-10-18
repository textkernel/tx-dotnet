// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Collections.Generic;
using Textkernel.Tx.Models.API.Parsing;

namespace Textkernel.Tx.Models.API.Matching.Request
{
    /// <summary>
    /// Settings for searching/matching
    /// </summary>
    public class SearchMatchSettings
    {
        /// <summary>
        /// Set to <see langword="true"/> to turn off variation matches in job titles.
        /// </summary>
        public bool PositionTitlesMustHaveAnExactMatch { get; set; }

        /// <summary>
        /// Normalize the first three job titles specified in FilterCriteria.JobTitles and automatically include them in the query
        /// (<see href="https://sovren.com/technical-specs/latest/rest-api/overview/#transaction-cost">additional charges apply</see>).
        /// <br/><br/>
        /// You will only benefit from using this parameter if the data in your index was parsed with <see cref="ProfessionsSettings.Normalize"/> enabled.
        /// <br/><br/>
        /// Normalized job titles help identify more matches by looking beyond the exact job title. Normalization uses lists 
        /// of synonyms behind the scenes. For example, a search for "HR Advisor" will also return results for "Human Resources Consultant".
        /// <br/><br/>
        /// When matching, the normalized job title is automatically included in the query if the data in your index was 
        /// parsed with <see cref="ProfessionsSettings.Normalize"/> enabled.
        /// </summary>
        public bool NormalizeJobTitles { get; set; }
        
        /// <summary>
        /// Specify the language (ISO 639-1 code) of the Job Title to be normalized. This defaults to "en". 
        /// See <see href="https://developer.textkernel.com/Professions/master/">list of supoprted languages.</see>
        /// </summary>
        public string NormalizeJobTitlesLanguage { get; set; }
    }

    /// <summary>
    /// Base class for match/search requests
    /// </summary>
    public abstract class SearchMatchRequestBase
    {
        /// <summary>
        /// The ids of the indexes in which you want to find results (case-insensitive).
        /// </summary>
        public List<string> IndexIdsToSearchInto { get; set; }

        /// <summary>
        /// The settings to use during searching/matching queries
        /// </summary>
        public SearchMatchSettings Settings { get; set; }

        /// <summary>
        /// Required criteria for the result set.
        /// </summary>
        public FilterCriteria FilterCriteria { get; set; }
    }
}

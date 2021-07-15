// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.Resume.Employment;
using Sovren.Models.Skills;
using System.Collections.Generic;

namespace Sovren.Models.API.Matching.Request
{
    /// <summary>
    /// A range of integers
    /// </summary>
    public class IntegerRange
    {
        /// <summary>
        /// The minimum for this range
        /// </summary>
        public int Minimum { get; set; }

        /// <summary>
        /// The maximum for this range
        /// </summary>
        public int Maximum { get; set; }
    }

    /// <summary>
    /// A range of revision dates in ISO 8601 (yyyy-MM-dd) format
    /// </summary>
    public class RevisionDateRange
    {
        /// <summary>
        /// the minimum (oldest) date in ISO 8601 (yyyy-MM-dd) format
        /// </summary>
        public string Minimum { get; set; }

        /// <summary>
        /// the maximum (most recent) date in ISO 8601 (yyyy-MM-dd) format
        /// </summary>
        public string Maximum { get; set; }
    }

    /// <summary>
    /// Criteria for filtering on a specific job title
    /// </summary>
    public class JobTitleFilter
    {
        /// <summary>
        /// The name of the Job Title. Supports (*, ?) wildcard characters after the third character
        /// in the term as defined in <see href="https://sovren.com/technical-specs/latest/rest-api/ai-matching/overview/querying/#ai-filtering-special-operators"/>
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Whether or not the job title must be a current job title
        /// </summary>
        public bool IsCurrent { get; set; }
    }

    /// <summary>
    /// Criteria for filtering search/match results
    /// </summary>
    public class FilterCriteria
    {
        /// <summary>
        /// When specified, the revision date of documents must fall within this range.
        /// </summary>
        public RevisionDateRange RevisionDateRange { get; set; }

        /// <summary>
        /// Results must have one of the specified document ids (case-insensitive).
        /// </summary>
        public List<string> DocumentIds { get; set; }

        /// <summary>
        /// List of user-defined tags. Either all or at least one are required depending on the value of
        /// <see cref="UserDefinedTagsMustAllExist"/>
        /// </summary>
        public List<string> UserDefinedTags { get; set; }

        /// <summary>
        /// When <see langword="true"/>, all of the user-defined tags in <see cref="UserDefinedTags"/> must be found. 
        /// By default, this is <see langword="false"/>, which means that at least one of the <see cref="UserDefinedTags"/> must be found.
        /// </summary>
        public bool UserDefinedTagsMustAllExist { get; set; }

        /// <summary>
        /// Use to filter results based on location.
        /// </summary>
        public LocationCriteria LocationCriteria { get; set; }

        /// <summary>
        /// Full-text boolean semantic expresion. For full details on the syntax and supported 
        /// operations refer to <see href="https://sovren.com/technical-specs/latest/rest-api/ai-matching/overview/querying/#ai-filtering-fulltext"/>
        /// </summary>
        public string SearchExpression { get; set; }

        /// <summary>
        /// If <see langword="true"/>, results must have/require patent experience.
        /// </summary>
        public bool HasPatents { get; set; }

        /// <summary>
        /// If <see langword="true"/>, results must have/require security credentials.
        /// </summary>
        public bool HasSecurityCredentials { get; set; }

        /// <summary>
        /// Results must have/require at least one of the security credentials specified.
        /// Supports (*, ?) wildcard characters after the third character in the term as defined 
        /// in <see href="https://sovren.com/technical-specs/latest/rest-api/ai-matching/overview/querying/#ai-filtering-special-operators"/>
        /// </summary>
        public List<string> SecurityCredentials { get; set; }

        /// <summary>
        /// If <see langword="true"/>, results must have/require experience as an author.
        /// </summary>
        public bool IsAuthor { get; set; }

        /// <summary>
        /// If <see langword="true"/>, results must have/require public speaking experience.
        /// </summary>
        public bool IsPublicSpeaker { get; set; }

        /// <summary>
        /// If <see langword="true"/>, results must have/require military experience.
        /// </summary>
        public bool IsMilitary { get; set; }

        /// <summary>
        /// Results must have at least one of the specified school names. Supports (*, ?) wildcard 
        /// characters after the third character in the term as defined in
        /// <see href="https://sovren.com/technical-specs/latest/rest-api/ai-matching/overview/querying/#ai-filtering-special-operators"/>
        /// </summary>
        public List<string> SchoolNames { get; set; }

        /// <summary>
        /// Results must have at least one of the specified degree names. Supports (*, ?) wildcard 
        /// characters after the third character in the term as defined in
        /// <see href="https://sovren.com/technical-specs/latest/rest-api/ai-matching/overview/querying/#ai-filtering-special-operators"/>
        /// </summary>
        public List<string> DegreeNames { get; set; }

        /// <summary>
        /// Results must have at least one of the specified degree types.
        /// One of:
        /// <br/> specialeducation
        /// <br/> someHighSchoolOrEquivalent
        /// <br/> ged
        /// <br/> secondary
        /// <br/> highSchoolOrEquivalent
        /// <br/> certification
        /// <br/> vocational
        /// <br/> someCollege
        /// <br/> HND_HNC_OrEquivalent
        /// <br/> associates
        /// <br/> international
        /// <br/> bachelors
        /// <br/> somePostgraduate
        /// <br/> masters
        /// <br/> intermediategraduate
        /// <br/> professional
        /// <br/> postprofessional
        /// <br/> doctorate
        /// <br/> postdoctorate
        /// </summary>
        public List<string> DegreeTypes { get; set; }

        /// <summary>
        /// Results must have at least one of the specified employers. Supports (*, ?) wildcard 
        /// characters after the third character in the term as defined in
        /// <see href="https://sovren.com/technical-specs/latest/rest-api/ai-matching/overview/querying/#ai-filtering-special-operators"/>
        /// </summary>
        public List<string> Employers { get; set; }

        /// <summary>
        /// When <see langword="true"/>, at least one employer in <see cref="Employers"/> must be found in the current time frame.
        /// </summary>
        public bool EmployersMustAllBeCurrentEmployer { get; set; }

        /// <summary>
        /// When specified, total work experience must fall within this range.
        /// </summary>
        public IntegerRange MonthsExperience { get; set; }

        /// <summary>
        /// Results must be written in one of the specified languages. (2-letter ISO 639-1 language codes)
        /// </summary>
        public List<string> DocumentLanguages { get; set; }

        /// <summary>
        /// Results must have/require at least one of the specified skills.
        /// </summary>
        public List<SkillFilter> Skills { get; set; }

        /// <summary>
        /// When <see langword="true"/>, <b>all</b> of the skills in <see cref="Skills"/> must be found. By default, this is <see langword="false"/>, 
        /// which means that <b>only one</b> of the <see cref="Skills"/> must be found. 
        /// </summary>
        public bool SkillsMustAllExist { get; set; }

        /// <summary>
        /// Results must have an education with a normalized GPA of .75 or higher (for example, 3.0/4.0 or higher).
        /// </summary>
        public bool IsTopStudent { get; set; }

        /// <summary>
        /// Results must have an education section listed as '- current'.
        /// </summary>
        public bool IsCurrentStudent { get; set; }

        /// <summary>
        /// Results must have graduated within the last 18 months.
        /// </summary>
        public bool IsRecentGraduate { get; set; }

        /// <summary>
        /// Results must have at least one of the specified job titles.
        /// </summary>
        public List<JobTitleFilter> JobTitles { get; set; }

        /// <summary>
        /// Results must have at least one of the following types of executive experience:
        /// <br/> None
        /// <br/> Executive
        /// <br/> Admin
        /// <br/> Accounting
        /// <br/> Operations
        /// <br/> Financial
        /// <br/> Marketing
        /// <br/> Business_Dev
        /// <br/> IT
        /// <br/> General
        /// <br/> Learning
        /// </summary>
        public List<string> ExecutiveType { get; set; }

        /// <summary>
        /// Results must have at least one of the specified certifications. Supports (*, ?) wildcard 
        /// characters after the third character in the term as defined in
        /// <see href="https://sovren.com/technical-specs/latest/rest-api/ai-matching/overview/querying/#ai-filtering-special-operators"/>
        /// </summary>
        public List<string> Certifications { get; set; }

        /// <summary>
        /// Results must have management experience within this months range.
        /// </summary>
        public IntegerRange MonthsManagementExperience { get; set; }

        /// <summary>
        /// Results must currently have at least one of the following management levels:
        /// <br/> None
        /// <br/> Low
        /// <br/> Mid
        /// <br/> High
        /// </summary>
        public string CurrentManagementLevel { get; set; }

        /// <summary>
        /// Results must have/require these language competencies (2-letter ISO 639-1 language codes).
        /// <br/>Either all or at least one are required depending on the value of <see cref="LanguagesKnownMustAllExist"/>
        /// </summary>
        public List<string> LanguagesKnown { get; set; }

        /// <summary>
        /// When <see langword="true"/>, <b>all</b> of the languages in <see cref="LanguagesKnown"/> must be found. By default, this is <see langword="false"/>, which means 
        /// that <b>only one</b> of the <see cref="LanguagesKnown"/> must be found. 
        /// </summary>
        public bool LanguagesKnownMustAllExist { get; set; }

        /// <summary>
        /// Results must contain at least one of the specified best-fit taxonomy IDs or best-fit subtaxonomy IDs.
        /// <br/>See <see cref="Taxonomy.SovrenDefaults"/>
        /// </summary>
        public List<string> Taxonomies { get; set; }

        /// <summary>
        /// Results much have <see cref="ExperienceSummary.AverageMonthsPerEmployer"/> within this range.
        /// Only applicable for resumes; setting this when filtering jobs will cause an error.
        /// </summary>
        public IntegerRange AverageMonthsPerEmployer { get; set; }

        /// <summary>
        /// Results much have <see cref="ExperienceSummary.FulltimeDirectHirePredictiveIndex"/> within this range.
        /// Only applicable for resumes; setting this when filtering jobs will cause an error.
        /// </summary>
        public IntegerRange JobPredictiveIndex { get; set; }
    }
}

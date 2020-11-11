// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.Job.Skills;
using System;
using System.Collections.Generic;

namespace Sovren.Models.Job
{
    /// <summary>
    /// All of the information extracted while parsing a job
    /// </summary>
    public class ParsedJob
    {
        /// <summary>
        /// Whether or not the job is a management position. Used by Sovren for AI Matching
        /// </summary>
        public bool CurrentJobIsManagement { get; set; }

        /// <summary>
        /// The management score. Used by Sovren for AI Matching
        /// </summary>
        public SovrenNullable<int> HighestManagementScore { get; set; }

        /// <summary>
        /// The management level. Used by Sovren for AI Matching
        /// </summary>
        public string ManagementLevel { get; set; }

        /// <summary>
        /// What kind of executive position the job is, if any. Used by Sovren for AI Matching
        /// </summary>
        public string ExecutiveType { get; set; }

        /// <summary>
        /// The minimum years experience for the job, if listed. Used by Sovren for AI Matching
        /// </summary>
        public SovrenNullable<int> MinimumYears { get; set; }

        /// <summary>
        /// The maximum years experience for the job, if listed. Used by Sovren for AI Matching
        /// </summary>
        public SovrenNullable<int> MaximumYears { get; set; }

        /// <summary>
        /// The minimum years of management experience, if listed. Used by Sovren for AI Matching
        /// </summary>
        public SovrenNullable<int> MinimumYearsManagement { get; set; }

        /// <summary>
        /// The maximum years of management experience, if listed. Used by Sovren for AI Matching
        /// </summary>
        public SovrenNullable<int> MaximumYearsManagement { get; set; }

        /// <summary>
        /// The required educational degree, if listed. Used by Sovren for AI Matching
        /// </summary>
        public string RequiredDegree { get; set; }

        /// <summary>
        /// The start date for the job, if listed.
        /// </summary>
        public SovrenNullable<DateTime> StartDate { get; set; }

        /// <summary>
        /// The end date for the job, if listed.
        /// </summary>
        public SovrenNullable<DateTime> EndDate { get; set; }

        /// <summary>
        /// The bill rate for the job, if listed.
        /// </summary>
        public PayRate BillRate { get; set; }

        /// <summary>
        /// The pay rate for the job, if listed
        /// </summary>
        public PayRate PayRate { get; set; }

        /// <summary>
        /// The full job description
        /// </summary>
        public string JobDescription { get; set; }

        /// <summary>
        /// Any requirement listed by the job
        /// </summary>
        public string JobRequirements { get; set; }

        /// <summary>
        /// The job titles found in the job. Used by Sovren for AI Matching
        /// </summary>
        public JobTitles JobTitles { get; set; }

        /// <summary>
        /// The employer names found in the job.
        /// </summary>
        public EmployerNames EmployerNames { get; set; }

        /// <summary>
        /// The educational degrees found listed in the job. Used by Sovren for AI Matching
        /// </summary>
        public List<JobDegree> Degrees { get; set; }

        /// <summary>
        /// Any school names listed in the job
        /// </summary>
        public List<string> SchoolNames { get; set; }

        /// <summary>
        /// Any certifications/licenses listed in the job. Used by Sovren for AI Matching
        /// </summary>
        public List<string> CertificationsAndLicenses { get; set; }

        /// <summary>
        /// Any languages listed in the job. Used by Sovren for AI Matching
        /// </summary>
        public List<string> LanguageCodes { get; set; }

        /// <summary>
        /// The location of the job, if listed. Used by Sovren for AI Matching
        /// </summary>
        public Location CurrentLocation { get; set; }

        /// <summary>
        /// Terms of interest listed in the job
        /// </summary>
        public List<string> TermsOfInterest { get; set; }

        /// <summary>
        /// Any owners of the job posting, if listed.
        /// </summary>
        public List<string> Owners { get; set; }

        /// <summary>
        /// The skills found in the job. Used by Sovren for AI Matching
        /// </summary>
        public List<JobTaxonomyRoot> SkillsData { get; set; }

        /// <summary>
        /// Metadata about the parsed job
        /// </summary>
        public JobMetadata JobMetadata { get; set; }

        /// <summary>
        /// A list of <see href="https://docs.sovren.com/Documentation/AIMatching#ai-custom-values">Custom Value Ids</see> 
        /// that are assigned to this job. These are used to filter search/match queries in the AI Matching Engine.
        /// <br/>
        /// <b>NOTE: you may add/remove these prior to indexing. This is the only property you may modify prior to indexing.</b>
        /// </summary>
        public List<string> CustomValueIds { get; set; }

        /// <summary>
        /// You should never create one of these. Instead, these are output by the Sovren Job Parser
        /// </summary>
        [Obsolete("You should never create one of these. Instead, these are output by the Sovren Job Parser")]
        public ParsedJob() { }

        /// <summary>
        /// Returns the job as a formatted json string
        /// </summary>
        public override string ToString() => this.ToJson(true);
    }
}

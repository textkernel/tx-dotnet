// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.Resume.Employment
{
    /// <summary>
    /// A summary of a candidate's work history
    /// </summary>
    public class ExperienceSummary
    {
        /// <summary>
        /// A paragraph of text that summarizes the candidate's experience. This paragraph is generated based on other data
        /// points in the <see cref="ExperienceSummary"/>. It will be the same language as the resume for Czech, Dutch, 
        /// English, French, German, Greek, Hungarian, Italian, Norwegian, Russian, Spanish, and Swedish. To always generate the 
        /// summary in English, set "OutputFormat.AllSummariesInEnglish = True;" in the configuration string when parsing.
        /// <br/>
        /// <br/>
        /// <b>In order for this value to be accurate, you must have provided an accurate RevisionDate when you parsed this resume.</b>
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The number of months of work experience as indicated by the range of 
        /// start and end date values in the various jobs/positions in the resume. 
        /// Overlapping date ranges are not double-counted. This value is NOT derived from text like "I have 15 years of experience". 
        /// <br/>
        /// <br/>
        /// <b>In order for this value to be accurate, you must have provided an accurate RevisionDate when you parsed this resume.</b>
        /// </summary>
        public int MonthsOfWorkExperience { get; set; }

        /// <summary>
        /// The number of months of management experience as indicated by the range of 
        /// start and end date values in the various jobs/positions in the resume that have been 
        /// determined to be management-level positions. Overlapping date ranges are not double-counted.
        /// This value is NOT derived from text like "I have 10 years of management experience". 
        /// <br/>
        /// <br/>
        /// <b>In order for this value to be accurate, you must have provided an accurate RevisionDate when you parsed this resume.</b>
        /// </summary>
        public int MonthsOfManagementExperience { get; set; }

        /// <summary>
        /// If <see cref="ManagementScore"/> is at least 30 (mid-level+), then the job titles are examined to determine the best 
        /// category for the executive experience. One of:
        /// <br/> NONE
        /// <br/> ADMIN
        /// <br/> ACCOUNTING
        /// <br/> BUSINESS_DEV
        /// <br/> EXECUTIVE
        /// <br/> FINANCIAL
        /// <br/> GENERAL
        /// <br/> IT
        /// <br/> LEARNING
        /// <br/> MARKETING
        /// <br/> OPERATIONS
        /// </summary>
        public string ExecutiveType { get; set; }

        /// <summary>
        /// The average number of months a candidate has spent at each employer. Note that this number is per employer, not per job.
        /// <br/>
        /// <br/>
        /// <b>In order for this value to be accurate, you must have provided an accurate RevisionDate when you parsed this resume.</b>
        /// </summary>
        public int AverageMonthsPerEmployer { get; set; }

        /// <summary>
        /// A score (0-100), where 0 means a candidate is more likely to have had (and want/pursue) short-term/part-time/temp/contracting 
        /// jobs and 100 means a candidate is more likely to have had (and want/pursue) traditional full-time, direct-hire jobs. 
        /// <br/>
        /// <br/>
        /// <b>In order for this value to be accurate, you must have provided an accurate RevisionDate when you parsed this resume.</b>
        /// </summary>
        public int FulltimeDirectHirePredictiveIndex { get; set; }

        /// <summary>
        /// A paragraph of text that summarizes the candidate's management experience (in English).
        /// </summary>
        public string ManagementStory { get; set; }

        /// <summary>
        /// Computed level of management for the current position. One of:
        /// <br/>low-or-no-level
        /// <br/>low-level
        /// <br/>mid-level
        /// <br/>somewhat high-level
        /// <br/>high-level
        /// <br/>executive-level
        /// </summary>
        public string CurrentManagementLevel { get; set; }

        /// <summary>
        /// The highest score calculated from any of the position titles. The score is based on the 
        /// wording of the title, not on the experience described within the position description. 
        /// <br/>0-29 = Low level
        /// <br/>30-59 = Mid level
        /// <br/>60+ = High level
        /// </summary>
        public int ManagementScore { get; set; }

        /// <summary>
        /// Any abnormal findings about the candidate's career will be reported here. For example, if the candidate
        /// held a management-level position in a previous job, but not their current job.
        /// </summary>
        public string AttentionNeeded { get; set; }
    }
}

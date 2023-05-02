// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.Job.Skills;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using Sovren.Models.API.Parsing;

namespace Sovren.Models.Job
{
    /// <summary>
    /// All of the information extracted while parsing a job
    /// </summary>
    public class ParsedJob : ParsedDocument
    {
        /// <summary>
        /// Whether or not the job is a management position. Used by Sovren for AI Matching
        /// </summary>
        public bool CurrentJobIsManagement { get; set; }

        /// <summary>
        /// The management score. Used by Sovren for AI Matching
        /// </summary>
        public SovrenPrimitive<int> HighestManagementScore { get; set; }

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
        public SovrenPrimitive<int> MinimumYears { get; set; }

        /// <summary>
        /// The maximum years experience for the job, if listed. Used by Sovren for AI Matching
        /// </summary>
        public SovrenPrimitive<int> MaximumYears { get; set; }

        /// <summary>
        /// The minimum years of management experience, if listed. Used by Sovren for AI Matching
        /// </summary>
        public SovrenPrimitive<int> MinimumYearsManagement { get; set; }

        /// <summary>
        /// The maximum years of management experience, if listed. Used by Sovren for AI Matching
        /// </summary>
        public SovrenPrimitive<int> MaximumYearsManagement { get; set; }

        /// <summary>
        /// The required educational degree, if listed. Used by Sovren for AI Matching
        /// </summary>
        public string RequiredDegree { get; set; }

        /// <summary>
        /// The start date of the job.
        /// </summary>
        public SovrenPrimitive<DateTime> StartDate { get; set; }

        /// <summary>
        /// The end date for the job, if listed.
        /// </summary>
        public SovrenPrimitive<DateTime> EndDate { get; set; }

        /// <summary>
        /// Section containing information about the job. Job description strictly includes duties, tasks, and responsibilities for the role with as little irrelevant text as possible.
        /// </summary>
        public string JobDescription { get; set; }

        /// <summary>
        /// Full text of any requirements listed by the job.
        /// </summary>
        public string JobRequirements { get; set; }

        /// <summary>
        /// Full text of any benefits listed by the job.
        /// </summary>
        public string Benefits { get; set; }

        /// <summary>
        /// Full text of any employer description listed by the job.
        /// </summary>
        public string EmployerDescription { get; set; }

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
        /// The location of the job, if listed. If no job location is found, this is the location of the company, if listed.
        /// </summary>
        public Location CurrentLocation { get; set; }

        /// <summary>
        /// Information about the application process.
        /// </summary>
        public ApplicationDetails ApplicationDetails { get; set; }

        /// <summary>
        /// The salary found for the position
        /// <br/>
        /// If no lexical cues are available from the vacancy, the time scale is guessed based on predefined salary ranges. Here are some rough salary ranges (note: country-specific conditions may apply):
        /// <br/>- 1 or 2 digits salary (9, 12): hourly
        /// <br/>- 3 or 4 digits salary (3800, 5000): monthly
        /// <br/>- 5 digit salary (38000, 50000): yearly
        /// <br/>
        /// If a monthly salary is extracted, to get the annual salary it is multiplied by 14 (if country = AT) or 12 (all other countries).
        /// </summary>
        public PayRange Salary { get; set; }

        /// <summary>
        /// The minimum number of working hours per week
        /// </summary>
        public SovrenPrimitive<int> MinimumWorkingHours { get; set; }
        
        /// <summary>
        /// The maximum number of working hours per week
        /// </summary>
        public SovrenPrimitive<int> MaximumWorkingHours { get; set; }

        /// <summary>
        /// The type of working hours. One of:
        /// <br/>regular
        /// <br/>irregular
        /// </summary>
        public string WorkingHours { get; set; }

        /// <summary>
        /// Whether or not the position is remote. Includes fulltime, partial and temporary remote working opportunities.
        /// </summary>
        public bool IsRemote { get; set; }
        
        /// <summary>
        /// Any drivers license requirements
        /// </summary>
        public List<string> DriversLicenses { get; set; }
        
        /// <summary>
        /// The type of employment. One of:
        /// <br/>unspecified
        /// <br/>fulltime
        /// <br/>parttime
        /// <br/>fulltime/parttime
        /// </summary>
        public string EmploymentType { get; set; }
        
        /// <summary>
        /// The contract type. One of:
        /// <br/>unspecified
        /// <br/>permanent
        /// <br/>temporary
        /// <br/>possibly_permanent
        /// <br/>interim
        /// <br/>franchise
        /// <br/>side
        /// <br/>internship
        /// <br/>voluntary
        /// <br/>freelance
        /// <br/>apprenticeship
        /// <br/>assisted
        /// </summary>
        public string ContractType { get; set; }

        /// <summary>
        /// Terms of interest listed in the job
        /// </summary>
        public List<string> TermsOfInterest { get; set; }

        /// <summary>
        /// Any owners of the job posting, if listed.
        /// </summary>
        public List<string> Owners { get; set; }

        /// <summary>
        /// The skills found in the job when <see cref="SkillsSettings.TaxonomyVersion"/> is set to (or defaults to) "V1".
        /// </summary>
        [Obsolete("You should use the V2 skills taxonomy instead.")]
        public List<JobTaxonomyRoot> SkillsData { get; set; }

        /// <summary>
        /// Skills output when <see cref="SkillsSettings.TaxonomyVersion"/> is set to (or defaults to) "V2".
        /// </summary>
        public JobV2Skills Skills { get; set; }

        /// <summary>
        /// Metadata about the parsed job
        /// </summary>
        public JobMetadata JobMetadata { get; set; }

        /// <summary>
        /// A list of <see href="https://sovren.com/technical-specs/latest/rest-api/ai-matching/overview/user-defined-tags/">user-defined tags</see> 
        /// that are assigned to this job. These are used to filter search/match queries in the AI Matching Engine.
        /// <br/>
        /// <b>NOTE: you may add/remove these prior to indexing. This is the only property you may modify prior to indexing.</b>
        /// </summary>
        public List<string> UserDefinedTags { get; set; }

        /// <summary>
        /// You should never create one of these. Instead, these are output by the Sovren Job Parser.
        /// Sovren does not support manually created jobs to be used in the AI Matching engine.
        /// <br/>
        /// <strong>
        /// To create a job from a json string, use <see cref="FromJson(string)"/> or <see cref="FromFile(string)"/>
        /// </strong>
        /// </summary>
        [Obsolete("You should never create one of these. Instead, these are output by the Sovren Job Parser")]
        public ParsedJob() { }

        /// <summary>
        /// Create a parsed job from json. This is useful when you have stored parse results to disk for use later.
        /// </summary>
        /// <param name="utf8json">The UTF-8 encoded json string</param>
        public static ParsedJob FromJson(string utf8json)
        {
            ParsedJob newJob = JsonSerializer.Deserialize<ParsedJob>(utf8json, SovrenJsonSerialization.DefaultOptions);

            if (newJob.JobMetadata == null)
            {
                //this should never happen, it was bad json
                throw new JsonException("The provided JSON is not a valid ParsedJob created by the Sovren Job Parser");
            }

            return newJob;
        }

        /// <summary>
        /// Load a parsed job from a json file using UTF-8 encoding. This is useful when you have stored parse results to disk for use later.
        /// </summary>
        /// <param name="path">The full path to the json file</param>
        public static ParsedJob FromFile(string path)
        {
            return FromJson(File.ReadAllText(path, Encoding.UTF8));
        }

        /// <summary>
        /// Outputs a JSON string that can be saved to disk or any other data storage.
        /// <br/>NOTE: be sure to save with UTF-8 encoding!
        /// </summary>
        /// <param name="formatted"><see langword="true"/> for pretty-printing</param>
        public override string ToJson(bool formatted)
        {
            JsonSerializerOptions options = SovrenJsonSerialization.DefaultOptions;
            options.WriteIndented = formatted;
            return JsonSerializer.Serialize(this, options);
        }
    }
}

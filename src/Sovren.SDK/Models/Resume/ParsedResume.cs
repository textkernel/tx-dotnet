// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.Resume.ContactInfo;
using Sovren.Models.Resume.Education;
using Sovren.Models.Resume.Employment;
using Sovren.Models.Resume.Metadata;
using Sovren.Models.Resume.Military;
using Sovren.Models.Resume.Skills;
using System;
using System.Collections.Generic;

namespace Sovren.Models.Resume
{
    /// <summary>
    /// All of the information extracted while parsing a resume
    /// </summary>
    public class ParsedResume
    {
        /// <summary>
        /// The candidate's contact information found on the resume
        /// </summary>
        public ContactInformation ContactInformation { get; set; }

        /// <summary>
        /// The professional summary from the resume
        /// </summary>
        public string ProfessionalSummary { get; set; }

        /// <summary>
        /// The candidate's written objective
        /// </summary>
        public string Objective { get; set; }

        /// <summary>
        /// Personal information provided by the candidate on the resume
        /// </summary>
        public PersonalAttributes PersonalAttributes { get; set; }

        /// <summary>
        /// The candidate's education history found on the resume
        /// </summary>
        public EducationHistory Education { get; set; }

        /// <summary>
        /// The candidate's employment/work history found on the resume
        /// </summary>
        public EmploymentHistory EmploymentHistory { get; set; }

        /// <summary>
        /// All the skills found in the resume
        /// </summary>
        public List<ResumeTaxonomyRoot> SkillsData { get; set; }

        /// <summary>
        /// Certifications found on a resume.
        /// </summary>
        public List<Certification> Certifications { get; set; }

        /// <summary>
        /// Licenses found on a resume. These are professional licenses, not driving licenses.
        /// For driving licenses, see <see cref="PersonalAttributes.DrivingLicense"/>
        /// </summary>
        public List<LicenseDetails> Licenses { get; set; }

        /// <summary>
        /// Associations/organizations found on a resume
        /// </summary>
        public List<Association> Associations { get; set; }

        /// <summary>
        /// Any language competencies (fluent in, can read, can write, etc)
        /// found in the resume.
        /// </summary>
        public List<LanguageCompetency> LanguageCompetencies { get; set; }

        /// <summary>
        /// Any military experience listed on the resume
        /// </summary>
        public List<MilitaryDetails> MilitaryExperience { get; set; }

        /// <summary>
        /// Any security credentials/clearances listed on the resume
        /// </summary>
        public List<SecurityCredential> SecurityCredentials { get; set; }

        /// <summary>
        /// References listed on a resume.
        /// </summary>
        public List<CandidateReference> References { get; set; }

        /// <summary>
        /// Any achievements listed on the resume
        /// </summary>
        public List<string> Achievements { get; set; }

        /// <summary>
        /// Any training listed on the resume
        /// </summary>
        public TrainingHistory Training { get; set; }

        /// <summary>
        /// A standalone 'skills' section, if listed on the resume
        /// </summary>
        public string QualificationSummary { get; set; }

        /// <summary>
        /// Any hobbies listed on the resume
        /// </summary>
        public string Hobbies { get; set; }

        /// <summary>
        /// Any patents listed on the resume
        /// </summary>
        public string Patents { get; set; }

        /// <summary>
        /// Any publications listed on the resume
        /// </summary>
        public string Publications { get; set; }

        /// <summary>
        /// Any speaking engagements/appearances listed on the resume
        /// </summary>
        public string SpeakingEngagements { get; set; }

        /// <summary>
        /// Metadata about the parsed resume
        /// </summary>
        public ResumeMetadata ResumeMetadata { get; set; }

        /// <summary>
        /// A list of <see href="https://docs.sovren.com/Documentation/AIMatching#ai-custom-values">Custom Value Ids</see> 
        /// that are assigned to this resume. These are used to filter search/match queries in the AI Matching Engine.
        /// <br/>
        /// <b>NOTE: you may add/remove these prior to indexing. This is the only property you may modify prior to indexing.</b>
        /// </summary>
        public List<string> CustomValues { get; set; }

        /// <summary>
        /// You should never create one of these. Instead, these are output by the Sovren Resume Parser.
        /// Sovren does not support manually-created resumes to be used in the AI Matching engine.
        /// </summary>
        [Obsolete("You should never create one of these. Instead, these are output by the Sovren Resume Parser")]
        public ParsedResume() { }
    }
}

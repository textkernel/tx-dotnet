// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.Resume.Metadata
{
    /// <summary>
    /// A section in the resume (work history, education, etc)
    /// </summary>
    public class ResumeSection
    {
        /// <summary>
        /// The first line of the section (zero-based). This refers to the lines (delimited by newline) in the <see cref="ParsedDocumentMetadata.PlainText"/>
        /// </summary>
        public int FirstLineNumber { get; set; }

        /// <summary>
        /// The last line of the section (zero-based). This refers to the lines (delimited by newline) in the <see cref="ParsedDocumentMetadata.PlainText"/>
        /// </summary>
        public int LastLineNumber { get; set; }

        /// <summary>
        /// The type of section. One of:
        /// <br/>- ARTICLES
        /// <br/>- AVAILABILITY
        /// <br/>- BOOKS
        /// <br/>- CERTIFICATIONS
        /// <br/>- CONFERENCE_PAPERS
        /// <br/>- CONTACT_INFO
        /// <br/>- EDUCATION
        /// <br/>- HOBBIES
        /// <br/>- IGNORE_DATA_AFTER
        /// <br/>- LANGUAGES
        /// <br/>- LICENSES
        /// <br/>- MILITARY
        /// <br/>- OBJECTIVE
        /// <br/>- OTHER_PUBLICATIONS
        /// <br/>- PATENTS
        /// <br/>- PERSONAL_INTERESTS_AND_ACCOMPLISHMENTS
        /// <br/>- PROFESSIONAL_AFFILIATIONS
        /// <br/>- QUALIFICATIONS_SUMMARY
        /// <br/>- REFERENCES
        /// <br/>- SECURITY_CLEARANCES
        /// <br/>- SKILLS
        /// <br/>- SPEAKING
        /// <br/>- SUMMARY
        /// <br/>- TRAINING
        /// <br/>- WORK_HISTORY
        /// <br/>- WORK_STATUS
        /// </summary>
        public string SectionType { get; set; }

        /// <summary>
        /// The exact text that was used to identify the beginning of the section.
        /// If there was no text indicator and the location was calculated, then the value is "CALCULATED"
        /// </summary>
        public string HeaderTextFound { get; set; }
    }
}

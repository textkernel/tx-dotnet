// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Collections.Generic;

namespace Sovren.Models.Resume.Metadata
{
    /// <summary>
    /// The level/severity of a <see cref="ResumeQualityAssessment"/>
    /// </summary>
    public class ResumeQualityLevel
    {
        /// <summary>
        /// Only minor issues were found
        /// </summary>
        public static ResumeQualityLevel SuggestedImprovement = "Suggested Improvements";

        /// <summary>
        /// Some data was missing that should be included in a resume
        /// </summary>
        public static ResumeQualityLevel DataMissing = "Data Missing";

        /// <summary>
        /// A major issue was found in the resume that will reduce the quality of parse results
        /// </summary>
        public static ResumeQualityLevel MajorIssue = "Major Issues Found";

        /// <summary>
        /// A fatal issue was found in the resume. Parse results may have severe inaccuracies
        /// </summary>
        public static ResumeQualityLevel FatalProblem = "Fatal Problems Found";

        /// <summary>
        /// The string value for this level/severity
        /// </summary>
        public string Value { get; protected set; }

        private ResumeQualityLevel(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Converts a string to a <see cref="ResumeQualityLevel"/>
        /// </summary>
        /// <param name="value">the string value</param>
        public static implicit operator ResumeQualityLevel(string value)
        {
            return new ResumeQualityLevel(value);
        }
    }

    /// <summary>
    /// A list of <see cref="ResumeQualityFinding"/> of the same level/severity
    /// </summary>
    public class ResumeQualityAssessment
    {
        /// <summary>
        /// The level/severity of this assessment. One of:
        /// <br/><see cref="ResumeQualityLevel.FatalProblem"/>
        /// <br/><see cref="ResumeQualityLevel.MajorIssue"/>
        /// <br/><see cref="ResumeQualityLevel.DataMissing"/>
        /// <br/><see cref="ResumeQualityLevel.SuggestedImprovement"/>
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// A list of findings of the same severity/level
        /// </summary>
        public List<ResumeQualityFinding> Findings { get; set; }
    }

    
}

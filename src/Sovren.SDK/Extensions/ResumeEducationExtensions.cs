// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.Resume.Education;
using System.Collections.Generic;
using System.Linq;

namespace Sovren
{
    /// <summary></summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public static class ResumeEducationExtensions
    {
        /// <summary>
        /// Gets the candidate's highest education degree (if found) or <see langword="null"/>
        /// </summary>
        public static Degree GetHighestDegree(this ParseResumeResponseExtensions exts)
        {
            return exts.Response.Value.ResumeData?.Education.HighestDegree;
        }

        /// <summary>
        /// Gets the number of education entries the candidate listed (if found) or 0
        /// </summary>
        public static int GetNumberOfEducations(this ParseResumeResponseExtensions exts)
        {
            return exts.Response.Value.ResumeData?.Education?.EducationDetails?.Count ?? 0;
        }

        /// <summary>
        /// Gets the candidate's nth education entry (if exists) or <see langword="null"/>
        /// <br/>NOTE: this is 1-based, so pass in 1 to get the 1st entry, etc
        /// </summary>
        /// <param name="exts"></param>
        /// <param name="n">The 1-based index to use</param>
        public static EducationDetails GetNthEducation1Based(this ParseResumeResponseExtensions exts, int n)
        {
            return exts.Response.Value.ResumeData?.Education?.EducationDetails?.ElementAtOrDefault(n - 1);
        }

        /// <summary>
        /// Gets all education majors/minors listed on the resume (if found) or <see langword="null"/>
        /// </summary>
        public static IEnumerable<string> GetAllEducationFocusAreas(this ParseResumeResponseExtensions exts)
        {
            return exts.Response.Value.ResumeData?.Education?.EducationDetails?
                .SelectMany(e => e.Majors.Concat(e.Minors));
        }
    }
}

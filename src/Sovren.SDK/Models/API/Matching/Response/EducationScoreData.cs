// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.API.Matching.Response
{
    /// <inheritdoc/>
    public class EducationScoreData : CategoryScoreData
    {
        /// <summary>
        /// Requested level of education.
        /// </summary>
        public string ExpectedEducation { get; set; }

        /// <summary>
        /// Actual level of education found.
        /// </summary>
        public string ActualEducation { get; set; }

        /// <summary>
        /// How the <see cref="ActualEducation"/> compares to the <see cref="ExpectedEducation"/>. One of:
        /// <br/> DoesNotMeetExpected
        /// <br/> MeetsExpected
        /// <br/> ExceedsExpected
        /// </summary>
        public string Comparison { get; set; }
    }
}

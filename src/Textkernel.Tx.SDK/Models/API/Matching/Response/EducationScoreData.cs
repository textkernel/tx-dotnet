// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.API.Matching.Response
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

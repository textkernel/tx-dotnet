// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.Resume.Education
{
    /// <summary>
    /// Information about a GPA (or equivalent)
    /// </summary>
    public class GradePointAverage
    {
        /// <summary>
        /// The score found in the resume
        /// </summary>
        public string Score { get; set; }

        /// <summary>
        /// The scoring system used on the resume
        /// </summary>
        public string ScoringSystem { get; set; }

        /// <summary>
        /// The max score in the <see cref="ScoringSystem"/>
        /// </summary>
        public string MaxScore { get; set; }

        /// <summary>
        /// The minimum score in the <see cref="ScoringSystem"/>
        /// </summary>
        public string MinimumScore { get; set; }

        /// <summary>
        /// The <see cref="Score"/>, normalized to a 0.0-1.0 scale
        /// </summary>
        public decimal NormalizedScore { get; set; }
    }
}

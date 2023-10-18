// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Collections.Generic;

namespace Sovren.Models.Resume.Education
{
    /// <summary>
    /// Information about education history found on a resume
    /// </summary>
    public class EducationHistory
    {
        /// <summary>
        /// The highest degree obtained by a candidate
        /// </summary>
        public Degree HighestDegree { get; set; }

        /// <summary>
        /// All of the education details listed on a resume
        /// </summary>
        public List<EducationDetails> EducationDetails { get; set; }
    }
}

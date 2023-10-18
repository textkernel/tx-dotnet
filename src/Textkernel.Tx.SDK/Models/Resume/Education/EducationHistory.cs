// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System.Collections.Generic;

namespace Textkernel.Tx.Models.Resume.Education
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

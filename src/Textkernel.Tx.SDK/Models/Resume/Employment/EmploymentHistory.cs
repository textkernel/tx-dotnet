// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System.Collections.Generic;

namespace Textkernel.Tx.Models.Resume.Employment
{
    /// <summary>
    /// Work history found on a resume
    /// </summary>
    public class EmploymentHistory
    {
        /// <summary>
        /// A summary of all the work history with important calculated metadata
        /// </summary>
        public ExperienceSummary ExperienceSummary { get; set; }

        /// <summary>
        /// A list of jobs/positions found on the resume
        /// </summary>
        public List<Position> Positions { get; set; }
    }
}

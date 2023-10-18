// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Collections.Generic;

namespace Sovren.Models.Resume.Employment
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

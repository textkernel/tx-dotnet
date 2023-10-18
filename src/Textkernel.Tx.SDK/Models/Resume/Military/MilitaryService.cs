// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.Resume.Military
{
    /// <summary>
    /// A branch/name/rank for a military post/job
    /// </summary>
    public class MilitaryService
    {
        /// <summary>
        /// The name of the post/job
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The branch of the military
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// The military rank of the candidate
        /// </summary>
        public string Rank { get; set; }
    }
}

// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.Resume.Military
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

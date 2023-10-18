// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.Resume.Military
{
    /// <summary>
    /// Information about military post/job listed on a resume
    /// </summary>
    public class MilitaryDetails
    {
        /// <summary>
        /// The country that the military belongs to
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// The branch/name/rank for this post/job
        /// </summary>
        public MilitaryService Service { get; set; }

        /// <summary>
        /// The start date for this post/job
        /// </summary>
        public SovrenDate StartDate { get; set; }

        /// <summary>
        /// The end date for this post/job
        /// </summary>
        public SovrenDate EndDate { get; set; }

        /// <summary>
        /// The full text where Sovren found this military post/job in the resume
        /// </summary>
        public string FoundInContext { get; set; }
    }
}

// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.Resume.Military
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
        public TxDate StartDate { get; set; }

        /// <summary>
        /// The end date for this post/job
        /// </summary>
        public TxDate EndDate { get; set; }

        /// <summary>
        /// The full text where this military post/job was found in the resume
        /// </summary>
        public string FoundInContext { get; set; }
    }
}

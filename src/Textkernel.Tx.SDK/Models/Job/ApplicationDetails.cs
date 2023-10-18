// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;

namespace Textkernel.Tx.Models.Job
{
    /// <summary>
    /// An object containing details about the application process
    /// </summary>
    public class ApplicationDetails
    {
        /// <summary>
        /// Full text description of the application process
        /// </summary>
        public string ApplicationDescription { get; set; }
        
        /// <summary>
        /// Full name of the main contact person for the application
        /// </summary>
        public string ContactPerson { get; set; }

        /// <summary>
        /// Normalized phone of the organization with international calling prefix. Can contain multiple values (concatenated by comma)
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// Displayable email of the organization. Can contain multiple values (concatenated by comma)
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        /// Validated and normalized displayable website of the organization. Can contain multiple values (concatenated by comma)
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// Deadline to apply for the job
        /// </summary>
        public SovrenPrimitive<DateTime> ApplicationDeadline { get; set; }

        /// <summary>
        /// Date the job was posted
        /// </summary>
        public SovrenPrimitive<DateTime> PostedDate { get; set; }

        /// <summary>
        /// Any reference number found for the job application
        /// </summary>
        public string ReferenceNumber { get; set; }
    }
}

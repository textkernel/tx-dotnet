// Copyright © 2023 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;

namespace Sovren.Models.Job
{
    /// <summary>
    /// Additional details regarding the job application
    /// </summary>
    public class ApplicationDetails
    {
        /// <summary>
        /// Full text description of the application details
        /// </summary>
        public string ApplicationDescription { get; set; }
        
        /// <summary>
        /// Name of the main contact person for the application
        /// </summary>
        public string ContactPerson { get; set; }

        /// <summary>
        /// Phone number related to the application
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// Email related to the application
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        /// Application website
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// Deadline to apply for the job
        /// </summary>
        public SovrenPrimitive<DateTime> ApplicationDeadline { get; set; }

        /// <summary>
        /// Any reference number found for the job application
        /// </summary>
        public string ReferenceNumber { get; set; }
    }
}

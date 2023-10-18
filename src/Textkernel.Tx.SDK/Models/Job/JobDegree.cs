// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.Job
{
    /// <summary>
    /// A preferred/required educational degree found in a job
    /// </summary>
    public class JobDegree
    {
        /// <summary>
        /// The name of the educational degree
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of the educational degree
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The normalized, local education level based on the job's country. Returns the Code ID based on the table found <see href="https://developer.textkernel.com/Sovren/v10/job-order-parser/api/?h=Value.JobData.Degrees[i].LocalEducationLevel">here</see>.
        /// </summary>
        public string LocalEducationLevel { get; set; }
    }
}

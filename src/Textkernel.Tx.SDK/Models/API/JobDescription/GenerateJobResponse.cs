// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.API.JobDescription
{
    /// <inheritdoc/>
    public class GenerateJobResponse : ApiResponse<GenerateJobResponseValue> { }

    /// <summary>
    /// The <see cref="ApiResponse{T}.Value"/> from a 'Generate Job' response
    /// </summary>
    public class GenerateJobResponseValue
    {
        /// <summary>
        /// The generated job description
        /// </summary>
        public string JobDescription { get; set; }
    }
}

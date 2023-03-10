// Copyright © 2023 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.Job
{
    /// <summary>
    /// An object containing details about a job position's pay.
    /// </summary>
    public class PayRange
    {
        /// <summary>
        /// The normalized minimum yearly salary
        /// </summary>
        public SovrenPrimitive<int> Minimum { get; set; }

        /// <summary>
        /// The normalized maximum yearly salary
        /// </summary>
        public SovrenPrimitive<int> Maximum { get; set; }

        /// <summary>
        /// Currency code (ISO 4217) applied to the <see cref="Minimum"/> and <see cref="Maximum"/>
        /// </summary>
        public string Currency { get; set; }
    }
}

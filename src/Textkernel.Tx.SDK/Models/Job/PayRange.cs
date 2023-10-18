// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Textkernel.Tx.Models.Job
{
    /// <summary>
    /// An object containing details about a job position's pay.
    /// </summary>
    public class PayRange
    {
        /// <summary>
        /// The normalized minimum yearly salary
        /// </summary>
        public TxPrimitive<int> Minimum { get; set; }

        /// <summary>
        /// The normalized maximum yearly salary
        /// </summary>
        public TxPrimitive<int> Maximum { get; set; }

        /// <summary>
        /// The raw, un-normalized, minimum value. This is returned as is in the text, so there is no guarantee that it will evaluate to a valid number and not a string.
        /// </summary>
        public string RawMinimum { get; set; }

        /// <summary>
        /// The raw, un-normalized, maximum value. This is returned as is in the text, so there is no guarantee that it will evaluate to a valid number and not a string.
        /// </summary>
        public string RawMaximum { get; set; }

        /// <summary>
        /// Currency code (ISO 4217) applied to the <see cref="Minimum"/> and <see cref="Maximum"/>
        /// </summary>
        public string Currency { get; set; }
    }
}

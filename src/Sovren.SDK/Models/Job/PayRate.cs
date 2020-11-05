// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.Job
{
    /// <summary>
    /// Information about an hourly/yearly pay rate
    /// </summary>
    public class PayRate
    {
        /// <summary>
        /// The amount per <see cref="UnitOfTime"/>
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// The currency code. For example: "USD"
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Options: "hour", "day", "week", "month", "year"
        /// </summary>
        public string UnitOfTime { get; set; }
    }
}

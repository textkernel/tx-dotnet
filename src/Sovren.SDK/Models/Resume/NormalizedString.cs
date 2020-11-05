// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.Resume
{
    /// <summary>
    /// A string with a raw and normalized value
    /// </summary>
    public class NormalizedString
    {
        /// <summary>
        /// The raw value found in the text
        /// </summary>
        public string Raw { get; set; }

        /// <summary>
        /// The normalized value
        /// </summary>
        public string Normalized { get; set; }
    }
}

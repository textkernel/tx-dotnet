// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System.Text.Json.Serialization;

namespace Textkernel.Tx.Models.API.Parsing
{
    /// <summary>
    /// Metadata about a parsing transaction
    /// </summary>
    public class ParsingMetadata
    {
        /// <summary>
        /// How long it took to parse the document, in milliseconds.
        /// This is a subset of <see cref="ApiResponseInfo.TotalElapsedMilliseconds"/>
        /// </summary>
        public int ElapsedMilliseconds { get; set; }

        /// <summary>
        /// Whether or not the transaction timed out. If this is <see langword="true"/>, the returned data may be incomplete
        /// </summary>
        public bool TimedOut { get; set; }

        /// <summary>
        /// If <see cref="TimedOut"/> is <see langword="true"/>, this is how much time was spent parsing before the timeout occurred 
        /// </summary>
        public TxPrimitive<int> TimedOutAtMilliseconds { get; set; }

        /// <summary>
        /// For self-hosted customers only. The serial number of the current license being used for parsing.
        /// </summary>
        public string LicenseSerialNumber { get; set; }
    }
}

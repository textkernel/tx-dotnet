// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.API
{
    /// <summary>
    /// Information/metadata for an individual REST API call.
    /// <br/>See <see href="https://developer.textkernel.com/tx-platform/v10/overview/#http-status-codes"/>
    /// </summary>
    public class ApiResponseInfoLite
    {
        /// <summary>
        /// See <see href="https://developer.textkernel.com/tx-platform/v10/overview/#http-status-codes"/>
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// A short human-readable description explaining the <see cref="Code"/> value
        /// </summary>
        public string Message { get; set; }

        internal bool IsSuccess
        {
            get
            {
                switch (Code)
                {
                    case "Success":
                    case "WarningsFoundDuringParsing":
                    case "PossibleTruncationFromTimeout":
                    case "SomeErrors":
                        return true;
                }

                return false;
            }
        }
    }
}

// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.API
{
    /// <summary>
    /// Information/metadata for an individual REST API call.
    /// <br/>See <see href="https://sovren.com/technical-specs/latest/rest-api/overview/#http-status-codes"/>
    /// </summary>
    public class ApiResponseInfoLite
    {
        /// <summary>
        /// See <see href="https://sovren.com/technical-specs/latest/rest-api/overview/#http-status-codes"/>
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

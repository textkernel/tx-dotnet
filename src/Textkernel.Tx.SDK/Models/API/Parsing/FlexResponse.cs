// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System.Collections.Generic;

namespace Textkernel.Tx.Models.API.Parsing
{
    /// <summary>
    /// Information about the FlexRequests transaction, if any were provided.
    /// </summary>
    public class FlexResponse
    {
        /// <summary>
        /// See <see href="https://developer.textkernel.com/tx-platform/v10/overview/#http-status-codes"/>
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// A short human-readable description explaining the <see cref="Code"/> value
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Responses to FlexRequests
        /// </summary>
        public List<FlexResponseItem> Responses { get; set; }

        internal bool IsSuccess
        {
            get
            {
                switch (Code)
                {
                    case "Success":
                        return true;
                }

                return false;
            }
        }
    }

    /// <summary>
    /// Responses to FlexRequests
    /// </summary>
    public class FlexResponseItem
    {
        /// <summary>
        /// Unique field name assigned to the respective FlexRequest
        /// </summary>
        public string Identifier { get; set; }

        /// <summary>
        /// Reply to the FlexRequest Prompt
        /// </summary>
        public string Reply { get; set; }
        /// <summary>
        /// List of replies to the FlexRequest Prompt if the FlexRequest had a <see cref="FlexRequestDataType.List"/> DataType
        /// </summary>
        public List<string> ReplyList { get; set; }
    }
}

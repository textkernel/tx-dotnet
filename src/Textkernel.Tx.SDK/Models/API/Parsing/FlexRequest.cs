// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Textkernel.Tx.Models.API.Parsing
{
    /// <summary>
    /// Custom requests to ask during parsing. 
    /// See the <see href="https://developer.textkernel.com/tx-platform/v10/resume-parser/overview/llm-parser/#flex-requests">overview documentation</see> for more details.
    /// <see href="https://developer.textkernel.com/tx-platform/v10/overview/#transaction-cost">Additional charges</see> will apply.
    /// </summary>
    public class FlexRequest
    {
        /// <summary>
        /// The prompt to be sent to the LLM Parsing Engine
        /// </summary>
        public string Prompt { get; set; }
        /// <summary>
        /// Unique field name to be returned alongside the reply in the response
        /// </summary>
        public string Identifier { get; set; }
        /// <summary>
        /// The data type for the reply
        /// </summary>
        [JsonConverter(typeof(EnumMemberConverter<FlexRequestDataType>))]
        public FlexRequestDataType DataType { get; set; }
        /// <summary>
        /// If DataType is <see cref="FlexRequestDataType.Enumeration"/>, this is the list of possible replies. This is limited to a maximum of 50 values.
        /// </summary>
        public List<string> EnumerationValues { get; set; }
    }

    /// <summary>
    /// Possible data types for FlexRequests
    /// </summary>
    public enum FlexRequestDataType
    {
        Text,
        Numeric,
        Bool,
        List,
        Enumeration
    }
}

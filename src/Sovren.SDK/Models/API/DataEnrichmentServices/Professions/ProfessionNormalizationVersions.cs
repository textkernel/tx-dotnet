// Copyright © 2022 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Sovren.Models.API.DataEnrichmentServices.Professions
{
    /// <summary>
    /// Versions to use when normalizing professions if more than one is available for a taxonomy
    /// </summary>
    public class ProfessionNormalizationVersions
    {
        /// <summary>
        /// The ONET Version to use when normalizing Professions. Defaults to ONET2010
        /// </summary>
        [JsonConverter(typeof(EnumMemberConverter<ONETVersion>))]
        public ONETVersion ONET { get; set; }
   
    }

    public enum ONETVersion 
    {
        [EnumMember(Value = "2010")]
        ONET2010,
        [EnumMember(Value = "2019")]
        ONET2019
    }
}

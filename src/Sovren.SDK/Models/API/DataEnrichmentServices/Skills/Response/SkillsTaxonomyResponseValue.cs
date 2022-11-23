// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Sovren.Models.API.DataEnrichmentServices.Skills.Response
{
    public class SkillsTaxonomyResponseValue
    {
        [JsonPropertyName("Codes")]
        public List<Code> Skills { get; set; }
        [JsonPropertyName("CsvOutput")]
        public string CsvOutput { get; set; }
    }

    public class Code
    {
        [JsonPropertyName("CodeId")]
        public string CodeId { get; set; }

        [JsonPropertyName("Descriptions")]
        public LangDescriptions Descriptions { get; set; }

        [JsonPropertyName("Category")]
        public string Category { get; set; }

        [JsonPropertyName("IsoCode")]
        public string IsoCode { get; set; }
    }

    public class LangDescriptions
    {
        [JsonPropertyName("EnUs")]
        public string EnglishUS { get; set; }

        [JsonPropertyName("En")]
        public string English { get; set; }

        [JsonPropertyName("De")]
        public string German { get; set; }
        [JsonPropertyName("It")]
        public string Italian { get; set; }
        [JsonPropertyName("He")]
        public string Hebrew { get; set; }
        [JsonPropertyName("Nl")]
        public string Dutch { get; set; }
        [JsonPropertyName("Fr")]
        public string French { get; set; }
        [JsonPropertyName("Es")]
        public string Spanish { get; set; }
        [JsonPropertyName("Zh")]
        public string Chinese { get; set; }
        [JsonPropertyName("ZhTw")]
        public string Tawainese { get; set; }
        [JsonPropertyName("Ar")]
        public string Arabic { get; set; }
    }
}

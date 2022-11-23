// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Sovren.Models.API.DataEnrichmentServices.Professions.Response
{
    public class ProfessionsTaxonomyResponseValue
    {
        [JsonPropertyName("Codes")]
        public List<ProfessionGroupClass> Professions { get; set; }
        [JsonPropertyName("CsvOutput")]
        public string CsvOutput { get; set; }
    }

    public class ProfessionGroupClass
    {
        [JsonPropertyName("TK")]
        public ProfessionGroupClassInfo TKInfo { get; set; }
    }

    public class ProfessionGroupClassInfo
    {
        [JsonPropertyName("Class")]
        public GroupOrClassInfo Class { get; set; }
        [JsonPropertyName("Group")]
        public GroupOrClassInfo Group { get; set; }
    }

    public class GroupOrClassInfo
    {
        [JsonPropertyName("CodeId")]
        public int CodeId { get; set; }
        [JsonPropertyName("Descriptions")]
        public AllLangsDescriptions Descriptions { get; set; }
    }

    public class AllLangsDescriptions
    {
        [JsonPropertyName("EnUS")]
        public string EnglishUS { get; set; }
        [JsonPropertyName("En")]
        public string English { get; set; }
        [JsonPropertyName("De")]
        public string German { get; set; }
        [JsonPropertyName("It")]
        public string Italian { get; set; }
        [JsonPropertyName("Nl")]
        public string Dutch { get; set; }
        [JsonPropertyName("Fr")]
        public string French { get; set; }
        [JsonPropertyName("Es")]
        public string Spanish { get; set; }
        [JsonPropertyName("Zh")]
        public string Chinese { get; set; }
    }
}

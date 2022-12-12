// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API.DataEnrichmentServices.Ontology.Response;
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
        public OntologyMetadata Metadata { get; set; }
        public string CsvOutput { get; set; }
    }

    public class ProfessionGroupClass
    {
        [JsonPropertyName("TK")]
        public ProfessionGroupClassInfo TKInfo { get; set; }

        [JsonPropertyName("Onet")]
        public GroupOrClassInfo<string> Onet { get; set; }

        [JsonPropertyName("Onet2019")]
        public GroupOrClassInfo<string> Onet2019 { get; set; }

        [JsonPropertyName("Kldb2020")]
        public GroupOrClassInfo<string> Kldb2020 { get; set; }

        [JsonPropertyName("UwvBoc")]
        public GroupOrClassInfo<string> UwvBoc { get; set; }

        [JsonPropertyName("UkSoc2010")]
        public GroupOrClassInfo<string> UkSoc2010 { get; set; }

        [JsonPropertyName("Isco")]
        public GroupOrClassInfo<string> Isco { get; set; }
    }

    public class ProfessionGroupClassInfo
    {
        [JsonPropertyName("Class")]
        public GroupOrClassInfo<int> Class { get; set; }

        [JsonPropertyName("Group")]
        public GroupOrClassInfo<int> Group { get; set; }

        [JsonPropertyName("CodeId")]
        public int CodeId { get; set; }

        [JsonPropertyName("Descriptions")]
        public AllLangsDescriptions Descriptions { get; set; }
    }

    public class GroupOrClassInfo<T>
    {
        [JsonPropertyName("CodeId")]
        public T CodeId { get; set; }
        [JsonPropertyName("Descriptions")]
        public AllLangsDescriptions Descriptions { get; set; }
    }

    public class AllLangsDescriptions
    {
        public string EnglishUS { get; set; }
        public string EnglishGB { get; set; }
        public string English { get; set; }
        public string Polish { get; set; }
        public string Japanese { get; set; }
        public string Portuguese { get; set; }
        public string PortugueseBR { get; set; }
        public string PortuguesePT { get; set; }
        public string Swedish { get; set; }
        public string Hungarian { get; set; }
        public string German { get; set; }
        public string Italian { get; set; }
        public string Dutch { get; set; }
        public string French { get; set; }
        public string Spanish { get; set; }
        public string Chinese { get; set; }
    }
}

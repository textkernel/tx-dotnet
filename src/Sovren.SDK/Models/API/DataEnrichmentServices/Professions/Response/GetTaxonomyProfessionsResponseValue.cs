// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Newtonsoft.Json;
using Sovren.Models.API.DataEnrichmentServices.Ontology.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Sovren.Models.API.DataEnrichmentServices.Professions.Response
{
    public class GetTaxonomyProfessionsResponseValue
    {
        public List<ProfessionGroupClass> Professions { get; set; }
        public string CsvOutput { get; set; }
    }

    public class ProfessionGroupClass : ProfessionGroupClassInfo
    {
        public GroupOrClassInfo<string> Onet { get; set; }
        public GroupOrClassInfo<string> Onet2019 { get; set; }
        public GroupOrClassInfo<string> Kldb2020 { get; set; }
        public GroupOrClassInfo<string> UwvBoc { get; set; }
        public GroupOrClassInfo<string> UkSoc2010 { get; set; }
        public GroupOrClassInfo<string> Isco { get; set; }
    }

    public class ProfessionGroupClassInfo
    {
        [JsonProperty(Order = -5)]
        public int CodeId { get; set; }
        [JsonProperty(Order = -4)]
        public AllLangsDescriptions Descriptions { get; set; }
        [JsonProperty(Order = -3)]
        public GroupOrClassInfo<int> Group { get; set; }
        [JsonProperty(Order = -2)]
        public GroupOrClassInfo<int> Class { get; set; }
    }

    public class GroupOrClassInfo<T>
    {
        public T CodeId { get; set; }
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

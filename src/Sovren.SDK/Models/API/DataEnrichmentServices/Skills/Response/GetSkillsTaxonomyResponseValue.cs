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
    public class GetSkillsTaxonomyResponseValue
    {
        public List<Code> Skills { get; set; }
        public string CsvOutput { get; set; }
    }

    public class Code
    {
        public string Id { get; set; }
        public LangDescriptions Descriptions { get; set; }
        public string Type { get; set; }
        /// <summary>
        /// only exists when Category = Language
        /// </summary>
        public string IsoCode { get; set; }
    }

    public class LangDescriptions
    {
        public string English { get; set; }
        public string German { get; set; }
        public string Italian { get; set; }
        public string Hebrew { get; set; }
        public string Dutch { get; set; }
        public string French { get; set; }
        public string Spanish { get; set; }
        public string Chinese { get; set; }
        public string ChineseTW { get; set; }
        public string Arabic { get; set; }
        public string Portuguese { get; set; }
        public string Japanese { get; set; }
    }
}

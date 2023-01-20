// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API.DataEnrichmentServices.Ontology.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Sovren.Models.API.DataEnrichmentServices.Skills.Response
{
    public class LookupSkillCodesResponseValue
    {
        public List<SkillCode> Skills { get; set; }
    }

    public class SkillCode
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}

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
    public class AutoCompleteSkillsResponseValue
    {
        public List<AutoCompleteSkill> Skills { get; set; }
    }

    public class AutoCompleteSkill
    {
        public string Description { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
    }
}

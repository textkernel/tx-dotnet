// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.DataEnrichmentServices.Ontology.Request
{
    public class SuggestProfessionsRequest
    {
        public List<string> SkillIds { get; set; }
        public bool ReturnMissingSkills { get; set; } = false;
        public int Limit { get; set; } = 10;
    }
}

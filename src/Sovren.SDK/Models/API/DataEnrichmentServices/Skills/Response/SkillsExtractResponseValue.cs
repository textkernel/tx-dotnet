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
    public class SkillsExtractResponseValue
    {
        public bool Truncated { get; set; }
        public List<ExtractedSkill> Skills { get; set; }
    }

    public class ExtractedSkill : BaseSkill
    {
        public float Confidence { get; set; }
        public List<Match> Matches { get; set; }
    }

    public class Match
    {
        public int BeginSpan { get; set; }
        public int EndSpan { get; set; }
        public float Likelihood { get; set; }
        public string SurfaceForm { get; set; }
    }
}

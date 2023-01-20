// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.DataEnrichmentServices.Ontology.Response
{
    public class CompareSkillsResponseValue
    {
        public float SimilarityScore { get; set; }
        public List<SkillScore> CommonSkills { get; set; }
        public List<ProfessionExclusiveSkills> ExclusiveSkillsByProfession { get; set; }
    }

    public class ProfessionExclusiveSkills
    {
        public int ProfessionCodeId { get; set; }
        public List<SkillScore> SkillsFoundOnlyInThisProfession { get; set; }
    }

    public class ProfessionScore
    {
        public float Score { get; set; }
        public int CodeId { get; set; }
    }
}

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
        public OntologyMetadata Metadata { get; set; }
        public float SimilarityScore { get; set; }
        public List<BaseProfession> CommonSkills { get; set; }
        public List<ExclusiveSkill> ExclusiveSkills { get; set; }
    }

    public class ExclusiveSkill
    {
        public string CodeId { get; set; }
        public List<BaseProfession> Professions { get; set; }
    }

    public class OntologyMetadata
    {
        public string ServiceVersion { get; set; }
        public string TaxonomyRelease { get; set; }
    }

    public class BaseProfession
    {
        public float Score { get; set; }
        public string CodeId { get; set; }
    }
}

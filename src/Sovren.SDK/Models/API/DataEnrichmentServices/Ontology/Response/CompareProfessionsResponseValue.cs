// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.DataEnrichmentServices.Ontology.Response
{
    public class CompareProfessionsResponseValue
    {
        public OntologyMetadata Meta { get; set; }
        public float SimilarityScore { get; set; }
        public List<BaseProfession> CommonSkills { get; set; }
        public CompareProfessionsSkills ExclusiveSkills { get; set; }
    }

    public class CompareProfessionsSkills
    {
        public List<ExclusiveSkill> SkillBasedProfessions { get; set; }
        public List<BaseProfession> InputSkills { get; set; }
    }
}

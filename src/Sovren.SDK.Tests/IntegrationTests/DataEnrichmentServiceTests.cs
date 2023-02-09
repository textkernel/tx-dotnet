// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using NUnit.Framework;
using Sovren.Models.API.DataEnrichment;
using Sovren.Models.API.DataEnrichment.Ontology.Request;
using Sovren.Models.API.DataEnrichment.Ontology.Response;
using Sovren.Models.API.DataEnrichment.Professions;
using Sovren.Models.API.DataEnrichment.Professions.Request;
using Sovren.Models.API.DataEnrichment.Professions.Response;
using Sovren.Models.API.DataEnrichment.Skills.Request;
using Sovren.Models.API.DataEnrichment.Skills.Response;
using System.Collections.Generic;

namespace Sovren.SDK.Tests.IntegrationTests
{
    public class DataEnrichmentServiceTests : TestBase
    {

        [Test]
        public void TestSkillsTaxonomy()
        {
            GetSkillsTaxonomyResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.GetSkillsTaxonomy(Models.API.DataEnrichment.TaxonomyFormat.json); });
            Assert.NotNull(response?.Value?.Skills);
            Assert.GreaterOrEqual(response?.Value?.Skills.Count, 1);
        }

        [Test]
        public void TestSkillsMetadata()
        {
            GetMetadataResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.GetSkillsTaxonomyMetadata(); });
            Assert.NotNull(response.Value.ServiceVersion);
            Assert.NotNull(response.Value.TaxonomyReleaseDate);
        }

        [Test]
        public void TestProfessionsTaxonomy()
        {
            GetProfessionsTaxonomyResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.GetProfessionsTaxonomy("en", Models.API.DataEnrichment.TaxonomyFormat.json); });
            Assert.NotNull(response?.Value?.Professions);
            Assert.GreaterOrEqual(response?.Value?.Professions.Count, 1);
        }

        [Test]
        public void TestProfessionsMetadata()
        {
            GetMetadataResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.GetProfessionsTaxonomyMetadata(); });
            Assert.NotNull(response.Value.ServiceVersion);
            Assert.NotNull(response.Value.TaxonomyReleaseDate);
        }

        [Test]
        public void TestSkillsAutoComplete()
        {
            SkillsAutoCompleteRequest request = new SkillsAutoCompleteRequest { Prefix = "soft", Languages = new List<string> { "en" }, Types = new List<string> { "all" } };
            AutoCompleteSkillsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.AutocompleteSkill("soft"); });
            Assert.NotNull(response?.Value?.Skills);
            Assert.GreaterOrEqual(response?.Value?.Skills.Count, 1);
        }

        [Test]
        public void TestProfessionsAutoComplete()
        {
            AutoCompleteProfessionsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.AutocompleteProfession("soft"); });
            Assert.NotNull(response?.Value?.Professions);
            Assert.GreaterOrEqual(response?.Value?.Professions.Count, 1);
        }

        [Test]
        public void TestSkillsLookup()
        {
            LookupSkillCodesResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.LookupSkills(new List<string> { "KS120B874P2P6BK1MQ0T" }); });
            Assert.NotNull(response?.Value?.Skills);
            Assert.GreaterOrEqual(response?.Value?.Skills.Count, 1);
        }

        [Test]
        public void TestProfessionsLookup()
        {
            LookupProfessionCodesResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.LookupProfessions(new List<int> { 2000 }); });
            Assert.NotNull(response?.Value?.Professions);
            Assert.GreaterOrEqual(response?.Value?.Professions.Count, 1);
        }

        [Test]
        public void TestSkillsNormalize()
        {
            NormalizeSkillsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.NormalizeSkills(new List<string> { "Microsoft excel" }); });
            Assert.NotNull(response?.Value?.Skills);
            Assert.GreaterOrEqual(response?.Value?.Skills.Count, 1);
        }

        [Test]
        public void TestProfessionsNormalize()
        {
            NormalizeProfessionsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.NormalizeProfessions(new List<string> { "Software Engineer" }); });
            Assert.GreaterOrEqual(response?.Value?.Professions.Count, 1);
        }

        [Test]
        public void TestSkillsExtract()
        {
            ExtractSkillsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.ExtractSkills("Microsoft, developer python, software, clerical office assistant, excel"); });
            Assert.NotNull(response?.Value?.Skills);
            Assert.GreaterOrEqual(response?.Value?.Skills.Count, 1);
        }

        [Test]
        public void TestCompareProfessions()
        {
            CompareProfessionsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.CompareProfessions(696, 3178); });
            Assert.NotNull(response?.Value?.CommonSkills);
            Assert.GreaterOrEqual(response?.Value?.CommonSkills.Count, 1);
            Assert.NotNull(response?.Value?.ExclusiveSkillsByProfession);
            Assert.GreaterOrEqual(response?.Value?.ExclusiveSkillsByProfession.Count, 1);
        }

        [Test]
        public void TestCompareSkillsToProfession()
        {
            CompareSkillsToProfessionResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.CompareSkillsToProfession(696, "KS120076FGP5WGWYMP0F", "KS04UWLJBN9X1M3N0PZ4"); });
            Assert.NotNull(response?.Value?.CommonSkills);
            Assert.GreaterOrEqual(response?.Value?.CommonSkills.Count, 1);
        }

        [Test]
        public void TestSuggestSkills()
        {
            SuggestSkillsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.SuggestSkills(new List<int> { 696 }); });
            Assert.NotNull(response?.Value?.SuggestedSkills);
            Assert.GreaterOrEqual(response?.Value?.SuggestedSkills.Count, 1);
        }

        [Test]
        public void TestSuggestProfessions()
        {
            SuggestProfessionsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.SuggestProfessions(new List<string> { "KS120076FGP5WGWYMP0F", "KS125HH5XDBPZT3RFGZZ", "KS124PR62MV42B5C9S9F" }); });
            Assert.NotNull(response?.Value?.SuggestedProfessions);
            Assert.GreaterOrEqual(response?.Value?.SuggestedProfessions.Count, 1);
        }
    }
}


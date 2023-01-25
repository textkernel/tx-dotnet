// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using NUnit.Framework;
using Sovren.Models.API.DataEnrichmentServices.Ontology.Request;
using Sovren.Models.API.DataEnrichmentServices.Ontology.Response;
using Sovren.Models.API.DataEnrichmentServices.Professions;
using Sovren.Models.API.DataEnrichmentServices.Professions.Request;
using Sovren.Models.API.DataEnrichmentServices.Professions.Response;
using Sovren.Models.API.DataEnrichmentServices.Skills.Request;
using Sovren.Models.API.DataEnrichmentServices.Skills.Response;
using System.Collections.Generic;

namespace Sovren.SDK.Tests.IntegrationTests
{
    public class DataEnrichmentServiceTests : TestBase
    {
        SovrenClient client = new SovrenClient("38643208", "YGmU8vCINSxn1ws9Yr1HmSQR5QXqDHc/3nVQlrjh", new DataCenter("https://rest-local.sovren.com/v10/"));

        [Test]
        public void TestSkillTaxonomy()
        {
            GetSkillsTaxonomyRequest request = new GetSkillsTaxonomyRequest { Format = "json"};
            GetSkillsTaxonomyResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.GetSkillsTaxonomy(request); });
            Assert.NotNull(response?.Value?.Skills);
            Assert.GreaterOrEqual(response?.Value?.Skills.Count, 1);
        }

        [Test]
        public void TestProfessionTaxonomy()
        {
            GetProfessionsTaxonomyRequest request = new GetProfessionsTaxonomyRequest { Format = "json", Language = "en" };
            GetProfessionsTaxonomyResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.GetProfessionsTaxonomy(request); });
            Assert.NotNull(response?.Value?.Professions);
            Assert.GreaterOrEqual(response?.Value?.Professions.Count, 1);
        }

        [Test]
        public void TestSkillAutoComplete()
        {
            SkillsAutoCompleteRequest request = new SkillsAutoCompleteRequest { Prefix = "soft", Languages = new List<string> { "en" }, Types = new List<string> { "all" } };
            AutoCompleteSkillsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.SkillsAutoComplete(request); });
            Assert.NotNull(response?.Value?.Skills);
            Assert.GreaterOrEqual(response?.Value?.Skills.Count, 1);
        }

        [Test]
        public void TestProfessionAutoComplete()
        {
            ProfessionsAutoCompleteRequest request = new ProfessionsAutoCompleteRequest { Prefix = "soft", Languages = new List<string> { "en" }};
            AutoCompleteProfessionsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.ProfessionsAutoComplete(request); });
            Assert.NotNull(response?.Value?.Professions);
            Assert.GreaterOrEqual(response?.Value?.Professions.Count, 1);
        }

        [Test]
        public void TestSkillsLookup()
        {
            LookupSkillCodesRequest request = new LookupSkillCodesRequest {  SkillIds = new List<string> { "KS120B874P2P6BK1MQ0T" } };
            LookupSkillCodesResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.SkillsLookup(request); });
            Assert.NotNull(response?.Value?.Skills);
            Assert.GreaterOrEqual(response?.Value?.Skills.Count, 1);
        }

        [Test]
        public void TestProfessionsLookup()
        {
            LookupProfessionCodesRequest request = new LookupProfessionCodesRequest { CodeIds = new List<int> { 2000 } };
            LookupProfessionCodesResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.ProfessionsLookup(request); });
            Assert.NotNull(response?.Value?.ProfessionCodes);
            Assert.GreaterOrEqual(response?.Value?.ProfessionCodes.Count, 1);
        }

        [Test]
        public void TestSkillsNormalize()
        {
            NormalizeSkillsRequest request = new NormalizeSkillsRequest { Skills = new List<string> { "Microsoft excel" }, Language = "en" };
            NormalizeSkillsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.SkillsNormalize(request); });
            Assert.NotNull(response?.Value?.Skills);
            Assert.GreaterOrEqual(response?.Value?.Skills.Count, 1);
        }

        [Test]
        public void TestProfessionsNormalize()
        {
            NormalizeProfessionsRequest request = new NormalizeProfessionsRequest { JobTitles = new List<string> { "Software Engineer" } };
            NormalizeProfessionsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.ProfessionsNormalize(request); });
            Assert.GreaterOrEqual(response?.Value?.Professions.Count, 1);
        }

        [Test]
        public void TestSkillsExtract()
        {
            ExtractSkillsRequest request = new ExtractSkillsRequest { Text = "Microsoft, developer python, software, clerical office assistant, excel", Language = "en", OutputLanguage = "en" };
            ExtractSkillsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.SkillsExtract(request); });
            Assert.NotNull(response?.Value?.Skills);
            Assert.GreaterOrEqual(response?.Value?.Skills.Count, 1);
        }

        [Test]
        public void TestCompareProfessions()
        {
            CompareProfessionsRequest request = new CompareProfessionsRequest { ProfessionCodeIds = new List<int> { 696, 3178 } };
            CompareProfessionsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.CompareProfessions(request); });
            Assert.NotNull(response?.Value?.CommonSkills);
            Assert.GreaterOrEqual(response?.Value?.CommonSkills.Count, 1);
            Assert.NotNull(response?.Value?.ExclusiveSkillsByProfession);
            Assert.GreaterOrEqual(response?.Value?.ExclusiveSkillsByProfession.Count, 1);
        }

        [Test]
        public void TestCompareSkillsToProfession()
        {
            CompareSkillsToProfessionRequest request = new CompareSkillsToProfessionRequest { SkillIds = new List<string> { "KS120076FGP5WGWYMP0F", "KS04UWLJBN9X1M3N0PZ4" }, ProfessionCodeId = 696 };
            CompareSkillsToProfessionResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.CompareSkillsToProfession(request); });
            Assert.NotNull(response?.Value?.CommonSkills);
            Assert.GreaterOrEqual(response?.Value?.CommonSkills.Count, 1);
        }

        [Test]
        public void TestSuggestSkills()
        {
            SuggestSkillsRequest request = new SuggestSkillsRequest { ProfessionCodeIds = new List<int> { 696 } };
            SuggestSkillsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.SuggestSkills(request); });
            Assert.NotNull(response?.Value?.SuggestedSkills);
            Assert.GreaterOrEqual(response?.Value?.SuggestedSkills.Count, 1);
        }

        [Test]
        public void TestSuggestProfessions()
        {
            SuggestProfessionsRequest request = new SuggestProfessionsRequest { SkillIds = new List<string> { "KS120076FGP5WGWYMP0F", "KS125HH5XDBPZT3RFGZZ", "KS124PR62MV42B5C9S9F" } };
            SuggestProfessionsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.SuggestProfessions(request); });
            Assert.NotNull(response?.Value?.SuggestedProfessions);
            Assert.GreaterOrEqual(response?.Value?.SuggestedProfessions.Count, 1);
        }
    }
}


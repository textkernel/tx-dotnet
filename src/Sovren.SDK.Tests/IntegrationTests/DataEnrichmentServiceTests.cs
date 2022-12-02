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
            GetSkillsTaxonomyRequest request = new GetSkillsTaxonomyRequest { Format = "json", Language = "en" };
            GetSkillsTaxonomyResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.GetSkillsTaxonomy(request); });
            Assert.NotNull(response?.Value?.Skills);
            Assert.GreaterOrEqual(response?.Value?.Skills.Count, 0);
        }

        [Test]
        public void TestProfessionTaxonomy()
        {
            GetProfessionsTaxonomyRequest request = new GetProfessionsTaxonomyRequest { Format = "json", Language = "en" };
            GetProfessionsTaxonomyResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.GetProfessionsTaxonomy(request); });
            Assert.NotNull(response?.Value?.Professions);
            Assert.GreaterOrEqual(response?.Value?.Professions.Count, 0);
        }

        [Test]
        public void TestSkillAutoComplete()
        {
            SkillsAutoCompleteRequest request = new SkillsAutoCompleteRequest { Prefix = "soft", Languages = new List<string> { "en" }, Categories = new List<string> { "all" } };
            SkillsAutoCompleteResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.SkillsAutoComplete(request); });
            Assert.NotNull(response?.Value?.AutoCompletes);
            Assert.GreaterOrEqual(response?.Value?.AutoCompletes.Count, 0);
        }

        [Test]
        public void TestProfessionAutoComplete()
        {
            ProfessionsAutoCompleteRequest request = new ProfessionsAutoCompleteRequest { Prefix = "soft", Languages = new List<string> { "en" }, Categories = new List<string> { "all" } };
            ProfessionsAutoCompleteResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.ProfessionsAutoComplete(request); });
            Assert.NotNull(response?.Value?.AutoCompletes);
            Assert.GreaterOrEqual(response?.Value?.AutoCompletes.Count, 0);
        }

        [Test]
        public void TestSkillsLookup()
        {
            SkillsLookupRequest request = new SkillsLookupRequest {  CodeIds = new List<string> { "KS120B874P2P6BK1MQ0T" } };
            SkillsLookupResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.SkillsLookup(request); });
            Assert.NotNull(response?.Value?.Skills);
            Assert.GreaterOrEqual(response?.Value?.Skills.Count, 0);
        }

        [Test]
        public void TestProfessionsLookup()
        {
            ProfessionsLookupRequest request = new ProfessionsLookupRequest { CodeIds = new List<string> { "2000" } };
            ProfessionsLookupResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.ProfessionsLookup(request); });
            Assert.NotNull(response?.Value?.ProfessionCodes);
            Assert.GreaterOrEqual(response?.Value?.ProfessionCodes.Count, 0);
        }

        [Test]
        public void TestSkillsNormalize()
        {
            SkillsNormalizeRequest request = new SkillsNormalizeRequest { Skills = new List<string> { "Microsoft excel" }, Language = "en" };
            SkillsNormalizeResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.SkillsNormalize(request); });
            Assert.NotNull(response?.Value?.Skills);
            Assert.GreaterOrEqual(response?.Value?.Skills.Count, 0);
        }

        [Test]
        public void TestProfessionsNormalize()
        {
            ProfessionsNormalizeRequest request = new ProfessionsNormalizeRequest { JobTitles = new List<string> { "Software Engineer" } };
            ProfessionsNormalizeResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.ProfessionsNormalize(request); });
            Assert.NotNull(response?.Value?[0]);
        }

        [Test]
        public void TestProfessionsNormalizeWithVersion()
        {
            ProfessionsNormalizeRequest request = new ProfessionsNormalizeRequest { JobTitles = new List<string> { "Software Engineer" }, Version = new ProfessionNormalizationVersions() { ONET = ONETVersion.ONET2019 } };
            ProfessionsNormalizeResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.ProfessionsNormalize(request); });
            Assert.NotNull(response?.Value?[0]);
        }

        [Test]
        public void TestSkillsExtract()
        {
            SkillsExtractRequest request = new SkillsExtractRequest { Text = "Microsoft, developer python, software, clerical office assistant, excel", Language = "en", OutputLanguage = "en" };
            SkillsExtractResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.SkillsExtract(request); });
            Assert.NotNull(response?.Value?.Skills);
            Assert.GreaterOrEqual(response?.Value?.Skills.Count, 0);
        }

        [Test]
        public void TestCompareSkills()
        {
            CompareSkillsRequest request = new CompareSkillsRequest { CodeIds = new List<string> { "696", "3178" } };
            CompareSkillsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.CompareSkills(request); });
            Assert.NotNull(response?.Value?.CommonSkills);
            Assert.GreaterOrEqual(response?.Value?.CommonSkills.Count, 0);
            Assert.NotNull(response?.Value?.ExclusiveSkills);
            Assert.GreaterOrEqual(response?.Value?.ExclusiveSkills.Count, 0);
        }

        [Test]
        public void TestCompareSkillsToProfessions()
        {
            CompareSkillsToProfessionsRequest request = new CompareSkillsToProfessionsRequest { SkillCodeIds = new List<string> { "KS04QIYB82UAEDED1GIQ", "KS04UWLJBN9X1M3N0PZ4" }, ProfessionCodeId = "696" };
            CompareSkillsToProfessionsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.CompareSkillsToProfessions(request); });
            Assert.NotNull(response?.Value?.CommonSkills);
            Assert.GreaterOrEqual(response?.Value?.CommonSkills.Count, 0);
            Assert.NotNull(response?.Value?.ExclusiveSkills);
            Assert.GreaterOrEqual(response?.Value?.ExclusiveSkills.SkillBasedProfessions.Count, 0);
        }

        [Test]
        public void TestSuggestSkills()
        {
            SuggestSkillsRequest request = new SuggestSkillsRequest { CodeIds = new List<string> { "696" } };
            SuggestSkillsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.SuggestSkills(request); });
            Assert.NotNull(response?.Value?.SuggestedSkills);
            Assert.GreaterOrEqual(response?.Value?.SuggestedSkills.Count, 0);
        }

        [Test]
        public void TestSuggestProfessions()
        {
            SuggestProfessionsRequest request = new SuggestProfessionsRequest { CodeIds = new List<string> { "KS120076FGP5WGWYMP0F", "KS125HH5XDBPZT3RFGZZ", "KS124PR62MV42B5C9S9F" } };
            SuggestProfessionsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await client.SuggestProfessions(request); });
            Assert.NotNull(response?.Value?.SuggestedProfessions);
            Assert.GreaterOrEqual(response?.Value?.SuggestedProfessions.Count, 0);
        }
    }
}


// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using NUnit.Framework;
using Textkernel.Tx.Models.API.DataEnrichment;
using Textkernel.Tx.Models.API.DataEnrichment.Ontology.Request;
using Textkernel.Tx.Models.API.DataEnrichment.Ontology.Response;
using Textkernel.Tx.Models.API.DataEnrichment.Professions;
using Textkernel.Tx.Models.API.DataEnrichment.Professions.Request;
using Textkernel.Tx.Models.API.DataEnrichment.Professions.Response;
using Textkernel.Tx.Models.API.DataEnrichment.Skills.Request;
using Textkernel.Tx.Models.API.DataEnrichment.Skills.Response;
using System.Collections.Generic;
using System.Linq;

namespace Textkernel.Tx.SDK.Tests.IntegrationTests
{
    public class DataEnrichmentServiceTests : TestBase
    {

        [Test]
        public void TestSkillsTaxonomy()
        {
            GetSkillsTaxonomyResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.SkillsIntelligence.GetSkillsTaxonomy(Models.API.DataEnrichment.TaxonomyFormat.json); });
            Assert.NotNull(response?.Value?.Skills);
            Assert.GreaterOrEqual(response?.Value?.Skills.Count, 1);
        }

        [Test]
        public void TestSkillsMetadata()
        {
            GetMetadataResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.SkillsIntelligence.GetSkillsTaxonomyMetadata(); });
            Assert.NotNull(response.Value.ServiceVersion);
            Assert.NotNull(response.Value.TaxonomyReleaseDate);
        }

        [Test]
        public void TestProfessionsTaxonomy()
        {
            GetProfessionsTaxonomyResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.SkillsIntelligence.GetProfessionsTaxonomy("en", Models.API.DataEnrichment.TaxonomyFormat.json); });
            Assert.NotNull(response?.Value?.Professions);
            Assert.GreaterOrEqual(response?.Value?.Professions.Count, 1);
        }

        [Test]
        public void TestProfessionsMetadata()
        {
            GetMetadataResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.SkillsIntelligence.GetProfessionsTaxonomyMetadata(); });
            Assert.NotNull(response.Value.ServiceVersion);
            Assert.NotNull(response.Value.TaxonomyReleaseDate);
        }

        [Test]
        public void TestSkillsAutoComplete()
        {
            AutoCompleteSkillsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.SkillsIntelligence.AutocompleteSkill("soft", new List<string> { "en" }, "en", new List<string> { "all" } ); });
            Assert.NotNull(response?.Value?.Skills);
            Assert.GreaterOrEqual(response?.Value?.Skills.Count, 1);
        }

        [Test]
        public void TestSkillsAutoCompleteV2()
        {
            AutoCompleteSkillsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await ClientSNTV2.SkillsIntelligence.AutocompleteSkill("soft", new List<string> { "en" }, "en", new List<string> { "certification" }); });
            Assert.NotNull(response?.Value?.Skills);
            Assert.GreaterOrEqual(response?.Value?.Skills.Count, 1);
            Assert.AreEqual("Certification", response.Value.Skills.First().Type);
        }

        [Test]
        public void TestProfessionsAutoComplete()
        {
            AutoCompleteProfessionsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.SkillsIntelligence.AutocompleteProfession("soft"); });
            Assert.NotNull(response?.Value?.Professions);
            Assert.GreaterOrEqual(response?.Value?.Professions.Count, 1);
        }

        [Test]
        public void TestSkillsLookup()
        {
            LookupSkillCodesResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.SkillsIntelligence.LookupSkills(new List<string> { "KS120B874P2P6BK1MQ0T" }); });
            Assert.NotNull(response?.Value?.Skills);
            Assert.GreaterOrEqual(response?.Value?.Skills.Count, 1);
        }

        [Test]
        public void TestProfessionsLookup()
        {
            LookupProfessionCodesResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.SkillsIntelligence.LookupProfessions(new List<int> { 2000 }); });
            Assert.NotNull(response?.Value?.Professions);
            Assert.GreaterOrEqual(response?.Value?.Professions.Count, 1);
        }

        [Test]
        public void TestSkillsNormalize()
        {
            NormalizeSkillsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.SkillsIntelligence.NormalizeSkills(new List<string> { "Microsoft excel" }); });
            Assert.NotNull(response?.Value?.Skills);
            Assert.GreaterOrEqual(response?.Value?.Skills.Count, 1);
        }

        [Test]
        public void TestProfessionsNormalize()
        {
            NormalizeProfessionsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.SkillsIntelligence.NormalizeProfessions(new List<string> { "Software Engineer" }); });
            Assert.GreaterOrEqual(response?.Value?.Professions.Count, 1);
        }

        [Test]
        public void TestSkillsExtract()
        {
            ExtractSkillsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.SkillsIntelligence.ExtractSkills("Microsoft, developer python, software, clerical office assistant, excel"); });
            Assert.NotNull(response?.Value?.Skills);
            Assert.GreaterOrEqual(response?.Value?.Skills.Count, 1);
        }

        [Test]
        public void TestCompareProfessions()
        {
            CompareProfessionsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.SkillsIntelligence.CompareProfessions(696, 3178); });
            Assert.NotNull(response?.Value?.CommonSkills);
            Assert.GreaterOrEqual(response?.Value?.CommonSkills.Count, 1);
            Assert.NotNull(response?.Value?.ExclusiveSkillsByProfession);
            Assert.GreaterOrEqual(response?.Value?.ExclusiveSkillsByProfession.Count, 1);
        }

        [Test]
        public void TestCompareSkillsToProfession()
        {
            CompareSkillsToProfessionResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.SkillsIntelligence.CompareSkillsToProfession(696, "en",
                new SkillScore { Id = "KS120076FGP5WGWYMP0F" }, new SkillScore { Id = "KS04UWLJBN9X1M3N0PZ4" }); });
            Assert.NotNull(response?.Value?.CommonSkills);
            Assert.GreaterOrEqual(response?.Value?.CommonSkills.Count, 1);
        }

        [Test]
        public void TestSuggestSkills()
        {
            SuggestSkillsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.SkillsIntelligence.SuggestSkillsFromProfessions(new List<int> { 696 }, 5, "en"); });
            Assert.NotNull(response?.Value?.SuggestedSkills);
            Assert.GreaterOrEqual(response?.Value?.SuggestedSkills.Count, 1);
            Assert.NotNull(response?.Value?.SuggestedSkills?.FirstOrDefault()?.Description);
            Assert.IsNotEmpty(response?.Value?.SuggestedSkills?.FirstOrDefault()?.Description);
        }

        [Test]
        public void TestSuggestProfessions()
        {
            SuggestProfessionsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.SkillsIntelligence.SuggestProfessionsFromSkills(new List<SkillScore> 
            { 
                new SkillScore { Id = "KS120076FGP5WGWYMP0F" }, 
                new SkillScore { Id = "KS125HH5XDBPZT3RFGZZ" }, 
                new SkillScore { Id = "KS124PR62MV42B5C9S9F" } 
            }); });
            Assert.NotNull(response?.Value?.SuggestedProfessions);
            Assert.GreaterOrEqual(response?.Value?.SuggestedProfessions.Count, 1);
        }

        [Test]
        public void TestSuggestSkillsFromSkills()
        {
            SuggestSkillsResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.SkillsIntelligence.SuggestSkillsFromSkills(new List<SkillScore>
            {
                new SkillScore { Id = "KS120076FGP5WGWYMP0F" },
                new SkillScore { Id = "KS125HH5XDBPZT3RFGZZ" },
                new SkillScore { Id = "KS124PR62MV42B5C9S9F" }
            }, 10, "en"); });
            Assert.NotNull(response?.Value?.SuggestedSkills);
            Assert.GreaterOrEqual(response?.Value?.SuggestedSkills.Count, 1);
            Assert.NotNull(response?.Value?.SuggestedSkills?.FirstOrDefault()?.Description);
            Assert.IsNotEmpty(response?.Value?.SuggestedSkills?.FirstOrDefault()?.Description);
        }

        [Test]
        public void TestSkillsSimilarityScore()
        {
            SkillsSimilarityScoreResponse response = null;

            Assert.DoesNotThrowAsync(async () =>
            {
                response = await Client.SkillsIntelligence.SkillsSimilarityScore(
                    new List<SkillScore>
                    {
                        new SkillScore { Id = "KS120076FGP5WGWYMP0F" },
                        new SkillScore { Id = "KS125HH5XDBPZT3RFGZZ" },
                        new SkillScore { Id = "KS124PR62MV42B5C9S9F" }
                    },
                    new List<SkillScore>
                    {
                        new SkillScore { Id = "KS120076FGP5WGWYMP0F" },
                        new SkillScore { Id = "KS125HH5XDBPZT3RFGZZ" },
                        new SkillScore { Id = "KS124PR62MV42B5C9S9F" }
                    });
            });
            Assert.NotNull(response?.Value);
            Assert.Greater(response?.Value?.SimilarityScore, 0);
        }
    }
}


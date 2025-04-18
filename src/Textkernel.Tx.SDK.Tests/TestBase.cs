// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using NUnit.Framework;
using Textkernel.Tx.Models;
using Textkernel.Tx.Models.API;
using Textkernel.Tx.Models.API.BimetricScoring;
using Textkernel.Tx.Models.API.Geocoding;
using Textkernel.Tx.Models.API.Parsing;
using Textkernel.Tx.Models.Job;
using Textkernel.Tx.Models.Resume;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Textkernel.Tx.SDK.Tests
{
    public abstract class TestBase
    {
        protected static TxClient Client;
        protected static TxClient ClientSNTV2;
        protected static GeocodeCredentials GeocodeCredentials;

        protected static readonly ParsedResume TestParsedResume;
        protected static readonly ParsedResume TestParsedResumeV2;
        protected static readonly ParsedResume TestParsedResumeWithAddress;
        protected static readonly ParsedJob TestParsedJob;
        protected static readonly ParsedJob TestParsedJobWithAddress;
        protected static readonly ParsedJob TestParsedJobTech;
        protected static readonly ParsedJobWithId TestParsedJobWithId;
        protected static readonly ParsedResumeWithId TestParsedResumeWithId;

        public static DataCenter TestDataCenter = new DataCenter("https://api-acc.us.textkernel.com/tx/v10");

        internal class Credentials
        {
            public string AccountId { get; set; }
            public string ServiceKey { get; set; }
            public string GeocodeProviderKey { get; set; }
        }

        static TestBase()
        {
            Credentials data = JsonSerializer.Deserialize<Credentials>(File.ReadAllText("credentials.json"));

            GeocodeCredentials = new GeocodeCredentials()
            {
                Provider = GeocodeProvider.Google,
                ProviderKey = data.GeocodeProviderKey
            };

            Client = new TxClient(new System.Net.Http.HttpClient(), new TxClientSettings
            {
                AccountId = data.AccountId,
                ServiceKey = data.ServiceKey,
                DataCenter = TestDataCenter,
                SkillsIntelligenceIncludeCertifications = false,
                MatchV2Environment = Clients.MatchV2Environment.PROD
            });

            ClientSNTV2 = new TxClient(new System.Net.Http.HttpClient(), new TxClientSettings
            {
                AccountId = data.AccountId,
                ServiceKey = data.ServiceKey,
                DataCenter = TestDataCenter,
                SkillsIntelligenceIncludeCertifications = true
            });

            ParseResumeResponseValue parseResumeResponseValue = Client.Parser.ParseResume(new ParseRequest(TestData.Resume)).Result.Value;
            TestParsedResume = parseResumeResponseValue.ResumeData;

            parseResumeResponseValue = Client.Parser.ParseResume(new ParseRequest(TestData.Resume, new ParseOptions
            {
                ProfessionsSettings = new ProfessionsSettings { Normalize = true },
                SkillsSettings = new SkillsSettings
                {
                    Normalize = true,
                    TaxonomyVersion = "V2"
                }
            })).Result.Value;
            TestParsedResumeV2 = parseResumeResponseValue.ResumeData;

            parseResumeResponseValue = Client.Parser.ParseResume(new ParseRequest(TestData.ResumeWithAddress)).Result.Value;
            TestParsedResumeWithAddress = parseResumeResponseValue.ResumeData;

            ParseJobResponseValue parseJobResponseValue = Client.Parser.ParseJob(new ParseRequest(TestData.JobOrder)).Result.Value;
            TestParsedJob = parseJobResponseValue.JobData;

            parseJobResponseValue = Client.Parser.ParseJob(new ParseRequest(TestData.JobOrderWithAddress)).Result.Value;
            TestParsedJobWithAddress = parseJobResponseValue.JobData;

            parseJobResponseValue = Client.Parser.ParseJob(new ParseRequest(TestData.JobOrderTech)).Result.Value;
            TestParsedJobTech = parseJobResponseValue.JobData;

            TestParsedJobWithId = new ParsedJobWithId()
            {
                Id = "1",
                JobData = TestParsedJob
            };

            TestParsedResumeWithId = new ParsedResumeWithId()
            {
                Id = "1",
                ResumeData = TestParsedResume
            };
        }

        public async Task DelayForIndexSync(int ms = 1000)
        {
            await Task.Delay(ms);
        }

        public Document GetTestFileAsDocument(string filename)
        {
            return new Document("TestData/" + filename);
        }

        public void AssertDateNotNull(Models.TxDate date)
        {
            Assert.IsNotNull(date);
            Assert.IsNotNull(date.Date);
        }

        public void AssertDegreeNotNull(Models.Resume.Education.Degree degree)
        {
            Assert.IsNotNull(degree);
            Assert.IsNotNull(degree.Name);
            Assert.IsNotNull(degree.Name.Raw);
            Assert.IsNotNull(degree.Name.Normalized);
            Assert.IsNotNull(degree.NormalizedLocal);
            Assert.IsNotNull(degree.NormalizedInternational);
            Assert.IsNotNull(degree.NormalizedInternational.Code);
            Assert.IsNotNull(degree.NormalizedInternational.Description);
        }

        public void AssertLocationNotNull(Location loc, bool checkStreetLevel = false, bool checkGeo = false)
        {
            Assert.IsNotNull(loc);
            Assert.IsNotNull(loc.CountryCode);
            Assert.IsNotEmpty(loc.Regions);
            Assert.IsNotNull(loc.Municipality);

            if (checkStreetLevel)
            {
                Assert.IsNotNull(loc.PostalCode);
                Assert.IsNotEmpty(loc.StreetAddressLines);
            }

            if (checkGeo)
            {
                Assert.IsNotNull(loc.GeoCoordinates);
                Assert.NotZero(loc.GeoCoordinates.Latitude);
                Assert.NotZero(loc.GeoCoordinates.Longitude);
            }
        }

        /// <summary>
        /// This method is useful in finally blocks to cleanup indexes created for a test case
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public async Task CleanUpIndex(string indexName)
        {
            try
            {
                await Client.SearchMatch.DeleteIndex(indexName);
            }
            catch { }
        }
    }
}
// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using NUnit.Framework;
using Sovren.Models;
using Sovren.Models.API;
using Sovren.Models.API.BimetricScoring;
using Sovren.Models.API.Geocoding;
using Sovren.Models.API.Parsing;
using Sovren.Models.Job;
using Sovren.Models.Resume;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sovren.SDK.Tests
{
    public abstract class TestBase
    {
        protected static SovrenClient Client;
        protected static GeocodeCredentials GeocodeCredentials;

        protected static readonly ParsedResume TestParsedResume;
        protected static readonly ParsedResume TestParsedResumeWithAddress;
        protected static readonly ParsedJob TestParsedJob;
        protected static readonly ParsedJob TestParsedJobWithAddress;
        protected static readonly ParsedJob TestParsedJobTech;
        protected static readonly ParsedJobWithId TestParsedJobWithId;
        protected static readonly ParsedResumeWithId TestParsedResumeWithId;

        private class Credentials
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

            Client = new SovrenClient(data.AccountId, data.ServiceKey, new DataCenter("https://staging-rest.resumeparsing.com", "v10", true));

            ParseResumeResponseValue parseResumeResponseValue = Client.ParseResume(new ParseRequest(TestData.Resume)).Result.Value;
            TestParsedResume = parseResumeResponseValue.ResumeData;

            parseResumeResponseValue = Client.ParseResume(new ParseRequest(TestData.ResumeWithAddress)).Result.Value;
            TestParsedResumeWithAddress = parseResumeResponseValue.ResumeData;

            ParseJobResponseValue parseJobResponseValue = Client.ParseJob(new ParseRequest(TestData.JobOrder)).Result.Value;
            TestParsedJob = parseJobResponseValue.JobData;

            parseJobResponseValue = Client.ParseJob(new ParseRequest(TestData.JobOrderWithAddress)).Result.Value;
            TestParsedJobWithAddress = parseJobResponseValue.JobData;

            parseJobResponseValue = Client.ParseJob(new ParseRequest(TestData.JobOrderTech)).Result.Value;
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

        public async Task DelayForIndexSync()
        {
            await Task.Delay(1000);
        }

        public Document GetTestFileAsDocument(string filename)
        {
            return new Document("TestData/" + filename);
        }

        public void AssertDateNotNull(Models.SovrenDate date)
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
            Assert.IsNotNull(degree.Type);
            Assert.IsNotNull(degree.Type);
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
                await Client.DeleteIndex(indexName);
            }
            catch { }
        }
    }
}
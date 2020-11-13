// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using NUnit.Framework;
using Sovren.Models;
using Sovren.Models.API.Geocoding;
using Sovren.Models.API.Indexes;
using Sovren.Models.API.Parsing;
using Sovren.Models.Job;
using Sovren.Models.Resume;
using Sovren.Services;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sovren.SDK.Tests
{
    public abstract class TestBase
    {
        protected static SovrenClient Client;
        protected static ParsingService ParsingService;
        protected static AIMatchingService AIMatchingService;
        protected static BimetricScoringService BimetricScoringService;
        protected static IndexService IndexService;
        protected static GeocodingService GeocodingService;

        protected static ParsedResume TestParsedResume;
        protected static ParsedResume TestParsedResumeWithAddress;
        protected static ParsedJob TestParsedJob;
        protected static ParsedJob TestParsedJobWithAddress;

        private class Credentials
        {
            public string AccountId { get; set; }
            public string ServiceKey { get; set; }
            public string GeocodeProviderKey { get; set; }
        }

        static TestBase()
        {
            var data = JsonSerializer.Deserialize<Credentials>(File.ReadAllText("credentials.json"));
            Client = new SovrenClient(data.AccountId, data.ServiceKey, DataCenter.US); //new DataCenter("https://rest-local.sovren.com/v10/"));

            ParsingService = new ParsingService(Client, new ParseOptions() {
                OutputCandidateImage = true,
                OutputHtml = true,
                OutputPdf = true,
                OutputRtf = true
            });

            AIMatchingService = new AIMatchingService(Client);
            BimetricScoringService = new BimetricScoringService(Client);
            IndexService = new IndexService(Client);

            GeocodeCredentials geocodeCredentials = new GeocodeCredentials()
            {
                Provider = GeocodeProvider.Google,
                ProviderKey = data.GeocodeProviderKey
            };

            GeocodingService = new GeocodingService(Client, geocodeCredentials);

            ParseResumeResponseValue parseResumeResponseValue = ParsingService.ParseResume(TestData.Resume).Result;
            TestParsedResume = parseResumeResponseValue.ResumeData;

            parseResumeResponseValue = ParsingService.ParseResume(TestData.ResumeWithAddress).Result;
            TestParsedResumeWithAddress = parseResumeResponseValue.ResumeData;

            ParseJobResponseValue parseJobResponseValue = ParsingService.ParseJob(TestData.JobOrder).Result;
            TestParsedJob = parseJobResponseValue.JobData;

            parseJobResponseValue = ParsingService.ParseJob(TestData.JobOrderWithAddress).Result;
            TestParsedJobWithAddress = parseJobResponseValue.JobData;
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
                await IndexService.DeleteIndex(indexName);
            }
            catch { }
        }
    }
}
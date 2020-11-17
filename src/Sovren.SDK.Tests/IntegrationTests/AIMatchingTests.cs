using NUnit.Framework;
using Sovren.Models.API.Matching;
using Sovren.Models.API.Matching.Request;
using Sovren.Models.API.Matching.UI;
using Sovren.Models.Matching;
using Sovren.Rest;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sovren.SDK.Tests.IntegrationTests
{
    public class AIMatchingTests : TestBase
    {
        private const string _jobIndexId = "SDK-job-" + nameof(AIMatchingTests);
        private const string _resumeIndexId = "SDK-resume-" + nameof(AIMatchingTests);
        private const string _documentId = "1";

        private List<string> _resumesIndexes = new List<string>() { _resumeIndexId };
        private List<string> _jobsIndexes = new List<string>() { _jobIndexId };


        [OneTimeSetUp]
        public async Task SetupAIMatchingIndexes()
        {
            // create indexes
            await Client.CreateIndex(IndexType.Job, _jobIndexId);
            await Client.CreateIndex(IndexType.Resume, _resumeIndexId);
            await DelayForIndexSync();

            // add a document to each index
            await Client.AddDocumentToIndex(TestParsedJobTech, _jobIndexId, _documentId);
            await Client.AddDocumentToIndex(TestParsedResume, _resumeIndexId, _documentId);
            await DelayForIndexSync();
        }

        [OneTimeTearDown]
        public async Task TeardownMatchingIndexes()
        {
            await CleanUpIndex(_jobIndexId);
            await CleanUpIndex(_resumeIndexId);
        }

        [TestCase(_jobIndexId, "Developer")]
        [TestCase(_resumeIndexId, "Javascript")]
        public async Task TestSearch(string indexId, string validSearchTerm)
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await Client.Search(null, null);
            });

            List<string> indexesToQuery = new List<string>() { indexId };
            Assert.ThrowsAsync<SovrenException>(async () =>
            {
                await Client.Search(indexesToQuery, null);
            });

            FilterCriteria filterCritera = new FilterCriteria();
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await Client.Search(null, filterCritera);
            });

            Assert.ThrowsAsync<SovrenException>(async () =>
            {
                await Client.Search(new List<string>() { "fake-index-id" }, filterCritera);
            });

            Assert.ThrowsAsync<SovrenException>(async () =>
            {
                await Client.Search(indexesToQuery, filterCritera);
            });

            filterCritera.SearchExpression = validSearchTerm;
            Assert.DoesNotThrowAsync(async () =>
            {
                SearchResponseValue response = Client.Search(indexesToQuery, filterCritera).Result.Value;
                Assert.AreEqual(1, response.CurrentCount);
                Assert.AreEqual(1, response.TotalCount);
            });

            filterCritera.SearchExpression = "ThisIsATermThatIsntInTheDocument";
            Assert.DoesNotThrowAsync(async () =>
            {
                SearchResponseValue response = Client.Search(indexesToQuery, filterCritera).Result.Value;
                Assert.AreEqual(0, response.CurrentCount);
                Assert.AreEqual(0, response.TotalCount);
            });
        }

        [Test]
        public async Task TestMatchJob()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await Client.MatchJob(null, null);
            });

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await Client.MatchJob(TestParsedJobTech, null);
            });

            Assert.ThrowsAsync<SovrenException>(async () =>
            {
                await Client.MatchJob(null, _resumesIndexes);
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                MatchResponseValue matchResponse = Client.MatchJob(TestParsedJobTech, _jobsIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                MatchResponseValue matchResponse = Client.MatchJob(TestParsedJobTech, _resumesIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });
        }

        [Test]
        public async Task TestMatchResume()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await Client.MatchResume(null, null);
            });

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await Client.MatchResume(TestParsedResume, null);
            });

            Assert.ThrowsAsync<SovrenException>(async () =>
            {
                await Client.MatchResume(null, _resumesIndexes);
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                MatchResponseValue matchResponse = Client.MatchResume(TestParsedResume, _jobsIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                MatchResponseValue matchResponse = Client.MatchResume(TestParsedResume, _resumesIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });
        }

        [Test]
        public async Task TestMatchIndexedDocument()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await Client.MatchIndexedDocument(null, null, null);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.MatchIndexedDocument(null, _documentId, _resumesIndexes);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.MatchIndexedDocument("", _documentId, _resumesIndexes);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.MatchIndexedDocument(" ", _documentId, _resumesIndexes);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.MatchIndexedDocument(_resumeIndexId, null, _resumesIndexes); ;
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.MatchIndexedDocument(_resumeIndexId, "", _resumesIndexes); ;
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.MatchIndexedDocument(_resumeIndexId, " ", _resumesIndexes); ;
            });

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await Client.MatchIndexedDocument(_resumeIndexId, _documentId, null); ;
            });

            Assert.ThrowsAsync<SovrenException>(async () =>
            {
                await Client.MatchIndexedDocument(_resumeIndexId, _documentId, new List<string>()); ;
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                MatchResponseValue matchResponse = Client.MatchIndexedDocument(_resumeIndexId, _documentId, _resumesIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                MatchResponseValue matchResponse = Client.MatchIndexedDocument(_resumeIndexId, _documentId, _jobsIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                MatchResponseValue matchResponse = Client.MatchIndexedDocument(_jobIndexId, _documentId, _resumesIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                MatchResponseValue matchResponse = Client.MatchIndexedDocument(_jobIndexId, _documentId, _jobsIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });
        }

        [Test]
        public async Task TestMatchUISearch()
        {
            GenerateUIResponse uiResponse = null;

            Assert.ThrowsAsync<ArgumentNullException>(async () => {
                await Client.UI().Search(null, null);
            });

            Assert.ThrowsAsync<SovrenException>(async () => {
                await Client.UI().Search(new List<string>(), null);
            });

            Assert.DoesNotThrowAsync(async () => {
                uiResponse = await Client.UI().Search(_resumesIndexes, null);
            });
            
            Assert.That(await DoesURLExist(uiResponse.URL));
        }

        [Test]
        public async Task TestMatchUIMatchJob()
        {
            GenerateUIResponse uiResponse = null;

            Assert.ThrowsAsync<ArgumentNullException>(async () => {
                await Client.UI().MatchJob(null, null);
            });

            Assert.ThrowsAsync<ArgumentNullException>(async () => {
                await Client.UI().MatchJob(TestParsedJobTech, null);
            });

            Assert.ThrowsAsync<SovrenException>(async () => {
                await Client.UI().MatchJob(null, _resumesIndexes);
            });

            Assert.DoesNotThrowAsync(async () => {
                uiResponse = await Client.UI().MatchJob(TestParsedJobTech, _resumesIndexes);
            });

            Assert.That(await DoesURLExist(uiResponse.URL));

            Assert.DoesNotThrowAsync(async () => {
                uiResponse = await Client.UI().MatchJob(TestParsedJobTech, _jobsIndexes);
            });

            Assert.That(await DoesURLExist(uiResponse.URL));
        }

        [Test]
        public async Task TestMatchUIMatchResume()
        {
            GenerateUIResponse uiResponse = null;

            Assert.ThrowsAsync<ArgumentNullException>(async () => {
                await Client.UI().MatchResume(null, null);
            });

            Assert.ThrowsAsync<ArgumentNullException>(async () => {
                await Client.UI().MatchResume(TestParsedResume, null);
            });

            Assert.ThrowsAsync<SovrenException>(async () => {
                await Client.UI().MatchResume(null, _resumesIndexes);
            });

            Assert.DoesNotThrowAsync(async () => {
                uiResponse = await Client.UI().MatchResume(TestParsedResume, _resumesIndexes);
                Assert.That(await DoesURLExist(uiResponse.URL));
            });

            Assert.DoesNotThrowAsync(async () => {
                uiResponse = await Client.UI().MatchResume(TestParsedResume, _jobsIndexes);
                Assert.That(await DoesURLExist(uiResponse.URL));
            });
        }

        [Test]
        public async Task TestMatchUIMatchIndexedDocument()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await Client.UI().MatchIndexedDocument(null, null, null);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.UI().MatchIndexedDocument(null, _documentId, _resumesIndexes);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.UI().MatchIndexedDocument("", _documentId, _resumesIndexes);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.UI().MatchIndexedDocument(" ", _documentId, _resumesIndexes);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.UI().MatchIndexedDocument(_resumeIndexId, null, _resumesIndexes); ;
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.UI().MatchIndexedDocument(_resumeIndexId, "", _resumesIndexes); ;
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.UI().MatchIndexedDocument(_resumeIndexId, " ", _resumesIndexes); ;
            });

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await Client.UI().MatchIndexedDocument(_resumeIndexId, _documentId, null); ;
            });

            Assert.ThrowsAsync<SovrenException>(async () =>
            {
                await Client.UI().MatchIndexedDocument(_resumeIndexId, _documentId, new List<string>()); ;
            });

            GenerateUIResponse uiResponse = null;

            Assert.DoesNotThrowAsync(async () =>
            {
                uiResponse = await Client.UI().MatchIndexedDocument(_resumeIndexId, _documentId, _resumesIndexes); ;
                Assert.That(await DoesURLExist(uiResponse.URL));
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                uiResponse = await Client.UI().MatchIndexedDocument(_resumeIndexId, _documentId, _jobsIndexes); ;
                Assert.That(await DoesURLExist(uiResponse.URL));
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                uiResponse = await Client.UI().MatchIndexedDocument(_jobIndexId, _documentId, _resumesIndexes); ;
                Assert.That(await DoesURLExist(uiResponse.URL));
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                uiResponse = await Client.UI().MatchIndexedDocument(_jobIndexId, _documentId, _jobsIndexes); ;
                Assert.That(await DoesURLExist(uiResponse.URL));
            });
        }

        private async Task<bool> DoesURLExist(string url)
        {
            RestResponse response = await new RestClient(url).ExecuteAsync(new RestRequest(RestMethod.GET));
            return response.IsSuccessful;
        }
    }
}

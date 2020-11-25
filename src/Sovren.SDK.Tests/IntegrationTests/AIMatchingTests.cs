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

        private readonly List<string> _resumesIndexes = new List<string>() { _resumeIndexId };
        private readonly List<string> _jobsIndexes = new List<string>() { _jobIndexId };


        [OneTimeSetUp]
        public async Task SetupAIMatchingIndexes()
        {
            // create indexes
            await Client.CreateIndex(IndexType.Job, _jobIndexId);
            await Client.CreateIndex(IndexType.Resume, _resumeIndexId);
            await DelayForIndexSync();

            // add a document to each index
            await Client.IndexDocument(TestParsedJobTech, _jobIndexId, _documentId);
            await Client.IndexDocument(TestParsedResume, _resumeIndexId, _documentId);
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
            Assert.DoesNotThrow(() =>
            {
                SearchResponseValue response = Client.Search(indexesToQuery, filterCritera).Result.Value;
                Assert.AreEqual(1, response.CurrentCount);
                Assert.AreEqual(1, response.TotalCount);
            });

            filterCritera.SearchExpression = "ThisIsATermThatIsntInTheDocument";
            Assert.DoesNotThrow(() =>
            {
                SearchResponseValue response = Client.Search(indexesToQuery, filterCritera).Result.Value;
                Assert.AreEqual(0, response.CurrentCount);
                Assert.AreEqual(0, response.TotalCount);
            });

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestMatchJob()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await Client.Match(TestParsedJobTech, null);
            });

            Assert.DoesNotThrow(() =>
            {
                MatchResponseValue matchResponse = Client.Match(TestParsedJobTech, _jobsIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            Assert.DoesNotThrow(() =>
            {
                MatchResponseValue matchResponse = Client.Match(TestParsedJobTech, _resumesIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestMatchResume()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await Client.Match(TestParsedResume, null);
            });

            Assert.DoesNotThrow(() =>
            {
                MatchResponseValue matchResponse = Client.Match(TestParsedResume, _jobsIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            Assert.DoesNotThrow(() =>
            {
                MatchResponseValue matchResponse = Client.Match(TestParsedResume, _resumesIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestMatchIndexedDocument()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await Client.Match("", null, null);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.Match(null, _documentId, _resumesIndexes);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.Match("", _documentId, _resumesIndexes);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.Match(" ", _documentId, _resumesIndexes);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.Match(_resumeIndexId, null, _resumesIndexes); ;
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.Match(_resumeIndexId, "", _resumesIndexes); ;
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.Match(_resumeIndexId, " ", _resumesIndexes); ;
            });

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await Client.Match(_resumeIndexId, _documentId, null); ;
            });

            Assert.ThrowsAsync<SovrenException>(async () =>
            {
                await Client.Match(_resumeIndexId, _documentId, new List<string>()); ;
            });

            Assert.DoesNotThrow(() =>
            {
                MatchResponseValue matchResponse = Client.Match(_resumeIndexId, _documentId, _resumesIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            Assert.DoesNotThrow(() =>
            {
                MatchResponseValue matchResponse = Client.Match(_resumeIndexId, _documentId, _jobsIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            Assert.DoesNotThrow(() =>
            {
                MatchResponseValue matchResponse = Client.Match(_jobIndexId, _documentId, _resumesIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            Assert.DoesNotThrow(() =>
            {
                MatchResponseValue matchResponse = Client.Match(_jobIndexId, _documentId, _jobsIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            await Task.CompletedTask;
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
                await Client.UI().Match(TestParsedJobTech, null);
            });

            Assert.DoesNotThrowAsync(async () => {
                uiResponse = await Client.UI().Match(TestParsedJobTech, _resumesIndexes);
            });

            Assert.That(await DoesURLExist(uiResponse.URL));

            Assert.DoesNotThrowAsync(async () => {
                uiResponse = await Client.UI().Match(TestParsedJobTech, _jobsIndexes);
            });

            Assert.That(await DoesURLExist(uiResponse.URL));
        }

        [Test]
        public async Task TestMatchUIMatchResume()
        {
            GenerateUIResponse uiResponse = null;

            Assert.ThrowsAsync<ArgumentNullException>(async () => {
                await Client.UI().Match(TestParsedResume, null);
            });

            Assert.DoesNotThrowAsync(async () => {
                uiResponse = await Client.UI().Match(TestParsedResume, _resumesIndexes);
                Assert.That(await DoesURLExist(uiResponse.URL));
            });

            Assert.DoesNotThrowAsync(async () => {
                uiResponse = await Client.UI().Match(TestParsedResume, _jobsIndexes);
                Assert.That(await DoesURLExist(uiResponse.URL));
            });

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestMatchUIMatchIndexedDocument()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await Client.UI().Match("", null, null);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.UI().Match(null, _documentId, _resumesIndexes);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.UI().Match("", _documentId, _resumesIndexes);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.UI().Match(" ", _documentId, _resumesIndexes);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.UI().Match(_resumeIndexId, null, _resumesIndexes); ;
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.UI().Match(_resumeIndexId, "", _resumesIndexes); ;
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.UI().Match(_resumeIndexId, " ", _resumesIndexes); ;
            });

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await Client.UI().Match(_resumeIndexId, _documentId, null); ;
            });

            Assert.ThrowsAsync<SovrenException>(async () =>
            {
                await Client.UI().Match(_resumeIndexId, _documentId, new List<string>()); ;
            });

            GenerateUIResponse uiResponse = null;

            Assert.DoesNotThrowAsync(async () =>
            {
                uiResponse = await Client.UI().Match(_resumeIndexId, _documentId, _resumesIndexes); ;
                Assert.That(await DoesURLExist(uiResponse.URL));
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                uiResponse = await Client.UI().Match(_resumeIndexId, _documentId, _jobsIndexes); ;
                Assert.That(await DoesURLExist(uiResponse.URL));
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                uiResponse = await Client.UI().Match(_jobIndexId, _documentId, _resumesIndexes); ;
                Assert.That(await DoesURLExist(uiResponse.URL));
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                uiResponse = await Client.UI().Match(_jobIndexId, _documentId, _jobsIndexes); ;
                Assert.That(await DoesURLExist(uiResponse.URL));
            });

            await Task.CompletedTask;
        }

        private async Task<bool> DoesURLExist(string url)
        {
            RestResponse response = await new RestClient(url).ExecuteAsync<object>(new RestRequest(RestMethod.GET));
            return response.IsSuccessful;
        }
    }
}

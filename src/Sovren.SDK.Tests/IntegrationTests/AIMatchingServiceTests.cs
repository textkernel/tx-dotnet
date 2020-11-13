using NUnit.Framework;
using Sovren.Models.API.Matching;
using Sovren.Models.API.Matching.Request;
using Sovren.Models.Matching;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sovren.SDK.Tests.IntegrationTests
{
    public class AIMatchingServiceTests : TestBase
    {
        private const string _jobIndexId = "SDK-job-" + nameof(AIMatchingServiceTests);
        private const string _resumeIndexId = "SDK-resume-" + nameof(AIMatchingServiceTests);
        private const string _documentId = "1";

        private List<string> _resumesIndexes = new List<string>() { _resumeIndexId };
        private List<string> _jobsIndexes = new List<string>() { _jobIndexId };


        [OneTimeSetUp]
        public async Task SetupAIMatchingIndexes()
        {
            // create indexes
            await IndexService.CreateIndex(IndexType.Job, _jobIndexId);
            await IndexService.CreateIndex(IndexType.Resume, _resumeIndexId);
            await DelayForIndexSync();

            // add a document to each index
            await IndexService.AddDocumentToIndex(TestParsedJobTech, _jobIndexId, _documentId);
            await IndexService.AddDocumentToIndex(TestParsedResume, _resumeIndexId, _documentId);
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
                await AIMatchingService.Search(null, null);
            });

            List<string> indexesToQuery = new List<string>() { indexId };
            Assert.ThrowsAsync<SovrenException>(async () =>
            {
                await AIMatchingService.Search(indexesToQuery, null);
            });

            FilterCriteria filterCritera = new FilterCriteria();
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await AIMatchingService.Search(null, filterCritera);
            });

            Assert.ThrowsAsync<SovrenException>(async () =>
            {
                await AIMatchingService.Search(new List<string>() { "fake-index-id" }, filterCritera);
            });

            Assert.ThrowsAsync<SovrenException>(async () =>
            {
                await AIMatchingService.Search(indexesToQuery, filterCritera);
            });

            filterCritera.SearchExpression = validSearchTerm;
            Assert.DoesNotThrowAsync(async () =>
            {
                SearchResponseValue response = await AIMatchingService.Search(indexesToQuery, filterCritera);
                Assert.AreEqual(1, response.CurrentCount);
                Assert.AreEqual(1, response.TotalCount);
            });

            filterCritera.SearchExpression = "ThisIsATermThatIsntInTheDocument";
            Assert.DoesNotThrowAsync(async () =>
            {
                SearchResponseValue response = await AIMatchingService.Search(indexesToQuery, filterCritera);
                Assert.AreEqual(0, response.CurrentCount);
                Assert.AreEqual(0, response.TotalCount);
            });
        }

        [Test]
        public async Task TestMatchJob()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await AIMatchingService.MatchJob(null, null);
            });

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await AIMatchingService.MatchJob(TestParsedJobTech, null);
            });

            Assert.ThrowsAsync<SovrenException>(async () =>
            {
                await AIMatchingService.MatchJob(null, _resumesIndexes);
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                MatchResponseValue matchResponse = await AIMatchingService.MatchJob(TestParsedJobTech, _jobsIndexes);
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                MatchResponseValue matchResponse = await AIMatchingService.MatchJob(TestParsedJobTech, _resumesIndexes);
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
                await AIMatchingService.MatchResume(null, null);
            });

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await AIMatchingService.MatchResume(TestParsedResume, null);
            });

            Assert.ThrowsAsync<SovrenException>(async () =>
            {
                await AIMatchingService.MatchResume(null, _resumesIndexes);
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                MatchResponseValue matchResponse = await AIMatchingService.MatchResume(TestParsedResume, _jobsIndexes);
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                MatchResponseValue matchResponse = await AIMatchingService.MatchResume(TestParsedResume, _resumesIndexes);
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });
        }

        // test match by doc id
        [Test]
        public async Task TestMatchIndexedDocument()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await AIMatchingService.MatchIndexedDocument(null, null, null);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await AIMatchingService.MatchIndexedDocument(null, _documentId, _resumesIndexes);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await AIMatchingService.MatchIndexedDocument("", _documentId, _resumesIndexes);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await AIMatchingService.MatchIndexedDocument(" ", _documentId, _resumesIndexes);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await AIMatchingService.MatchIndexedDocument(_resumeIndexId, null, _resumesIndexes); ;
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await AIMatchingService.MatchIndexedDocument(_resumeIndexId, "", _resumesIndexes); ;
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await AIMatchingService.MatchIndexedDocument(_resumeIndexId, " ", _resumesIndexes); ;
            });

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await AIMatchingService.MatchIndexedDocument(_resumeIndexId, _documentId, null); ;
            });

            Assert.ThrowsAsync<SovrenException>(async () =>
            {
                await AIMatchingService.MatchIndexedDocument(_resumeIndexId, _documentId, new List<string>()); ;
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                MatchResponseValue matchResponse = await AIMatchingService.MatchIndexedDocument(_resumeIndexId, _documentId, _resumesIndexes); ;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                MatchResponseValue matchResponse = await AIMatchingService.MatchIndexedDocument(_resumeIndexId, _documentId, _jobsIndexes); ;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                MatchResponseValue matchResponse = await AIMatchingService.MatchIndexedDocument(_jobIndexId, _documentId, _resumesIndexes); ;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            Assert.DoesNotThrowAsync(async () =>
            {
                MatchResponseValue matchResponse = await AIMatchingService.MatchIndexedDocument(_jobIndexId, _documentId, _jobsIndexes); ;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });
        }
    }
}

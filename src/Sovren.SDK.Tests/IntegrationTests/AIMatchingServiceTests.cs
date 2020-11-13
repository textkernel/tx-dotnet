using NUnit.Framework;
using Sovren.Models.Matching;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sovren.SDK.Tests.IntegrationTests
{
    public class AIMatchingServiceTests : TestBase
    {
        string jobIndexId = "SDK-job-" + nameof(AIMatchingServiceTests);
        string resumeIndexId = "SDK-resume-" + nameof(AIMatchingServiceTests);
        string documentId = "1";

        [OneTimeSetUp]
        public async Task SetupAIMatchingIndexes()
        {
            // create indexes
            await IndexService.CreateIndex(IndexType.Job, jobIndexId);
            await IndexService.CreateIndex(IndexType.Resume, resumeIndexId);
            await DelayForIndexSync();

            // add a document to each index
            await IndexService.AddDocumentToIndex(TestParsedJob, jobIndexId, documentId);
            await IndexService.AddDocumentToIndex(TestParsedResume, resumeIndexId, documentId);
            await DelayForIndexSync();
        }

        [OneTimeTearDown]
        public async Task TeardownMatchingIndexes()
        {
            await CleanUpIndex(jobIndexId);
            await CleanUpIndex(resumeIndexId);
        }

        [Test]
        public async Task TestSearch()
        {
            Assert.ThrowsAsync<SovrenException>(async () =>
            {
                await AIMatchingService.Search(null, null);
            });

            List<string> indexesToQuery = new List<string>() { jobIndexId };
            Assert.ThrowsAsync<SovrenException>(async () =>
            {
                await AIMatchingService.Search(indexesToQuery, null);
            });
        }

        // test match by job
        // test match by resume
        // test match by doc id
    }
}

using NUnit.Framework;
using Sovren.Models.Matching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Collections;
using Sovren.Models.API.Matching.Request;
using Sovren.Models.API.Matching;
using Sovren.Models.API.Indexes;
using Sovren.Models.Resume;
using Sovren.Models.Job;
using Newtonsoft.Json;

namespace Sovren.SDK.Tests.IntegrationTests
{
    public class IndexServiceTests : TestBase
    {
        const string resumeIndexId = "SDK-IntegrationTest-Resume";
        const string jobIndexId = "SDK-IntegrationTest-Job";

        private static string GetIndexName(IndexType indexType)
        {
            return indexType == IndexType.Resume ? resumeIndexId : jobIndexId;
        }

        [Test]
        public async Task TestGetAccount()
        {
            await TestGetAccount(IndexService);
        }

        public static IEnumerable BadIndexNames
        {
            get
            {
                yield return new TestCaseData("invalid=index");
                yield return new TestCaseData(null);
                yield return new TestCaseData("");
                yield return new TestCaseData(" ");
            }
        }

        [TestCaseSource(typeof(IndexServiceTests), nameof(BadIndexNames))]

        public async Task TestCreateIndexBadInput(string indexName)
        {
            // validate can't create bad index name
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await IndexService.CreateIndex(IndexType.Job, indexName);
            });

            exception = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await IndexService.CreateIndex(IndexType.Resume, indexName);
            });
        }

        [TestCaseSource(typeof(IndexServiceTests), nameof(BadIndexNames))]
        public async Task TestDeleteIndexBadInput(string indexName)
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await IndexService.DeleteIndex(indexName);
            });
        }

        [TestCase(IndexType.Resume)]
        [TestCase(IndexType.Job)]
        public async Task TestIndexLifecycle(IndexType indexType)
        {
            string indexName = GetIndexName(indexType);

            try
            {
                // verify index doesn't exist
                List<Index> indexes = await IndexService.GetAllIndexes();
                Assert.False(await DoesIndexExist(indexName));

                // create index
                Assert.DoesNotThrowAsync(async () =>
                {
                    await IndexService.CreateIndex(indexType, indexName);
                });

                await DelayForIndexSync();

                // create index already exists
                SovrenException sovrenException = Assert.ThrowsAsync<SovrenException>(async () =>
                {
                    await IndexService.CreateIndex(indexType, indexName);
                });
                Assert.AreEqual(SovrenErrorCodes.DuplicateAsset, sovrenException.SovrenErrorCode);

                // verify index created
                Assert.True(await DoesIndexExist(indexName));

                // delete the index
                Assert.DoesNotThrowAsync(async () =>
                {
                    await IndexService.DeleteIndex(indexName);
                });

                await DelayForIndexSync();

                // verify index doesn't exist
                Assert.False(await DoesIndexExist(indexName));

                // try to delete an index that doesn't exist
                sovrenException = Assert.ThrowsAsync<SovrenException>(async () =>
                {
                    await IndexService.DeleteIndex(indexName);
                });
                Assert.AreEqual(SovrenErrorCodes.DataNotFound, sovrenException.SovrenErrorCode);
            }
            finally
            {
                // clean up assets in case the test failed someone before the delete calls were executed
                await CleanUpIndex(indexName);
                await DelayForIndexSync();
            }
        }

        [Test]
        public async Task TestResumeLifeCycle()
        {
            const string documentId = "1";
            try
            {
                // verify can't retrieve a document that doesn't exist
                SovrenException sovrenException = Assert.ThrowsAsync<SovrenException>(async () => {
                    await IndexService.GetResume(resumeIndexId, documentId);
                });
                Assert.AreEqual(SovrenErrorCodes.DataNotFound, SovrenErrorCodes.DataNotFound);

                // verify can't add document to an index that doesn't exist
                sovrenException = Assert.ThrowsAsync<SovrenException>(async () => {
                    await IndexService.AddDocumentToIndex(TestParsedResume, resumeIndexId, documentId);
                });
                Assert.AreEqual(SovrenErrorCodes.DataNotFound, SovrenErrorCodes.DataNotFound);

                // create the index
                await IndexService.CreateIndex(IndexType.Resume, resumeIndexId);
                await DelayForIndexSync();

                // verify document still doesn't exist
                sovrenException = Assert.ThrowsAsync<SovrenException>(async () => {
                    await IndexService.GetResume(resumeIndexId, documentId);
                });
                Assert.AreEqual(SovrenErrorCodes.DataNotFound, SovrenErrorCodes.DataNotFound);

                // add resume to index
                await IndexService.AddDocumentToIndex(TestParsedResume, resumeIndexId, documentId);
                await DelayForIndexSync();

                // confirm you can now retrieve the resume
                await IndexService.GetResume(resumeIndexId, documentId);
                
                // confirm the resume shows up in searches
                List<string> indexesToQuery = new List<string>() { resumeIndexId };
                FilterCriteria filterCriteria = new FilterCriteria()
                {
                    DocumentIds = new List<string>() { documentId }
                };

                SearchResponseValue searchResponse = await AIMatchingService.Search(indexesToQuery, filterCriteria);
                Assert.AreEqual(1, searchResponse.TotalCount);
                Assert.AreEqual(1, searchResponse.CurrentCount);
                Assert.AreEqual(documentId, searchResponse.Matches[0].Id);

                // update the resume
                List<string> userDefinedTags = new List<string> { "userDefinedTag1" };
                await IndexService.UpdateResumeUserDefinedTags(resumeIndexId, documentId,
                    userDefinedTags, UserDefinedTagsMethod.Overwrite);

                await DelayForIndexSync();

                // verify those updates have taken effect
                filterCriteria.UserDefinedTags = userDefinedTags;
                searchResponse = await AIMatchingService.Search(indexesToQuery, filterCriteria);
                Assert.AreEqual(1, searchResponse.TotalCount);
                Assert.AreEqual(1, searchResponse.CurrentCount);
                Assert.AreEqual(documentId, searchResponse.Matches[0].Id);

                // confirm you can retrieve the tags
                ParsedResume resume = await IndexService.GetResume(resumeIndexId, documentId);
                Assert.AreEqual(1, resume.UserDefinedTags.Count);
                Assert.AreEqual(userDefinedTags[0], resume.UserDefinedTags[0]);

                // delete the document
                await IndexService.DeleteDocumentFromIndex(resumeIndexId, documentId);
                await DelayForIndexSync();

                // verify can't retrieve a document that doesn't exist
                sovrenException = Assert.ThrowsAsync<SovrenException>(async () => {
                    await IndexService.GetResume(resumeIndexId, documentId);
                });
                Assert.AreEqual(SovrenErrorCodes.DataNotFound, sovrenException.SovrenErrorCode);

                sovrenException = Assert.ThrowsAsync<SovrenException>(async () => {
                    await IndexService.DeleteDocumentFromIndex(resumeIndexId, documentId);
                });
                Assert.AreEqual(SovrenErrorCodes.DataNotFound, sovrenException.SovrenErrorCode);

                await IndexService.DeleteIndex(resumeIndexId);
                await DelayForIndexSync();

                sovrenException = Assert.ThrowsAsync<SovrenException>(async () => {
                    await IndexService.DeleteDocumentFromIndex(resumeIndexId, documentId);
                });
                Assert.AreEqual(SovrenErrorCodes.DataNotFound, SovrenErrorCodes.DataNotFound);
            }
            catch(Exception ex) { throw; }
            finally
            {
                await CleanUpIndex(resumeIndexId);
            }            
        }

        [Test]
        public async Task TestJobLifeCycle()
        {
            const string documentId = "1";
            try
            {
                // verify can't retrieve a document that doesn't exist
                SovrenException sovrenException = Assert.ThrowsAsync<SovrenException>(async () => {
                    await IndexService.GetJob(jobIndexId, documentId);
                });
                Assert.AreEqual(SovrenErrorCodes.DataNotFound, SovrenErrorCodes.DataNotFound);

                // verify can't add document to an index that doesn't exist
                sovrenException = Assert.ThrowsAsync<SovrenException>(async () => {
                    await IndexService.AddDocumentToIndex(TestParsedJob, jobIndexId, documentId);
                });
                Assert.AreEqual(SovrenErrorCodes.DataNotFound, SovrenErrorCodes.DataNotFound);

                // create the index
                await IndexService.CreateIndex(IndexType.Job, jobIndexId);
                await DelayForIndexSync();

                // verify document still doesn't exist
                sovrenException = Assert.ThrowsAsync<SovrenException>(async () => {
                    await IndexService.GetJob(jobIndexId, documentId);
                });
                Assert.AreEqual(SovrenErrorCodes.DataNotFound, SovrenErrorCodes.DataNotFound);

                // add resume to index
                await IndexService.AddDocumentToIndex(TestParsedJob, jobIndexId, documentId);
                await DelayForIndexSync();

                // confirm you can now retrieve the resume
                await IndexService.GetJob(jobIndexId, documentId);

                // confirm the resume shows up in searches
                List<string> indexesToQuery = new List<string>() { jobIndexId };
                FilterCriteria filterCriteria = new FilterCriteria()
                {
                    DocumentIds = new List<string>() { documentId }
                };

                SearchResponseValue searchResponse = await AIMatchingService.Search(indexesToQuery, filterCriteria);
                Assert.AreEqual(1, searchResponse.TotalCount);
                Assert.AreEqual(1, searchResponse.CurrentCount);
                Assert.AreEqual(documentId, searchResponse.Matches[0].Id);

                // update the resume
                List<string> userDefinedTags = new List<string> { "userDefinedTag1" };
                await IndexService.UpdateJobUserDefinedTags(jobIndexId, documentId,
                    userDefinedTags, UserDefinedTagsMethod.Overwrite);

                await DelayForIndexSync();

                // verify those updates have taken effect
                filterCriteria.UserDefinedTags = userDefinedTags;
                searchResponse = await AIMatchingService.Search(indexesToQuery, filterCriteria);
                Assert.AreEqual(1, searchResponse.TotalCount);
                Assert.AreEqual(1, searchResponse.CurrentCount);
                Assert.AreEqual(documentId, searchResponse.Matches[0].Id);

                // confirm you can retrieve the tags
                ParsedJob job = await IndexService.GetJob(jobIndexId, documentId);
                Assert.AreEqual(1, job.UserDefinedTags.Count);
                Assert.AreEqual(userDefinedTags[0], job.UserDefinedTags[0]);

                // delete the document
                await IndexService.DeleteDocumentFromIndex(jobIndexId, documentId);
                await DelayForIndexSync();

                // verify can't retrieve a document that doesn't exist
                sovrenException = Assert.ThrowsAsync<SovrenException>(async () => {
                    await IndexService.GetResume(jobIndexId, documentId);
                });
                Assert.AreEqual(SovrenErrorCodes.DataNotFound, SovrenErrorCodes.DataNotFound);

                sovrenException = Assert.ThrowsAsync<SovrenException>(async () => {
                    await IndexService.DeleteDocumentFromIndex(jobIndexId, documentId);
                });
                Assert.AreEqual(SovrenErrorCodes.DataNotFound, SovrenErrorCodes.DataNotFound);

                await IndexService.DeleteIndex(jobIndexId);
                await DelayForIndexSync();

                sovrenException = Assert.ThrowsAsync<SovrenException>(async () => {
                    await IndexService.DeleteDocumentFromIndex(jobIndexId, documentId);
                });
                Assert.AreEqual(SovrenErrorCodes.DataNotFound, SovrenErrorCodes.DataNotFound);
            }
            finally
            {
                await CleanUpIndex(jobIndexId);
            }
        }

        private async Task<bool> DoesIndexExist(string indexName)
        {
            List<Index> indexes = await IndexService.GetAllIndexes();

            // check if any of the indexes found share the specified index name
            return indexes.Exists(x => x.Name.Equals(indexName, StringComparison.OrdinalIgnoreCase));
        }
    }
}

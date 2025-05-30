// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using NUnit.Framework;
using Textkernel.Tx.Models.Matching;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
using Textkernel.Tx.Models.API.Matching.Request;
using Textkernel.Tx.Models.API.Matching;
using Textkernel.Tx.Models.API.Indexes;
using Textkernel.Tx.Models.Resume;
using Textkernel.Tx.Models.Job;
using Index = Textkernel.Tx.Models.Matching.Index;

namespace Textkernel.Tx.SDK.Tests.IntegrationTests
{
    public class IndexTests : TestBase
    {
        const string resumeIndexId = "dotnet-SDK-IntegrationTest-Resume";
        const string jobIndexId = "dotnet-SDK-IntegrationTest-Job";

        private static string GetIndexName(IndexType indexType)
        {
            return indexType == IndexType.Resume ? resumeIndexId : jobIndexId;
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

        [TestCaseSource(typeof(IndexTests), nameof(BadIndexNames))]

        public async Task TestCreateIndexBadInput(string indexName)
        {
            // validate can't create bad index name
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.SearchMatchV1.CreateIndex(IndexType.Job, indexName);
            });

            exception = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.SearchMatchV1.CreateIndex(IndexType.Resume, indexName);
            });

            await Task.CompletedTask;
        }

        [TestCaseSource(typeof(IndexTests), nameof(BadIndexNames))]
        public async Task TestDeleteIndexBadInput(string indexName)
        {
            ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.SearchMatchV1.DeleteIndex(indexName);
            });

            await Task.CompletedTask;
        }

        [TestCase(IndexType.Resume)]
        [TestCase(IndexType.Job)]
        public async Task TestIndexLifecycle(IndexType indexType)
        {
            string indexName = GetIndexName(indexType);

            try
            {
                // verify index doesn't exist
                List<Index> indexes = Client.SearchMatchV1.GetAllIndexes().Result.Value;
                Assert.False(await DoesIndexExist(indexName));

                // create index
                Assert.DoesNotThrowAsync(async () =>
                {
                    await Client.SearchMatchV1.CreateIndex(indexType, indexName);
                });

                await DelayForIndexSync();

                // create index already exists
                TxException txException = Assert.ThrowsAsync<TxException>(async () =>
                {
                    await Client.SearchMatchV1.CreateIndex(indexType, indexName);
                });
                Assert.AreEqual(TxErrorCodes.DuplicateAsset, txException.TxErrorCode);

                // verify index created
                Assert.True(await DoesIndexExist(indexName));

                // delete the index
                Assert.DoesNotThrowAsync(async () =>
                {
                    await Client.SearchMatchV1.DeleteIndex(indexName);
                });

                await DelayForIndexSync();

                // verify index doesn't exist
                Assert.False(await DoesIndexExist(indexName));

                // try to delete an index that doesn't exist
                txException = Assert.ThrowsAsync<TxException>(async () =>
                {
                    await Client.SearchMatchV1.DeleteIndex(indexName);
                });
                Assert.AreEqual(TxErrorCodes.DataNotFound, txException.TxErrorCode);
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
                TxException txException = Assert.ThrowsAsync<TxException>(async () => {
                    await Client.SearchMatchV1.GetResume(resumeIndexId, documentId);
                });
                Assert.AreEqual(TxErrorCodes.DataNotFound, txException.TxErrorCode);

                // verify can't add document to an index that doesn't exist
                txException = Assert.ThrowsAsync<TxException>(async () => {
                    await Client.SearchMatchV1.IndexDocument(TestParsedResume, resumeIndexId, documentId);
                });
                Assert.AreEqual(TxErrorCodes.DataNotFound, txException.TxErrorCode);

                // create the index
                await Client.SearchMatchV1.CreateIndex(IndexType.Resume, resumeIndexId);
                await DelayForIndexSync();

                // verify document still doesn't exist
                txException = Assert.ThrowsAsync<TxException>(async () => {
                    await Client.SearchMatchV1.GetResume(resumeIndexId, documentId);
                });
                Assert.AreEqual(TxErrorCodes.DataNotFound, txException.TxErrorCode);

                // add resume to index
                await Client.SearchMatchV1.IndexDocument(TestParsedResume, resumeIndexId, documentId);
                await DelayForIndexSync();

                // confirm you can now retrieve the resume
                await Client.SearchMatchV1.GetResume(resumeIndexId, documentId);

                // add v2 resume to index
                await Client.SearchMatchV1.IndexDocument(TestParsedResumeV2, resumeIndexId, documentId);
                await DelayForIndexSync();

                // confirm you can now retrieve the resume
                await Client.SearchMatchV1.GetResume(resumeIndexId, documentId);

                // confirm the resume shows up in searches
                List<string> indexesToQuery = new List<string>() { resumeIndexId };
                FilterCriteria filterCriteria = new FilterCriteria()
                {
                    DocumentIds = new List<string>() { documentId }
                };

                SearchResponseValue searchResponse = Client.SearchMatchV1.Search(indexesToQuery, filterCriteria).Result.Value;
                Assert.AreEqual(1, searchResponse.TotalCount);
                Assert.AreEqual(1, searchResponse.CurrentCount);
                Assert.AreEqual(documentId, searchResponse.Matches[0].Id);

                // update the resume
                List<string> userDefinedTags = new List<string> { "userDefinedTag1" };
                await Client.SearchMatchV1.UpdateResumeUserDefinedTags(resumeIndexId, documentId,
                    userDefinedTags, UserDefinedTagsMethod.Overwrite);

                await DelayForIndexSync();

                // verify those updates have taken effect
                filterCriteria.UserDefinedTags = userDefinedTags;
                searchResponse = Client.SearchMatchV1.Search(indexesToQuery, filterCriteria).Result.Value;
                Assert.AreEqual(1, searchResponse.TotalCount);
                Assert.AreEqual(1, searchResponse.CurrentCount);
                Assert.AreEqual(documentId, searchResponse.Matches[0].Id);

                // confirm you can retrieve the tags
                ParsedResume resume = Client.SearchMatchV1.GetResume(resumeIndexId, documentId).Result.Value;
                Assert.AreEqual(1, resume.UserDefinedTags.Count);
                Assert.AreEqual(userDefinedTags[0], resume.UserDefinedTags[0]);

                // delete the document
                await Client.SearchMatchV1.DeleteDocument(resumeIndexId, documentId);
                await DelayForIndexSync();

                // verify can't retrieve a document that doesn't exist
                txException = Assert.ThrowsAsync<TxException>(async () => {
                    await Client.SearchMatchV1.GetResume(resumeIndexId, documentId);
                });
                Assert.AreEqual(TxErrorCodes.DataNotFound, txException.TxErrorCode);

                txException = Assert.ThrowsAsync<TxException>(async () => {
                    await Client.SearchMatchV1.DeleteDocument(resumeIndexId, documentId);
                });
                Assert.AreEqual(TxErrorCodes.DataNotFound, txException.TxErrorCode);

                await Client.SearchMatchV1.DeleteIndex(resumeIndexId);
                await DelayForIndexSync();

                txException = Assert.ThrowsAsync<TxException>(async () => {
                    await Client.SearchMatchV1.DeleteDocument(resumeIndexId, documentId);
                });
                Assert.AreEqual(TxErrorCodes.DataNotFound, txException.TxErrorCode);
            }
            catch(Exception) { throw; }
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
                TxException txException = Assert.ThrowsAsync<TxException>(async () => {
                    await Client.SearchMatchV1.GetJob(jobIndexId, documentId);
                });
                Assert.AreEqual(TxErrorCodes.DataNotFound, txException.TxErrorCode);

                // verify can't add document to an index that doesn't exist
                txException = Assert.ThrowsAsync<TxException>(async () => {
                    await Client.SearchMatchV1.IndexDocument(TestParsedJob, jobIndexId, documentId);
                });
                Assert.AreEqual(TxErrorCodes.DataNotFound, txException.TxErrorCode);

                // create the index
                await Client.SearchMatchV1.CreateIndex(IndexType.Job, jobIndexId);
                await DelayForIndexSync();

                // verify document still doesn't exist
                txException = Assert.ThrowsAsync<TxException>(async () => {
                    await Client.SearchMatchV1.GetJob(jobIndexId, documentId);
                });
                Assert.AreEqual(TxErrorCodes.DataNotFound, txException.TxErrorCode);

                // add resume to index
                await Client.SearchMatchV1.IndexDocument(TestParsedJob, jobIndexId, documentId);
                await DelayForIndexSync();

                // confirm you can now retrieve the resume
                await Client.SearchMatchV1.GetJob(jobIndexId, documentId);

                // confirm the resume shows up in searches
                List<string> indexesToQuery = new List<string>() { jobIndexId };
                FilterCriteria filterCriteria = new FilterCriteria()
                {
                    DocumentIds = new List<string>() { documentId }
                };

                SearchResponseValue searchResponse = Client.SearchMatchV1.Search(indexesToQuery, filterCriteria).Result.Value;
                Assert.AreEqual(1, searchResponse.TotalCount);
                Assert.AreEqual(1, searchResponse.CurrentCount);
                Assert.AreEqual(documentId, searchResponse.Matches[0].Id);

                // update the resume
                List<string> userDefinedTags = new List<string> { "userDefinedTag1" };
                await Client.SearchMatchV1.UpdateJobUserDefinedTags(jobIndexId, documentId,
                    userDefinedTags, UserDefinedTagsMethod.Overwrite);

                await DelayForIndexSync();

                // verify those updates have taken effect
                filterCriteria.UserDefinedTags = userDefinedTags;
                searchResponse = Client.SearchMatchV1.Search(indexesToQuery, filterCriteria).Result.Value;
                Assert.AreEqual(1, searchResponse.TotalCount);
                Assert.AreEqual(1, searchResponse.CurrentCount);
                Assert.AreEqual(documentId, searchResponse.Matches[0].Id);

                // confirm you can retrieve the tags
                ParsedJob job = Client.SearchMatchV1.GetJob(jobIndexId, documentId).Result.Value;
                Assert.AreEqual(1, job.UserDefinedTags.Count);
                Assert.AreEqual(userDefinedTags[0], job.UserDefinedTags[0]);

                // delete the document
                await Client.SearchMatchV1.DeleteDocument(jobIndexId, documentId);
                await DelayForIndexSync();

                // verify can't retrieve a document that doesn't exist
                txException = Assert.ThrowsAsync<TxException>(async () => {
                    await Client.SearchMatchV1.GetJob(jobIndexId, documentId);
                });
                Assert.AreEqual(TxErrorCodes.DataNotFound, txException.TxErrorCode);

                txException = Assert.ThrowsAsync<TxException>(async () => {
                    await Client.SearchMatchV1.DeleteDocument(jobIndexId, documentId);
                });
                Assert.AreEqual(TxErrorCodes.DataNotFound, txException.TxErrorCode);

                await Client.SearchMatchV1.DeleteIndex(jobIndexId);
                await DelayForIndexSync();

                txException = Assert.ThrowsAsync<TxException>(async () => {
                    await Client.SearchMatchV1.DeleteDocument(jobIndexId, documentId);
                });
                Assert.AreEqual(TxErrorCodes.DataNotFound, txException.TxErrorCode);
            }
            finally
            {
                await CleanUpIndex(jobIndexId);
            }
        }

        [Test]
        public async Task TestDeleteMultiple()
        {
            const string documentId1 = "1";
            const string documentId2 = "2";
            try
            {
                // create the index
                await Client.SearchMatchV1.CreateIndex(IndexType.Resume, resumeIndexId);
                await DelayForIndexSync();

                // add resume to index
                await Client.SearchMatchV1.IndexDocument(TestParsedResume, resumeIndexId, documentId1);
                await Client.SearchMatchV1.IndexDocument(TestParsedResume, resumeIndexId, documentId2);
                await DelayForIndexSync();

                // confirm you can now retrieve the resumes
                await Client.SearchMatchV1.GetResume(resumeIndexId, documentId1);
                await Client.SearchMatchV1.GetResume(resumeIndexId, documentId2);

                // delete the document
                await Client.SearchMatchV1.DeleteMultipleDocuments(resumeIndexId, new List<string> { documentId1, documentId2 });
                await DelayForIndexSync();

                // verify can't retrieve a document that doesn't exist
                TxException txException = Assert.ThrowsAsync<TxException>(async () => {
                    await Client.SearchMatchV1.GetResume(resumeIndexId, documentId1);
                });
                Assert.AreEqual(TxErrorCodes.DataNotFound, txException.TxErrorCode);

                txException = Assert.ThrowsAsync<TxException>(async () => {
                    await Client.SearchMatchV1.GetResume(resumeIndexId, documentId2);
                });
                Assert.AreEqual(TxErrorCodes.DataNotFound, txException.TxErrorCode);

                await Client.SearchMatchV1.DeleteIndex(resumeIndexId);
                await DelayForIndexSync();
            }
            catch (Exception) { throw; }
            finally
            {
                await CleanUpIndex(resumeIndexId);
            }
        }

        private async Task<bool> DoesIndexExist(string indexName)
        {
            List<Index> indexes = Client.SearchMatchV1.GetAllIndexes().Result.Value;

            await Task.CompletedTask;

            // check if any of the indexes found share the specified index name
            return indexes.Exists(x => x.Name.Equals(indexName, StringComparison.OrdinalIgnoreCase));
        }
    }
}

using NUnit.Framework;
using Sovren.Models.Matching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Collections;

namespace Sovren.SDK.Tests.IntegrationTests
{
    public class IndexServiceTests : TestBase
    {
        const string resumeIndexName = "SDK-IntegrationTest-Resume";
        const string jobIndexName = "SDK-IntegrationTest-Job";

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
            string indexName = indexType == IndexType.Resume ? resumeIndexName : jobIndexName;

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
                await CleanUpIndex(resumeIndexName);
                await CleanUpIndex(jobIndexName);

                await DelayForIndexSync();
            }
        }

        

        private async Task<bool> DoesIndexExist(string indexName)
        {
            List<Index> indexes = await IndexService.GetAllIndexes();

            // check if any of the indexes found share the specified index name
            return indexes.Exists(x => x.Name.Equals(indexName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// This method is useful in finally blocks to cleanup indexes created for a test case
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        private async Task CleanUpIndex(string indexName)
        {
            try
            {
                await IndexService.DeleteIndex(indexName);
            }
            catch { }
        }
    }
}

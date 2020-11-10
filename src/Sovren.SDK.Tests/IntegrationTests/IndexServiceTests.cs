using NUnit.Framework;
using Sovren.Models.Matching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sovren.SDK.Tests.IntegrationTests
{
    public class IndexServiceTests : TestBase
    {
        const string resumeIndexName = "SDK-IntegrationTest-Resume";
        const string jobIndexName = "SDK-IntegrationTest-Job";

        [TestCase(IndexType.Resume)]
        [TestCase(IndexType.Job)]
        public async Task IndexCrudOperations(IndexType indexType)
        {
            string indexName = indexType == IndexType.Resume ? resumeIndexName : jobIndexName;
            
            try
            {
                // validate can't create bad index name
                //SovrenException exception = Assert.ThrowsAsync<SovrenException>(async () => {
                //    await IndexService.CreateIndex(indexType, "invalid=index");
                //});
                //Assert.AreEqual(SovrenErrorCodes.InvalidParameter, exception.SovrenErrorCode);

                // validate can't create no index name
                ArgumentException exception = Assert.ThrowsAsync<ArgumentException>(async () => {
                    await IndexService.CreateIndex(indexType, null);
                });

                // validate can't create empty index name
                exception = Assert.ThrowsAsync<ArgumentException>(async () => {
                    await IndexService.CreateIndex(indexType, "");
                });

                // validate can't create whitespace index name
                exception = Assert.ThrowsAsync<ArgumentException>(async () => {
                    await IndexService.CreateIndex(indexType, "    ");
                });

                // create successful index
                // create index already exists
                // create index no name
                // create index empty name
                // verify index created



                //Assert.DoesNotThrowAsync(async () => {
                //    await IndexService.CreateIndex(IndexType.Resume, resumeIndexName);
                //});

                //Assert.DoesNotThrowAsync(async () => {
                //    await IndexService.CreateIndex(IndexType.Job, jobIndexName);
                //});

                //List<Index> getIndexesResponse = null;
                //Assert.DoesNotThrowAsync(async () => {
                //    getIndexesResponse = await IndexService.GetAllIndexes();
                //});

                //// check that each of the newly create indexes exist
                //Assert.IsNotNull(getIndexesResponse.FirstOrDefault(x => x.Name.Equals(resumeIndexName, StringComparison.OrdinalIgnoreCase)));
                //Assert.IsNotNull(getIndexesResponse.FirstOrDefault(x => x.Name.Equals(jobIndexName, StringComparison.OrdinalIgnoreCase)));

                //Task<Models.API.Parsing.ParseResumeResponseValue> resume = ParsingService.ParseResume(TestData.Resume);

                //Assert.DoesNotThrowAsync(async () => {
                //    await IndexService.DeleteIndex(resumeIndexName);
                //});
                //Assert.DoesNotThrowAsync(async () => {
                //    await IndexService.DeleteIndex(jobIndexName);
                //});
            }
            catch (Exception ex)
            {

            }
            finally
            {
                // clean up assets in case the test failed someone before the delete calls were executed
                try { await IndexService.DeleteIndex(resumeIndexName); }
                catch { }

                try { await IndexService.DeleteIndex(jobIndexName); }
                catch {}
            }
        }

        [Test]
        public async Task TestCreateIndex()
        {

        }
    }
}

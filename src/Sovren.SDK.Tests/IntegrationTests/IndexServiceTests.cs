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
        [Test]
        public async Task IndexCrudOperations()
        {
            const string resumeIndexName = "SDK-IntegrationTest-Resume";
            const string jobIndexName = "SDK-IntegrationTest-Job";
            try
            {
                Assert.DoesNotThrowAsync(async () => {
                    await IndexService.CreateIndex(IndexType.Resume, resumeIndexName);
                });

                Assert.DoesNotThrowAsync(async () => {
                    await IndexService.CreateIndex(IndexType.Job, jobIndexName);
                });

                List<Index> getIndexesResponse = null;
                Assert.DoesNotThrowAsync(async () => {
                    getIndexesResponse = await IndexService.GetAllIndexes();
                });

                // check that each of the newly create indexes exist
                Assert.IsNotNull(getIndexesResponse.FirstOrDefault(x => x.Name.Equals(resumeIndexName, StringComparison.OrdinalIgnoreCase)));
                Assert.IsNotNull(getIndexesResponse.FirstOrDefault(x => x.Name.Equals(jobIndexName, StringComparison.OrdinalIgnoreCase)));

                Task<Models.API.Parsing.ParseResumeResponseValue> resume = ParsingService.ParseResume(TestData.Resume);

                Assert.DoesNotThrowAsync(async () => {
                    await IndexService.DeleteIndex(resumeIndexName);
                });
                Assert.DoesNotThrowAsync(async () => {
                    await IndexService.DeleteIndex(jobIndexName);
                });
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

    }
}

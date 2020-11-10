using NUnit.Framework;
using Sovren.Models.API.Indexes;
using Sovren.Models.Matching;
using System;
using System.Collections.Generic;
using System.Text;
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
                CreateIndexResponse createResumeIndexResponse = await IndexService.CreateIndex(IndexType.Resume, resumeIndexName);
                Assert.IsTrue(createResumeIndexResponse.Info.IsSuccess);

                CreateIndexResponse createJobIndexResponse = await IndexService.CreateIndex(IndexType.Job, jobIndexName);
                Assert.IsTrue(createJobIndexResponse.Info.IsSuccess);

                var getIndexesResponse = await IndexService.GetAllIndexes();


                await IndexService.DeleteIndex(resumeIndexName);
                await IndexService.DeleteIndex(jobIndexName);
            }
            finally
            {
                // clean up assets incase the test failed someone before the delete calls were executed
                try
                {
                    await IndexService.DeleteIndex(resumeIndexName);
                }
                catch { }

                try
                {
                    await IndexService.DeleteIndex(jobIndexName);
                }
                catch {}
            }
        }
    }
}

using NUnit.Framework;
using Sovren.Models.API.Geocoding;
using Sovren.Models.API.Indexes;
using Sovren.Models.Matching;
using System.Threading.Tasks;

namespace Sovren.SDK.Tests.IntegrationTests
{
    public class GeocodingServiceTests : TestBase
    {
        [Test]
        public async Task TestGetAccount()
        {
            await TestGetAccount(GeocodingService);
        }

        [Test]
        public async Task TestResumeNoAddress()
        {
            Assert.ThrowsAsync<SovrenException>(async () => {
                await GeocodingService.Geocode(TestParsedResume);
            });

            Assert.ThrowsAsync<SovrenException>(async () => {
                await GeocodingService.Geocode(TestParsedResume, null);
            });

            Assert.ThrowsAsync<SovrenException>(async () => {
                await GeocodingService.Geocode(TestParsedResume, new Address());
            });

            Assert.ThrowsAsync<SovrenException>(async () => {
                await GeocodingService.Geocode(TestParsedResume, new Address() {
                    CountryCode = "US"
                });
            });

            Assert.DoesNotThrowAsync(async () => {
                await GeocodingService.Geocode(TestParsedResume, new Address()
                {
                    CountryCode = "US",
                    Municipality = "Dallas",
                    Region = "TX"
                });
            });

            Assert.DoesNotThrowAsync(async () => {
                await GeocodingService.Geocode(TestParsedResumeWithAddress);
            });
        }

        [Test]
        public async Task TestJobNoAddress()
        {
            Assert.ThrowsAsync<SovrenException>(async () => {
                await GeocodingService.Geocode(TestParsedJob);
            });

            Assert.ThrowsAsync<SovrenException>(async () => {
                await GeocodingService.Geocode(TestParsedJob, null);
            });

            Assert.ThrowsAsync<SovrenException>(async () => {
                await GeocodingService.Geocode(TestParsedJob, new Address());
            });

            Assert.ThrowsAsync<SovrenException>(async () => {
                await GeocodingService.Geocode(TestParsedJob, new Address()
                {
                    CountryCode = "US"
                });
            });

            Assert.DoesNotThrowAsync(async () => {
                await GeocodingService.Geocode(TestParsedJob, new Address()
                {
                    CountryCode = "US",
                    Municipality = "Dallas",
                    Region = "TX"
                });
            });

            Assert.DoesNotThrowAsync(async () => {
                await GeocodingService.Geocode(TestParsedJobWithAddress);
            });
        }

        [Test]
        public async Task TestResumeGeocodeIndex()
        {
            string indexId = "SDK-resume-" + nameof(TestJobGeocodeIndex);
            string documentId = "1";

            try
            {
                await IndexService.CreateIndex(IndexType.Resume, indexId);

                // missing indexing options
                Assert.ThrowsAsync<SovrenException>(async () =>
                {
                    await GeocodingService.GeocodeAndIndex(TestParsedResumeWithAddress, null);
                });

                // empty indexing options
                IndexSingleDocumentInfo indexingOptions = new IndexSingleDocumentInfo();
                Assert.ThrowsAsync<SovrenException>(async () =>
                {
                    await GeocodingService.GeocodeAndIndex(TestParsedResumeWithAddress, indexingOptions);
                });

                // missing documentid
                indexingOptions.IndexId = indexId;
                Assert.ThrowsAsync<SovrenException>(async () =>
                {
                    await GeocodingService.GeocodeAndIndex(TestParsedResumeWithAddress, indexingOptions);
                });

                indexingOptions.DocumentId = documentId;

                // not enough data points to index
                Assert.ThrowsAsync<SovrenException>(async () => {
                    await GeocodingService.GeocodeAndIndex(TestParsedResume, indexingOptions);
                });

                Assert.DoesNotThrowAsync(async () => {
                    await GeocodingService.GeocodeAndIndex(TestParsedResumeWithAddress, indexingOptions);
                });

                Assert.DoesNotThrowAsync(async () => {
                    await IndexService.GetResume(indexId, documentId);
                });
            }
            finally
            {
                await CleanUpIndex(indexId);
            }
        }

        [Test]
        public async Task TestJobGeocodeIndex()
        {
            string indexId = "SDK-job-" + nameof(TestJobGeocodeIndex);
            string documentId = "1";

            try
            {
                await IndexService.CreateIndex(IndexType.Job, indexId);

                // missing indexing options
                Assert.ThrowsAsync<SovrenException>(async () => {
                    await GeocodingService.GeocodeAndIndex(TestParsedJobWithAddress, null);
                });

                // empty indexing options
                IndexSingleDocumentInfo indexingOptions = new IndexSingleDocumentInfo();
                Assert.ThrowsAsync<SovrenException>(async () => {
                    await GeocodingService.GeocodeAndIndex(TestParsedJobWithAddress, indexingOptions);
                });

                // missing documentid
                indexingOptions.IndexId = indexId;
                Assert.ThrowsAsync<SovrenException>(async () => {
                    await GeocodingService.GeocodeAndIndex(TestParsedJobWithAddress, indexingOptions);
                });

                indexingOptions.DocumentId = documentId;

                // not enough data points to index
                Assert.ThrowsAsync<SovrenException>(async () => {
                    await GeocodingService.GeocodeAndIndex(TestParsedJob, indexingOptions);
                });

                Assert.DoesNotThrowAsync(async () => {
                    await GeocodingService.GeocodeAndIndex(TestParsedJobWithAddress, indexingOptions);
                });

                Assert.DoesNotThrowAsync(async () => {
                    await IndexService.GetJob(indexId, documentId);
                });
            }
            finally
            {
                await CleanUpIndex(indexId);
            }
        }
    }
}

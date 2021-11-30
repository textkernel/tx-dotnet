// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using NUnit.Framework;
using Sovren.Models.API.Geocoding;
using Sovren.Models.API.Indexes;
using Sovren.Models.Matching;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Sovren.Models.API.Matching.Request;
using Sovren.Models.API.Matching;

namespace Sovren.SDK.Tests.IntegrationTests
{
    public class GeocodingTests : TestBase
    {
        [Test]
        public async Task TestResumeNoAddress()
        {
            Assert.ThrowsAsync<SovrenException>(async () => {
                await Client.Geocode(TestParsedResume);
            });

            Assert.ThrowsAsync<SovrenException>(async () => {
                await Client.Geocode(TestParsedResume, null);
            });

            Assert.ThrowsAsync<SovrenException>(async () => {
                await Client.Geocode(TestParsedResume, new Address());
            });

            Assert.ThrowsAsync<SovrenException>(async () => {
                await Client.Geocode(TestParsedResume, new Address() {
                    CountryCode = "US"
                });
            });

            Assert.DoesNotThrowAsync(async () => {
                await Client.Geocode(TestParsedResume, new Address()
                {
                    CountryCode = "US",
                    Municipality = "Dallas",
                    Region = "TX"
                });
            });

            Assert.DoesNotThrowAsync(async () => {
                await Client.Geocode(TestParsedResumeWithAddress);
            });

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestJobNoAddress()
        {
            Assert.ThrowsAsync<SovrenException>(async () => {
                await Client.Geocode(TestParsedJob);
            });

            Assert.ThrowsAsync<SovrenException>(async () => {
                await Client.Geocode(TestParsedJob, null);
            });

            Assert.ThrowsAsync<SovrenException>(async () => {
                await Client.Geocode(TestParsedJob, new Address());
            });

            Assert.ThrowsAsync<SovrenException>(async () => {
                await Client.Geocode(TestParsedJob, new Address()
                {
                    CountryCode = "US"
                });
            });

            Assert.DoesNotThrowAsync(async () => {
                await Client.Geocode(TestParsedJob, new Address()
                {
                    CountryCode = "US",
                    Municipality = "Dallas",
                    Region = "TX"
                });
            });

            Assert.DoesNotThrowAsync(async () => {
                await Client.Geocode(TestParsedJobWithAddress);
            });

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestResumeGeocodeIndex()
        {
            string indexId = "SDK-resume-" + nameof(TestJobGeocodeIndex);
            string documentId = "1";

            try
            {
                await Client.CreateIndex(IndexType.Resume, indexId);

                // missing indexing options
                Assert.ThrowsAsync<SovrenException>(async () =>
                {
                    await Client.GeocodeAndIndex(TestParsedResumeWithAddress, null);
                });

                // empty indexing options
                IndexSingleDocumentInfo indexingOptions = new IndexSingleDocumentInfo();
                Assert.ThrowsAsync<SovrenException>(async () =>
                {
                    await Client.GeocodeAndIndex(TestParsedResumeWithAddress, indexingOptions);
                });

                // missing documentid
                indexingOptions.IndexId = indexId;
                Assert.ThrowsAsync<SovrenException>(async () =>
                {
                    await Client.GeocodeAndIndex(TestParsedResumeWithAddress, indexingOptions);
                });

                indexingOptions.DocumentId = documentId;

                // not enough data points to index
                Assert.ThrowsAsync<SovrenException>(async () => {
                    await Client.GeocodeAndIndex(TestParsedResume, indexingOptions);
                });

                Assert.DoesNotThrowAsync(async () => {
                    await Client.GeocodeAndIndex(TestParsedResumeWithAddress, indexingOptions);
                });

                Assert.DoesNotThrowAsync(async () => {
                    await Client.GetResume(indexId, documentId);
                });
            }
            finally
            {
                await CleanUpIndex(indexId);
            }

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestJobGeocodeIndex()
        {
            string indexId = "SDK-job-" + nameof(TestJobGeocodeIndex);
            string documentId = "1";

            try
            {
                await Client.CreateIndex(IndexType.Job, indexId);

                // missing indexing options
                Assert.ThrowsAsync<SovrenException>(async () => {
                    await Client.GeocodeAndIndex(TestParsedJobWithAddress, null);
                });

                // empty indexing options
                IndexSingleDocumentInfo indexingOptions = new IndexSingleDocumentInfo();
                Assert.ThrowsAsync<SovrenException>(async () => {
                    await Client.GeocodeAndIndex(TestParsedJobWithAddress, indexingOptions);
                });

                // missing documentid
                indexingOptions.IndexId = indexId;
                Assert.ThrowsAsync<SovrenException>(async () => {
                    await Client.GeocodeAndIndex(TestParsedJobWithAddress, indexingOptions);
                });

                indexingOptions.DocumentId = documentId;

                // not enough data points to index
                Assert.ThrowsAsync<SovrenException>(async () => {
                    await Client.GeocodeAndIndex(TestParsedJob, indexingOptions);
                });

                Assert.DoesNotThrowAsync(async () => {
                    await Client.GeocodeAndIndex(TestParsedJobWithAddress, indexingOptions);
                });

                Assert.DoesNotThrowAsync(async () => {
                    await Client.GetJob(indexId, documentId);
                });
            }
            finally
            {
                await CleanUpIndex(indexId);
            }

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestResumeGeocodeIndexReplaceAddress()
        {
            string indexId = "SDK-resume-" + nameof(TestResumeGeocodeIndexReplaceAddress);
            string documentId = "1";

            try
            {
                await Client.CreateIndex(IndexType.Resume, indexId);

                IndexSingleDocumentInfo indexingOptions = new IndexSingleDocumentInfo
                {
                    IndexId = indexId,
                    DocumentId = documentId
                };

                GeocodeCredentials geocodeCredentials = new GeocodeCredentials() { 
                    Provider = GeocodeProvider.None
                };

                Address address = new Address()
                {
                    Municipality = "Dallas",
                    Region = "TX",
                    CountryCode = "US",
                    PostalCode = "75214"
                };

                GeocodeAndIndexResumeResponse response = await Client.GeocodeAndIndex(TestParsedResumeWithAddress, indexingOptions, address, geocodeCredentials);

                Assert.Multiple(() => {
                    Assert.AreEqual(0m, response.Info.TransactionCost);
                    Assert.AreEqual(address.CountryCode, response.Value.ResumeData.ContactInformation.Location.CountryCode);
                    Assert.AreEqual(address.Region, response.Value.ResumeData.ContactInformation.Location.Regions.FirstOrDefault());
                    Assert.AreEqual(address.Municipality, response.Value.ResumeData.ContactInformation.Location.Municipality);
                    Assert.AreEqual(address.PostalCode, response.Value.ResumeData.ContactInformation.Location.PostalCode);
                });

                Assert.DoesNotThrowAsync(async () => {
                    await Client.GetResume(indexId, documentId);
                });

                SearchResponse searchResponse = await Client.Search(new[] { indexId }, new FilterCriteria()
                {
                    LocationCriteria = new LocationCriteria()
                    {
                        Locations = new List<FilterLocation>()
                            {
                                new FilterLocation()
                                {
                                    CountryCode = address.CountryCode,
                                    Municipality = address.Municipality,
                                    PostalCode = address.PostalCode,
                                    Region = address.Region
                                }
                            }
                    }
                });

                Assert.AreEqual(1, searchResponse.Value.CurrentCount);
            }
            finally
            {
                await CleanUpIndex(indexId);
            }

            await Task.CompletedTask;
        }


        [Test]
        public async Task TestJobGeocodeIndexReplaceAddress()
        {
            string indexId = "SDK-job-" + nameof(TestJobGeocodeIndexReplaceAddress);
            string documentId = "1";

            try
            {
                await Client.CreateIndex(IndexType.Job, indexId);

                IndexSingleDocumentInfo indexingOptions = new IndexSingleDocumentInfo
                {
                    IndexId = indexId,
                    DocumentId = documentId
                };

                GeocodeCredentials geocodeCredentials = new GeocodeCredentials()
                {
                    Provider = GeocodeProvider.None
                };

                Address address = new Address()
                {
                    Municipality = "Dallas",
                    Region = "TX",
                    CountryCode = "US",
                    PostalCode = "75214"
                };

                GeocodeAndIndexJobResponse response = await Client.GeocodeAndIndex(TestParsedJobWithAddress, indexingOptions, address, geocodeCredentials);

                Assert.Multiple(() => {
                    Assert.AreEqual(0m, response.Info.TransactionCost);
                    Assert.AreEqual(address.CountryCode, response.Value.JobData.CurrentLocation.CountryCode);
                    Assert.AreEqual(address.Region, response.Value.JobData.CurrentLocation.Regions.FirstOrDefault());
                    Assert.AreEqual(address.Municipality, response.Value.JobData.CurrentLocation.Municipality);
                    Assert.AreEqual(address.PostalCode, response.Value.JobData.CurrentLocation.PostalCode);
                });

                Assert.DoesNotThrowAsync(async () => {
                    await Client.GetJob(indexId, documentId);
                });

                SearchResponse searchResponse = await Client.Search(new[] { indexId }, new FilterCriteria()
                {
                    LocationCriteria = new LocationCriteria()
                    {
                        Locations = new List<FilterLocation>()
                            {
                                new FilterLocation()
                                {
                                    CountryCode = address.CountryCode,
                                    Municipality = address.Municipality,
                                    PostalCode = address.PostalCode,
                                    Region = address.Region
                                }
                            }
                    }
                });

                Assert.AreEqual(1, searchResponse.Value.CurrentCount);
            }
            finally
            {
                await CleanUpIndex(indexId);
            }

            await Task.CompletedTask;
        }
    }
}

// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using NUnit.Framework;
using Textkernel.Tx.Models.API.Geocoding;
using Textkernel.Tx.Models.API.Indexes;
using Textkernel.Tx.Models.Matching;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Textkernel.Tx.Models.API.Matching.Request;
using Textkernel.Tx.Models.API.Matching;

namespace Textkernel.Tx.SDK.Tests.IntegrationTests
{
    public class GeocodingTests : TestBase
    {
        [Test]
        public async Task TestResumeNoAddress()
        {
            Assert.ThrowsAsync<TxException>(async () => {
                await Client.Geocoder.Geocode(TestParsedResume);
            });

            Assert.ThrowsAsync<TxException>(async () => {
                await Client.Geocoder.Geocode(TestParsedResume, null);
            });

            Assert.ThrowsAsync<TxException>(async () => {
                await Client.Geocoder.Geocode(TestParsedResume, new Address());
            });

            Assert.ThrowsAsync<TxException>(async () => {
                await Client.Geocoder.Geocode(TestParsedResume, new Address() {
                    CountryCode = "US"
                });
            });

            Assert.DoesNotThrowAsync(async () => {
                await Client.Geocoder.Geocode(TestParsedResume, new Address()
                {
                    CountryCode = "US",
                    Municipality = "Dallas",
                    Region = "TX"
                });
            });

            Assert.DoesNotThrowAsync(async () => {
                await Client.Geocoder.Geocode(TestParsedResumeWithAddress);
            });

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestJobNoAddress()
        {
            Assert.ThrowsAsync<TxException>(async () => {
                await Client.Geocoder.Geocode(TestParsedJob);
            });

            Assert.ThrowsAsync<TxException>(async () => {
                await Client.Geocoder.Geocode(TestParsedJob, null);
            });

            Assert.ThrowsAsync<TxException>(async () => {
                await Client.Geocoder.Geocode(TestParsedJob, new Address());
            });

            Assert.ThrowsAsync<TxException>(async () => {
                await Client.Geocoder.Geocode(TestParsedJob, new Address()
                {
                    CountryCode = "US"
                });
            });

            Assert.DoesNotThrowAsync(async () => {
                await Client.Geocoder.Geocode(TestParsedJob, new Address()
                {
                    CountryCode = "US",
                    Municipality = "Dallas",
                    Region = "TX"
                });
            });

            Assert.DoesNotThrowAsync(async () => {
                await Client.Geocoder.Geocode(TestParsedJobWithAddress);
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
                await Client.SearchMatch.CreateIndex(IndexType.Resume, indexId);

                // missing indexing options
                Assert.ThrowsAsync<TxException>(async () =>
                {
                    await Client.Geocoder.GeocodeAndIndex(TestParsedResumeWithAddress, null);
                });

                // empty indexing options
                IndexingOptionsGeneric indexingOptions = new IndexingOptionsGeneric();
                Assert.ThrowsAsync<TxException>(async () =>
                {
                    await Client.Geocoder.GeocodeAndIndex(TestParsedResumeWithAddress, indexingOptions);
                });

                // missing documentid
                indexingOptions.IndexId = indexId;
                Assert.ThrowsAsync<TxException>(async () =>
                {
                    await Client.Geocoder.GeocodeAndIndex(TestParsedResumeWithAddress, indexingOptions);
                });

                indexingOptions.DocumentId = documentId;

                // not enough data points to index
                Assert.ThrowsAsync<TxException>(async () => {
                    await Client.Geocoder.GeocodeAndIndex(TestParsedResume, indexingOptions);
                });

                Assert.DoesNotThrowAsync(async () => {
                    await Client.Geocoder.GeocodeAndIndex(TestParsedResumeWithAddress, indexingOptions);
                });

                await DelayForIndexSync();

                Assert.DoesNotThrowAsync(async () => {
                    await Client.SearchMatch.GetResume(indexId, documentId);
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
                await Client.SearchMatch.CreateIndex(IndexType.Job, indexId);

                // missing indexing options
                Assert.ThrowsAsync<TxException>(async () => {
                    await Client.Geocoder.GeocodeAndIndex(TestParsedJobWithAddress, null);
                });

                // empty indexing options
                IndexingOptionsGeneric indexingOptions = new IndexingOptionsGeneric();
                Assert.ThrowsAsync<TxException>(async () => {
                    await Client.Geocoder.GeocodeAndIndex(TestParsedJobWithAddress, indexingOptions);
                });

                // missing documentid
                indexingOptions.IndexId = indexId;
                Assert.ThrowsAsync<TxException>(async () => {
                    await Client.Geocoder.GeocodeAndIndex(TestParsedJobWithAddress, indexingOptions);
                });

                indexingOptions.DocumentId = documentId;

                // not enough data points to index
                Assert.ThrowsAsync<TxException>(async () => {
                    await Client.Geocoder.GeocodeAndIndex(TestParsedJob, indexingOptions);
                });

                Assert.DoesNotThrowAsync(async () => {
                    await Client.Geocoder.GeocodeAndIndex(TestParsedJobWithAddress, indexingOptions);
                });

                await DelayForIndexSync();

                Assert.DoesNotThrowAsync(async () => {
                    await Client.SearchMatch.GetJob(indexId, documentId);
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
                await Client.SearchMatch.CreateIndex(IndexType.Resume, indexId);

                IndexingOptionsGeneric indexingOptions = new IndexingOptionsGeneric
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

                GeocodeAndIndexResumeResponse response = await Client.Geocoder.GeocodeAndIndex(TestParsedResumeWithAddress, indexingOptions, address, geocodeCredentials);

                Assert.Multiple(() => {
                    Assert.AreEqual(address.CountryCode, response.Value.ResumeData.ContactInformation.Location.CountryCode);
                    Assert.AreEqual(address.Region, response.Value.ResumeData.ContactInformation.Location.Regions.FirstOrDefault());
                    Assert.AreEqual(address.Municipality, response.Value.ResumeData.ContactInformation.Location.Municipality);
                    Assert.AreEqual(address.PostalCode, response.Value.ResumeData.ContactInformation.Location.PostalCode);
                });

                await DelayForIndexSync();

                Assert.DoesNotThrowAsync(async () => {
                    await Client.SearchMatch.GetResume(indexId, documentId);
                });

                SearchResponse searchResponse = await Client.SearchMatch.Search(new[] { indexId }, new FilterCriteria()
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
                await Client.SearchMatch.CreateIndex(IndexType.Job, indexId);

                IndexingOptionsGeneric indexingOptions = new IndexingOptionsGeneric
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

                GeocodeAndIndexJobResponse response = await Client.Geocoder.GeocodeAndIndex(TestParsedJobWithAddress, indexingOptions, address, geocodeCredentials);

                Assert.Multiple(() => {
                    Assert.AreEqual(address.CountryCode, response.Value.JobData.CurrentLocation.CountryCode);
                    Assert.AreEqual(address.Region, response.Value.JobData.CurrentLocation.Regions.FirstOrDefault());
                    Assert.AreEqual(address.Municipality, response.Value.JobData.CurrentLocation.Municipality);
                    Assert.AreEqual(address.PostalCode, response.Value.JobData.CurrentLocation.PostalCode);
                });

                await DelayForIndexSync();

                Assert.DoesNotThrowAsync(async () => {
                    await Client.SearchMatch.GetJob(indexId, documentId);
                });

                SearchResponse searchResponse = await Client.SearchMatch.Search(new[] { indexId }, new FilterCriteria()
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

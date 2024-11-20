// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using NUnit.Framework;
using Textkernel.Tx.Models.API.BimetricScoring;
using Textkernel.Tx.Models.API.Matching;
using Textkernel.Tx.Models.API.Matching.Request;
using Textkernel.Tx.Models.Matching;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Textkernel.Tx.SDK.Tests.IntegrationTests
{
    public class AIMatchingTests : TestBase
    {
        private const string _jobIndexId = "dotnet-SDK-job-" + nameof(AIMatchingTests);
        private const string _resumeIndexId = "dotnet-SDK-resume-" + nameof(AIMatchingTests);
        private const string _documentId = "1";

        private readonly List<string> _resumesIndexes = new List<string>() { _resumeIndexId };
        private readonly List<string> _jobsIndexes = new List<string>() { _jobIndexId };


        [OneTimeSetUp]
        public async Task SetupAIMatchingIndexes()
        {
            // create indexes
            await Client.SearchMatch.CreateIndex(IndexType.Job, _jobIndexId);
            await Client.SearchMatch.CreateIndex(IndexType.Resume, _resumeIndexId);
            await DelayForIndexSync();

            // add a document to each index
            await Client.SearchMatch.IndexDocument(TestParsedJobTech, _jobIndexId, _documentId);
            await Client.SearchMatch.IndexDocument(TestParsedResume, _resumeIndexId, _documentId);
            await DelayForIndexSync();
        }

        [OneTimeTearDown]
        public async Task TeardownMatchingIndexes()
        {
            await CleanUpIndex(_jobIndexId);
            await CleanUpIndex(_resumeIndexId);
        }

        [TestCase(_jobIndexId, "Developer")]
        [TestCase(_resumeIndexId, "VB6")]
        public async Task TestSearch(string indexId, string validSearchTerm)
        {
            Assert.ThrowsAsync<TxException>(async () =>
            {
                await Client.SearchMatch.Search(null, null);
            });

            List<string> indexesToQuery = new List<string>() { indexId };
            Assert.ThrowsAsync<TxException>(async () =>
            {
                await Client.SearchMatch.Search(indexesToQuery, null);
            });

            FilterCriteria filterCritera = new FilterCriteria();
            Assert.ThrowsAsync<TxException>(async () =>
            {
                await Client.SearchMatch.Search(null, filterCritera);
            });

            Assert.ThrowsAsync<TxException>(async () =>
            {
                await Client.SearchMatch.Search(new List<string>() { "fake-index-id" }, filterCritera);
            });

            Assert.ThrowsAsync<TxException>(async () =>
            {
                await Client.SearchMatch.Search(indexesToQuery, filterCritera);
            });

            filterCritera.SearchExpression = validSearchTerm;
            Assert.DoesNotThrow(() =>
            {
                SearchResponseValue response = Client.SearchMatch.Search(indexesToQuery, filterCritera).Result.Value;
                Assert.AreEqual(1, response.CurrentCount);
                Assert.AreEqual(1, response.TotalCount);
            });

            filterCritera.SearchExpression = "ThisIsATermThatIsntInTheDocument";
            Assert.DoesNotThrow(() =>
            {
                SearchResponseValue response = Client.SearchMatch.Search(indexesToQuery, filterCritera).Result.Value;
                Assert.AreEqual(0, response.CurrentCount);
                Assert.AreEqual(0, response.TotalCount);
            });

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestMatchJob()
        {
            Assert.ThrowsAsync<TxException>(async () =>
            {
                await Client.SearchMatch.Match(TestParsedJobTech, null);
            });

            Assert.DoesNotThrow(() =>
            {
                MatchResponseValue matchResponse = Client.SearchMatch.Match(TestParsedJobTech, _jobsIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            Assert.DoesNotThrow(() =>
            {
                MatchResponseValue matchResponse = Client.SearchMatch.Match(TestParsedJobTech, _resumesIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestMatchResume()
        {
            Assert.ThrowsAsync<TxException>(async () =>
            {
                await Client.SearchMatch.Match(TestParsedResume, null);
            });

            Assert.DoesNotThrow(() =>
            {
                MatchResponseValue matchResponse = Client.SearchMatch.Match(TestParsedResume, _jobsIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            Assert.DoesNotThrow(() =>
            {
                MatchResponseValue matchResponse = Client.SearchMatch.Match(TestParsedResume, _resumesIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestMatchIndexedDocument()
        {
            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.SearchMatch.Match("", null, null);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.SearchMatch.Match(null, _documentId, _resumesIndexes);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.SearchMatch.Match("", _documentId, _resumesIndexes);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.SearchMatch.Match(" ", _documentId, _resumesIndexes);
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.SearchMatch.Match(_resumeIndexId, null, _resumesIndexes); ;
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.SearchMatch.Match(_resumeIndexId, "", _resumesIndexes); ;
            });

            Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                await Client.SearchMatch.Match(_resumeIndexId, " ", _resumesIndexes); ;
            });

            Assert.ThrowsAsync<TxException>(async () =>
            {
                await Client.SearchMatch.Match(_resumeIndexId, _documentId, null); ;
            });

            Assert.ThrowsAsync<TxException>(async () =>
            {
                await Client.SearchMatch.Match(_resumeIndexId, _documentId, new List<string>()); ;
            });

            Assert.DoesNotThrow(() =>
            {
                MatchResponseValue matchResponse = Client.SearchMatch.Match(_resumeIndexId, _documentId, _resumesIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            Assert.DoesNotThrow(() =>
            {
                MatchResponseValue matchResponse = Client.SearchMatch.Match(_resumeIndexId, _documentId, _jobsIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            Assert.DoesNotThrow(() =>
            {
                MatchResponseValue matchResponse = Client.SearchMatch.Match(_jobIndexId, _documentId, _resumesIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            Assert.DoesNotThrow(() =>
            {
                MatchResponseValue matchResponse = Client.SearchMatch.Match(_jobIndexId, _documentId, _jobsIndexes).Result.Value;
                Assert.AreEqual(1, matchResponse.CurrentCount);
                Assert.AreEqual(1, matchResponse.TotalCount);
                Assert.AreEqual(1, matchResponse.Matches.Count);
            });

            await Task.CompletedTask;
        }
    }
}

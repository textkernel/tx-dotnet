// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using NUnit.Framework;
using Sovren.Models.API.BimetricScoring;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sovren.SDK.Tests.IntegrationTests
{
    public class BimetricScoringTests : TestBase
    {
        [Test]
        public async Task TestBimetricScoringResume()
        {
            Assert.ThrowsAsync<SovrenException>(async () => {
                await Client.BimetricScore(new ParsedResumeWithId(), new List<ParsedResumeWithId>());
            });

            Assert.ThrowsAsync<SovrenException>(async () => {
                await Client.BimetricScore(TestParsedResumeWithId, new List<ParsedResumeWithId>());
            });

            Assert.ThrowsAsync<SovrenException>(async () => {
                await Client.BimetricScore(new ParsedResumeWithId(), new List<ParsedResumeWithId>() { TestParsedResumeWithId });
            });

            Assert.DoesNotThrowAsync(async () => {
                await Client.BimetricScore(TestParsedResumeWithId, new List<ParsedResumeWithId>() { TestParsedResumeWithId });
            });

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestBimetricScoringJob()
        {
            Assert.ThrowsAsync<SovrenException>(async () => {
                await Client.BimetricScore(new ParsedJobWithId(), new List<ParsedJobWithId>());
            });

            Assert.ThrowsAsync<SovrenException>(async () => {
                await Client.BimetricScore(TestParsedJobWithId, new List<ParsedJobWithId>());
            });

            Assert.ThrowsAsync<SovrenException>(async () => {
                await Client.BimetricScore(new ParsedJobWithId(), new List<ParsedJobWithId>() { TestParsedJobWithId });
            });

            Assert.DoesNotThrowAsync(async () => {
                await Client.BimetricScore(TestParsedJobWithId, new List<ParsedJobWithId>() { TestParsedJobWithId });
            });

            await Task.CompletedTask;
        }
    }
}

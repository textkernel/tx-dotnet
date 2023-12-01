// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using NUnit.Framework;
using Textkernel.Tx.Models.API.BimetricScoring;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Textkernel.Tx.SDK.Tests.IntegrationTests
{
    public class BimetricScoringTests : TestBase
    {
        [Test]
        public async Task TestBimetricScoringResume()
        {
            Assert.ThrowsAsync<TxException>(async () => {
                await Client.BimetricScore(new ParsedResumeWithId(), new List<ParsedResumeWithId>());
            });

            Assert.ThrowsAsync<TxException>(async () => {
                await Client.BimetricScore(TestParsedResumeWithId, new List<ParsedResumeWithId>());
            });

            Assert.ThrowsAsync<TxException>(async () => {
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
            Assert.ThrowsAsync<TxException>(async () => {
                await Client.BimetricScore(new ParsedJobWithId(), new List<ParsedJobWithId>());
            });

            Assert.ThrowsAsync<TxException>(async () => {
                await Client.BimetricScore(TestParsedJobWithId, new List<ParsedJobWithId>());
            });

            Assert.ThrowsAsync<TxException>(async () => {
                await Client.BimetricScore(new ParsedJobWithId(), new List<ParsedJobWithId>() { TestParsedJobWithId });
            });

            Assert.DoesNotThrowAsync(async () => {
                await Client.BimetricScore(TestParsedJobWithId, new List<ParsedJobWithId>() { TestParsedJobWithId });
            });

            await Task.CompletedTask;
        }
    }
}

﻿using NUnit.Framework;
using Sovren.Models.API.BimetricScoring;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sovren.SDK.Tests.IntegrationTests
{
    public class BimetricScoringTests : TestBase
    {
        private static ParsedJobWithId TestParsedJobWithId = new ParsedJobWithId()
        {
            Id = "1",
            JobData = TestParsedJob
        };

        private static ParsedResumeWithId TestParsedResumeWithId = new ParsedResumeWithId()
        {
            Id = "1",
            ResumeData = TestParsedResume
        };

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
        }
    }
}
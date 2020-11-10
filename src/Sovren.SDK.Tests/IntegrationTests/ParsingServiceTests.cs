using NUnit.Framework;
using Sovren.Models.API.Parsing;
using Sovren.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sovren.SDK.Tests.IntegrationTests
{
    public class ParsingServiceTests : TestBase
    {
        [Test]
        public async Task TestParseResumeSuccess()
        {
            ParseResumeResponseValue response = null;

            Assert.DoesNotThrowAsync(async () => {
                response = await ParsingService.ParseResume(TestData.Resume); 
            });

            Assert.IsTrue(response.ParsingResponse.IsSuccess);
            Assert.IsNotNull(response.ResumeData);
            Assert.IsNotNull(response.Conversions);
            Assert.IsNotNull(response.ConversionMetadata);
            Assert.IsNotNull(response.ParsingMetadata);
            Assert.IsNotNull(response.ScrubbedResumeData);
        }

        [Test]
        public async Task TestJobOrderSuccess()
        {
            ParseJobResponseValue response = null;

            Assert.DoesNotThrowAsync(async () => {
                response = await ParsingService.ParseJob(TestData.JobOrder);
            });

            Assert.IsTrue(response.ParsingResponse.IsSuccess);
            Assert.IsNotNull(response.JobData);
            Assert.IsNotNull(response.Conversions);
            Assert.IsNotNull(response.ConversionMetadata);
            Assert.IsNotNull(response.ParsingMetadata);
        }
    }
}

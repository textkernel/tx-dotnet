using NUnit.Framework;
using Sovren.Models.API.Parsing;
using Sovren.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sovren.SDK.Tests.IntegrationTests
{
    public class ParsingServiceTests : TestBase
    {
        public static IEnumerable BadDocuments
        {
            get
            {
                yield return new TestCaseData(null, typeof(ArgumentNullException));
                yield return new TestCaseData(new Models.Document(new byte[0], DateTime.Now), typeof(SovrenException));
                yield return new TestCaseData(new Models.Document(new byte[1], DateTime.Now), typeof(SovrenException));       
            }
        }

        [TestCaseSource(typeof(ParsingServiceTests), nameof(BadDocuments))]
        public async Task TestParseBadInput(Models.Document document, Type expectedExceptionType)
        {
            Assert.That(async () => await ParsingService.ParseResume(document), Throws.Exception.TypeOf(expectedExceptionType));
            Assert.That(async () => await ParsingService.ParseJob(document), Throws.Exception.TypeOf(expectedExceptionType));
        }

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

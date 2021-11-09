// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using NUnit.Framework;
using Sovren.Models.API.Formatter;

namespace Sovren.SDK.Tests.IntegrationTests
{
    public class FormatterTests : TestBase
    {
        [Test]
        public void TestResumeGeneration()
        {
            FormatResumeRequest request = new FormatResumeRequest(TestParsedResume, ResumeType.DOCX);
            FormatResumeResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.FormatResume(request); });
            Assert.NotNull(response?.Value?.DocumentAsBase64String);
            Assert.IsNotEmpty(response?.Value?.DocumentAsBase64String);
            
            request.Options.OutputType = ResumeType.PDF;
            Assert.DoesNotThrowAsync(async () => { response = await Client.FormatResume(request); });
            Assert.NotNull(response?.Value?.DocumentAsBase64String);
            Assert.IsNotEmpty(response?.Value?.DocumentAsBase64String);
        }
    }
}

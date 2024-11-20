// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using NUnit.Framework;
using Textkernel.Tx.Models.API.Formatter;

namespace Textkernel.Tx.SDK.Tests.IntegrationTests
{
    public class FormatterTests : TestBase
    {
        [Test]
        public void TestResumeGeneration()
        {
            FormatResumeRequest request = new FormatResumeRequest(TestParsedResume, ResumeType.DOCX);
            FormatResumeResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.Formatter.FormatResume(request); });
            Assert.NotNull(response?.Value?.DocumentAsBase64String);
            Assert.IsNotEmpty(response?.Value?.DocumentAsBase64String);
            
            request.Options.OutputType = ResumeType.PDF;
            Assert.DoesNotThrowAsync(async () => { response = await Client.Formatter.FormatResume(request); });
            Assert.NotNull(response?.Value?.DocumentAsBase64String);
            Assert.IsNotEmpty(response?.Value?.DocumentAsBase64String);
        }

        [Test]
        public void TestResumeGenerationWithTemplate()
        {
            FormatResumeWithTemplateRequest request = new FormatResumeWithTemplateRequest(TestParsedResume, "TestData/template1.docx", ResumeType.DOCX);
            FormatResumeResponse response = null;

            Assert.DoesNotThrowAsync(async () => { response = await Client.Formatter.FormatResumeWithTemplate(request); });
            Assert.NotNull(response?.Value?.DocumentAsBase64String);
            Assert.IsNotEmpty(response?.Value?.DocumentAsBase64String);

            request.OutputType = ResumeType.PDF;
            Assert.DoesNotThrowAsync(async () => { response = await Client.Formatter.FormatResumeWithTemplate(request); });
            Assert.NotNull(response?.Value?.DocumentAsBase64String);
            Assert.IsNotEmpty(response?.Value?.DocumentAsBase64String);
        }
    }
}

// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using NUnit.Framework;
using Textkernel.Tx.Models.API.Parsing;
using Textkernel.Tx.Models.Resume;
using Textkernel.Tx.Models.Resume.ContactInfo;
using System.Collections.Generic;

namespace Textkernel.Tx.SDK.Tests.UnitTests
{
    public class EasyAccessTests
    {
        [Test]
        public void TestNullTelephones()
        {
            ParseResumeResponse fakeResponse = new ParseResumeResponse();
            fakeResponse.Value = new ParseResumeResponseValue
            {
#pragma warning disable CS0618 // Type or member is obsolete
                ResumeData = new ParsedResume
                {
                    ContactInformation = new ContactInformation
                    {
                        Telephones = null
                    }
                }
#pragma warning restore CS0618 // Type or member is obsolete
            };

            Assert.DoesNotThrow(() => { fakeResponse.EasyAccess().GetPhoneNumbers(); });
            IEnumerable<string> phones = fakeResponse.EasyAccess().GetPhoneNumbers();
            Assert.IsNull(phones);
        }
    }
}

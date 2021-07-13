// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using NUnit.Framework;
using Sovren.Models.API.Matching.Request;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Sovren.SDK.Tests.UnitTests
{
    public class SovrenClientTests
    {
        [Test]
        public async Task TestDebugRequestBody()
        {
            DataCenter fakeDC = new DataCenter("https://thisisnotarealurlatall-akmeaoiaefoij.com/");
            SovrenClient client = new SovrenClient("1234", "1234", fakeDC);
            client.ShowFullRequestBodyInExceptions = true;

            try
            {
                List<String> index = new List<string>();
                index.Add("testIndex");
                await client.Search(index, new FilterCriteria());
            }
            catch (SovrenException e)
            {
                String expectedRequest = "{\"IndexIdsToSearchInto\":[\"testIndex\"],\"FilterCriteria\":{\"UserDefinedTagsMustAllExist\":false,\"HasPatents\":false,\"HasSecurityCredentials\":false,\"IsAuthor\":false,\"IsPublicSpeaker\":false,\"IsMilitary\":false,\"EmployersMustAllBeCurrentEmployer\":false,\"SkillsMustAllExist\":false,\"IsTopStudent\":false,\"IsCurrentStudent\":false,\"IsRecentGraduate\":false,\"LanguagesKnownMustAllExist\":false}}";
                Assert.AreEqual(expectedRequest, e.RequestBody);
            }
        }

        [Test]
        public async Task Test404Message()
        {
            DataCenter fakeDC = new DataCenter("https://rest.resumeparsing.com/v9/fake");
            SovrenClient client = new SovrenClient("1234", "1234", fakeDC);

            try
            {
                await client.GetAccountInfo();
            }
            catch (SovrenException e)
            {
                Assert.AreEqual(HttpStatusCode.NotFound, e.HttpStatusCode);
                Assert.AreEqual("404 - Not Found", e.Message);
            }
        }

        [Test]
        public async Task Test500Message()
        {
            DataCenter fakeDC = new DataCenter("https://thisisnotarealurlatall-akmeaoiaefoij.com/");
            SovrenClient client = new SovrenClient("1234", "1234", fakeDC);

            try
            {
                await client.GetAccountInfo();
            }
            catch (SovrenException e)
            {
                Assert.AreEqual(HttpStatusCode.InternalServerError, e.HttpStatusCode);
                //the message for a 500 connection failed varies based on environment so do not check that
            }
        }
    }
}

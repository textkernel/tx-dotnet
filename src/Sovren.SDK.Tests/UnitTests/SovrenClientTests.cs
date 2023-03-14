// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Sovren.Models.API.Geocoding;
using Sovren.Models.API.Matching.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using static Sovren.SDK.Tests.TestBase;

namespace Sovren.SDK.Tests.UnitTests
{
    public class SovrenClientTests
    {
        [Test]
        public void TestDebugRequestBody()
        {
            DataCenter fakeDC = new DataCenter("https://rest.resumeparsing.com/v9/fake");
            SovrenClient client = new SovrenClient("1234", "1234", fakeDC);
            client.ShowFullRequestBodyInExceptions = true;

            SovrenException e = Assert.ThrowsAsync<SovrenException>(async () =>
            {
                List<String> index = new List<string>();
                index.Add("testIndex");
                await client.Search(index, new FilterCriteria());
            });

            string expectedRequest = "{\"IndexIdsToSearchInto\":[\"testIndex\"],\"FilterCriteria\":{\"UserDefinedTagsMustAllExist\":false,\"HasPatents\":false,\"HasSecurityCredentials\":false,\"IsAuthor\":false,\"IsPublicSpeaker\":false,\"IsMilitary\":false,\"EmployersMustAllBeCurrentEmployer\":false,\"SkillsMustAllExist\":false,\"IsTopStudent\":false,\"IsCurrentStudent\":false,\"IsRecentGraduate\":false,\"LanguagesKnownMustAllExist\":false}}";
            Assert.AreEqual(expectedRequest, e.RequestBody);
        }

        [Test]
        public void Test404Message()
        {
            DataCenter fakeDC = new DataCenter("https://rest.resumeparsing.com/v9/fake");
            SovrenClient client = new SovrenClient("1234", "1234", fakeDC);

            SovrenException e = Assert.ThrowsAsync<SovrenException>(client.GetAccountInfo);
            
            Assert.AreEqual(HttpStatusCode.NotFound, e.HttpStatusCode);
            Assert.AreEqual("404 - Not Found", e.Message);
        }

        [Test]
        public void Test500Error()
        {
            DataCenter fakeDC = new DataCenter("https://thisisnotarealurlatall-akmeaoiaefoij.com/");
            SovrenClient client = new SovrenClient("1234", "1234", fakeDC);

            Assert.ThrowsAsync<HttpRequestException>(client.GetAccountInfo);
        }

        [Test]
        public void TestDependencyInjection()
        {
            Credentials data = JsonSerializer.Deserialize<Credentials>(File.ReadAllText("credentials.json"));

            //create a fake service collection to mimick DI
            var services = new ServiceCollection();

            //use the same logic we recommend to setup the injection
            services.AddSingleton(_ => new SovrenClientSettings
            {
                AccountId = data.AccountId,
                ServiceKey = data.ServiceKey,
                DataCenter = TestBase.TestDataCenter,
            });

            services.AddHttpClient<ISovrenClient, SovrenClient>();

            //now test that the injection code works as expected
            var serviceProvider = services.BuildServiceProvider();
            var injectedClient = serviceProvider.GetRequiredService<ISovrenClient>();

            Assert.DoesNotThrowAsync(async () =>
            {
                var response = await injectedClient.GetAccountInfo();
                Assert.IsNotNull(response);
                Assert.True(response.Info.IsSuccess);
            });
        }
    }
}

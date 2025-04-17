// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Textkernel.Tx.Models.API.Geocoding;
using Textkernel.Tx.Models.API.Matching.Request;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using static Textkernel.Tx.SDK.Tests.TestBase;

namespace Textkernel.Tx.SDK.Tests.UnitTests
{
    public class TxClientTests
    {
        [Test]
        public void TestDebugRequestBody()
        {
            DataCenter fakeDC = new DataCenter("https://api.us.textkernel.com/tx/v9/fake");
            TxClient client = new TxClient("1234", "1234", fakeDC);
            TxClient.ShowFullRequestBodyInExceptions = true;

            TxException e = Assert.ThrowsAsync<TxException>(async () =>
            {
                List<String> index = new List<string>();
                index.Add("testIndex");
                await client.SearchMatch.Search(index, new FilterCriteria());
            });

            string expectedRequest = "{\"IndexIdsToSearchInto\":[\"testIndex\"],\"FilterCriteria\":{\"UserDefinedTagsMustAllExist\":false,\"HasPatents\":false,\"HasSecurityCredentials\":false,\"IsAuthor\":false,\"IsPublicSpeaker\":false,\"IsMilitary\":false,\"EmployersMustAllBeCurrentEmployer\":false,\"SkillsMustAllExist\":false,\"IsTopStudent\":false,\"IsCurrentStudent\":false,\"IsRecentGraduate\":false,\"LanguagesKnownMustAllExist\":false}}";
            Assert.AreEqual(expectedRequest, e.RequestBody);
        }

        [Test]
        public void Test401Error()
        {
            DataCenter fakeDC = new DataCenter("https://api.us.textkernel.com/tx/v9/fake");
            TxClient client = new TxClient("1234", "1234", fakeDC);

            TxException e = Assert.ThrowsAsync<TxException>(client.GetAccountInfo);
            
            Assert.AreEqual(HttpStatusCode.Unauthorized, e.HttpStatusCode);
        }

        [Test]
        public void Test500Error()
        {
            DataCenter fakeDC = new DataCenter("https://thisisnotarealurlatall-akmeaoiaefoij.com/");
            TxClient client = new TxClient("1234", "1234", fakeDC);

            Assert.ThrowsAsync<HttpRequestException>(client.GetAccountInfo);
        }

        [Test]
        public void TestDependencyInjection()
        {
            Credentials data = JsonSerializer.Deserialize<Credentials>(File.ReadAllText("credentials.json"));

            //create a fake service collection to mimick DI
            var services = new ServiceCollection();

            //use the same logic we recommend to setup the injection
            services.AddSingleton(_ => new TxClientSettings
            {
                AccountId = data.AccountId,
                ServiceKey = data.ServiceKey,
                DataCenter = TestBase.TestDataCenter,
            });

            services.AddHttpClient<ITxClient, TxClient>();

            //now test that the injection code works as expected
            var serviceProvider = services.BuildServiceProvider();
            var injectedClient = serviceProvider.GetRequiredService<ITxClient>();

            Assert.DoesNotThrowAsync(async () =>
            {
                var response = await injectedClient.GetAccountInfo();
                Assert.IsNotNull(response);
                Assert.True(response.Info.IsSuccess);
            });
        }
    }
}

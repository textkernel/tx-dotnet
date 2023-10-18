// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using NUnit.Framework;

namespace Textkernel.Tx.SDK.Tests.UnitTests
{
    public class DataCenterTests
    {
        [Test]
        public void TestSelfHostedUrl()
        {
            string url = "https://selfhosted.customer.com/something";
            DataCenter dc = new DataCenter(url);
            ApiEndpoints endpoints = new ApiEndpoints(dc);

            Assert.AreEqual("account", endpoints.GetAccountInfo().RequestUri.ToString());
        }
    }
}

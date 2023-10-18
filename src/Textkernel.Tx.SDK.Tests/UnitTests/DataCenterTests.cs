// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

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

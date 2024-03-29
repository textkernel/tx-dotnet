// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using NUnit.Framework;
using Textkernel.Tx.Models.API.Account;
using System.Threading.Tasks;

namespace Textkernel.Tx.SDK.Tests.IntegrationTests
{
    public class AccountTests : TestBase
    {
        [Test]
        public async Task TestGetAccount()
        {
            GetAccountInfoResponse accountInfo = await Client.GetAccountInfo();

            Assert.False(string.IsNullOrWhiteSpace(accountInfo.Info.CustomerDetails.AccountId));
            Assert.AreNotEqual(0, accountInfo.Info.CustomerDetails.CreditsRemaining);
            Assert.AreNotEqual(0, accountInfo.Info.CustomerDetails.CreditsUsed);
            Assert.False(string.IsNullOrWhiteSpace(accountInfo.Info.CustomerDetails.ExpirationDate));
            Assert.False(string.IsNullOrWhiteSpace(accountInfo.Info.CustomerDetails.IPAddress));
            Assert.True(accountInfo.Info.CustomerDetails.MaximumConcurrentRequests > 0);
            Assert.False(string.IsNullOrWhiteSpace(accountInfo.Info.CustomerDetails.Name));
            Assert.False(string.IsNullOrWhiteSpace(accountInfo.Info.CustomerDetails.Region));
            Assert.IsNotNull(accountInfo.Info.CustomerDetails.Region);
        }
    }
}

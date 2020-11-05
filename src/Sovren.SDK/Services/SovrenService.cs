// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API;
using Sovren.Models.API.Account;
using System;
using System.Threading.Tasks;

namespace Sovren.Services
{
    /// <summary>
    /// Note that this class is not thread-safe, therefore you should never share a service across multiple threads. Instead, use a single service per thread.
    /// </summary>
    public abstract class SovrenService
    {
        internal SovrenClient Client { get; private set; }

        /// <summary>
        /// The <see cref="ApiResponseInfo.CustomerDetails"/> from the most recent API response.
        /// Use this to check for remaining credits or max allowed concurrency after multiple transactions.
        /// </summary>
        public AccountInfo LatestCustomerDetails => Client.LatestCustomerDetails;

        internal SovrenService(SovrenClient client)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <summary>
        /// Get the account info (remaining credits, max concurrency, etc).
        /// </summary>
        /// <exception cref="SovrenException">Thrown when an API error occurs</exception>
        public async Task<AccountInfo> GetAccountInfo()
        {
            GetAccountInfoResponse response = await Client.GetAccountInfo();
            return response.Info.CustomerDetails;
        }
    }
}

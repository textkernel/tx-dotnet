// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren
{
    /// <summary>
    /// Contains your Sovren account information. You can find this information at <see href="https://portal.sovren.com/"/>
    /// </summary>
    public class Credentials
    {
        /// <summary>
        /// Your Sovren account id. You can find this information at <see href="https://portal.sovren.com/"/>
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// Your PRIVATE Sovren service key. You can find this information at <see href="https://portal.sovren.com/"/>
        /// </summary>
        public string ServiceKey { get; set; }
    }
}

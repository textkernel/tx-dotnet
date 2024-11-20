// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;

namespace Textkernel.Tx
{
    /// <summary>
    /// Use either <see cref="US"/>, <see cref="EU"/> or <see cref="AU"/>
    /// </summary>
    public class DataCenter
    {
        /// <summary>
        /// Represents the US datacenter. You can find out which datacenter your account is in at <see href="https://cloud.textkernel.com/tx/console/"/>
        /// </summary>
        public static DataCenter US = new DataCenter("https://api.us.textkernel.com/tx/v10");

        /// <summary>
        /// Represents the EU datacenter. You can find out which datacenter your account is in at <see href="https://cloud.textkernel.com/tx/console/"/>
        /// </summary>
        public static DataCenter EU = new DataCenter("https://api.eu.textkernel.com/tx/v10");
		
		 /// <summary>
        /// Represents the AU datacenter. You can find out which datacenter your account is in at <see href="https://cloud.textkernel.com/tx/console/"/>
        /// </summary>
        public static DataCenter AU = new DataCenter("https://api.au.textkernel.com/tx/v10");

        internal string Url { get; private set; }

        /// <summary>
        /// Create a DataCenter for a self-hosted instance
        /// </summary>
        /// <param name="url">The URL of your self-hosted instance</param>
        public DataCenter(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));
            Url = url;
        }
    }
}

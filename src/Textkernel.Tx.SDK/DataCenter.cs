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
        /// Represents the US datacenter. You can find out which datacenter your account is in at <see href="https://portal.sovren.com/"/>
        /// </summary>
        public static DataCenter US = new DataCenter("https://rest.resumeparsing.com", "v10", true);

        /// <summary>
        /// Represents the EU datacenter. You can find out which datacenter your account is in at <see href="https://portal.sovren.com/"/>
        /// </summary>
        public static DataCenter EU = new DataCenter("https://eu-rest.resumeparsing.com", "v10", true);
		
		 /// <summary>
        /// Represents the AU datacenter. You can find out which datacenter your account is in at <see href="https://portal.sovren.com/"/>
        /// </summary>
        public static DataCenter AU = new DataCenter("https://au-rest.resumeparsing.com", "v10", true);

        internal string Root { get; private set; }
        internal string Version { get; private set; }
        internal bool IsSaaS { get; private set; }

        internal DataCenter(string root, string version, bool isSaaS)
        {
            if (string.IsNullOrWhiteSpace(root)) throw new ArgumentNullException(nameof(root));

            Root = root;
            Version = version;
            IsSaaS = isSaaS;
        }

        /// <summary>
        /// Create a DataCenter for a self-hosted instance
        /// </summary>
        /// <param name="endpoint">The URL of your self-hosted instance</param>
        public DataCenter(string endpoint) : this(endpoint, null, false) { }
    }
}

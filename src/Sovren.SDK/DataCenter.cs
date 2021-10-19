// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren
{
    /// <summary>
    /// Use either <see cref="US"/>, <see cref="EU"/> or <see cref="AU"/>
    /// </summary>
    public class DataCenter
    {
        /// <summary>
        /// Represents the Sovren US datacenter. You can find out which datacenter your account is in at <see href="https://portal.sovren.com/"/>
        /// </summary>
        public static DataCenter US = new DataCenter("https://rest.resumeparsing.com", "v10", true);

        /// <summary>
        /// Represents the Sovren EU datacenter. You can find out which datacenter your account is in at <see href="https://portal.sovren.com/"/>
        /// </summary>
        public static DataCenter EU = new DataCenter("https://eu-rest.resumeparsing.com", "v10", true);
		
		 /// <summary>
        /// Represents the Sovren AU datacenter. You can find out which datacenter your account is in at <see href="https://portal.sovren.com/"/>
        /// </summary>
        public static DataCenter AU = new DataCenter("https://au-rest.resumeparsing.com", "v10", true);

        internal string Root { get; private set; }
        internal string Version { get; private set; }
        internal bool IsSovrenSaaS { get; private set; }

        internal DataCenter(string root, string version, bool isSaaS)
        {
            Root = root;
            Version = version;
            IsSovrenSaaS = isSaaS;
        }

        /// <summary>
        /// Create a DataCenter for a self-hosted instance
        /// </summary>
        /// <param name="endpoint">The URL of your self-hosted instance</param>
        public DataCenter(string endpoint) : this(endpoint, null, false) { }
    }
}

// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

namespace Sovren.Models.Resume.ContactInfo
{
    /// <summary>
    /// A type of <see cref="WebAddress"/>. These are useful instead of magic strings.
    /// </summary>
    public class WebAddressType
    {
        /// <summary>
        /// A personal website URL
        /// </summary>
        public static WebAddressType PersonalWebsite = "PersonalWebsite";

        /// <summary>
        /// A LinkedIn URL
        /// </summary>
        public static WebAddressType LinkedIn = "LinkedIn";

        /// <summary>
        /// A candidate's Twitter handle
        /// </summary>
        public static WebAddressType TwitterHandle = "Twitter";

        /// <summary>
        /// A candidate's Facebook profile URL
        /// </summary>
        public static WebAddressType Facebook = "Facebook";

        /// <summary>
        /// A candidate's Instagram username
        /// </summary>
        public static WebAddressType Instagram = "Instagram";

        /// <summary>
        /// A candidate's ICQ username
        /// </summary>
        public static WebAddressType ICQ = "ICQ";

        /// <summary>
        /// The raw string value
        /// </summary>
        public string Value { get; protected set; }

        private WebAddressType(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Converts a string to a <see cref="WebAddressType"/>
        /// </summary>
        /// <param name="value">the string to use</param>
        public static implicit operator WebAddressType(string value)
        {
            return new WebAddressType(value);
        }
    }

    /// <summary>
    /// A web address (URL, twitter handle, etc)
    /// </summary>
    public class WebAddress
    {
        /// <summary>
        /// The URL or username
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The type of address. One of:
        /// <br/><see cref="WebAddressType.PersonalWebsite"/>
        /// <br/><see cref="WebAddressType.LinkedIn"/>
        /// <br/><see cref="WebAddressType.TwitterHandle"/>
        /// <br/><see cref="WebAddressType.Facebook"/>
        /// <br/><see cref="WebAddressType.Instagram"/>
        /// <br/><see cref="WebAddressType.ICQ"/>
        /// </summary>
        public string Type { get; set; }
    }
}

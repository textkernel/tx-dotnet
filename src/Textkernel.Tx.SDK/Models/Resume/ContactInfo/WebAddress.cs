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
        /// An unknown internet handle/URL (the platform/website/app was not specified)
        /// </summary>
        public static WebAddressType Unknown = "Unknown";

        /// <summary>
        /// A personal website URL
        /// </summary>
        public static WebAddressType PersonalWebsite = "PersonalWebsite";

        /// <summary>
        /// A LinkedIn URL
        /// </summary>
        public static WebAddressType LinkedIn = "LinkedIn";

        /// <summary>
        /// A Twitter handle
        /// </summary>
        public static WebAddressType Twitter = "Twitter";

        /// <summary>
        /// A Facebook profile URL
        /// </summary>
        public static WebAddressType Facebook = "Facebook";

        /// <summary>
        /// An Instagram username
        /// </summary>
        public static WebAddressType Instagram = "Instagram";

        /// <summary>
        /// An ICQ username
        /// </summary>
        public static WebAddressType ICQ = "ICQ";

        /// <summary>
        /// A Quora username
        /// </summary>
        public static WebAddressType Quora = "Quora";

        /// <summary>
        /// A Skype username/URL
        /// </summary>
        public static WebAddressType Skype = "Skype";

        /// <summary>
        /// A WeChat username
        /// </summary>
        public static WebAddressType WeChat = "WeChat";

        /// <summary>
        /// A QQ username/number
        /// </summary>
        public static WebAddressType QQ = "QQ";

        /// <summary>
        /// A Telegraph username
        /// </summary>
        public static WebAddressType Telegraph = "Telegraph";

        /// <summary>
        /// A WhatsApp username/number
        /// </summary>
        public static WebAddressType WhatsApp = "WhatsApp";

        /// <summary>
        /// A Telegram username
        /// </summary>
        public static WebAddressType Telegram = "Telegram";

        /// <summary>
        /// A MeWe username/URL
        /// </summary>
        public static WebAddressType MeWe = "MeWe";

        /// <summary>
        /// A Parler username
        /// </summary>
        public static WebAddressType Parler = "Parler";

        /// <summary>
        /// A Gab username
        /// </summary>
        public static WebAddressType Gab = "Gab";

        /// <summary>
        /// A Reddit username/URL
        /// </summary>
        public static WebAddressType Reddit = "Reddit";

        /// <summary>
        /// A GitHub username/URL
        /// </summary>
        public static WebAddressType GitHub = "GitHub";

        /// <summary>
        /// A Signal username/number
        /// </summary>
        public static WebAddressType Signal = "Signal";

        /// <summary>
        /// A Stack Overflow username/URL
        /// </summary>
        public static WebAddressType StackOverflow = "StackOverflow";

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
        /// <br/><see cref="WebAddressType.Twitter"/>
        /// <br/><see cref="WebAddressType.GitHub"/>
        /// <br/><see cref="WebAddressType.Facebook"/>
        /// <br/><see cref="WebAddressType.Skype"/>
        /// <br/><see cref="WebAddressType.WhatsApp"/>
        /// <br/><see cref="WebAddressType.StackOverflow"/>
        /// <br/><see cref="WebAddressType.Instagram"/>
        /// <br/><see cref="WebAddressType.Reddit"/>
        /// <br/><see cref="WebAddressType.Signal"/>
        /// <br/><see cref="WebAddressType.Quora"/>
        /// <br/><see cref="WebAddressType.ICQ"/>
        /// <br/><see cref="WebAddressType.WeChat"/>
        /// <br/><see cref="WebAddressType.QQ"/>
        /// <br/><see cref="WebAddressType.Telegraph"/>
        /// <br/><see cref="WebAddressType.Telegram"/>
        /// <br/><see cref="WebAddressType.MeWe"/>
        /// <br/><see cref="WebAddressType.Parler"/>
        /// <br/><see cref="WebAddressType.Gab"/>
        /// <br/><see cref="WebAddressType.Unknown"/>
        /// </summary>
        public string Type { get; set; }
    }
}

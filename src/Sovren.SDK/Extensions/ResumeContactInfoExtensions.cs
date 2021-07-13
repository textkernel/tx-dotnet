// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models;
using Sovren.Models.Resume.ContactInfo;
using System.Collections.Generic;
using System.Linq;

namespace Sovren
{
    /// <summary></summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public static class ResumeContactInfoExtensions
    {
        /// <summary>
        /// Returns the contact information or <see langword="null"/>
        /// </summary>
        public static ContactInformation GetContactInfo(this ParseResumeResponseExtensions exts)
        {
            return exts.Response.Value.ResumeData?.ContactInformation;
        }

        /// <summary>
        /// Returns the email addresses or <see langword="null"/>
        /// </summary>
        public static IEnumerable<string> GetEmailAddresses(this ParseResumeResponseExtensions exts)
        {
            return exts.Response.Value.ResumeData?.ContactInformation?.EmailAddresses;
        }

        /// <summary>
        /// Returns the phone numbers or <see langword="null"/>
        /// </summary>
        public static IEnumerable<string> GetPhoneNumbers(this ParseResumeResponseExtensions exts)
        {
            return exts.Response.Value.ResumeData?.ContactInformation?.Telephones?.Select(t => t.Normalized);
        }

        /// <summary>
        /// Returns the candidate name or <see langword="null"/>
        /// </summary>
        public static PersonName GetCandidateName(this ParseResumeResponseExtensions exts)
        {
            return exts.Response.Value.ResumeData?.ContactInformation?.CandidateName;
        }

        /// <summary>
        /// Returns the address or <see langword="null"/>
        /// </summary>
        public static Location GetAddress(this ParseResumeResponseExtensions exts)
        {
            return exts.Response.Value.ResumeData?.ContactInformation?.Location;
        }

        /// <summary>
        /// Returns the specific type of web address if it exists or <see langword="null"/>
        /// </summary>
        /// <param name="type">
        /// One of:
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
        /// </param>
        /// <param name="exts"></param>
        public static string GetWebAddress(this ParseResumeResponseExtensions exts, WebAddressType type)
        {
            return exts.Response.Value.ResumeData?.ContactInformation?.WebAddresses?.FirstOrDefault(a => a.Type == type.Value)?.Address;
        }
    }
}

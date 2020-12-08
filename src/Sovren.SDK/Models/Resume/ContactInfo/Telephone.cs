using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.Resume.ContactInfo
{
    /// <summary>
    /// A phone number listed on the resume
    /// </summary>
    public class Telephone : NormalizedString
    {
        /// <summary>
        /// The international country code part of the phone number
        /// </summary>
        public string InternationalCountryCode { get; set; }
        /// <summary>
        /// The area code part of the phone number
        /// </summary>
        public string AreaCityCode { get; set; }
        /// <summary>
        /// The subscriber number part of the phone number
        /// </summary>
        public string SubscriberNumber { get; set; }
    }
}

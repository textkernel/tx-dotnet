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
        /// If OutputFormat.TelcomNumber.Style = Structured configuration setting is set, the international country code part of the phone number
        /// </summary>
        public string InternationalCountryCode { get; set; }
        /// <summary>
        /// If OutputFormat.TelcomNumber.Style = Structured configuration setting is set, the area code part of the phone number
        /// </summary>
        public string AreaCityCode { get; set; }
        /// <summary>
        /// If OutputFormat.TelcomNumber.Style = Structured configuration setting is set, the subscriber part of the phone number
        /// </summary>
        public string SubscriberNumber { get; set; }
    }
}

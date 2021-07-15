// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Collections.Generic;

namespace Sovren.Models.Resume.ContactInfo
{
    /// <summary>
    /// A candidate's contact information listed on a resume
    /// </summary>
    public class ContactInformation
    {
        /// <summary>
        /// The candidate's name
        /// </summary>
        public PersonName CandidateName { get; set; }

        /// <summary>
        /// The candidate's phone numbers. If multiple numbers are found, mobile phone numbers will be listed first
        /// </summary>
        public List<Telephone> Telephones { get; set; }

        /// <summary>
        /// The candidate's email addresses
        /// </summary>
        public List<string> EmailAddresses { get; set; }

        /// <summary>
        /// The candidate's location/address. The Parser does not standardize addresses. Address standardization
        /// services are available, including for example the Google Maps API, that can take the Parser's contact
        /// info fields and standardize/geocode the data.
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// The candidate's web addresses (URLs, social media) listed on the resume
        /// </summary>
        public List<WebAddress> WebAddresses { get; set; }
    }
}

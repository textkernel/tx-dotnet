// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.Resume.Employment;
using Sovren.Models.Resume.Education;

namespace Sovren.Models.Resume
{
    /// <summary>
    /// Information about a particular section of a resume
    /// </summary>
    public class SectionIdentifier
    {
        /// <summary>
        /// The section type
        /// </summary>
        public string SectionType { get; set; }

        /// <summary>
        /// If applicable, the <see cref="Position.Id"/> or <see cref="EducationDetails.Id"/>
        /// </summary>
        public string Id { get; set; }
    }
}

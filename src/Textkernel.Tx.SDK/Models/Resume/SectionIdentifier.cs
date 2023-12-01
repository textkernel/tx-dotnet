// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.Resume.Employment;
using Textkernel.Tx.Models.Resume.Education;

namespace Textkernel.Tx.Models.Resume
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

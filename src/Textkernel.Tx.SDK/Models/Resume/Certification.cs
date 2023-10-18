// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.Resume
{
    /// <summary>
    /// A certification found on a resume
    /// </summary>
    public class Certification
    {
        /// <summary>
        /// The name of the certification
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// <see langword="true"/> if this was found by matching to a known list of certifications.
        /// <see langword="false"/> if this was found by analyzing the context and determining it was a certification.
        /// </summary>
        public bool MatchedFromList { get; set; }

        /// <summary>
        /// The API generates several possible variations for some certifications to be used in AI Matching.
        /// This greatly improves Matching, since different candidates have different ways of listing a certification.
        /// If this certification is a generated variation of a certification found on the resume, this property will be
        /// <see langword="true"/>, otherwise <see langword="false"/>.
        /// </summary>
        public bool IsVariation { get; set; }
    }
}

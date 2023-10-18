// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Textkernel.Tx.Models.DataEnrichment;

namespace Textkernel.Tx.Models.Resume.Employment
{
    /// <summary>
    /// Normalized profession related to a specific job title.
    /// </summary>
    public class ParsingNormalizedProfession
    {
        /// <summary>
        /// Object containing the details of the profession concept.
        /// </summary>
        public ProfessionClassification<int> Profession { get; set; }

        /// <summary>
        /// The object of the group to which the profession concept belongs.
        /// </summary>
        public ProfessionClassification<int> Group { get; set; }

        /// <summary>
        /// The object of the class to which the profession concept belongs.
        /// </summary>
        public ProfessionClassification<int> Class { get; set; }

        /// <summary>
        /// The object of the ISCO profession concept
        /// </summary>
        public VersionedNormalizedProfessionClassification<int> ISCO { get; set; }

        /// <summary>
        /// The object of the ONET profession concept
        /// </summary>
        public VersionedNormalizedProfessionClassification<string> ONET { get; set; }

        /// <summary>
        /// Overall confidence that the input job title was normalized to the correct profession concept
        /// </summary>
        public float Confidence { get; set; }
    }


    /// <summary>
    /// Object representing a profession concept with taxonomy version
    /// </summary>
    public class VersionedNormalizedProfessionClassification<T> : ProfessionClassification<T>
    {
        /// <summary>
        /// The version of the profession taxonomy
        /// </summary>
        public string Version { get; set; }
    }
}

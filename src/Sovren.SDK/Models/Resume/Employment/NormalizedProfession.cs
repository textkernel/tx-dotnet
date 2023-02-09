// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Collections.Generic;

namespace Sovren.Models.Resume.Employment
{
    /// <summary>
    /// Normalized profession related to a specific job title.
    /// </summary>
    public class ParsingNormalizedProfession
    {
        /// <summary>
        /// Object containing the details of the profession concept.
        /// </summary>
        public NormalizedProfessionClassification<int> Profession { get; set; }

        /// <summary>
        /// The object of the group to which the profession concept belongs.
        /// </summary>
        public NormalizedProfessionClassification<int> Group { get; set; }

        /// <summary>
        /// The object of the class to which the profession concept belongs.
        /// </summary>
        public NormalizedProfessionClassification<int> Class { get; set; }

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
    /// Object representing a profession concept
    /// </summary>
    public class NormalizedProfessionClassification<T>
    {
        /// <summary>
        /// The code id of the profession concept.
        /// </summary>
        public T CodeId { get; set; }

        /// <summary>
        /// The description of the profession concept.
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// Object representing a profession concept with taxonomy version
    /// </summary>
    public class VersionedNormalizedProfessionClassification<T> : NormalizedProfessionClassification<T>
    {

        /// <summary>
        /// The version of the profession taxonomy
        /// </summary>
        public string Version { get; set; }
    }
}

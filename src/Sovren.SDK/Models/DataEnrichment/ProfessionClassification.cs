using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.DataEnrichment
{
    /// <summary>
    /// Object representing a profession concept
    /// </summary>
    public class ProfessionClassification<T>
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
    public class VersionedProfessionClassification<T> : ProfessionClassification<T>
    {

        /// <summary>
        /// The version of the profession taxonomy
        /// </summary>
        public string Version { get; set; }
    }
}

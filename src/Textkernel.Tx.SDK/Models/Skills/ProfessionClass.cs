// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System.Collections.Generic;

namespace Textkernel.Tx.Models.Skills
{
    /// <summary>
    /// Profession Class that describes a percentage of the source document.
    /// </summary>
    public class ProfessionClass
    {
        /// <summary>
        /// Name of the related profession.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id of the related profession.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Percent of overall document that relates to this profession.
        /// </summary>
        public int Percent { get; set; }

        /// <summary>
        /// Array of objects representing groups of professions.
        /// </summary>
        public List<ProfessionGroup> Groups { get; set; }

    }

    /// <summary>
    /// Profession Group that describes a percentage of the Profession Class.
    /// </summary>
    public class ProfessionGroup
    {
        /// <summary>
        /// Name of the profession group.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id of the profession group.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Percent of overall document described by this profession group. All groups across all classes will add up to 100%.
        /// </summary>
        public int Percent { get; set; }

        /// <summary>
        /// Array of normalized skills associated to this profession group.
        /// </summary>
        public List<string> NormalizedSkills { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.DataEnrichment
{
    /// <summary>
    /// A DES Skill
    /// </summary>
    public interface IDESSkill
    {
        /// <summary>
        /// The ID of the skill in the taxonomy.
        /// </summary>
        string Id { get; }
    }

    /// <summary>
    /// A skill from the Skills Taxonomy
    /// </summary>
    public class Skill : IDESSkill
    {
        /// <summary>
        /// The description of the skill in the requested language.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The ID of the skill in the taxonomy.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Type of skill. Possible values are Professional, IT, Language, or Soft.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The language ISO 639-1 code. This will only appear for language skills (Type = Language).
        /// </summary>
        public string IsoCode { get; set; }
    }
}

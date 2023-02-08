using Sovren.Models.DataEnrichment;
using Sovren.Models.Resume;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.Skills
{
    /// <summary>
    /// Skill exactly as it was found in the plain text of the document.
    /// </summary>
    public abstract class RawSkill
    {
        /// <summary>
        /// The exact skill text extracted from the document.
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Normalized skill concept representing one or more raw skills that were extracted.
    /// </summary>
    public abstract class NormalizedSkill
    {
        /// <summary>
        /// Name of the normalized skill.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id of this skill in the skills taxonomy.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Type of skill. Possible values are Professional, IT, or Soft.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Array of raw skills that were extracted that normalized to this skill.
        /// </summary>
        public List<string> RawSkills { get; set; } = new List<string>();
    }
}

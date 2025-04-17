using Textkernel.Tx.Models.API.DataEnrichment.Skills.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Textkernel.Tx.Models.DataEnrichment
{
    /// <summary>
    /// A skill from the Skills Taxonomy
    /// </summary>
    public class Skill
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
        /// Type of skill. Possible values are Professional, IT, Language, Soft, or Certification (only when using v2 endpoints).
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The language ISO 639-1 code. This will only appear for language skills (Type = Language).
        /// </summary>
        public string IsoCode { get; set; }
    }

    /// <summary>
    /// A skill object.
    /// </summary>
    public class SkillMultipleDescriptions : Skill
    {
        /// <summary>
        /// A list of descriptions of the skill in all supported/requested languages.
        /// </summary>
        public List<LangDescription> Descriptions { get; set; }

        /// <summary>
        /// The description of the skill in the requested language.
        /// <br/><b>NOTE: if multiple languages were requested, this is simply the first from <see cref="Descriptions"/></b>
        /// </summary>
        new public string Description => Descriptions?.FirstOrDefault()?.Description;
    }

    /// <summary>
    /// The description in the particular language.
    /// </summary>
    public class LangDescription
    {
        /// <summary>
        /// The <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO code</see> for the language of the description.
        /// </summary>
        public string IsoCode { get; set; }
        /// <summary>
        /// The description in the particular language.
        /// </summary>
        public string Description { get; set; }
    }
}

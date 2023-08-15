using Sovren.Models.API.DataEnrichment.Professions.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sovren.Models.DataEnrichment
{
    /// <summary>
    /// A profession ID/description from the Professions Taxonomy
    /// </summary>
    public class BasicProfession
    {
        /// <summary>
        /// The unique code ID of the profession in the <see href="https://sovren.com/technical-specs/latest/rest-api/data-enrichment/overview/#professions-taxonomies">Professions Taxonomy</see>.
        /// </summary>
        public int CodeId { get; set; }

        /// <summary>
        /// The description of the profession in the desired language.
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// A normalized profession from the Professions Taxonomy
    /// </summary>
    public class Profession : BasicProfession
    {
        /// <summary>
        /// The group which this profession belongs to.
        /// </summary>
        public ProfessionClassification<int> Group { get; set; }

        /// <summary>
        /// The class which this profession belongs to.
        /// </summary>
        public ProfessionClassification<int> Class { get; set; }

        /// <summary>
        /// The O*NET 2010 (deprecated) details of this profession.
        /// </summary>
        public ProfessionClassification<string> Onet { get; set; }
        /// <summary>
        /// The ISCO-2008 details of this profession.
        /// </summary>
        public ProfessionClassification<string> Isco { get; set; }
        /// <summary>
        /// The O*NET 2019 details of this profession.
        /// </summary>
        public ProfessionClassification<string> Onet2019 { get; set; }
        /// <summary>
        /// The KLDB-2020 details of this profession.
        /// </summary>
        public ProfessionClassification<string> Kldb2020 { get; set; }
        /// <summary>
        /// The UWV-BOC details of this profession.
        /// </summary>
        public ProfessionClassification<string> UwvBoc { get; set; }
        /// <summary>
        /// The UK-SOC-2010 details of this profession.
        /// </summary>
        public ProfessionClassification<string> UkSoc2010 { get; set; }
    }

    /// <summary>
    /// A normalized profession and the confidence that the normalization is correct/fitting
    /// </summary>
    public class NormalizedProfession : Profession
    {
        /// <summary>
        /// A value from [0 - 1] indicating how well the input job title matched to the normalized profession.
        /// </summary>
        public float Confidence { get; set; }
    }

    /// <summary>
    /// A skill object.
    /// </summary>
    public class ProfessionMultipleDescriptions : Profession
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
}

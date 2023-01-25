using Sovren.Models.API.DataEnrichmentServices.Ontology.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Sovren.Models.API.DataEnrichmentServices.Professions.Response
{
    /// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'LookupProfessions' response.
	/// </summary>
    public class LookupProfessionCodesResponseValue
    {
        /// <summary>
        /// A list of returned professions.
        /// </summary>
        [JsonPropertyName("Professions")]
        public List<LookupProfession> ProfessionCodes { get; set; }
    }

    /// <inheritdoc/>
    public class SovrenNormalizedProfession : LookupProfession
    {
        /// <summary>
        /// A value from [0 - 1] indicating how well the input job title matched to the normalized profession.
        /// </summary>
        public float Confidence { get; set; }
    }

    /// <inheritdoc/>
    public class LookupProfession : LookupProfessionGroupClassInfo
    {
        /// <summary>
        /// The O*NET 2010 (deprecated) details of this profession.
        /// </summary>
        public LookupGroupOrClassInfo<string> Onet { get; set; }
        /// <summary>
        /// The ISCO-2008 details of this profession.
        /// </summary>
        public LookupGroupOrClassInfo<string> Isco { get; set; }
        /// <summary>
        /// The O*NET 2019 details of this profession.
        /// </summary>
        public LookupGroupOrClassInfo<string> Onet2019 { get; set; }
        /// <summary>
        /// The KLDB-2020 details of this profession.
        /// </summary>
        public LookupGroupOrClassInfo<string> Kldb2020 { get; set; }
        /// <summary>
        /// The UWV-BOC details of this profession.
        /// </summary>
        public LookupGroupOrClassInfo<string> UwvBoc { get; set; }
        /// <summary>
        /// The UK-SOC-2010 details of this profession.
        /// </summary>
        public LookupGroupOrClassInfo<string> UkSoc2010 { get; set; }
    }

    /// <summary>
    /// Information about the lookup profession.
    /// </summary>
    public class LookupProfessionGroupClassInfo
    {
        /// <summary>
        /// The unique code ID of the profession in the <see href="https://sovren.com/technical-specs/latest/rest-api/data-enrichment-services/overview/#professions-taxonomies">Sovren Professions Taxonomy</see>.
        /// </summary>
        [JsonProperty(Order = -5)]
        public int CodeId { get; set; }

        /// <summary>
        /// The description of the profession in the desired language.
        /// </summary>
        [JsonProperty(Order = -4)]
        public string Description { get; set; }

        /// <summary>
        /// The group which this profession belongs to.
        /// </summary>
        [JsonProperty(Order = -3)]
        public LookupGroupOrClassInfo<int> Group { get; set; }

        /// <summary>
        /// The class which this profession belongs to.
        /// </summary>
        [JsonProperty(Order = -2)]
        public LookupGroupOrClassInfo<int> Class { get; set; }

    }

    /// <summary>
    /// The group or class info object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LookupGroupOrClassInfo<T>
    {
        /// <summary>
        /// The unique code ID of the item in the <see href="https://sovren.com/technical-specs/latest/rest-api/data-enrichment-services/overview/#professions-taxonomies">Sovren Professions Taxonomy</see>.
        /// </summary>
        public T CodeId { get; set; }
        /// <summary>
        /// The description of the item.
        /// </summary>
        public string Description { get; set; }
    }
}

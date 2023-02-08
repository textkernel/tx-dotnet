// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Newtonsoft.Json;
using Sovren.Models.API.DataEnrichmentServices.Skills.Response;
using Sovren.Models.DataEnrichment;
using System.Collections.Generic;

namespace Sovren.Models.API.DataEnrichmentServices.Professions.Response
{
    /// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'GetProfessionsTaxonomy' response
	/// </summary>
    public class GetTaxonomyProfessionsResponseValue : Taxonomy
    {
        /// <summary>
        /// A list of returned professions.
        /// </summary>
        public List<ProfessionGroupClass> Professions { get; set; }
    }

    /// <inheritdoc/>
    public class ProfessionGroupClass : ProfessionGroupClassInfo
    {
        /// <summary>
        /// The O*NET 2010 (deprecated) details of this profession.
        /// </summary>
        public GroupOrClassInfo<string> Onet { get; set; }
        /// <summary>
        /// The O*NET 2019 details of this profession.
        /// </summary>
        public GroupOrClassInfo<string> Onet2019 { get; set; }
        /// <summary>
        /// The KLDB-2020 details of this profession.
        /// </summary>
        public GroupOrClassInfo<string> Kldb2020 { get; set; }
        /// <summary>
        /// The UWV-BOC details of this profession.
        /// </summary>
        public GroupOrClassInfo<string> UwvBoc { get; set; }
        /// <summary>
        /// The UK-SOC-2010 details of this profession.
        /// </summary>
        public GroupOrClassInfo<string> UkSoc2010 { get; set; }
        /// <summary>
        /// The ISCO-2008 details of this profession.
        /// </summary>
        public GroupOrClassInfo<string> Isco { get; set; }
    }

    /// <summary>
    /// The profession group class info object.
    /// </summary>
    public class ProfessionGroupClassInfo
    {
        /// <summary>
        /// The unique code ID of the profession in the <see href="https://sovren.com/technical-specs/latest/rest-api/data-enrichment-services/overview/#professions-taxonomies">Sovren Professions Taxonomy</see>.
        /// </summary>
        [JsonProperty(Order = -5)]
        public int CodeId { get; set; }
        /// <summary>
        /// The description(s) of the profession in the desired language(s).
        /// </summary>
        [JsonProperty(Order = -4)]
        public List<LangDescription> Descriptions { get; set; }
        /// <summary>
        /// The group which this profession belongs to.
        /// </summary>
        [JsonProperty(Order = -3)]
        public GroupOrClassInfo<int> Group { get; set; }
        /// <summary>
        /// The class which this profession belongs to.
        /// </summary>
        [JsonProperty(Order = -2)]
        public GroupOrClassInfo<int> Class { get; set; }
    }

    /// <summary>
    /// The group or class info.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GroupOrClassInfo<T>
    {
        /// <summary>
        /// The code ID of the item in the <see href="https://sovren.com/technical-specs/latest/rest-api/data-enrichment-services/overview/#professions-taxonomies">Sovren Professions Taxonomy</see>.
        /// </summary>
        public T CodeId { get; set; }
        /// <summary>
        /// The description(s) of the item in the desired language(s).
        /// </summary>
        public List<LangDescription> Descriptions { get; set; }
    }
}

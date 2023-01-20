using Sovren.Models.API.DataEnrichmentServices.Ontology.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Sovren.Models.API.DataEnrichmentServices.Professions.Response
{
    public class LookupProfessionCodesResponseValue
    {
        [JsonPropertyName("Professions")]
        public List<LookupProfession> ProfessionCodes { get; set; }
    }

    public class SovrenNormalizedProfession : LookupProfession
    {
        public float Confidence { get; set; }
    }

    public class LookupProfession : LookupProfessionGroupClassInfo
    {
        public LookupGroupOrClassInfo<string> Onet { get; set; }
        public LookupGroupOrClassInfo<string> Isco { get; set; }
        public LookupGroupOrClassInfo<string> Onet2019 { get; set; }
        public LookupGroupOrClassInfo<string> Kldb2020 { get; set; }
        public LookupGroupOrClassInfo<string> UwvBoc { get; set; }
        public LookupGroupOrClassInfo<string> UkSoc2010 { get; set; }
    }

    public class LookupProfessionGroupClassInfo
    {
        [JsonProperty(Order = -5)]
        public int CodeId { get; set; }

        [JsonProperty(Order = -4)]
        public string Description { get; set; }

        [JsonProperty(Order = -3)]
        public LookupGroupOrClassInfo<int> Group { get; set; }

        [JsonProperty(Order = -2)]
        public LookupGroupOrClassInfo<int> Class { get; set; }

    }

    public class LookupGroupOrClassInfo<T>
    {
        public T CodeId { get; set; }
        public string Description { get; set; }
    }
}

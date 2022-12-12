using Sovren.Models.API.DataEnrichmentServices.Ontology.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Sovren.Models.API.DataEnrichmentServices.Professions.Response
{
    public class ProfessionsLookupResponseValue
    {
        [JsonPropertyName("Codes")]
        public List<LookupProfession> ProfessionCodes { get; set; }
        public OntologyMetadata Metadata { get; set; }
    }

    public class LookupProfession
    {
        [JsonPropertyName("TK")]
        public LookupProfessionGroupClassInfo TkInfo { get; set; }
        [JsonPropertyName("Onet")]
        public LookupGroupOrClassInfo<string> Onet { get; set; }
        [JsonPropertyName("Isco")]
        public LookupGroupOrClassInfo<string> Isco { get; set; }
        [JsonPropertyName("Onet2019")]
        public LookupGroupOrClassInfo<string> Onet2019 { get; set; }
        [JsonPropertyName("Kldb2020")]
        public LookupGroupOrClassInfo<string> Kldb2020 { get; set; }
        [JsonPropertyName("UwvBoc")]
        public LookupGroupOrClassInfo<string> UwvBoc { get; set; }
        [JsonPropertyName("UkSoc2010")]
        public LookupGroupOrClassInfo<string> UkSoc2010 { get; set; }
    }

    public class LookupProfessionGroupClassInfo
    {
        [JsonPropertyName("Class")]
        public LookupGroupOrClassInfo<int> Class { get; set; }

        [JsonPropertyName("Group")]
        public LookupGroupOrClassInfo<int> Group { get; set; }

        [JsonPropertyName("CodeId")]
        public int CodeId { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }
    }

    public class LookupGroupOrClassInfo<T>
    {
        [JsonPropertyName("CodeId")]
        public T CodeId { get; set; }
        [JsonPropertyName("Description")]
        public string Description { get; set; }
    }
}

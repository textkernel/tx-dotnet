using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sovren.Models.API.DataEnrichmentServices.Professions.Request
{
    public class ProfessionsNormalizeRequest
    {
        [JsonPropertyName("JobTitles")]
        public List<string> JobTitles { get; set; }
        [JsonPropertyName("Language")]
        public string Language { get; set; }
        [JsonPropertyName("OutputLanguage")]
        public string OutputLanguage { get; set; }
        /// <summary>
        /// Specifies the versions to use when normalizing professions if more than one is available for a taxonomy.
        /// </summary>
        public ProfessionNormalizationVersions Version { get; set; }
    }
}

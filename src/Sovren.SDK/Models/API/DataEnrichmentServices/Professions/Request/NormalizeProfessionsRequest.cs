using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sovren.Models.API.DataEnrichmentServices.Professions.Request
{
    public class NormalizeProfessionsRequest
    {
        public List<string> JobTitles { get; set; }
        public string Language { get; set; }
        public string OutputLanguage { get; set; }
    }
}

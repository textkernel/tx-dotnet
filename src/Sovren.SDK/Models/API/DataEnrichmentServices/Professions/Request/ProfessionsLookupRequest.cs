using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.DataEnrichmentServices.Professions.Request
{
    public class ProfessionsLookupRequest
    {
        public List<string> CodeIds { get; set; }
        public string OutputLanguage { get; set; } = "en";
    }
}

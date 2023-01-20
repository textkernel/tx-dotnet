using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.DataEnrichmentServices.Professions.Request
{
    public class LookupProfessionCodesRequest
    {
        public List<int> CodeIds { get; set; }
        public string OutputLanguage { get; set; } = "en";
    }
}

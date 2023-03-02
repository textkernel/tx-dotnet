using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.DataEnrichment.Professions.Request
{
    /// <summary>
    /// Request body for a 'LookupProfessions' request
    /// </summary>
    public class LookupProfessionCodesRequest
    {
        /// <summary>
        /// The profession code IDs to get details about from the <see href="https://sovren.com/technical-specs/latest/rest-api/data-enrichment/overview/#professions-taxonomies">Sovren Professions Taxonomy</see>.
        /// </summary>
        public List<int> CodeIds { get; set; }
        /// <summary>
        /// The language to use for professions descriptions (default is en). Must be an allowed <see href="https://sovren.com/technical-specs/latest/rest-api/data-enrichment/overview/#professions-languages">ISO code</see>.
        /// </summary>
        public string OutputLanguage { get; set; } = "en";
    }
}

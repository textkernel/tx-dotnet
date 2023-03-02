using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sovren.Models.API.DataEnrichment.Professions.Request
{
    /// <summary>
    /// Request body for a 'NormalizeProfessions' request
    /// </summary>
    public class NormalizeProfessionsRequest
    {
        /// <summary>
        /// The list of job titles to normalize (up to 10 job titles, each job title may not exceed 400 characters).
        /// </summary>
        public List<string> JobTitles { get; set; }
        /// <summary>
        /// The language of the input job titles. Must be one of the supported <see href="https://sovren.com/technical-specs/latest/rest-api/data-enrichment-services/overview/#professions-languages">ISO codes</see>.
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// The language to use for descriptions of the returned normalized professions. Must be one of the supported <see href="https://sovren.com/technical-specs/latest/rest-api/data-enrichment-services/overview/#professions-languages">ISO codes</see>.
        /// </summary>
        public string OutputLanguage { get; set; }
    }
}

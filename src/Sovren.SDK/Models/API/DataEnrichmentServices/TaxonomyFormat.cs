using Sovren.Models.DataEnrichment;

namespace Sovren.Models.API.DataEnrichmentServices
{
    /// <summary>
    /// The format used to retrieve the DES skills or professions taxonomy
    /// </summary>
    public enum TaxonomyFormat
    {
        /// <summary>
        /// Retrieves JSON, and all the response properties will be populated except the <see cref="Taxonomy.CsvOutput"/>
        /// </summary>
        json,

        /// <summary>
        /// Retrieves CSV, and the only response property populated will be the <see cref="Taxonomy.CsvOutput"/>
        /// </summary>
        csv
    }
}

using Sovren.Models.API.DataEnrichment;

namespace Sovren.Models.DataEnrichment
{
    /// <summary>
    /// Base class for Skills and Professions taxonomies
    /// </summary>
    public abstract class Taxonomy
    {
        /// <summary>
        /// If <see cref="TaxonomyFormat.csv" /> is requested, this string will contain the csv formatted taxonomy output.
        /// </summary>
        public string CsvOutput { get; set; }
    }
}

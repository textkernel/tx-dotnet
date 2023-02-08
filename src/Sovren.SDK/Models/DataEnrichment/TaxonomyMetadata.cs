using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.DataEnrichment
{
    /// <summary>
    /// Metadata about the Skills or Professions taxonomies
    /// </summary>
    public class TaxonomyMetadata
    {
        /// <summary>
        /// The version number of the professions service.
        /// </summary>
        public string ServiceVersion { get; set; }
        /// <summary>
        /// The date the taxonomy was released.
        /// </summary>
        public DateTime TaxonomyReleaseDate { get; set; }
    }
}

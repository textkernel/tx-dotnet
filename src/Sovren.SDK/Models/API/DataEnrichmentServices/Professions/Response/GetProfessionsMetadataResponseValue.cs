using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.DataEnrichmentServices.Professions.Response
{
    /// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'GetProfessionsMetadata' response.
	/// </summary>
    public class GetProfessionsMetadataResponseValue
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

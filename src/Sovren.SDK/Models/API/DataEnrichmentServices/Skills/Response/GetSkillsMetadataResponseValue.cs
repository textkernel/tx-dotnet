using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.DataEnrichmentServices.Skills.Response
{
    /// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'GetSkillsMetadata' response.
	/// </summary>
    public class GetSkillsMetadataResponseValue
    {
        /// <summary>
        /// The version number of the skills service.
        /// </summary>
        public string ServiceVersion { get; set; }
        /// <summary>
        /// The date the taxonomy was released.
        /// </summary>
        public DateTime TaxonomyReleaseDate { get; set; }
    }
}

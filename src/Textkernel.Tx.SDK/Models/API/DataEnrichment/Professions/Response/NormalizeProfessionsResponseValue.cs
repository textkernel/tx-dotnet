using Textkernel.Tx.Models.DataEnrichment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Textkernel.Tx.Models.API.DataEnrichment.Professions.Response
{
    /// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'NormalizeProfessions' response.
	/// </summary>
    public class NormalizeProfessionsResponseValue
    {
        /// <summary>
        /// A list of returned professions.
        /// </summary>
        public List<NormalizedProfession> Professions { get; set; }
    }
}

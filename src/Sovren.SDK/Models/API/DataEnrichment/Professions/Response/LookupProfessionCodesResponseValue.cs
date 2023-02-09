using Sovren.Models.API.DataEnrichment.Ontology.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Sovren.Models.DataEnrichment;

namespace Sovren.Models.API.DataEnrichment.Professions.Response
{
    /// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'LookupProfessions' response.
	/// </summary>
    public class LookupProfessionCodesResponseValue
    {
        /// <summary>
        /// A list of returned professions.
        /// </summary>
        public List<Profession> Professions { get; set; }
    }
}

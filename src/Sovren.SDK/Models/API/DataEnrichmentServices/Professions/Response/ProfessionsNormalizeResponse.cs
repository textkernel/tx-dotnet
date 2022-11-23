using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.DataEnrichmentServices.Professions.Response
{
    public class ProfessionsNormalizeResponse : ApiResponse<List<NormalizedProfession>>{ }

    public class NormalizedProfession
    {
        public NormalizedProfessionClassification<int> Profession { get; set; }
        public NormalizedProfessionClassification<int> Group { get; set; }
        public NormalizedProfessionClassification<int> Class { get; set; }
        public VersionedNormalizedProfessionClassification<int> ISCO { get; set; }
        public VersionedNormalizedProfessionClassification<string> ONET { get; set; }
        public float Confidence { get; set; }
    }

    public class NormalizedProfessionClassification<T>
    {
        public T CodeId { get; set; }
        public string Description { get; set; }
    }

    public class VersionedNormalizedProfessionClassification<T> : NormalizedProfessionClassification<T>
    {
        public string Version { get; set; }
    }
}

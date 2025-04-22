using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Textkernel.Tx.Models.API.MatchV2.Request
{
    /// <summary>
    /// The sort order for results
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortOrder
    {
        /// <summary>
        /// Ascending sort order
        /// </summary>
        ASCENDING,
        /// <summary>
        /// Descending sort order
        /// </summary>
        DESCENDING
    }

    /// <summary>
    /// Options to control sort order
    /// </summary>
    public class Sorting
    {
        /// <summary>
        /// The field name. Two special "fields" exist: _reranker and _score. _score means sorting by the engine score,
        /// _reranker means sorting by reranker score. For sorting with type _reranker a reranker needs to be configured.
        /// A sorting on field _reranker cannot have subsortings on other fields and cannot have a custom order.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Optional order of the sorting. The default behavior depends on the data type of the field: <see cref="SortOrder.DESCENDING" />
        /// for numeric and date types, <see cref="SortOrder.ASCENDING" /> for other types. For fields with location data type,
        /// results will be sorted according to the distance to the referenceLocation and only <see cref="SortOrder.ASCENDING" /> is supported.
        /// </summary>
        public SortOrder? Order { get; set; }

        /// <summary>
        /// Optional (required for fields with location data type specified) String representation of a location point in
        /// the form of "LATITUDE LONGITUDE" (e.g. "53.3478 -6.2597"). This is used for distance sorting on a location field.
        /// </summary>
        public string ReferenceLocation { get; set; }
    }
}

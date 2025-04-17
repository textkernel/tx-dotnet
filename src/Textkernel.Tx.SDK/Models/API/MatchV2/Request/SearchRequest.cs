using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Textkernel.Tx.Models.API.MatchV2.Response;

namespace Textkernel.Tx.Models.API.MatchV2.Request
{
    /// <summary>
    /// Request body for a Search request
    /// </summary>
    public class SearchRequest : MatchRequest
    {
        /// <summary>
        /// The query object that will drive the search.
        /// </summary>
        public SearchQuery Query { get; set; }
    }

    /// <summary>
    /// A search query
    /// </summary>
    public class SearchQuery
    {
        /// <summary>
        /// User typed keyword query (see query language description). It may be joined
        /// with the <see cref="QueryParts"/> if it is a single OR-combination part that can be joined.
        /// </summary>
        public string QueryString { get; set; }

        /// <summary>
        /// List of <see cref="QueryPart"/> coming from parsing a previous query.
        /// </summary>
        public IEnumerable<QueryPart> QueryParts { get; set; }
    }

    ///// <summary>
    ///// This class represents the information of a query part. They are used to visualize the parsed query
    ///// in the user interface, and allow fine-grained control over adapting the parsed query by sending
    ///// it back in a new query, possibly modified.
    ///// </summary>
    //public class QueryPart
    //{
    //    public string Field { get; set; }

    //    [JsonConverter(typeof(JsonStringEnumConverter))]
    //    public QueryPartCondition Condition { get; set; }
    //    public float Weight { get; set; }
    //    public string FieldLabel { get; set; }
    //    public IEnumerable<QueryPartItem> Items { get; set; }
    //}

    //public enum QueryPartCondition
    //{
    //    FAVORED,
    //    STRONGLY_FAVORED,
    //    REQUIRED,
    //    BANNED,
    //}

    //public class QueryPartItem
    //{
    //    public string Value { get; set; }
    //    public string Synonyms { get; set; }
    //    public string Label { get; set; }
    //    public IEnumerable<QueryPart> SubParts { get; set; }
    //}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Textkernel.Tx.Models.API.MatchV2.Request
{
    /// <summary>
    /// Document type for Search &amp; Match V2
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DocumentType
    {
        /// <summary>
        /// candidate documents (CVs/resumes)
        /// </summary>
        candidate,
        /// <summary>
        /// job documents (jobs)
        /// </summary>
        vacancy
    }

    /// <summary>
    /// Options for synonym expansion in queries
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SynonymExpansionMode
    {
        /// <summary>
        /// build the synonym map from the query only, expand the query. This is the default option.
        /// </summary>
        QUERY,
        /// <summary>
        /// build synonym map from both query and query parts, expand the query, prune and expand the query parts.
        /// </summary>
        QUERY_AND_QUERYPARTS,
        /// <summary>
        /// build synonym map from both query and query parts, expand the query, only prune the query parts.
        /// </summary>
        QUERY_AND_SYNONYMMAP
    }

    /// <summary>
    /// Options for a Search or Match request
    /// </summary>
    public class Options
    {
        /// <summary>
        /// The roles associated with the request. Defaults to "All" if none are provided.
        /// </summary>
        public IEnumerable<string> Roles { get; set; }

        /// <summary>
        /// If true then no result list is returned. Used for example when only the cloud is needed.
        /// </summary>
        public bool? SupressResultList { get; set; }
        /// <summary>
        /// If true then spelling correction on the input query is skipped.
        /// </summary>
        public bool? SupressCorrection { get; set; }

        /// <summary>
        /// Optional. If true search responses will be highlighted. Note: If a snippet was requested from the searcher as part of the
        /// result fields, the snippet can still contain highlighting even when false/null.
        /// </summary>
        public bool? Highlight { get; set; }

        /// <summary>
        /// Optional parameter to specify the number of result items (max 1500).
        /// Only use a high number in case you need a list of results for use in follow-up actions.
        /// Do not use a high number when presenting results in a user interface. Therefore, the max is 100 when used in combination
        /// with pagination parameters (<see cref="SearchAfter" /> or <see cref="ResultOffset" />). When this is larger than 100, facet counts
        /// and search term highlighting are not provided (<see cref="FacetCounts" /> and <see cref="Highlight" /> are implicitly set to false).
        /// If not provided, the pre-configured page size of the searcher will be used. This value is ignored for external searchers.
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// Used for pagination within Elastic Search Searcher (ignored for external searchers). Represents the sort values of the
        /// last item from the previous page. Must contain exactly the <see cref="Response.SearchResult.SearchAfter"/> returned from the previous page query.
        /// </summary>
        public string[] SearchAfter { get; set; }

        /// <summary>
        /// the result item offset used for pagination. For example, a value of 20 will skip the top 20 results and return the subsequent
        /// results starting with result 21. This is ignored if <see cref="SearchAfter" /> is set. <see cref="ResultOffset" /> + <see cref="PageSize" />
        /// cannot be more than 10000.
        /// </summary>
        public int? ResultOffset { get; set; }

        /// <summary>
        /// Optional List of language codes preferred by the user to filter synonyms by languages.
        /// </summary>
        public string[] SynonymLanguages { get; set; }

        /// <summary>
        /// Optional. If true the content of a query part item of type TEXT or LONG_TEXT (its term plus synonyms)
        /// that overlaps more than 75% with a previous query part of the same field and condition
        /// will be moved into the synonyms of that previous one.
        /// </summary>
        public bool? MergeOverLappingSynonyms { get; set; }

        /// <summary>
        /// Optional setting for synonym expansion mode. 
        /// </summary>
        public SynonymExpansionMode? SynonymExpansionMode { get; set; }

        /// <summary>
        /// Optional customization of the fields to be included in the results. If it consists of a single wildcard
        /// field marked by "*" (star) then all available fields will be returned. If empty then the default result
        /// field set will be returned. Remark: Reducing the returned fields this way, will not in general improve
        /// performance, since Search is optimized to return the standard configured field set. It is not possible to
        /// request system fields such as 'roles' or nested sub-fields.
        /// </summary>
        public string[] ResultFields { get; set; }

        /// <summary>
        /// Optional. If true the search responses will contain facet count. May result in slower response times.
        /// </summary>
        public bool? FacetCounts { get; set; }

        /// <summary>
        /// Optional sorting definition.
        /// </summary>
        public Sorting[] Sorting { get; set; }

        /// <summary>
        /// Optional flag indicating that the backend needs to use the Natural Language Query Service (NLQS) to interpret the query string.
        /// </summary>
        public bool UseNLQS { get; set; }
    }
}

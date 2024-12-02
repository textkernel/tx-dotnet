// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Textkernel.Tx.Models.API.MatchV2.Request;

namespace Textkernel.Tx.Models.API.MatchV2.Response
{
    /// <inheritdoc />
    public class SearchResponse : ApiResponse<SearchResponseValue> { }

    /// <summary>
    /// The <see cref="ApiResponse{T}.Value"/> from a Search/Match response
    /// </summary>
    public class SearchResponseValue
    {
        /// <summary>
        /// Actual list of search/match results
        /// </summary>
        public IEnumerable<SearchResponseItem> ResultItems { get; set; }

        /// <summary>
        /// All query parts of the executed query
        /// </summary>
        public IEnumerable<QueryPart> QueryParts { get; set; }

        /// <summary>
        /// Flag indicating that the new query part has been OR combined with a query part in the given <see cref="QueryParts"/>
        /// </summary>
        public bool IsOrCombined { get; set; }

        /// <summary>
        /// List of facets with their items and counts
        /// </summary>
        public IEnumerable<Facet> FacetCounts { get; set; }

        /// <summary>
        /// List of synonyms
        /// </summary>
        public IEnumerable<Synonym> Synonyms { get; set; }

        /// <summary>
        /// List of elastic search sort values that is used for pagination
        /// </summary>
        public IEnumerable<string> SearchAfter { get; set; }

        /// <summary>
        /// Total number of results found
        /// </summary>
        public long MatchSize { get; set; }

        /// <summary>
        /// Whether there are more result items for this query that did not fit in the response
        /// </summary>
        public bool HasMoreResults { get; set; }
    }

    /// <summary>
    /// A single result from a Search/Match response
    /// </summary>
    public class SearchResponseItem
    {
        /// <summary>
        /// Id of the document (given at indexing time)
        /// </summary>
        public string DocId { get; set; }

        /// <summary>
        /// The score of the result item with respect to the query
        /// </summary>
        public float Score { get; set; }

        /// <summary>
        /// Indicates which query parts are matched by the document
        /// </summary>
        public IEnumerable<int> QueryPartScores { get; set; }

        /// <summary>
        /// Map of field names with their corresponding values
        /// </summary>
        public IEnumerable<FieldValue> FieldValues { get; set; }
    }

    /// <summary>
    /// A Field name/value pair from a Search/Match result
    /// </summary>
    public class FieldValue
    {
        /// <summary>
        /// Name of a field
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Values of a field
        /// </summary>
        public IEnumerable<string> Value { get; set; }

        /// <summary>
        /// Nested field names with their corresponding values
        /// </summary>
        public IEnumerable<IEnumerable<FieldValue>> SubValues { get; set; }
    }
}

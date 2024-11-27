// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Textkernel.Tx.Models.API.MatchV2.Request
{
    /// <summary>
    /// Most MatchV2 requests have Roles
    /// </summary>
    public class MatchRequestBase
    {
        public IEnumerable<string> Roles { get; set; }
    }

    public class MatchRequest : MatchRequestBase
    {
        public Options Options { get; set; }
    }

    public class Options
    {
        public bool Highlight { get; set; }
        public int? PageSize { get; set; }
        public IEnumerable<string> SynonymLanguages { get; set; }
        public IEnumerable<string> ResultFields { get; set; }
        public bool FacetCounts { get; set; }
        public IEnumerable<Sorting> Sorting { get; set; }

        public IEnumerable<string> SearchAfter { get; set; }
        public bool SupressResultList { get; set; }
        public bool SupressCorrection { get; set; }

        public bool MergeOverlappingSynonyms { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SynonymExpansionMode SynonymExpansionMode { get; set; }
    }

    public enum SortOrder
    {
        ASCENDING,
        DESCENDING
    }

    public enum SynonymExpansionMode
    {
        QUERY,
        QUERY_AND_QUERYPARTS,
        QUERY_AND_SYNONYMMAP
    }

    public class Sorting
    {
        public string Field { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public SortOrder Order { get; set; }
        public string ReferenceLocation { get; set; }
    }

    public class SearchRequest : MatchRequest
    {
        public SearchQuery Query { get; set; }
    }

    public class SearchQuery
    {
        public string QueryString { get; set; }
        public IEnumerable<QueryPart> QueryParts { get; set; }
    }

    public class QueryPart
    {
        public string Field { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public QueryPartCondition Condition { get; set; }
        public float Weight { get; set; }
        public string FieldLabel { get; set; }
        public List<QueryPartItem> Items { get; set; }
    }

    public enum QueryPartCondition
    {
        FAVORED,
        STRONGLY_FAVORED,
        REQUIRED,
        BANNED,
    }

    public class QueryPartItem
    {
        public string Value { get; set; }
        public string Synonyms { get; set; }
        public string Label { get; set; }
    }
}

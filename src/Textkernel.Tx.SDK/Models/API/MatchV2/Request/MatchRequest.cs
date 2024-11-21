// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

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
        public bool SupressResultList { get; set; }
        public bool SupressCorrection { get; set; }
        public bool Highlight { get; set; }
        public int? PageSize { get; set; }
        public IEnumerable<string> SearchAfter { get; set; }
        public IEnumerable<string> SynonymLanguages { get; set; }
        public bool MergeOverlappingSynonyms { get; set; }
        public string SynonymExpansionMode { get; set; }
        public IEnumerable<string> ResultFields { get; set; }
        public bool FacetCounts { get; set; }
        public IEnumerable<Sorting> Sorting { get; set; }
    }

    public class Sorting
    {
        public string Field { get; set; }
        public string Order { get; set; }
        public string ReferenceLocation { get; set; }
    }

    public class SearchRequest : MatchRequest
    {
        public SearchQuery Request { get; set; }//TODO: should this be Query like ours or Request like theirs++++++++++++++++++++
    }

    public class SearchQuery
    {
        public string Query { get; set; }//TODO: should this be QueryString like ours or Query like theirs++++++++++++++++++++
        public IEnumerable<QueryPart> QueryParts { get; set; }
    }

    public class QueryPart
    {
        public string Field { get; set; }
        public string Condition { get; set; }
        public int Weight { get; set; }
        public string FieldLabel { get; set; }
        public List<QueryPartItem> Items { get; set; }
    }

    public class QueryPartItem
    {
        public string Value { get; set; }
        public List<string> Synonyms { get; set; }
        public string Label { get; set; }
    }
}

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
    public class SearchResponse : ApiResponse<SearchResult> { }


    //**************************************************************************************
    // EVERTHING BELOW HAS BEEN AUTO-GENERATED
    //**************************************************************************************


    /// <remarks/>
    public enum Datatype
    {

        /// <remarks/>
        TEXT,

        /// <remarks/>
        LONG_TEXT,

        /// <remarks/>
        UNANALYZED_TEXT,

        /// <remarks/>
        CODE,

        /// <remarks/>
        NUMERIC,

        /// <remarks/>
        DATE,

        /// <remarks/>
        LOCATION,

        /// <remarks/>
        OBJECT,

        /// <remarks/>
        TEXT_FINGERPRINT,
    }

    /// <remarks/>
    public enum Combinationtype
    {

        /// <remarks/>
        AND,

        /// <remarks/>
        OR,

        /// <remarks/>
        SINGLE,
    }

    /// <remarks/>
    public enum Guitype
    {

        /// <remarks/>
        FIXED,

        /// <remarks/>
        DYNAMIC,

        /// <remarks/>
        CLOUD,

        /// <remarks/>
        TEXTFIELD,

        /// <remarks/>
        LOCATION,

        /// <remarks/>
        PROJECT,

        /// <remarks/>
        GROUP,

        /// <remarks/>
        OBJECT,

        /// <remarks/>
        DATERANGE,

        /// <remarks/>
        NUMERICRANGE,
    }

    /// <remarks/>
    public enum Cloudtype
    {

        /// <remarks/>
        SPHERE,

        /// <remarks/>
        SPREAD,

        /// <remarks/>
        LIST,

        /// <remarks/>
        COLUMNS1,

        /// <remarks/>
        COLUMNS2,

        /// <remarks/>
        COLUMNS3,
    }

    /// <remarks/>
    public enum Condition
    {

        /// <remarks/>
        FAVORED,

        /// <remarks/>
        STRONGLY_FAVORED,

        /// <remarks/>
        REQUIRED,

        /// <remarks/>
        BANNED,
    }

    /// <remarks/>
    public partial class QueryContext
    {
        /// <remarks/>
        public string[] QueryTags { get; set; }

        /// <remarks/>
        public string MatchSource { get; set; }

        /// <remarks/>
        public string QueryID { get; set; }

        /// <remarks/>
        public string QuerySessionID { get; set; }
    }

    /// <remarks/>
    public partial class SynonymItem
    {
        /// <remarks/>
        public string Lang { get; set; }

        /// <remarks/>
        public bool AutoExpansion { get; set; }

        /// <remarks/>
        public string[] Values { get; set; }
    }

    /// <remarks/>
    public partial class SynonymSection
    {
        /// <remarks/>
        public string Name { get; set; }

        /// <remarks/>
        public bool Collapsed { get; set; }

        /// <remarks/>
        public SynonymItem[] Items { get; set; }
    }

    /// <remarks/>
    public partial class QueryPartItemCountsMapEntry
    {
        /// <remarks/>
        public string Key { get; set; }

        /// <remarks/>
        public FacetItem[] Value { get; set; }
    }

    /// <remarks/>
    public class FacetItem
    {
        /// <remarks/>
        public string Value { get; set; }

        /// <remarks/>
        public string Label { get; set; }

        /// <remarks/>
        public long Count { get; set; }

        /// <remarks/>
        public bool CountSpecified { get; set; }
    }

    /// <remarks/>
    public partial class Facet
    {
        /// <remarks/>
        public string Field { get; set; }

        /// <remarks/>
        public string FieldLabel { get; set; }

        /// <remarks/>
        public Datatype DataType { get; set; }

        /// <remarks/>
        public bool DataTypeSpecified { get; set; }

        /// <remarks/>
        public Combinationtype CombinationType { get; set; }

        /// <remarks/>
        public bool CombinationTypeSpecified { get; set; }

        /// <remarks/>
        public Guitype GuiType { get; set; }

        /// <remarks/>
        public bool GuiTypeSpecified { get; set; }

        /// <remarks/>
        public Cloudtype CloudType { get; set; }

        /// <remarks/>
        public bool CloudTypeSpecified { get; set; }

        /// <remarks/>
        public bool Collapsed { get; set; }

        /// <remarks/>
        public bool CollapsedSpecified { get; set; }

        /// <remarks/>
        public bool HideFacetIfAllZero { get; set; }

        /// <remarks/>
        public bool HideFacetIfAllZeroSpecified { get; set; }

        /// <remarks/>
        public bool HideZeroCountItems { get; set; }

        /// <remarks/>
        public bool HideZeroCountItemsSpecified { get; set; }

        /// <remarks/>
        public Condition DefaultCondition { get; set; }

        /// <remarks/>
        public bool DefaultConditionSpecified { get; set; }

        /// <remarks/>
        public bool HideConditionWidget { get; set; }

        /// <remarks/>
        public bool HideConditionWidgetSpecified { get; set; }

        /// <remarks/>
        public bool ReverseItemOrder { get; set; }

        /// <remarks/>
        public bool ReverseItemOrderSpecified { get; set; }

        /// <remarks/>
        public FacetItem[] Items { get; set; }

        /// <remarks/>
        public string[] Distances { get; set; }

        /// <remarks/>
        public string DefaultDistance { get; set; }

        /// <remarks/>
        public bool TextInputOnFacet { get; set; }

        /// <remarks/>
        public bool TextInputOnFacetSpecified { get; set; }

        /// <remarks/>
        public string Format { get; set; }

        /// <remarks/>
        public string[] NestedFields { get; set; }

        /// <remarks/>
        public bool ShowOnFacet { get; set; }

        /// <remarks/>
        public bool ShowOnFacetSpecified { get; set; }

        /// <remarks/>
        public bool ShowOnWidget { get; set; }

        /// <remarks/>
        public bool ShowOnWidgetSpecified { get; set; }

        /// <remarks/>
        public string GroupFieldName { get; set; }

        /// <remarks/>
        public string GroupName { get; set; }

        /// <remarks/>
        public Facet[] ChildFacets { get; set; }

        /// <remarks/>
        public Facet[] SubFacets { get; set; }

        /// <remarks/>
        public string SearchEngine { get; set; }

        /// <remarks/>
        public int DropdownSwitcherThreshold { get; set; }

        /// <remarks/>
        public bool DropdownSwitcherThresholdSpecified { get; set; }

        /// <remarks/>
        public QueryPartItemCountsMapEntry[] QueryPartItemCounts { get; set; }
    }

    /// <remarks/>
    public partial class FieldValue
    {
        /// <remarks/>
        public string Value { get; set; }

        /// <remarks/>
        public FieldValueEntry[] SubValues { get; set; }
    }

    /// <remarks/>
    public partial class FieldValueEntry
    {
        /// <remarks/>
        public string Key { get; set; }

        /// <remarks/>
        public string[] Value { get; set; }
    }

    /// <remarks/>
    public partial class ResultItem
    {
        /// <remarks/>
        public string DocID { get; set; }

        /// <remarks/>
        public ResultItemEntry[] FieldValues { get; set; }

        /// <remarks/>
        public float Score { get; set; }

        /// <remarks/>
        public System.Nullable<int>[] QueryPartScores { get; set; }
    }

    /// <remarks/>
    public partial class ResultItemEntry
    {
        /// <remarks/>
        public string Key { get; set; }

        /// <remarks/>
        public FieldValue[] Value { get; set; }
    }

    /// <remarks/>
    public partial class SearchResult
    {
        /// <remarks/>
        public long MatchSize { get; set; }

        /// <remarks/>
        public bool HasMoreResults { get; set; }

        /// <remarks/>
        public ResultItem[] ResultItems { get; set; }

        /// <remarks/>
        public QueryPart[] QueryParts { get; set; }

        /// <remarks/>
        public QueryPart[] NewQueryParts { get; set; }

        /// <remarks/>
        public bool IsOrCombined { get; set; }

        /// <remarks/>
        public string TransformedQuery { get; set; }

        /// <remarks/>
        public Facet[] FacetCounts { get; set; }

        /// <remarks/>
        public string SearchEngine { get; set; }

        /// <remarks/>
        public SearchResultEntry[] Synonyms { get; set; }

        /// <remarks/>
        public QueryContext QueryContext { get; set; }

        /// <remarks/>
        public string[] SearchAfter { get; set; }

        /// <remarks/>
        public long EsTimeMs { get; set; }

        /// <remarks/>
        public bool EsTimeMsSpecified { get; set; }

        /// <remarks/>
        public string[] Warning { get; set; }
    }

    /// <remarks/>
    public partial class QueryPart
    {
        /// <remarks/>
        public string Field { get; set; }

        /// <remarks/>
        public Condition Condition { get; set; }

        /// <remarks/>
        public bool ConditionSpecified { get; set; }

        /// <remarks/>
        public float Weight { get; set; }

        /// <remarks/>
        public bool WeightSpecified { get; set; }

        /// <remarks/>
        public QueryPartItem[] Items { get; set; }

        /// <remarks/>
        public string FieldLabel { get; set; }

        /// <remarks/>
        public string SearchEngine { get; set; }
    }

    /// <remarks/>
    public partial class QueryPartItem
    {
        /// <remarks/>
        public string Value { get; set; }

        /// <remarks/>
        public string[] Synonyms { get; set; }

        /// <remarks/>
        public QueryPart[] SubParts { get; set; }

        /// <remarks/>
        public string Label { get; set; }
    }

    /// <remarks/>
    public partial class SearchResultEntry
    {
        /// <remarks/>
        public string Key { get; set; }

        /// <remarks/>
        public SynonymSection[] Value { get; set; }
    }

    /// <remarks/>
    public partial class ResultSorting
    {
        /// <remarks/>
        public string Field { get; set; }

        /// <remarks/>
        public SortOrder Order { get; set; }

        /// <remarks/>
        public bool OrderSpecified { get; set; }

        /// <remarks/>
        public string ReferenceLocation { get; set; }

        /// <remarks/>
        public ResultSorting SubSorting { get; set; }
    }

    /// <remarks/>
    public partial class SearchRequest
    {
        /// <remarks/>
        public string Query { get; set; }

        /// <remarks/>
        public QueryPart[] QueryParts { get; set; }

        /// <remarks/>
        public int ResultOffset { get; set; }

        /// <remarks/>
        public bool ResultOffsetSpecified { get; set; }

        /// <remarks/>
        public string ProvideTagcloud { get; set; }

        /// <remarks/>
        public bool SuppressResultList { get; set; }

        /// <remarks/>
        public bool SuppressResultListSpecified { get; set; }

        /// <remarks/>
        public string SearchEngine { get; set; }

        /// <remarks/>
        public string OutputLanguage { get; set; }

        /// <remarks/>
        public bool SuppressCorrection { get; set; }

        /// <remarks/>
        public bool SuppressCorrectionSpecified { get; set; }

        /// <remarks/>
        public ResultSorting Sorting { get; set; }

        /// <remarks/>
        public QueryPart[] HiddenQueryParts { get; set; }

        /// <remarks/>
        public string Ip { get; set; }

        /// <remarks/>
        public string Agent { get; set; }

        /// <remarks/>
        public string Uuid { get; set; }

        /// <remarks/>
        public bool SuppressFacetCounts { get; set; }

        /// <remarks/>
        public bool SuppressFacetCountsSpecified { get; set; }

        /// <remarks/>
        public bool SuppressHighlighting { get; set; }

        /// <remarks/>
        public bool SuppressHighlightingSpecified { get; set; }

        /// <remarks/>
        public bool MergeOverlappingSynonyms { get; set; }

        /// <remarks/>
        public bool MergeOverlappingSynonymsSpecified { get; set; }

        /// <remarks/>
        public string[] SynonymLanguages { get; set; }

        /// <remarks/>
        public string[] ResultFields { get; set; }

        /// <remarks/>
        public QueryContext QueryContext { get; set; }

        /// <remarks/>
        public SynonymExpansionMode SynonymExpansionMode { get; set; }

        /// <remarks/>
        public bool SynonymExpansionModeSpecified { get; set; }

        /// <remarks/>
        public string[] SearchAfter { get; set; }

        /// <remarks/>
        public int PageSize { get; set; }

        /// <remarks/>
        public bool PageSizeSpecified { get; set; }

        /// <remarks/>
        public bool UseNlqs { get; set; }

        /// <remarks/>
        public bool UseNlqsSpecified { get; set; }
    }

}

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
    /// <summary>
    /// Defines how multiple facet selections are combined in a REQUIRED conditional query
    /// </summary>
    public enum FacetCombinationType
    {
        /// <summary>
        /// Results must contain one of the selections (e.g. Master OR Bachelor level)
        /// </summary>
        OR,
        /// <summary>
        /// Results must contain all selections (e.g. English AND French language skills)
        /// </summary>
        AND,

        /// <summary>
        /// The facet is restricted to contain only one selection
        /// </summary>
        SINGLE
    }

    /// <summary>
    /// Determines how a facet should be displayed
    /// </summary>
    public enum FacetGuiType
    {
        FIXED,
        DYNAMIC,
        CLOUD,
        TEXTFIELD,
        LOCATION,
        PROJECT,
        GROUP,
        OBJECT,
        DATERANGE,
        NUMERICRANGE
    }

    /// <summary>
    /// Data type of a facet field
    /// </summary>
    public enum FacetDataType
    {
        TEXT,
        LONG_TEXT,
        UNANALYZED_TEXT,
        CODE,
        NUMERIC,
        DATE,
        LOCATION,
        OBJECT,
        TEXT_FINGERPRINT
    }

    /// <summary>
    /// Display layout of a facet cloud when <see cref="Facet.GuiType" /> is <see cref="FacetGuiType.CLOUD"/>
    /// </summary>
    public enum FacetCloudType
    {
        /// <summary>
        /// 
        /// </summary>
        SPHERE,
        SPREAD,
        LIST,
        COLUMNS1,
        COLUMNS2,
        COLUMNS3
    }

    /// <summary>
    /// A search/match Facet
    /// </summary>
    public class Facet
    {
        /// <summary>
        /// Field name
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Field label
        /// </summary>
        public string FieldLabel { get; set; }

        /// <summary>
        /// The data type of the facet field
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FacetDataType DataType { get; set; }

        /// <summary>
        /// Defines how multiple facet selections are combined in a REQUIRED conditional query
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FacetCombinationType CombinationType { get; set; }

        /// <summary>
        /// Describes how to display a facet
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FacetGuiType GuiType { get; set; }

        /// <summary>
        /// Determines the display layout of the cloud
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public FacetCloudType CloudType { get; set; }

        /// <summary>
        /// If <see langword="true"/> the facet starts out collapsed instead of expanded
        /// </summary>
        public bool Collapsed { get; set; }

        /// <summary>
        /// If <see langword="true"/> the facet will be hidden when all items have zero counts
        /// </summary>
        public bool HideFacetIfAllZero { get; set; }

        /// <summary>
        /// If true items having a zero count will be hidden. This cannot be set when the fixed facet has
        /// <see cref="ShowOnWidget"/> set to <see langword="true"/>. This is also not advised on FIXED and DYNAMIC type 
        /// facets with an expected initial returned list of items higher than 25 as it hides selected values when 
        /// in the 'autocomplete select mode'.
        /// </summary>
        public bool HideZeroCountItems { get; set; }

        /// <summary>
        /// Default condition type
        /// </summary>
        public QueryPartCondition DefaultCondition { get; set; }

        /// <summary>
        /// If <see langword="true"/> the condition widget will be hidden.
        /// </summary>
        public bool HideConditionWidget { get; set; }

        /// <summary>
        /// If <see langword="true"/>, items will be displayed in reverse order.
        /// </summary>
        public bool ReverseItemOrder { get; set; }

        /// <summary>
        /// The selectable distances when <see cref="DataType"/> is <see cref="FacetDataType.LOCATION"/>
        /// </summary>
        /// <remarks>km</remarks>
        public IEnumerable<string> Distances { get; set; }

        /// <summary>
        /// Optional default distance within a LOCATION facet's distance selector (if not defined it is the first entry in <see cref="Distances"/>).
        /// </summary>
        /// <remarks>km</remarks>
        public string DefaultDistance { get; set; }

        /// <summary>
        /// Determines whether for CLOUD facets the input box is displayed in the facet bar, otherwise within the tag-cloud.
        /// </summary>
        public bool TextInputOnFacet { get; set; }

        public string Format { get; set; }

        /// <summary>
        /// Nested fields
        /// </summary>
        public IEnumerable<string> NestedFields { get; set; }

        /// <summary>
        /// Determines whether for CLOUD facets the input box is displayed in the facet side bar
        /// </summary>
        public bool ShowOnFacet { get; set; }

        /// <summary>
        /// Is displayed in the query part widget for refinement
        /// </summary>
        public bool ShowOnWidget { get; set; }

        /// <summary>
        /// Group field name
        /// </summary>
        public string GroupFieldName { get; set; }

        /// <summary>
        /// The translated name of the Facet group
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Items in this group
        /// </summary>
        public IEnumerable<FacetItem> Items { get; set; }

        /// <summary>
        /// The facets belonging to this group
        /// </summary>
        public IEnumerable<Facet> ChildFacets { get; set; }

        public IEnumerable<Facet> SubFacets { get; set; }

        /// <summary>
        ///  Query part item counts
        /// </summary>
        public IEnumerable<QueryPartPosition> QueryPartItemCounts { get; set; }
    }

    /// <summary>
    /// Facet item information
    /// </summary>
    public class FacetItem
    {
        /// <summary>
        /// The search value of the item
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The display label of the item
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The count of the item
        /// </summary>
        public long Count { get; set; }
    }

    /// <summary>
    /// Query part item counts
    /// </summary>
    public class QueryPartPosition
    {
        /// <summary>
        /// Query part position index
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// Facet items
        /// </summary>
        public IEnumerable<FacetItem> Items { get; set; }
    }
}

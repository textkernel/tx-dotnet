// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sovren.Models.API.Indexes
{
    /// <summary>
    /// A method to use when updating custom value ids on a document
    /// </summary>
    public enum CustomValueIdsMethod
    {
        /// <summary>
        /// Deletes the specified custom value ids from a document
        /// </summary>
        Delete,

        /// <summary>
        /// Adds the specified custom value ids to a document (in addition to any existing)
        /// </summary>
        Add,

        /// <summary>
        /// Overwrites any existing custom value ids with the ones specified
        /// </summary>
        Overwrite
    }

    /// <summary>
    /// Request body to update (add/remove/overwrite) custom value ids on an indexed document
    /// </summary>
    public class UpdateCustomValueIdsRequest
    {
        /// <summary>
        /// The custom value ids to add/delete/etc
        /// </summary>
        public List<string> CustomValues { get; set; }

        /// <summary>
        /// Which method to use for the specified custom value ids
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CustomValueIdsMethod Method { get; set; }
    }
}

// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sovren.Models.API.Indexes
{
    /// <summary>
    /// A method to use when updating user-defined tags on a document
    /// </summary>
    public enum UserDefinedTagsMethod
    {
        /// <summary>
        /// Deletes the specified user-defined tags from a document
        /// </summary>
        Delete,

        /// <summary>
        /// Adds the specified user-defined tags to a document (in addition to any existing)
        /// </summary>
        Add,

        /// <summary>
        /// Overwrites any existing user-defined tags with the specified tags
        /// </summary>
        Overwrite
    }

    /// <summary>
    /// Request body to update (add/remove/overwrite) user-defined tags on an indexed document
    /// </summary>
    public class UpdateUserDefinedTagsRequest
    {
        /// <summary>
        /// The user-defined tags to add/delete/etc
        /// </summary>
        public List<string> UserDefinedTags { get; set; }

        /// <summary>
        /// Which method to use for the specified user-defined tags
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserDefinedTagsMethod Method { get; set; }
    }
}

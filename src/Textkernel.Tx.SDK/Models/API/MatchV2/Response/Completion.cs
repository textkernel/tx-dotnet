// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Textkernel.Tx.Models.API.MatchV2.Response
{
    /// <summary>
    /// This object describes a single auto-completion result. The auto-completion service returns a
    /// list of these completions. Besides the actual suggested completion item, it contains the field
    /// name and field label of search field this suggestion should be targeted to.
    /// </summary>
    public class Completion
    {
        /// <summary>
        /// The field name.
        /// </summary>
        public string Field {  get; set; }
        /// <summary>
        /// The display label of the field.
        /// </summary>
        public string FieldLabel { get; set; }
        /// <summary>
        /// The completion suggestion.
        /// </summary>
        public string Item { get; set; }
        /// <summary>
        /// Same as the item, but with highlighting tags showing the matching parts of the suggestion.
        /// </summary>
        public string ItemHighlighted { get; set; }
    }
}

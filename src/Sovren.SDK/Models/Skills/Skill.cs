// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Collections.Generic;

namespace Sovren.Models.Skills
{
    /// <summary>
    /// A skill listed in a resume or job
    /// </summary>
    public abstract class Skill
    {
        /// <summary>
        /// The Id of the skill
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the skill
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Whether or not this skill was found verbatim in the text
        /// </summary>
        public bool ExistsInText { get; set; }
    }
}

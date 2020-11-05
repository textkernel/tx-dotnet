// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Collections.Generic;

namespace Sovren.Models.API.Matching.Request
{
    /// <summary>
    /// Settings for searching/matching
    /// </summary>
    public class SearchMatchSettings
    {
        /// <summary>
        /// Set to <see langword="true"/> to turn off variation matches in job titles.
        /// </summary>
        public bool PositionTitlesMustHaveAnExactMatch { get; set; }
    }

    /// <summary>
    /// Base class for match/search requests
    /// </summary>
    public abstract class SearchMatchRequestBase
    {
        /// <summary>
        /// The ids of the indexes in which you want to find results (case-insensitive).
        /// </summary>
        public List<string> IndexIdsToSearchInto { get; set; }

        /// <summary>
        /// The settings to use during searching/matching queries
        /// </summary>
        public SearchMatchSettings Settings { get; set; }

        /// <summary>
        /// Required criteria for the result set.
        /// </summary>
        public FilterCriteria FilterCriteria { get; set; }
    }
}

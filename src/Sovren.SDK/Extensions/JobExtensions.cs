// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.Job;
using System.Text.Json;

namespace Sovren
{
    /// <summary></summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public static class JobExtensions
    {
        /// <summary>
        /// Outputs a JSON string that can be saved to disk or any other data storage.
        /// <br/>NOTE: be sure to save with UTF-8 encoding!
        /// </summary>
        /// <param name="formatted"><see langword="true"/> for pretty-printing</param>
        /// <param name="job">the job</param>
        public static string ToJson(this ParsedJob job, bool formatted = false)
        {
            JsonSerializerOptions options = SovrenJsonSerialization.DefaultOptions;
            options.WriteIndented = formatted;
            return JsonSerializer.Serialize(job, options);
        }
    }
}

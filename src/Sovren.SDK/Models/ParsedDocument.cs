// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Text.Json;

namespace Sovren.Models
{
    /// <summary>
    /// Base class for parsed resumes/jobs
    /// </summary>
    public abstract class ParsedDocument
    {
        /// <summary>
        /// Returns the job as a formatted json string
        /// </summary>
        public override string ToString() => this.ToJson(true);

        /// <summary>
        /// Outputs a JSON string that can be saved to disk or any other data storage.
        /// <br/>NOTE: be sure to save with UTF-8 encoding!
        /// </summary>
        /// <param name="formatted"><see langword="true"/> for pretty-printing</param>
        public string ToJson(bool formatted = false)
        {
            JsonSerializerOptions options = SovrenJsonSerialization.DefaultOptions;
            options.WriteIndented = formatted;
            return JsonSerializer.Serialize(this, options);
        }
    }
}

﻿// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System.Text.Json;

namespace Textkernel.Tx.Models
{
    /// <summary>
    /// Base class for parsed resumes/jobs
    /// </summary>
    public abstract class ParsedDocument
    {
        /// <summary>
        /// Returns the job as a formatted json string
        /// </summary>
        public override string ToString() => ToJson(true);

        /// <summary>
        /// Outputs a JSON string that can be saved to disk or any other data storage.
        /// <br/>NOTE: be sure to save with UTF-8 encoding!
        /// </summary>
        /// <param name="formatted"><see langword="true"/> for pretty-printing</param>
        public abstract string ToJson(bool formatted);

        /// <summary>
        /// Save the json to disk using UTF-8 encoding
        /// </summary>
        /// <param name="filepath">The file to save to</param>
        /// <param name="formatted"><see langword="true"/> for pretty-printing</param>
        public void ToFile(string filepath, bool formatted)
        {
            string json = ToJson(formatted);
            if (json != null)
            {
                System.IO.File.WriteAllText(filepath, json, System.Text.Encoding.UTF8);
            }
        }
    }
}

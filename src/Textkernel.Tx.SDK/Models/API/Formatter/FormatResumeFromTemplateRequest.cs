// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.Resume;
using Textkernel.Tx.Models.API.Parsing;
using System.Text.Json.Serialization;

namespace Textkernel.Tx.Models.API.Formatter
{
	/// <summary>
	/// Request body for the Format Resume endpoint
	/// </summary>
	public class FormatResumeFromTemplateRequest
	{
		/// <summary>
		/// Should be provided from a parse response. See <see cref="ParseResumeResponseValue.ResumeData"/> 
		/// </summary>
		public ParsedResume ResumeData { get; set; }

        /// <summary>
        /// The output document type
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ResumeType OutputType { get; set; }

		/// <summary>
		/// A base64-encoded byte[] that is the template document in DOCX format. 
		/// See <see cref="System.IO.File.ReadAllBytes(string)"/> and <see cref="System.Convert.ToBase64String(byte[])"/>
		/// </summary>
        public string Template { get; set; }

        /// <summary>
        /// Any data that the template needs that is not in the extracted CV data. For example:
        /// <code>
        /// CustomData = new
        /// {
        ///     CandidateId = "12345",
        ///     DateApplied = DateTime.Now
        /// }
        /// </code>
        /// </summary>
        public object CustomData { get; set; }
    }
}

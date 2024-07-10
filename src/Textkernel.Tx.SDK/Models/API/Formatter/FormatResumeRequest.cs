// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.Resume;
using Textkernel.Tx.Models.API.Parsing;
using System.Text.Json.Serialization;
using System;

namespace Textkernel.Tx.Models.API.Formatter
{
	/// <summary>
	/// Request body for the Format Resume endpoint
	/// </summary>
	public class FormatResumeRequest
	{
		/// <summary>
		/// Either<see cref="ParseResumeResponseValue.ResumeData"/> to include the candidate's personal information or
		/// <see cref="ParseResumeResponseValue.RedactedResumeData"/> to exclude it.
		/// </summary>
		public ParsedResume ResumeData { get; set; }

		/// <summary>
		/// Options for content/formatting of the generated resume document.
		/// </summary>
		public FormatResumeOptions Options { get; set; } = new FormatResumeOptions();

		/// <summary>
		/// Creates a request to call the Resume Formatter endpoint.
		/// </summary>
		/// <param name="resume">
		/// Either<see cref="ParseResumeResponseValue.ResumeData"/> to include the candidate's personal information or
		/// <see cref="ParseResumeResponseValue.RedactedResumeData"/> to exclude it.
        /// </param>
		/// <param name="docType">The output document type</param>
		public FormatResumeRequest(ParsedResume resume, ResumeType docType)
        {
			ResumeData = resume;
			Options.OutputType = docType;
        }
	}

    /// <summary>
	/// Request body for the Format Resume With Template endpoint
	/// </summary>
	public class FormatResumeWithTemplateRequest
    {
        /// <summary>
        /// The <see cref="ParseResumeResponseValue.ResumeData"/> from a parse API call
        /// </summary>
        public ParsedResume ResumeData { get; set; }

        /// <summary>
		/// The output document type
		/// </summary>
		[JsonConverter(typeof(JsonStringEnumConverter))]
        public ResumeType OutputType { get; set; }

        /// <summary>
        /// A base64-encoded string of the DOCX template document file bytes. This should use the standard 'base64' 
		/// encoding as defined in RFC 4648 Section 4 (not the 'base64url' variant). .NET users can use the 
		/// <see cref="Convert.ToBase64String(byte[])"/> method. For more information on creating custom templates,
		/// see <see href="https://developer.textkernel.com/tx-platform/v10/resume-formatter/creating-custom-templates/">here</see>.
        /// </summary>
        public string Template { get; set; }

        /// <summary>
        /// Any data that the template needs that is not in the extracted CV data. For example:
		/// <code>
		/// {
        ///     "CandidateId": "12345",
        ///     "DateApplied": "2024-02-05"
        /// }
		/// </code>
        /// </summary>
        public object CustomData { get; set; }

        /// <summary>
        /// Creates a request to call the Resume Formatter endpoint with a provided template document.
        /// </summary>
        /// <param name="resume">The <see cref="ParseResumeResponseValue.ResumeData"/> from a parse API call</param>
        /// <param name="docType">The output document type</param>
        /// <param name="templatePath">The path to the template DOCX file on disk</param>
        public FormatResumeWithTemplateRequest(ParsedResume resume, string templatePath, ResumeType docType)
        {
            ResumeData = resume;
            OutputType = docType;
			Template = new Document(templatePath).AsBase64;
        }

        /// <summary>
        /// Creates a request to call the Resume Formatter endpoint with a provided template document.
        /// </summary>
        /// <param name="resume">The <see cref="ParseResumeResponseValue.ResumeData"/> from a parse API call</param>
        /// <param name="docType">The output document type</param>
        /// <param name="templateFileBytes">The bytes of the template DOCX file</param>
        public FormatResumeWithTemplateRequest(ParsedResume resume, byte[] templateFileBytes, ResumeType docType)
        {
            ResumeData = resume;
            OutputType = docType;
            Template = new Document(templateFileBytes, DateTime.Today).AsBase64;
        }
    }
}

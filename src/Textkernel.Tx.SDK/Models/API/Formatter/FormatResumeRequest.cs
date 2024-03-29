﻿// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.Resume;
using Textkernel.Tx.Models.API.Parsing;

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
		/// <param name="docType">The output document type</param>
		/// </param>
		public FormatResumeRequest(ParsedResume resume, ResumeType docType)
        {
			ResumeData = resume;
			Options.OutputType = docType;
        }
	}
}

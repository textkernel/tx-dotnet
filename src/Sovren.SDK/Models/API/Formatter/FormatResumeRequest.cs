// Copyright © 2021 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.Resume;
using Sovren.Models.API.Parsing;

namespace Sovren.Models.API.Formatter
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

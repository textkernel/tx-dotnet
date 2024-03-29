﻿// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;

namespace Textkernel.Tx.Models.API.Formatter
{
	/// <inheritdoc/>
	public class FormatResumeResponse : ApiResponse<FormatResumeResponseValue> { }

	/// <summary>
	/// The <see cref="ApiResponse{T}.Value"/> from a 'FormatResume' response
	/// </summary>
	public class FormatResumeResponseValue
	{
		/// <summary>
		/// The formatted resume document (either PDF or DOCX).
		/// This is a <see langword="byte"/>[] as a Base64-encoded string. You can use
		/// <see cref="Convert.FromBase64String(string)"/> to turn this back into a <see langword="byte"/>[]
		/// </summary>
		public string DocumentAsBase64String { get; set; }
	}
}

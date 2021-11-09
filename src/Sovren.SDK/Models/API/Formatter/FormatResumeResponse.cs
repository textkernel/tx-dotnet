// Copyright © 2021 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;

namespace Sovren.Models.API.Formatter
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

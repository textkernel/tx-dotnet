// Copyright © 2021 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.IO;
using System.Text.Json.Serialization;

namespace Sovren.Models.API.Formatter
{
	/// <summary>
	/// The output document type for a format resume request
	/// </summary>
	public enum ResumeType
	{
		/// <summary>
		/// A Microsoft Word DOCX file
		/// </summary>
		DOCX,

		/// <summary>
		/// A PDF file
		/// </summary>
		PDF
	}

	/// <summary>
	/// Options for where company info should be placed in a formatted resume
	/// </summary>
	public enum CompanyInfoPlacement
	{
		/// <summary>
		/// The company info is only placed at the top of the first page in the document
		/// </summary>
		FirstHeader,

		/// <summary>
		/// The company info is placed at the top of every page in the document
		/// </summary>
		AllHeaders
	}

	/// <summary>
	/// Options for showing/hiding employer names in a formatted resume
	/// </summary>
	public enum EmployerNameOptions
	{
		/// <summary>
		/// All company names are shown
		/// </summary>
		ShowAll,

		/// <summary>
		/// All company names are redacted
		/// </summary>
		HideAll,

		/// <summary>
		/// Only company names in the most recent (or any 'current) positions are redacted
		/// </summary>
		HideRecentAndCurrent
	}

	/// <summary>
	/// A company logo in a formatted resume
	/// </summary>
	public class ResumeLogo
	{
		/// <summary>
		/// The logo file <see langword="byte"/>[] as a Base64-encoded string
		/// </summary>
		public string Logo { get; set; }

		/// <summary>
		/// The width of the logo in points (1/72 of an inch)
		/// </summary>
		public int Width { get; set; }

		/// <summary>
		/// The height of the logo in points (1/72 of an inch)
		/// </summary>
		public int Height { get; set; }

		/// <summary>
		/// Create a ResumeLogo. If you use this constructor you must set <see cref="Logo"/>
		/// </summary>
		public ResumeLogo() { }

		/// <summary>
		/// Create a logo from a <see langword="byte"/>[]
		/// </summary>
		/// <param name="bytes">The image file byte array</param>
		/// <param name="width">The width of the logo in points (1/72 of an inch)</param>
		/// <param name="height">The height of the logo in points (1/72 of an inch)</param>
		public ResumeLogo(byte[] bytes, int width, int height)
		{
			Logo = Convert.ToBase64String(bytes);
			Width = width;
			Height = height;
		}

		/// <summary>
		/// Create a logo from a file on disk.
		/// </summary>
		/// <param name="filePath">The path to the image file (PNG, JPG, SVG)</param>
		/// <param name="width">The width of the logo in points (1/72 of an inch)</param>
		/// <param name="height">The height of the logo in points (1/72 of an inch)</param>
		public ResumeLogo(string filePath, int width, int height)
		{
			byte[] bytes = File.ReadAllBytes(filePath);
			Logo = Convert.ToBase64String(bytes);
			Width = width;
			Height = height;
		}
	}

	/// <summary>
	/// Options for putting company/firm information on the formatted resume header.
	/// </summary>
	public class CompanyInfo
	{
		/// <summary>
		/// The company name to include in the resume header.
		/// </summary>
		public string CompanyName { get; set; }

		/// <summary>
		/// The contact phone to include in the resume header.
		/// </summary>
		public string Phone { get; set; }

		/// <summary>
		/// The contact email to include in the resume header.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// The candidate id for this resume to include in the resume header.
		/// </summary>
		public string CandidateId { get; set; }

		/// <summary>
		/// The company logo to include in the resume header.
		/// </summary>
		public ResumeLogo Logo { get; set; }

		/// <summary>
		/// Any information (such as confidentiality clause) to put in the resume footer.
		/// </summary>
		public string Footer { get; set; }

		/// <summary>
		/// Where to put the company info in the formatted resume document.
		/// </summary>
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public CompanyInfoPlacement Placement { get; set; }
	}

	/// <summary>
	/// Options to show/hide Sovren metadata on the generated resume.
	/// </summary>
	public class MetadataOptions
	{
		/// <summary>
		/// Whether to hide the Sovren candidate summary in the generated resume.
		/// </summary>
		public bool HideCandidateSummary { get; set; }

		/// <summary>
		/// Whether to hide the 'Top Skills' tree in the generated resume.
		/// </summary>
		public bool HideTopSkills { get; set; }
	}

	/// <summary>
	/// Options to include/exclude certain data from the work history section of the resume.
	/// </summary>
	public class WorkHistoryOptions
	{
		/// <summary>
		/// The min/max number of positions/jobs to show.
		/// </summary>
		public Matching.Request.IntegerRange NumPositions { get; set; }

		/// <summary>
		/// Option for showing/redacting company names
		/// </summary>
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public EmployerNameOptions EmployerNames { get; set; }

		/// <summary>
		/// Hides any positions from the original resume that ended longer than N years ago.
		/// This only applies in the case that <see cref="NumPositions"/>.Minimum 
		/// has been met. The default for this value is 10 years.
		/// </summary>
		public int MaxYearsOfWorkHistory { get; set; }
	}

	/// <summary>
	/// Options for content/formatting of the generated resume document in a format resume request.
	/// </summary>
	public class FormatResumeOptions
	{
		/// <summary>
		/// The output document type
		/// </summary>
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public ResumeType OutputType { get; set; }

		/// <summary>
		/// Options for putting company/firm information on the formatted resume header.
		/// </summary>
		public CompanyInfo CompanyInfo { get; set; }

		/// <summary>
		/// Options to include/exclude certain data from the work history section of the resume.
		/// </summary>
		public WorkHistoryOptions WorkHistory { get; set; }

		/// <summary>
		/// Options to show/hide Sovren metadata on the generated resume.
		/// </summary>
		public MetadataOptions Metadata { get; set; }
	}
}

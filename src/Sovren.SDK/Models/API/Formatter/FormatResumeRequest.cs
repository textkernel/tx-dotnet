using Sovren.Models.Resume;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

namespace Sovren.Models.API.Generator
{
	public enum ResumeType
	{
		DOCX,
		PDF
	}

	public enum CompanyInfoPlacement
	{
		CoverPage,
		FirstHeader,
		AllHeaders
	}

	public enum EmployerNameOptions
	{
		ShowAll,
		HideAll,
		HideRecentAndCurrent
	}

	public class ResumeLogo
	{
		public string Logo { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

        public ResumeLogo() { }

        public ResumeLogo(byte[] bytes, int width, int height)
        {
			Logo = Convert.ToBase64String(bytes);
			Width = width;
			Height = height;
        }

        public ResumeLogo(string filePath, int width, int height)
        {
			byte[] bytes = File.ReadAllBytes(filePath);
			Logo = Convert.ToBase64String(bytes);
			Width = width;
			Height = height;
		}
	}

	public class CompanyInfo
	{
		public string CompanyName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string CandidateId { get; set; }

		public ResumeLogo Logo { get; set; }

		public string Footer { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public CompanyInfoPlacement Placement { get; set; }
	}

	public class MetadataOptions
	{
		public bool HideCandidateSummary { get; set; }
		public bool HideTopSkills { get; set; }
	}

	public class WorkHistoryOptions
	{
		public Matching.Request.IntegerRange NumPositions { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public EmployerNameOptions EmployerNames { get; set; }

		/// <summary>
		/// only applies to jobs in excess of <see cref="NumPositions"/>.Minimum
		/// </summary>
		public int MaxYearsOfWorkHistory { get; set; }
	}

	public class FormatResumeOptions
	{
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public ResumeType OutputType { get; set; }

		public CompanyInfo CompanyInfo { get; set; }
		public WorkHistoryOptions WorkHistory { get; set; }
		public MetadataOptions Metadata { get; set; }
	}

	public class FormatResumeRequest
	{
		public ParsedResume ResumeData { get; set; }
		public FormatResumeOptions Options { get; set; } = new FormatResumeOptions();

        public FormatResumeRequest(ParsedResume resume)
        {
			ResumeData = resume;
        }
	}

	public class FormatResumeResponse : ApiResponse<FormatResumeResponseValue> { }

	public class FormatResumeResponseValue
	{
		public string DocumentAsBase64String { get; set; }
	}
}

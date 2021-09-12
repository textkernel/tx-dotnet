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
		PDF,
		HTML
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

	public class GenerateResumeOptions
	{
		public ResumeLogo Logo { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public ResumeType OutputType { get; set; }
	}

	public class GenerateResumeRequest
	{
		public ParsedResume Resume { get; set; }
		public GenerateResumeOptions Options { get; set; } = new GenerateResumeOptions();

        public GenerateResumeRequest(ParsedResume resume)
        {
			Resume = resume;
        }
	}
}

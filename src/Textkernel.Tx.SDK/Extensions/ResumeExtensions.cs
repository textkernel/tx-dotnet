// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API.Parsing;
using Sovren.Models.Resume;
using Sovren.Models.Resume.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sovren
{
    /// <summary></summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public class ParseResumeResponseExtensions
    {
        internal ParseResumeResponse Response { get; set; }
        internal ParseResumeResponseExtensions(ParseResumeResponse response)
        {
            Response = response;
        }
    }

    /// <summary></summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public static class ResumeExtensions
    {
        /// <summary>
        /// Use this method to get easy access to most of the commonly-used data inside a parse response.
        /// <br/>For example <code>response.EasyAccess().GetCandidateName()</code>.
        /// <br/>This is just an alternative to writing your own logic to process the information provided in the response.
        /// </summary>
        public static ParseResumeResponseExtensions EasyAccess(this ParseResumeResponse response)
        {
            return new ParseResumeResponseExtensions(response);
        }

        /// <summary>
        /// Gets a list of certifications found (if any) or <see langword="null"/>
        /// </summary>
        /// <param name="exts"></param>
        /// <param name="onlyMatchedFromList">
        /// <see langword="true"/> to only return certifications that matched to Sovren's internal list of known certifications.
        /// <br/><see langword="false"/> to return all certifications, no matter how they were found
        /// </param>
        public static IEnumerable<string> GetCertifications(this ParseResumeResponseExtensions exts, bool onlyMatchedFromList = false)
        {
            return exts.Response.Value.ResumeData?.Certifications?.Where(c => !onlyMatchedFromList || c.MatchedFromList).Select(c => c.Name);
        }

        /// <summary>
        /// Gets a list of licenses found (if any) or <see langword="null"/>
        /// </summary>
        /// <param name="exts"></param>
        /// <param name="onlyMatchedFromList">
        /// <see langword="true"/> to only return licenses that matched to Sovren's internal list of known licenses.
        /// <br/><see langword="false"/> to return all licenses, no matter how they were found
        /// </param>
        public static IEnumerable<string> GetLicenses(this ParseResumeResponseExtensions exts, bool onlyMatchedFromList = false)
        {
            return exts.Response.Value.ResumeData?.Licenses?.Where(c => !onlyMatchedFromList || c.MatchedFromList).Select(c => c.Name);
        }

        /// <summary>
        /// Gets a list of ISO 639-1 codes for language competencies the candidate listed (if any) or <see langword="null"/>
        /// </summary>
        public static IEnumerable<string> GetLanguageCompetencies(this ParseResumeResponseExtensions exts)
        {
            return exts.Response.Value.ResumeData?.LanguageCompetencies?.Select(c => c.LanguageCode);
        }

        /// <summary>
        /// Gets the number of military experiences/posts found on a resume
        /// </summary>
        public static int GetNumberOfMilitaryExperience(this ParseResumeResponseExtensions exts)
        {
            return exts.Response.Value.ResumeData?.MilitaryExperience?.Count() ?? 0;
        }

        /// <summary>
        /// Gets whether or not security clearance was found on the resume
        /// </summary>
        public static bool HasSecurityClearance(this ParseResumeResponseExtensions exts)
        {
            return exts.Response.Value.ResumeData?.SecurityCredentials?.Any() ?? false;
        }

        /// <summary>
        /// Gets the severity level of the most severe resume quality finding. One of:
        /// <br/> <see cref="ResumeQualityLevel.FatalProblem"/>
        /// <br/> <see cref="ResumeQualityLevel.MajorIssue"/>
        /// <br/> <see cref="ResumeQualityLevel.DataMissing"/>
        /// <br/> <see cref="ResumeQualityLevel.SuggestedImprovement"/>
        /// <br/> null
        /// </summary>
        public static ResumeQualityLevel GetMostSevereResumeQualityFinding(this ParseResumeResponseExtensions exts)
        {
            var fatalErrors = exts.Response.Value.ResumeData?.ResumeMetadata?.ResumeQuality?.Where(r => r.Level == ResumeQualityLevel.FatalProblem.Value);
            var majorProblems = exts.Response.Value.ResumeData?.ResumeMetadata?.ResumeQuality?.Where(r => r.Level == ResumeQualityLevel.MajorIssue.Value);
            var dataMissing = exts.Response.Value.ResumeData?.ResumeMetadata?.ResumeQuality?.Where(r => r.Level == ResumeQualityLevel.DataMissing.Value);
            var improvements = exts.Response.Value.ResumeData?.ResumeMetadata?.ResumeQuality?.Where(r => r.Level == ResumeQualityLevel.SuggestedImprovement.Value);

            if (fatalErrors != null && fatalErrors.Any()) return ResumeQualityLevel.FatalProblem;
            if (majorProblems != null && majorProblems.Any()) return ResumeQualityLevel.MajorIssue;
            if (dataMissing != null && dataMissing.Any()) return ResumeQualityLevel.DataMissing;
            if (improvements != null && improvements.Any()) return ResumeQualityLevel.SuggestedImprovement;

            return null;//no issues found (amazing)
        }

        /// <summary>
        /// Gets the last-modified date of the resume, if you provided one. Otherwise <see cref="DateTime.MinValue"/>
        /// </summary>
        public static DateTime GetDocumentLastModified(this ParseResumeResponseExtensions exts)
        {
            return exts.Response.Value.ResumeData?.ResumeMetadata?.DocumentLastModified ?? DateTime.MinValue;
        }

        /// <summary>
        /// Gets the age of the resume, if it has a RevisionDate. Otherwise <see cref="TimeSpan.MaxValue"/>
        /// </summary>
        public static TimeSpan GetResumeAge(this ParseResumeResponseExtensions exts)
        {
            DateTime revDate = exts.GetDocumentLastModified();

            if (revDate == DateTime.MinValue) return TimeSpan.MaxValue;

            return DateTime.UtcNow - revDate;
        }

        /// <summary>
        /// Checks if the resume timed out during parsing. If <see langword="true"/>, the data in the resume may be incomplete
        /// </summary>
        public static bool DidTimeout(this ParseResumeResponseExtensions exts)
        {
            return exts.Response.Value.ParsingMetadata?.TimedOut ?? false;
        }

        /// <summary>
        /// Checks if Sovren found any possible problems in the converted text of the resume (prior to parsing).
        /// <br/>For more info, see <see href="https://sovren.com/technical-specs/latest/rest-api/resume-parser/overview/document-conversion-code/"/>
        /// </summary>
        public static bool HasConversionWarning(this ParseResumeResponseExtensions exts)
        {
            string code = exts.Response.Value.ConversionMetadata?.OutputValidityCode;

            switch (code)
            {
                case "ovProbableGarbageInText":
                case "ovUnknown":
                case "ovAvgWordLengthGreaterThan20":
                case "ovAvgWordLengthLessThan4":
                case "ovTooFewLineBreaks":
                case "ovLinesSeemTooShort":
                case "ovTruncated":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Use this to get the resume as a JSON string (to save to disk or other data storage).
        /// <br/>NOTE: be sure to save with UTF-8 encoding!
        /// </summary>
        /// <param name="exts"></param>
        /// <param name="piiRedacted"><see langword="true"/> for the redacted version of the resume, otherwise <see langword="false"/></param>
        /// <param name="formatted"><see langword="true"/> for pretty-printing</param>
        public static string GetResumeAsJsonString(this ParseResumeResponseExtensions exts, bool formatted, bool piiRedacted)
        {
            ParsedResume resume = piiRedacted ? exts.Response?.Value?.RedactedResumeData : exts.Response?.Value?.ResumeData;
            return resume?.ToJson(formatted);
        }

        /// <summary>
        /// Save the resume to disk using UTF-8 encoding
        /// </summary>
        /// <param name="exts"></param>
        /// <param name="piiRedacted"><see langword="true"/> to save the redacted version of the resume, otherwise <see langword="false"/></param>
        /// <param name="filePath">The file to save to</param>
        /// <param name="formatted"><see langword="true"/> for pretty-printing</param>
        public static void SaveResumeJsonToFile(this ParseResumeResponseExtensions exts, string filePath, bool formatted, bool piiRedacted)
        {
            ParsedResume resume = piiRedacted ? exts.Response?.Value?.RedactedResumeData : exts.Response?.Value?.ResumeData;
            resume?.ToFile(filePath, formatted);
        }

        /// <summary>
        /// Gets the xx-XX language/culture value for the resume
        /// </summary>
        public static string GetCulture(this ParseResumeResponseExtensions exts)
        {
            return exts.Response.Value.ResumeData?.ResumeMetadata?.DocumentCulture;
        }

        /// <summary>
        /// Gets the ISO 639-1 language code for the language the resume was written in
        /// </summary>
        public static string GetLanguage(this ParseResumeResponseExtensions exts)
        {
            return exts.Response.Value.ResumeData?.ResumeMetadata?.DocumentLanguage;
        }

        /// <summary>
        /// Gets the full text for a specific section in <see cref="ResumeMetadata.FoundSections"/>.
        /// </summary>
        /// <param name="exts"></param>
        /// <param name="section">The section to get the text for</param>
        public static string GetSectionText(this ParseResumeResponseExtensions exts, ResumeSection section)
        {
            if (section == null) return null;

            string[] lines = exts.Response.Value.ResumeData?.ResumeMetadata?.PlainText?.Split('\n');
            
            if (lines != null && lines.Length > section.FirstLineNumber && lines.Length > section.LastLineNumber)
            {
                return string.Join("\n", lines
                    .Skip(section.FirstLineNumber)
                    .Take((section.LastLineNumber - section.FirstLineNumber) + 1));
            }

            return null;
        }
    }
}

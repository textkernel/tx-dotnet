// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using NUnit.Framework;
using Textkernel.Tx.Models.Resume;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Textkernel.Tx.SDK.Tests.UnitTests
{
    public class DocumentTests
    {
        [Test]
        public void TestDocumentConstructor()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Models.Document(null);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                new Models.Document("");
            });

            Assert.Throws<FileNotFoundException>(() =>
            {
                new Models.Document("notarealfile.docx");
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                new Models.Document(null, DateTime.Now);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                new Models.Document(new byte[0], DateTime.MinValue);
            });

            Assert.Throws<ArgumentException>(() =>
            {
                new Models.Document(new byte[0], new DateTime(1970, 1, 1));
            });
        }

        [Test]
        public void TestDeserializeProfClassGroupPercentDecimal()
        {
            string jsonWithDecimal = "{\"ContactInformation\":{\"CandidateName\":{\"FormattedName\":\"John Wesson\",\"GivenName\":\"John\",\"FamilyName\":\"Wesson\"},\"Telephones\":[{\"InternationalCountryCode\":\"1\",\"AreaCityCode\":\"345\",\"SubscriberNumber\":\"987-8765\",\"Raw\":\"345-987-8765\",\"Normalized\":\"+1 345-987-8765\"}],\"EmailAddresses\":[\"jw.iscool.not@gmail.com\"],\"Location\":{\"CountryCode\":\"US\",\"PostalCode\":\"74250\",\"Regions\":[\"TX\"],\"Municipality\":\"Dallas\",\"StreetAddressLines\":[\"1231 Main St\"]}},\"EmploymentHistory\":{\"ExperienceSummary\":{\"Description\":\"John Wesson's skills align with Programmers (Information and Communication Technology). John Wesson appears to be an entry-level candidate, with 22 months of experience.\",\"MonthsOfWorkExperience\":22,\"MonthsOfManagementExperience\":0,\"ExecutiveType\":\"NONE\",\"AverageMonthsPerEmployer\":22,\"FulltimeDirectHirePredictiveIndex\":50,\"ManagementStory\":\"\",\"CurrentManagementLevel\":\"\",\"ManagementScore\":0},\"Positions\":[{\"Id\":\"POS-1\",\"Employer\":{\"Name\":{\"Probability\":\"Probable\",\"Raw\":\"Google, Inc\",\"Normalized\":\"Google\"}},\"IsSelfEmployed\":false,\"IsCurrent\":true,\"JobTitle\":{\"Raw\":\"Software Developer\",\"Normalized\":\"Software Developer\",\"Probability\":\"Confident\"},\"StartDate\":{\"Date\":\"2022-01-01\",\"IsCurrentDate\":false,\"FoundYear\":true,\"FoundMonth\":false,\"FoundDay\":false},\"EndDate\":{\"Date\":\"2023-11-08\",\"IsCurrentDate\":true,\"FoundYear\":true,\"FoundMonth\":true,\"FoundDay\":false},\"JobType\":\"directHire\",\"JobLevel\":\"Entry Level\",\"TaxonomyPercentage\":0,\"Description\":\"Used C# to develop software\"}]},\"Skills\":{\"Raw\":[{\"MonthsExperience\":{\"Value\":22},\"LastUsed\":{\"Value\":\"2023-11-08\"},\"FoundIn\":[{\"SectionType\":\"WORK HISTORY\",\"Id\":\"POS-1\"}],\"Name\":\"C#\"}],\"Normalized\":[{\"MonthsExperience\":{\"Value\":22},\"LastUsed\":{\"Value\":\"2023-11-08\"},\"FoundIn\":[{\"SectionType\":\"WORK HISTORY\",\"Id\":\"POS-1\"}],\"Name\":\"C Sharp (Programming Language)\",\"Id\":\"KS1219N6Z3XQ19V0HSKR\",\"Type\":\"IT\",\"RawSkills\":[\"C#\"]}],\"RelatedProfessionClasses\":[{\"Name\":\"Information and Communication Technology\",\"Id\":\"9\",\"Percent\":100.0,\"Groups\":[{\"Name\":\"Programmers\",\"Id\":\"82\",\"Percent\":100.0,\"NormalizedSkills\":[\"KS1219N6Z3XQ19V0HSKR\"]}]}]},\"LanguageCompetencies\":[{\"Language\":\"English\",\"LanguageCode\":\"en\",\"FoundInContext\":\"[RESUME_LANGUAGE]\"}],\"ResumeMetadata\":{\"FoundSections\":[{\"FirstLineNumber\":6,\"LastLineNumber\":10,\"SectionType\":\"WORK HISTORY\",\"HeaderTextFound\":\"Work History\"}],\"ResumeQuality\":[{\"Level\":\"Fatal Problems Found\",\"Findings\":[{\"QualityCode\":\"414\",\"Message\":\"We found no EDUCATION HISTORY for the candidate. A resume should always include an education history section.\"}]}],\"ReservedData\":{\"Phones\":[\"345-987-8765\"],\"Names\":[\"John Wesson\"],\"EmailAddresses\":[\"jw.iscool.not@gmail.com\"],\"OtherData\":[\"1231 Main St\"]},\"PlainText\":\"John Wesson\\n345-987-8765\\njw.iscool.not@gmail.com\\n1231 Main St\\nDallas, TX 74250\\n\\nWork History\\n2022 - Now\\nGoogle, Inc.\\nSoftware Developer\\nUsed C# to develop software\",\"DocumentLanguage\":\"en\",\"DocumentCulture\":\"en-US\",\"ParserSettings\":\"Coverage.PersonalAttributes = True; Coverage.Training = True; Culture.CountryCodeForUnitedKingdomIsUK = True; Culture.DefaultCountryCode = US; Culture.Language = English; Culture.PreferEnglishVersionIfTwoLanguagesInDocument = False; Data.NormalizeSkills = True; Data.UserDefinedParsing = False; Data.UseV2SkillsTaxonomy = True; OutputFormat.AssumeCompanyNameFromPrecedingJob = False; OutputFormat.ContactMethod.PackStyle = Split; OutputFormat.DateOutputStyle = ExplicitlyKnownDateInfoOnly; OutputFormat.NestJobsBasedOnDateRanges = True; OutputFormat.NormalizeRegions = False; OutputFormat.SimpleCustomLinkedIn = False; OutputFormat.SkillsStyle = Default; OutputFormat.StripParsedDataFromPositionHistoryDescription = True; OutputFormat.TelcomNumber.Style = Raw; OutputFormat.XmlFormat = HrXmlResume25\",\"DocumentLastModified\":\"2023-11-08\"}}"; ;
            ParsedResume resume = null;

            Assert.DoesNotThrow(() =>
            {
                resume = ParsedResume.FromJson(jsonWithDecimal);
            });

            string outputJson = resume.ToJson(false);
            Assert.AreEqual(outputJson, jsonWithDecimal.Replace("Percent\":100.0", "Percent\":100"));//2 instances of .0 should get removed
        }
    }
}

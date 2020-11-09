// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using NUnit.Framework;
using Sovren.Models;
using Sovren.Models.API.Parsing;
using Sovren.Models.Resume.Metadata;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sovren.SDK.Tests
{
    public class Tests : TestBase
    {
        //[SetUp]
        //public void Setup()
        //{
        //}

        [Test]
        public void TestSovrenNullableIntSerialize()
        {
            SovrenNullable<int> nullInt = new SovrenNullable<int>();

            nullInt.HasValue = true;
            nullInt.Value = 5;

            string json = JsonSerializer.Serialize(nullInt);
            nullInt = JsonSerializer.Deserialize<SovrenNullable<int>>(json);

            Assert.That(nullInt.HasValue);
            Assert.AreEqual(nullInt.Value, 5);

            nullInt.HasValue = false;

            json = JsonSerializer.Serialize(nullInt);
            nullInt = JsonSerializer.Deserialize<SovrenNullable<int>>(json);

            Assert.That(!nullInt.HasValue);
            Assert.Throws<InvalidOperationException>(() => { int x = nullInt.Value; });
        }

        [Test]
        public void TestSovrenNullableBoolSerialize()
        {
            SovrenNullable<bool> nullInt = new SovrenNullable<bool>();

            nullInt.HasValue = true;
            nullInt.Value = true;

            string json = JsonSerializer.Serialize(nullInt);
            nullInt = JsonSerializer.Deserialize<SovrenNullable<bool>>(json);

            Assert.That(nullInt.HasValue);
            Assert.AreEqual(nullInt.Value, true);

            nullInt.HasValue = false;

            json = JsonSerializer.Serialize(nullInt);
            nullInt = JsonSerializer.Deserialize<SovrenNullable<bool>>(json);

            Assert.That(!nullInt.HasValue);
            Assert.Throws<InvalidOperationException>(() => { bool x = nullInt.Value; });
        }

        [Test]
        public void TestSkillsData()
        {
            string resume = @"
John Wesson

Work History
Sr. Software Developer at Sovren Inc.   07/2017 - 07/2018
- used Javascript and ReactJS to make a web app
";

            var response = ParseResumeText(resume);

            Assert.AreEqual(response.Value.ResumeData.SkillsData[0].Taxonomies[0].SubTaxonomies[0].Skills[0].MonthsExperience.Value, 12);
            Assert.AreEqual(response.Value.ResumeData.SkillsData[0].Taxonomies[0].SubTaxonomies[0].Skills[0].LastUsed.Date.ToString("yyyy-MM-dd"), "2018-07-01");
            Assert.AreEqual(response.Value.ResumeData.SkillsData[0].Taxonomies[0].SubTaxonomies[0].Skills[0].LastUsed.FoundDay, false);
            Assert.AreEqual(response.Value.ResumeData.SkillsData[0].Taxonomies[0].SubTaxonomies[0].Skills[0].LastUsed.FoundMonth, true);
            Assert.AreEqual(response.Value.ResumeData.SkillsData[0].Taxonomies[0].SubTaxonomies[0].Skills[0].Variations[0].MonthsExperience.Value, 12);
            Assert.AreEqual(response.Value.ResumeData.SkillsData[0].Taxonomies[0].SubTaxonomies[0].Skills[0].Variations[0].LastUsed.Date.ToString("yyyy-MM-dd"), "2018-07-01");
            Assert.AreEqual(response.Value.ResumeData.SkillsData[0].Taxonomies[0].SubTaxonomies[0].Skills[0].Variations[0].LastUsed.FoundDay, false);
            Assert.AreEqual(response.Value.ResumeData.SkillsData[0].Taxonomies[0].SubTaxonomies[0].Skills[0].Variations[0].LastUsed.FoundMonth, true);
        }


        [Test]
        public void TestPersonalInfoAndResumeQuality()
        {
            string resume = @"
John Wesson

Personal Information
Birthplace: Fort Worth, TX
DOB: 5/5/1980
Driver's License: TX98765432
Father's Name: Janplop
Gender: M
Marital Status: Single
Mother Tongue: English
Nationality: USA
Passport Number: 5234098423478
";

            var response = ParseResumeText(resume);

            Assert.IsNotNull(response.Value.ResumeData.PersonalAttributes.Birthplace);
            Assert.IsNotNull(response.Value.ResumeData.PersonalAttributes.DateOfBirth);
            Assert.AreEqual(response.Value.ResumeData.PersonalAttributes.DateOfBirth.Date.ToString("yyyy-MM-dd"), "1980-05-05");
            Assert.IsNotNull(response.Value.ResumeData.PersonalAttributes.DrivingLicense);
            Assert.IsNotNull(response.Value.ResumeData.PersonalAttributes.FathersName);
            Assert.IsNotNull(response.Value.ResumeData.PersonalAttributes.Gender);
            Assert.IsNotNull(response.Value.ResumeData.PersonalAttributes.MaritalStatus);
            Assert.IsNotNull(response.Value.ResumeData.PersonalAttributes.MotherTongue);
            Assert.IsNotNull(response.Value.ResumeData.PersonalAttributes.Nationality);
            Assert.IsNotNull(response.Value.ResumeData.PersonalAttributes.PassportNumber);

            //fatal 413
            Assert.That(response.Value.ResumeData.ResumeMetadata.ResumeQuality, Has.Count.AtLeast(1));
            Assert.IsNotNull(response.Value.ResumeData.ResumeMetadata.ResumeQuality[0].Level);
            Assert.AreEqual(response.Value.ResumeData.ResumeMetadata.ResumeQuality[0].Level, ResumeQualityLevel.FatalProblem.Value);
            Assert.IsNotNull(response.Value.ResumeData.ResumeMetadata.ResumeQuality[0].Findings);
            Assert.That(response.Value.ResumeData.ResumeMetadata.ResumeQuality[0].Findings, Has.Count.AtLeast(1));
            Assert.IsNotNull(response.Value.ResumeData.ResumeMetadata.ResumeQuality[0].Findings[0].Message);
            Assert.NotZero(response.Value.ResumeData.ResumeMetadata.ResumeQuality[0].Findings[0].QualityCode);
            Assert.AreEqual(response.Value.ResumeData.ResumeMetadata.ResumeQuality[0].Findings[0].QualityCode, 413);
            Assert.IsNotNull(response.Value.ResumeData.ResumeMetadata.ResumeQuality[3].Findings[0].Identifiers);
            Assert.That(response.Value.ResumeData.ResumeMetadata.ResumeQuality[3].Findings[0].Identifiers, Has.Count.AtLeast(1));
        }

        [Test]
        public void TestGeneralOutput()
        {
            ParseResumeResponse response = ParseResume(@"C:\Users\dev4\Desktop\resume.docx");

            Assert.IsTrue(response.Info.IsSuccess);

            Assert.IsNotNull(response.Info);
            Assert.IsTrue(response.Info.IsSuccess);
            Assert.IsNotNull(response.Info.Message);
            Assert.IsNotNull(response.Info.Code);
            Assert.IsNotNull(response.Info.TransactionId);
            Assert.NotZero(response.Info.TotalElapsedMilliseconds);
            Assert.IsNotEmpty(response.Info.ApiVersion);
            Assert.IsNotEmpty(response.Info.EngineVersion);
            Assert.IsNotNull(response.Info.CustomerDetails);
            Assert.IsNotNull(response.Info.CustomerDetails.AccountId);
            Assert.NotZero(response.Info.CustomerDetails.CreditsRemaining);
            Assert.NotZero(response.Info.CustomerDetails.CreditsUsed);
            Assert.IsNotNull(response.Info.CustomerDetails.ExpirationDate);
            Assert.IsNotNull(response.Info.CustomerDetails.IPAddress);
            Assert.NotZero(response.Info.CustomerDetails.MaximumConcurrentRequests);
            Assert.IsNotNull(response.Info.CustomerDetails.Name);
            Assert.IsNotNull(response.Info.CustomerDetails.Region);

            Assert.IsNotNull(response.Value);

            Assert.IsNotNull(response.Value.ConversionMetadata);
            //Assert.IsNotNull(response.Value.Conversions);
            Assert.IsNotNull(response.Value.ParsingMetadata);
            Assert.IsNotNull(response.Value.ResumeData);

            Assert.AreEqual(response.Value.ConversionMetadata.DetectedType, "WordDocx");
            Assert.AreEqual(response.Value.ConversionMetadata.SuggestedFileExtension, "docx");
            Assert.AreEqual(response.Value.ConversionMetadata.OutputValidityCode, "ovIsProbablyValid");
            Assert.NotZero(response.Value.ConversionMetadata.ElapsedMilliseconds);
            
            Assert.AreEqual(response.Value.ResumeData.ResumeMetadata.DocumentCulture, "en-US");
            //Assert.IsTrue(response.Value.ParsingMetadata.DetectedLanguage == "en");
            Assert.NotZero(response.Value.ParsingMetadata.ElapsedMilliseconds);
            Assert.AreEqual(response.Value.ParsingMetadata.TimedOut, false);
            Assert.AreEqual(response.Value.ParsingMetadata.TimedOutAtMilliseconds.HasValue, false);

            Assert.IsNotNull(response.Value.ResumeData.Certifications);
            Assert.That(response.Value.ResumeData.Certifications, Has.Count.AtLeast(1));
            Assert.IsNotNull(response.Value.ResumeData.Certifications[0].Name);
            Assert.IsNotNull(response.Value.ResumeData.Certifications[0].FoundInContext);

            Assert.IsNotNull(response.Value.ResumeData.ContactInformation);
            Assert.IsNotNull(response.Value.ResumeData.ContactInformation.CandidateName);
            Assert.IsNotNull(response.Value.ResumeData.ContactInformation.CandidateName.FamilyName);
            Assert.IsNotNull(response.Value.ResumeData.ContactInformation.CandidateName.FormattedName);
            Assert.IsNotNull(response.Value.ResumeData.ContactInformation.CandidateName.GivenName);
            Assert.IsNotNull(response.Value.ResumeData.ContactInformation.CandidateName.MiddleName);
            Assert.IsNotNull(response.Value.ResumeData.ContactInformation.EmailAddresses);
            Assert.That(response.Value.ResumeData.ContactInformation.EmailAddresses, Has.Count.AtLeast(1));
            Assert.IsNotNull(response.Value.ResumeData.ContactInformation.EmailAddresses[0]);
            AssertLocationNotNull(response.Value.ResumeData.ContactInformation.Location, true, false);
            Assert.IsNotNull(response.Value.ResumeData.ContactInformation.Telephones);
            Assert.That(response.Value.ResumeData.ContactInformation.Telephones, Has.Count.AtLeast(1));
            Assert.IsNotNull(response.Value.ResumeData.ContactInformation.Telephones[0].Normalized);
            Assert.IsNotNull(response.Value.ResumeData.ContactInformation.Telephones[0].Raw);
            Assert.IsNotNull(response.Value.ResumeData.ContactInformation.WebAddresses);
            Assert.That(response.Value.ResumeData.ContactInformation.WebAddresses, Has.Count.AtLeast(1));
            Assert.IsNotNull(response.Value.ResumeData.ContactInformation.WebAddresses[0].Address);
            Assert.IsNotNull(response.Value.ResumeData.ContactInformation.WebAddresses[0].Type);

            Assert.IsNotNull(response.Value.ResumeData.Education);
            AssertDegreeNotNull(response.Value.ResumeData.Education.HighestDegree);
            Assert.IsNotNull(response.Value.ResumeData.Education.EducationDetails);
            Assert.That(response.Value.ResumeData.Education.EducationDetails, Has.Count.AtLeast(1));
            AssertDegreeNotNull(response.Value.ResumeData.Education.EducationDetails[0].Degree);
            Assert.IsNotNull(response.Value.ResumeData.Education.EducationDetails[0].GPA);
            Assert.IsNotNull(response.Value.ResumeData.Education.EducationDetails[0].GPA.MaxScore);
            Assert.NotZero(response.Value.ResumeData.Education.EducationDetails[0].GPA.NormalizedScore);
            Assert.IsNotNull(response.Value.ResumeData.Education.EducationDetails[0].GPA.Score);
            Assert.IsNotNull(response.Value.ResumeData.Education.EducationDetails[0].GPA.ScoringSystem);
            Assert.IsNotNull(response.Value.ResumeData.Education.EducationDetails[0].Graduated);
            AssertDateNotNull(response.Value.ResumeData.Education.EducationDetails[0].LastEducationDate);
            Assert.IsNotNull(response.Value.ResumeData.Education.EducationDetails[0].Majors);
            Assert.That(response.Value.ResumeData.Education.EducationDetails[0].Majors, Has.Count.AtLeast(1));
            Assert.IsNotNull(response.Value.ResumeData.Education.EducationDetails[0].SchoolName);
            Assert.IsNotNull(response.Value.ResumeData.Education.EducationDetails[0].SchoolName.Normalized);
            Assert.IsNotNull(response.Value.ResumeData.Education.EducationDetails[0].SchoolName.Raw);
            Assert.IsNotNull(response.Value.ResumeData.Education.EducationDetails[0].Text);

            Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory);
            Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.ExperienceSummary);
            Assert.NotZero(response.Value.ResumeData.EmploymentHistory.ExperienceSummary.AverageMonthsPerEmployer);
            Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.ExperienceSummary.CurrentManagementLevel);
            Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.ExperienceSummary.Description);
            Assert.NotZero(response.Value.ResumeData.EmploymentHistory.ExperienceSummary.FulltimeDirectHirePredictiveIndex);
            Assert.NotZero(response.Value.ResumeData.EmploymentHistory.ExperienceSummary.ManagementScore);
            Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.ExperienceSummary.ManagementStory);
            Assert.NotZero(response.Value.ResumeData.EmploymentHistory.ExperienceSummary.MonthsOfWorkExperience);
            Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.Positions);
            Assert.That(response.Value.ResumeData.EmploymentHistory.Positions, Has.Count.AtLeast(1));
            Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.Positions[0].Description);
            Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.Positions[0].Employer);
            Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.Positions[0].Employer.Name);
            Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.Positions[0].Employer.Name.Normalized);
            Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.Positions[0].Employer.Name.Raw);
            Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.Positions[0].Employer.Name.Probability);
            //AssertLocationNotNull(response.Value.ResumeData.EmploymentHistory.Positions[0].Employer.Location, false, false);
            AssertDateNotNull(response.Value.ResumeData.EmploymentHistory.Positions[0].EndDate);
            Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.Positions[0].Id);
            Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.Positions[0].JobLevel);
            Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.Positions[0].JobTitle);
            Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.Positions[0].JobTitle.Normalized);
            Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.Positions[0].JobTitle.Probability);
            Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.Positions[0].JobTitle.Raw);
            Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.Positions[0].JobTitle.Variations);
            Assert.That(response.Value.ResumeData.EmploymentHistory.Positions[0].JobTitle.Variations, Has.Count.AtLeast(1));
            Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.Positions[0].JobType);
            //Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.Positions[0].RelatedToByCompanyName);
            //Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.Positions[0].RelatedToByCompanyName.CompanyName);
            //Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.Positions[0].RelatedToByCompanyName.PositionId);
            Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.Positions[0].SubTaxonomyName);
            Assert.IsNotNull(response.Value.ResumeData.EmploymentHistory.Positions[0].TaxonomyName);
            AssertDateNotNull(response.Value.ResumeData.EmploymentHistory.Positions[0].StartDate);

            Assert.IsNotNull(response.Value.ResumeData.ResumeMetadata);
            Assert.IsNotNull(response.Value.ResumeData.ResumeMetadata.FoundSections);
            Assert.That(response.Value.ResumeData.ResumeMetadata.FoundSections, Has.Count.AtLeast(1));
            Assert.IsNotNull(response.Value.ResumeData.ResumeMetadata.FoundSections[0].SectionType);
            Assert.NotZero(response.Value.ResumeData.ResumeMetadata.FoundSections[0].LastLine);
            Assert.IsNotNull(response.Value.ResumeData.ResumeMetadata.ReservedData);
            Assert.IsNotNull(response.Value.ResumeData.ResumeMetadata.SovrenSignature);
            Assert.IsNotNull(response.Value.ResumeData.ResumeMetadata.ParserSettings);
            Assert.IsNotNull(response.Value.ResumeData.ResumeMetadata.ResumeQuality);
            

            Assert.IsNotNull(response.Value.ResumeData.ProfessionalSummary);
            Assert.IsNotNull(response.Value.ResumeData.SkillsData);
            Assert.That(response.Value.ResumeData.SkillsData, Has.Count.AtLeast(1));
            Assert.IsNotNull(response.Value.ResumeData.SkillsData[0].Root);
            Assert.IsNotNull(response.Value.ResumeData.SkillsData[0].Taxonomies);
            Assert.That(response.Value.ResumeData.SkillsData[0].Taxonomies, Has.Count.AtLeast(1));
            Assert.IsNotNull(response.Value.ResumeData.SkillsData[0].Taxonomies[0].Id);
            Assert.IsNotNull(response.Value.ResumeData.SkillsData[0].Taxonomies[0].Name);
            Assert.NotZero(response.Value.ResumeData.SkillsData[0].Taxonomies[0].PercentOfOverall);
            Assert.IsNotNull(response.Value.ResumeData.SkillsData[0].Taxonomies[0].SubTaxonomies);
            Assert.That(response.Value.ResumeData.SkillsData[0].Taxonomies[0].SubTaxonomies, Has.Count.AtLeast(1));
            Assert.NotZero(response.Value.ResumeData.SkillsData[0].Taxonomies[0].SubTaxonomies[0].PercentOfOverall);
            Assert.NotZero(response.Value.ResumeData.SkillsData[0].Taxonomies[0].SubTaxonomies[0].PercentOfParent);
            Assert.IsNotNull(response.Value.ResumeData.SkillsData[0].Taxonomies[0].SubTaxonomies[0].SubTaxonomyId);
            Assert.IsNotNull(response.Value.ResumeData.SkillsData[0].Taxonomies[0].SubTaxonomies[0].SubTaxonomyName);
            Assert.IsNotNull(response.Value.ResumeData.SkillsData[0].Taxonomies[0].SubTaxonomies[0].Skills);
            Assert.That(response.Value.ResumeData.SkillsData[0].Taxonomies[0].SubTaxonomies[0].Skills, Has.Count.AtLeast(1));
            Assert.IsNotNull(response.Value.ResumeData.SkillsData[0].Taxonomies[0].SubTaxonomies[0].Skills[0].FoundIn);
            Assert.IsNotNull(response.Value.ResumeData.SkillsData[0].Taxonomies[0].SubTaxonomies[0].Skills[0].Id);
            Assert.IsNotNull(response.Value.ResumeData.SkillsData[0].Taxonomies[0].SubTaxonomies[0].Skills[0].Name);
        }

        private void AssertDateNotNull(Models.SovrenDate date)
        {
            Assert.IsNotNull(date);
            Assert.IsNotNull(date.Date);
        }

        private void AssertDegreeNotNull(Models.Resume.Education.Degree degree)
        {
            Assert.IsNotNull(degree);
            Assert.IsNotNull(degree.Name);
            Assert.IsNotNull(degree.Name.Raw);
            Assert.IsNotNull(degree.Name.Normalized);
            Assert.IsNotNull(degree.Type);
            Assert.IsNotNull(degree.Type);
        }

        private void AssertLocationNotNull(Location loc, bool checkStreetLevel = false, bool checkGeo = false)
        {
            Assert.IsNotNull(loc);
            Assert.IsNotNull(loc.CountryCode);
            Assert.IsNotEmpty(loc.Regions);
            Assert.IsNotNull(loc.Municipality);

            if (checkStreetLevel)
            {
                Assert.IsNotNull(loc.PostalCode);
                Assert.IsNotEmpty(loc.StreetAddressLines);
            }

            if (checkGeo)
            {
                Assert.IsNotNull(loc.GeoCoordinates);
                Assert.NotZero(loc.GeoCoordinates.Latitude);
                Assert.NotZero(loc.GeoCoordinates.Longitude);
            }
        }
    }

    public abstract class TestBase
    {
        protected static SovrenClient Client;

        private class Credentials
        {
            public string AccountId { get; set; }
            public string ServiceKey { get; set; }
        }

        static TestBase()
        {
            var data = JsonSerializer.Deserialize<Credentials>(File.ReadAllText("credentials.json"));
            Client = new SovrenClient(data.AccountId, data.ServiceKey, DataCenter.US);
        }

        protected ParseResumeResponse ParseResume(string file)
        {
            Document doc = new Document(file);
            ParseRequest request = new ParseRequest(doc);
            return Client.ParseResume(request).Result;
        }

        protected ParseResumeResponse ParseResumeText(string text)
        {
            Document doc = new Document(Encoding.UTF8.GetBytes(text), DateTime.Today);
            ParseRequest request = new ParseRequest(doc);
            return Client.ParseResume(request).Result;
        }
    }
}
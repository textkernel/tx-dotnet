// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using NUnit.Framework;
using Sovren.Models;
using Sovren.Models.API.Geocoding;
using Sovren.Models.API.Indexes;
using Sovren.Models.API.Parsing;
using Sovren.Models.Job;
using Sovren.Models.Matching;
using Sovren.Models.Resume;
using Sovren.Models.Resume.Metadata;
using System;
using System.Collections;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sovren.SDK.Tests.IntegrationTests
{
    public class ParsingTests : TestBase
    {
        public static IEnumerable BadDocuments
        {
            get
            {
                yield return new TestCaseData(null, typeof(ArgumentNullException));
                yield return new TestCaseData(new Models.Document(new byte[1], DateTime.Now), typeof(SovrenException));
            }
        }

        [TestCaseSource(typeof(ParsingTests), nameof(BadDocuments))]
        public async Task TestParseBadInput(Document document, Type expectedExceptionType)
        {
            Assert.That(async () => await Client.ParseResume(new ParseRequest(document)), Throws.Exception.TypeOf(expectedExceptionType));
            Assert.That(async () => await Client.ParseJob(new ParseRequest(document)), Throws.Exception.TypeOf(expectedExceptionType));

            await Task.CompletedTask;
        }

        [Test]
        public void TestLargeDocumentParse()
        {
            SovrenException e = Assert.ThrowsAsync<SovrenException>(async () => {
                await Client.ParseResume(new ParseRequest(new Document(new byte[20_000_000], DateTime.Now)));
            });

            Assert.AreEqual(e.Message, "Request body was too large.");
        }

        [Test]
        public async Task TestBullets()
        {
            ParseResumeResponseValue response = null;

            Assert.DoesNotThrow(() => {
                response = Client.ParseResume(
                    new ParseRequest(TestData.Resume,
                        new ParseOptions()
                        {
                            Configuration = "OutputFormat.CreateBullets = true"
                        }
                    )).Result.Value;
            });

            Assert.IsTrue(response.ParsingResponse.IsSuccess);
            Assert.IsNotNull(response.ResumeData);
            Assert.IsNotNull(response.ResumeData.EmploymentHistory);
            Assert.IsNotEmpty(response.ResumeData.EmploymentHistory.Positions);
            Assert.IsNotEmpty(response.ResumeData.EmploymentHistory.Positions[0].Bullets);
            Assert.IsNotNull(response.ResumeData.EmploymentHistory.Positions[0].Bullets[0].Text);
            Assert.IsNotEmpty(response.ResumeData.EmploymentHistory.Positions[0].Bullets[0].Text);
            Assert.IsNotNull(response.ResumeData.EmploymentHistory.Positions[0].Bullets[0].Type);
            Assert.IsNotEmpty(response.ResumeData.EmploymentHistory.Positions[0].Bullets[0].Type);

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestParseResumeSuccess()
        {
            ParseResumeResponseValue response = null;

            Assert.DoesNotThrow(() => {
                response = Client.ParseResume(
                    new ParseRequest(TestData.Resume,
                        new ParseOptions()
                        {
                            OutputCandidateImage = true,
                            OutputHtml = true,
                            OutputPdf = true,
                            OutputRtf = true
                        }
                    )).Result.Value; 
            });

            Assert.IsTrue(response.ParsingResponse.IsSuccess);
            Assert.IsNotNull(response.ResumeData);
            Assert.IsNotNull(response.Conversions);
            Assert.IsNotNull(response.Conversions.HTML);
            Assert.IsNotNull(response.Conversions.PDF);
            Assert.IsNotNull(response.Conversions.RTF);
            Assert.IsNotNull(response.ConversionMetadata);
            Assert.IsNotNull(response.ParsingMetadata);
            Assert.IsNotNull(response.RedactedResumeData);

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestJobOrderSuccess()
        {
            ParseJobResponseValue response = null;

            Assert.DoesNotThrow(() => {
                response = Client.ParseJob(
                        new ParseRequest(TestData.JobOrder, 
                            new ParseOptions()
                            {
                                OutputCandidateImage = true,
                                OutputHtml = true,
                                OutputPdf = true,
                                OutputRtf = true
                            }
                        )).Result.Value;
            });

            Assert.IsTrue(response.ParsingResponse.IsSuccess);
            Assert.IsNotNull(response.JobData);
            Assert.IsNotNull(response.Conversions);
            Assert.IsNotNull(response.Conversions.HTML);
            Assert.IsNotNull(response.Conversions.PDF);
            Assert.IsNotNull(response.Conversions.RTF);
            Assert.IsNotNull(response.ConversionMetadata);
            Assert.IsNotNull(response.ParsingMetadata);

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestParseResumeGeocodeIndex()
        {
            string indexId = "SDK-" + nameof(TestParseResumeGeocodeIndex);
            string documentId = "1";

            GeocodeOptions geocodeOptions = new GeocodeOptions()
            {
                IncludeGeocoding = true
            };

            IndexSingleDocumentInfo indexingOptions = new IndexSingleDocumentInfo()
            {
                IndexId = indexId
            };

            // since there isn't an address this will throw an exception
            Assert.ThrowsAsync<SovrenGeocodeResumeException>(async () => {
                await Client.ParseResume(new ParseRequest(TestData.Resume)
                {
                    GeocodeOptions = geocodeOptions,
                    IndexingOptions = indexingOptions
                });
            });

            
            // confirm you can geocode but indexing fails
            Assert.ThrowsAsync<SovrenIndexResumeException>(async () => {
                await Client.ParseResume(new ParseRequest(TestData.ResumeWithAddress)
                {
                    GeocodeOptions = geocodeOptions,
                    IndexingOptions = indexingOptions
                });
            });

            try
            {
                // set the document id and create the index
                indexingOptions.DocumentId = documentId;
                await Client.CreateIndex(IndexType.Resume, indexId);
                await DelayForIndexSync();

                // confirm you can parse/geocode/index
                Assert.DoesNotThrowAsync(async () => {
                    await Client.ParseResume(new ParseRequest(TestData.ResumeWithAddress)
                    {
                        GeocodeOptions = geocodeOptions,
                        IndexingOptions = indexingOptions
                    });
                });

                // verify the resume exists in the index
                await DelayForIndexSync();
                await Client.GetResume(indexId, documentId);
            }
            finally
            {
                await CleanUpIndex(indexId);
            }
        }

        [Test]
        public async Task TestResumeToFromJson()
        {
            string tempFile1 = Guid.NewGuid().ToString();
            string tempFile2 = Guid.NewGuid().ToString();

            try
            {
                ParseResumeResponse response = await Client.ParseResume(new ParseRequest(TestData.Resume));

                string unformatted = response.Value.ResumeData.ToJson(false);
                string formatted = response.Value.ResumeData.ToJson(true);

                response.Value.ResumeData.ToFile(tempFile1, true);
                response.Value.ResumeData.ToFile(tempFile2, false);

                ParsedResume resume1 = ParsedResume.FromJson(unformatted);
                ParsedResume resume2 = ParsedResume.FromJson(formatted);

                Assert.NotNull(resume1);
                Assert.NotNull(resume2);

                Assert.NotNull(resume1?.ContactInformation?.CandidateName?.FormattedName);
                Assert.NotNull(resume2?.ContactInformation?.CandidateName?.FormattedName);

                resume1 = ParsedResume.FromFile(tempFile1);
                resume2 = ParsedResume.FromFile(tempFile2);

                Assert.NotNull(resume1);
                Assert.NotNull(resume2);

                Assert.NotNull(resume1?.ContactInformation?.CandidateName?.FormattedName);
                Assert.NotNull(resume2?.ContactInformation?.CandidateName?.FormattedName);

                Assert.Throws<JsonException>(() => ParsedResume.FromJson("{}"));
            }
            finally
            {
                File.Delete(tempFile1);
                File.Delete(tempFile2);
            }
        }

        [Test]
        public async Task TestJobToFromJson()
        {
            string tempFile1 = Guid.NewGuid().ToString();
            string tempFile2 = Guid.NewGuid().ToString();

            try
            {
                ParseJobResponse response = await Client.ParseJob(new ParseRequest(TestData.JobOrder));

                string unformatted = response.Value.JobData.ToJson(false);
                string formatted = response.Value.JobData.ToJson(true);

                response.Value.JobData.ToFile(tempFile1, true);
                response.Value.JobData.ToFile(tempFile2, false);

                ParsedJob job1 = ParsedJob.FromJson(unformatted);
                ParsedJob job2 = ParsedJob.FromJson(formatted);

                Assert.NotNull(job1);
                Assert.NotNull(job2);

                Assert.NotNull(job1?.JobTitles?.MainJobTitle);
                Assert.NotNull(job2?.JobTitles?.MainJobTitle);

                job1 = ParsedJob.FromFile(tempFile1);
                job2 = ParsedJob.FromFile(tempFile2);

                Assert.NotNull(job1);
                Assert.NotNull(job2);

                Assert.NotNull(job1?.JobTitles?.MainJobTitle);
                Assert.NotNull(job2?.JobTitles?.MainJobTitle);

                Assert.Throws<JsonException>(() => ParsedJob.FromJson("{}"));
            }
            finally
            {
                File.Delete(tempFile1);
                File.Delete(tempFile2);
            }
        }

        [Test]
        public async Task TestParseJobGeocodeIndex()
        {
            string indexId = "SDK-" + nameof(TestParseJobGeocodeIndex);
            string documentId = "1";

            GeocodeOptions geocodeOptions = new GeocodeOptions()
            {
                IncludeGeocoding = true
            };

            IndexSingleDocumentInfo indexingOptions = new IndexSingleDocumentInfo()
            {
                IndexId = indexId
            };

            // since there isn't an address this will throw an exception
            Assert.ThrowsAsync<SovrenGeocodeJobException>(async () => {
                await Client.ParseJob(new ParseRequest(TestData.JobOrder)
                {
                    GeocodeOptions = geocodeOptions,
                    IndexingOptions = indexingOptions
                });
            });

            // confirm you can geocode but indexing fails
            Assert.ThrowsAsync<SovrenIndexJobException>(async () => {
                await Client.ParseJob(new ParseRequest(TestData.JobOrderWithAddress)
                {
                    GeocodeOptions = geocodeOptions,
                    IndexingOptions = indexingOptions
                });
            });

            try
            {
                // set the document id and create the index
                indexingOptions.DocumentId = documentId;
                await Client.CreateIndex(IndexType.Job, indexId);
                await DelayForIndexSync();

                // confirm you can parse/geocode/index
                Assert.DoesNotThrowAsync(async () => {
                    await Client.ParseJob(new ParseRequest(TestData.JobOrderWithAddress)
                    {
                        GeocodeOptions = geocodeOptions,
                        IndexingOptions = indexingOptions
                    });
                });

                // verify the resume exists in the index
                await DelayForIndexSync();
                await Client.GetJob(indexId, documentId);
            }
            finally
            {
                await CleanUpIndex(indexId);
            }
        }

        

        [Test]
        public async Task TestSkillsData()
        {
            ParseResumeResponseValue response = Client.ParseResume(new ParseRequest(TestData.Resume)).Result.Value;

            Assert.AreEqual(response.ResumeData.SkillsData[0].Taxonomies[0].SubTaxonomies[0].Skills[1].MonthsExperience.Value, 12);
            Assert.AreEqual(response.ResumeData.SkillsData[0].Taxonomies[0].SubTaxonomies[0].Skills[1].LastUsed.Value.ToString("yyyy-MM-dd"), "2018-07-01");
            Assert.AreEqual(response.ResumeData.SkillsData[0].Taxonomies[0].SubTaxonomies[0].Skills[1].Variations[0].MonthsExperience.Value, 12);
            Assert.AreEqual(response.ResumeData.SkillsData[0].Taxonomies[0].SubTaxonomies[0].Skills[1].Variations[0].LastUsed.Value.ToString("yyyy-MM-dd"), "2018-07-01");

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestPersonalInfo()
        {
            ParseResumeResponseValue response = Client.ParseResume(new ParseRequest(TestData.ResumePersonalInformation)).Result.Value;

            Assert.IsNotNull(response.ResumeData.PersonalAttributes.Birthplace);
            Assert.IsNotNull(response.ResumeData.PersonalAttributes.DateOfBirth);
            Assert.AreEqual(response.ResumeData.PersonalAttributes.DateOfBirth.Date.ToString("yyyy-MM-dd"), "1980-05-05");
            Assert.IsNotNull(response.ResumeData.PersonalAttributes.DrivingLicense);
            Assert.IsNotNull(response.ResumeData.PersonalAttributes.FathersName);
            Assert.IsNotNull(response.ResumeData.PersonalAttributes.Gender);
            Assert.IsNotNull(response.ResumeData.PersonalAttributes.MaritalStatus);
            //Assert.IsNotNull(response.ResumeData.PersonalAttributes.MotherTongue);
            Assert.IsNotNull(response.ResumeData.PersonalAttributes.Nationality);
            Assert.IsNotNull(response.ResumeData.PersonalAttributes.PassportNumber);

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestResumeQuality()
        {
            ParseResumeResponseValue response = Client.ParseResume(new ParseRequest(GetTestFileAsDocument("resume.docx"))).Result.Value;

            Assert.That(response.ResumeData.ResumeMetadata.ResumeQuality, Has.Count.AtLeast(1));
            Assert.IsNotNull(response.ResumeData.ResumeMetadata.ResumeQuality[0].Level);
            Assert.AreEqual(response.ResumeData.ResumeMetadata.ResumeQuality[0].Level, ResumeQualityLevel.SuggestedImprovement.Value);
            Assert.IsNotNull(response.ResumeData.ResumeMetadata.ResumeQuality[0].Findings);
            Assert.That(response.ResumeData.ResumeMetadata.ResumeQuality[0].Findings, Has.Count.AtLeast(1));
            Assert.IsNotNull(response.ResumeData.ResumeMetadata.ResumeQuality[0].Findings[0].Message);
            Assert.IsNotNull(response.ResumeData.ResumeMetadata.ResumeQuality[0].Findings[0].QualityCode);
            //do not test these since they are subject to change somewhat frequently
            //Assert.AreEqual("111", response.ResumeData.ResumeMetadata.ResumeQuality[0].Findings[0].QualityCode);
            //Assert.IsNotNull(response.ResumeData.ResumeMetadata.ResumeQuality[0].Findings[3].SectionIdentifiers);
            //Assert.That(response.ResumeData.ResumeMetadata.ResumeQuality[0].Findings[3].SectionIdentifiers, Has.Count.AtLeast(1));

            await Task.CompletedTask;
        }

        [Test]
        public async Task TestGeneralOutput()
        {
            Document document = GetTestFileAsDocument("resume.docx");
            ParseResumeResponse response = await Client.ParseResume(new ParseRequest(document));

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
            Assert.AreEqual(response.Value.ConversionMetadata.DocumentHash, "96E36138DAFB03B057D1607B86C452FE");
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
            Assert.IsNull(response.Value.ParsingMetadata.TimedOutAtMilliseconds);

            Assert.IsNotNull(response.Value.ResumeData.Licenses);
            Assert.That(response.Value.ResumeData.Licenses, Has.Count.AtLeast(1));
            Assert.IsNotNull(response.Value.ResumeData.Licenses[0].Name);

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
            Assert.IsNull(response.Value.ResumeData.Education.EducationDetails[0].Graduated);
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
            Assert.NotZero(response.Value.ResumeData.ResumeMetadata.FoundSections[0].LastLineNumber);
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
    }
}

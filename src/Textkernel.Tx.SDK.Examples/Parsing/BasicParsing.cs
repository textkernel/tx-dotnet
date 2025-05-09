﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Textkernel.Tx.Models;
using Textkernel.Tx.Models.API.Parsing;
using Textkernel.Tx.Models.Resume.ContactInfo;

namespace Textkernel.Tx.SDK.Examples.Parsing
{
    internal class BasicParsing : IExample
    {
        public async Task Run(TxClient client)
        {
            //A Document is an unparsed File (PDF, Word Doc, etc)
            Document doc = new Document("resume.docx");

            //when you create a ParseRequest, you can specify many configuration settings
            //in the ParseOptions. See https://developer.textkernel.com/tx-platform/v10/resume-parser/api/
            ParseRequest request = new ParseRequest(doc, new ParseOptions()
            {
                ProfessionsSettings = new ProfessionsSettings
                {
                    Normalize = true,
                },
                SkillsSettings = new SkillsSettings
                {
                    Normalize = true,
                    TaxonomyVersion = "V2"
                }
            });

            try
            {
                ParseResumeResponse response = await client.Parser.ParseResume(request);
                //if we get here, it was 200-OK and all operations succeeded

                //now we can use the response to output some of the data from the resume
                PrintBasicResumeInfo(response);
            }
            catch (TxException e)
            {
                //the document could not be parsed, always try/catch for TxExceptions when using TxClient
                Console.WriteLine($"Error: {e.TxErrorCode}, Message: {e.Message}");
            }
        }

        static void PrintBasicResumeInfo(ParseResumeResponse response)
        {
            PrintContactInfo(response);
            PrintPersonalInfo(response);
            PrintWorkHistory(response.Value);
            PrintEducation(response.Value);
        }

        static void PrintHeader(string headerName)
        {
            Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}--------------- {headerName} ---------------");
        }

        static void PrintContactInfo(ParseResumeResponse response)
        {
            //general contact information (only some examples listed here, there are many others)
            PrintHeader("CONTACT INFORMATION");
            Console.WriteLine("Name: " + response.Value.ResumeData?.ContactInformation?.CandidateName?.FormattedName);
            Console.WriteLine("Email: " + response.Value.ResumeData?.ContactInformation?.EmailAddresses?.FirstOrDefault());
            Console.WriteLine("Phone: " + response.Value.ResumeData?.ContactInformation?.Telephones?.Select(t => t.Normalized)?.FirstOrDefault());
            Console.WriteLine("City: " + response.Value.ResumeData?.ContactInformation?.Location?.Municipality);
            Console.WriteLine("Region: " + response.Value.ResumeData?.ContactInformation?.Location?.Regions?.FirstOrDefault());
            Console.WriteLine("Country: " + response.Value.ResumeData?.ContactInformation?.Location?.CountryCode);
            Console.WriteLine("LinkedIn: " + response.Value.ResumeData?.ContactInformation?.WebAddresses?.FirstOrDefault(a => a.Type == WebAddressType.LinkedIn.Value)?.Address);
        }

        static void PrintPersonalInfo(ParseResumeResponse response)
        {
            //personal information (only some examples listed here, there are many others)
            PrintHeader("PERSONAL INFORMATION");
            Console.WriteLine("Date of Birth: " + response.Value.ResumeData?.PersonalAttributes?.DateOfBirth?.Date.ToShortDateString());
            Console.WriteLine("Driving License: " + response.Value.ResumeData?.PersonalAttributes?.DrivingLicense);
            Console.WriteLine("Nationality: " + response.Value.ResumeData?.PersonalAttributes?.Nationality);
            Console.WriteLine("Visa Status: " + response.Value.ResumeData?.PersonalAttributes?.VisaStatus);
        }

        static void PrintWorkHistory(ParseResumeResponseValue response)
        {
            //basic work history display
            PrintHeader("EXPERIENCE SUMMARY");
            Console.WriteLine("Years Experience: " + Math.Round((response.ResumeData?.EmploymentHistory?.ExperienceSummary?.MonthsOfWorkExperience ?? 0) / 12.0, 1));
            Console.WriteLine("Avg Years Per Employer: " + Math.Round((response.ResumeData?.EmploymentHistory?.ExperienceSummary?.AverageMonthsPerEmployer ?? 0) / 12.0, 1));
            Console.WriteLine("Years Management Experience: " + Math.Round((response.ResumeData?.EmploymentHistory?.ExperienceSummary?.MonthsOfManagementExperience ?? 0) / 12.0, 1));

            response.ResumeData?.EmploymentHistory?.Positions?.ForEach(position =>
            {
                Console.WriteLine($"{Environment.NewLine}POSITION '{position.Id}'");
                Console.WriteLine($"Employer: {position.Employer?.Name?.Normalized}");
                Console.WriteLine($"Title (normalized): {position.NormalizedProfession?.Profession?.Description}");
                Console.WriteLine($"Date Range: {GetTxDateAsString(position.StartDate)} - {GetTxDateAsString(position.EndDate)}");
            });
        }

        static void PrintEducation(ParseResumeResponseValue response)
        {
            //basic education display
            PrintHeader("EDUCATION SUMMARY");
            Console.WriteLine($"Highest Degree: {response.ResumeData?.Education?.HighestDegree?.Name?.Normalized}");

            response.ResumeData?.Education?.EducationDetails?.ForEach(edu =>
            {
                Console.WriteLine($"{Environment.NewLine}EDUCATION '{edu.Id}'");
                Console.WriteLine($"School: {edu.SchoolName?.Normalized}");
                Console.WriteLine($"Degree: {edu.Degree?.Name?.Normalized}");
                if (edu.Majors != null)
                    Console.WriteLine($"Focus: {string.Join(", ", edu.Majors)}");
                if (edu.GPA != null)
                    Console.WriteLine($"GPA: {edu.GPA?.NormalizedScore}/1.0 ({edu.GPA?.Score}/{edu.GPA?.MaxScore})");
                string endDateRepresents = edu.Graduated?.Value ?? false ? "Graduated" : "Last Attended";
                Console.WriteLine($"{endDateRepresents}: {GetTxDateAsString(edu.EndDate)}");
            });
        }

        static string GetTxDateAsString(TxDate date)
        {
            //a TxDate represents a date found on a resume, so it can either be 
            //'current', as in "July 2018 - current"
            //a year, as in "2018 - 2020"
            //a year and month, as in "2018/06 - 2020/07"
            //a year/month/day, as in "5/4/2018 - 7/2/2020"

            if (date == null) return "";
            if (date.IsCurrentDate) return "current";

            string format = "yyyy";

            if (date.FoundMonth)
            {
                format += "-MM";//only print the month if it was actually found on the resume/job

                if (date.FoundDay) format += "-dd";//only print the day if it was actually found
            }

            return date.Date.ToString(format);
        }
    }
}

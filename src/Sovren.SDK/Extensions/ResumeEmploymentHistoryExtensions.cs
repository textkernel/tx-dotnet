// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.Resume.Employment;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sovren
{
    /// <summary></summary>
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public static class ResumeEmploymentHistoryExtensions
    {
        /// <summary>
        /// Gets the current job, or the first job marked as 'current' if there are more than one
        /// </summary>
        public static Position GetCurrentJob(this ParseResumeResponseExtensions exts)
        {
            //if more than 1 'current', report the one w/ the smallest date range (most recent start date)
            //      if the same, report the one w/ co name and title
            //      if both have, report first one

            IEnumerable<Position> currentJobs = exts.Response.Value.ResumeData?.EmploymentHistory?.Positions?.Where(p => p.IsCurrent);

            if (currentJobs == null || currentJobs.Count() == 0)
            {
                return null;
            }
            else if (currentJobs.Count() == 1)
            {
                //this is the easy case, only 1 job that has 'xx\xxxx - current' on it
                return currentJobs.First();
            }
            else
            {
                //there are 2+ jobs listed as 'current', we will be smart about which one to report
                DateTime maxStartDate = currentJobs.Max(j => j.StartDate.Date);
                IEnumerable<Position> jobsWithMaxStartDate = currentJobs.Where(j => j.StartDate.Date == maxStartDate);
                IEnumerable<Position> jobsWithCoNameAndTitle = jobsWithMaxStartDate
                    .Where(j => !string.IsNullOrWhiteSpace(j.Employer?.Name?.Normalized) &&
                                !string.IsNullOrWhiteSpace(j.JobTitle?.Normalized));

                if (jobsWithMaxStartDate.Count() == 1 || jobsWithCoNameAndTitle.Count() == 0)
                {
                    //there is only 1 job w/ a recent start date, or there are 2+ but none have good data
                    return jobsWithMaxStartDate.First();
                }
                else
                {
                    //there are 2+ jobs w/ a recent start date, return one that has company name and title (good data)
                    return jobsWithCoNameAndTitle.First();
                }
                
            }
        }

        /// <summary>
        /// Gets the number of work history entries the candidate listed (if found) or 0
        /// </summary>
        public static int GetNumberOfPositions(this ParseResumeResponseExtensions exts)
        {
            return exts.Response.Value.ResumeData?.EmploymentHistory?.Positions?.Count ?? 0;
        }

        /// <summary>
        /// Gets the candidate's nth work history entry (if exists) or <see langword="null"/>
        /// <br/>NOTE: this is 1-based, so pass in 1 to get the 1st entry, etc
        /// </summary>
        /// <param name="exts"></param>
        /// <param name="n">The 1-based index to use</param>
        public static Position GetNthPosition1Based(this ParseResumeResponseExtensions exts, int n)
        {
            return exts.Response.Value.ResumeData?.EmploymentHistory?.Positions?.ElementAtOrDefault(n - 1);
        }

        /// <summary>
        /// The highest number of employees supervised at any position. Will return 0 if there is no evidence of supervising any employees.
        /// </summary>
        public static int GetHighestNumEmployeesSupervised(this ParseResumeResponseExtensions exts)
        {
            return exts.Response.Value.ResumeData?.EmploymentHistory?.Positions?.Max(p => (p.NumberEmployeesSupervised?.Value ?? 0)) ?? 0;
        }

        /// <summary>
        /// Gets a list of all job titles the candidate listed (if any) or <see langword="null"/>
        /// </summary>
        public static IEnumerable<string> GetAllJobTitles(this ParseResumeResponseExtensions exts)
        {
            return exts.Response.Value.ResumeData?.EmploymentHistory?.Positions?
                .Where(p => !string.IsNullOrWhiteSpace(p.JobTitle?.Normalized))
                .Select(p => p.JobTitle?.Normalized);
        }

        /// <summary>
        /// Gets a list of all employers the candidate listed (if any) or <see langword="null"/>
        /// </summary>
        public static IEnumerable<string> GetAllEmployers(this ParseResumeResponseExtensions exts)
        {
            return exts.Response.Value.ResumeData?.EmploymentHistory?.Positions?
                .Where(p => !string.IsNullOrWhiteSpace(p.Employer?.Name?.Normalized))
                .Select(p => p.Employer?.Name?.Normalized);
        }
    }
}

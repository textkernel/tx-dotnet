// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Collections.Generic;

namespace Sovren.Models.Resume
{
    /// <summary>
    /// Personal attributes found on a resume
    /// </summary>
    public class PersonalAttributes
    {
        /// <summary>
        /// The availability of the candidate
        /// </summary>
        public string Availability { get; set; }
        
        /// <summary>
        /// The birthplace of the candidate
        /// </summary>
        public string Birthplace { get; set; }

        /// <summary>
        /// The current location listed on the resume
        /// </summary>
        public string CurrentLocation { get; set; }
        
        /// <summary>
        /// The current salary listed on the resume
        /// </summary>
        public Salary CurrentSalary { get; set; }

        /// <summary>
        /// The date of birth given on the resume
        /// </summary>
        public SovrenDate DateOfBirth { get; set; }

        /// <summary>
        /// A driving license listed on the resume
        /// </summary>
        public string DrivingLicense { get; set; }

        /// <summary>
        /// The family composition
        /// </summary>
        public string FamilyComposition { get; set; }

        /// <summary>
        /// The candidate's father's name listed on the resume
        /// </summary>
        public string FathersName { get; set; }

        /// <summary>
        /// The candidate's gender listed on the resume
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Used in Chinese resumes
        /// </summary>
        public string HukouCity { get; set; }

        /// <summary>
        /// Used in Chinese resumes
        /// </summary>
        public string HukouArea { get; set; }

        /// <summary>
        /// The marital status listed on the resume
        /// </summary>
        public string MaritalStatus { get; set; }

        /// <summary>
        /// The candidate's mother's maiden name listed on the resume
        /// </summary>
        public string MothersMaidenName { get; set; }

        /// <summary>
        /// The candidate's mother tongue (native language) listed on the resume
        /// </summary>
        public string MotherTongue { get; set; }

        /// <summary>
        /// Any national identities provided on the resume
        /// </summary>
        public List<NationalIdentity> NationalIdentities { get; set; }

        /// <summary>
        /// The candidate's nationality listed on the resume
        /// </summary>
        public string Nationality { get; set; }

        /// <summary>
        /// The candidate's passport number listed on the resume
        /// </summary>
        public string PassportNumber { get; set; }

        /// <summary>
        /// The candidate's preferred location listed on the resume
        /// </summary>
        public string PreferredLocation { get; set; }

        /// <summary>
        /// The candidate's required salary listed on the resume
        /// </summary>
        public Salary RequiredSalary { get; set; }

        /// <summary>
        /// The candidate's visa status listed on the resume
        /// </summary>
        public string VisaStatus { get; set; }

        /// <summary>
        /// Whether the candidate is willing to relocate
        /// </summary>
        public string WillingToRelocate { get; set; }
    }

    /// <summary>
    /// A national identity found on a resume
    /// </summary>
    public class NationalIdentity
    {
        /// <summary>
        /// The national identity number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// The type of identity/number this is (for example, SSN)
        /// </summary>
        public string Phrase { get; set; }
    }

    /// <summary>
    /// A salary found in a resume
    /// </summary>
    public class Salary
    {
        /// <summary>
        /// The three-letter currency, eg: USD
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// The amount of the salary (usually yearly when listed on a resume)
        /// </summary>
        public decimal Amount { get; set; }
    }
}

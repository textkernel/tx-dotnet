// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.Resume.Education
{
    /// <summary>
    /// An educational degree
    /// </summary>
    public class Degree
    {
        /// <summary>
        /// The name of the degree
        /// </summary>
        public NormalizedString Name { get; set; }

        /// <summary>
        /// These values are not very global-friendly, but the Parser does normalize all degrees
        /// to one of these pre-defined types.This list is sorted, as well as possible, by increasing
        /// level of education, although there are certainly ambiguities from one discipline to
        /// another, such as whether professional is above or below bachelors. Here are the possible values:
        /// <br/>
        /// <br/>- lessThanHighSchool
        /// <br/>- specialeducation
        /// <br/>- someHighSchoolOrEquivalent
        /// <br/>- ged
        /// <br/>- certification
        /// <br/>- vocational
        /// <br/>- secondary
        /// <br/>- highSchoolOrEquivalent
        /// <br/>- someCollege
        /// <br/>- HND_HNC_OrEquivalent
        /// <br/>- ASc
        /// <br/>- associates
        /// <br/>- international
        /// <br/>- professional
        /// <br/>- postprofessional
        /// <br/>- BSc
        /// <br/>- bachelors
        /// <br/>- somePostgraduate
        /// <br/>- MBA
        /// <br/>- MSc
        /// <br/>- masters
        /// <br/>- intermediategraduate
        /// <br/>- doctorate
        /// <br/>- postdoctorate
        /// </summary>
        public string Type { get; set; }
    }
}

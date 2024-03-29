// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Textkernel.Tx.Models.Resume.Education
{
    /// <summary>
    /// An education entry on a resume
    /// </summary>
    public class EducationDetails
    {
        /// <summary>
        /// The id of this education entry (one-based, so EDU-1 is the first, etc)
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The raw text from the resume
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The name of the school attended
        /// </summary>
        public NormalizedString SchoolName { get; set; }

        /// <summary>
        /// The type of the school attended. One of:
        /// <br/>UNSPECIFIED
        /// <br/>lowerSchool
        /// <br/>highschool
        /// <br/>secondary
        /// <br/>trade
        /// <br/>professional
        /// <br/>vocational
        /// <br/>community
        /// <br/>college
        /// <br/>university
        /// </summary>
        public string SchoolType { get; set; }

        /// <summary>
        /// The Country/Region/City of the school, if found
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// The degree obtained (or worked toward)
        /// </summary>
        public Degree Degree { get; set; }

        /// <summary>
        /// Any majors/areas of major focus
        /// </summary>
        public List<string> Majors { get; set; }

        /// <summary>
        /// Any minors/areas of minor focus
        /// </summary>
        public List<string> Minors { get; set; }

        /// <summary>
        /// The GPA/marks listed on the resume
        /// </summary>
        public GradePointAverage GPA { get; set; }

        /// <summary>
        /// The date graduated or education ended
        /// </summary>
        [Obsolete("You should use the EndDate instead.")]
        public TxDate LastEducationDate { get; set; }

        /// <summary>
        /// The date education started
        /// </summary>
        public TxDate StartDate { get; set; }

        /// <summary>
        /// The date graduated or education ended
        /// </summary>
        public TxDate EndDate { get; set; }

        /// <summary>
        /// Whether or not the candidate graduated
        /// </summary>
        public TxPrimitive<bool> Graduated { get; set; }
    }
}

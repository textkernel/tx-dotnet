// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.SDK.Tests
{
    public static class TestData
    {
        private static string _resumeText = @"
John Wesson

Work History
Sr. Software Developer at Sovren Inc.   07/2017 - 07/2018
- used Javascript and ReactJS to make a web app";

        public static Document Resume = new Document(Encoding.UTF8.GetBytes(_resumeText), DateTime.Today);

        private static string _resumeTextWithAddress = @"
John Wesson

4544 McKinney Ave 
Dallas, TX 75205

Work History
Sr. Software Developer at Sovren Inc.   07/2017 - 07/2018
- used Javascript and ReactJS to make a web app";

        public static Document ResumeWithAddress = new Document(Encoding.UTF8.GetBytes(_resumeTextWithAddress), DateTime.Today);


        private static string _resumePersonalInformationText = @"
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
Passport Number: 5234098423478";

        public static Document ResumePersonalInformation = new Document(Encoding.UTF8.GetBytes(_resumePersonalInformationText), DateTime.Today);


        private static string _jobOrderText = @"
Position Title: Sales Manager

Skills:  
    Budgeting
    Audit
    Financial Statements";

        public static Document JobOrder = new Document(Encoding.UTF8.GetBytes(_jobOrderText), DateTime.Today);

        private static string _jobOrderTextWithAddress = @"
Position Title: Sales Manager

City:	  San Francisco
State:	  CA
Zipcode:  45678

Skills:  
    Budgeting
    Audit
    Financial Statements";

        public static Document JobOrderWithAddress = new Document(Encoding.UTF8.GetBytes(_jobOrderTextWithAddress), DateTime.Today);

        private static string _jobOrderTextTech = @"
Position Title: Sr. Software Developer

Skills:  
    JavaScript
    ReactJS";

        public static Document JobOrderTech = new Document(Encoding.UTF8.GetBytes(_jobOrderTextTech), DateTime.Today);

    }
}

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

        private static string _jobOrderText = @"
Position Title: Sales Manager

City:	  San Francisco
State:	  CA
Zipcode:  45678

Skills:  
    Budgeting
    Audit
    Financial Statements";

        public static Document JobOrder = new Document(Encoding.UTF8.GetBytes(_jobOrderText), DateTime.Today);
    }
}

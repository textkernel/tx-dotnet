// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Text;
using Textkernel.Tx.Models.Job;
using Textkernel.Tx.Models.Resume;

namespace Textkernel.Tx.Models.API.MatchV2.Request
{
    public class AddCandidateRequest : MatchV2RequestBase
    {
        public bool Anonymize { get; set; }
        public ParsedResume ResumeData { get; set; }
    }
}

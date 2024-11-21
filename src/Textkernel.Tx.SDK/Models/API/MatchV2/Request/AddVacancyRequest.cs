// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using System;
using System.Collections.Generic;
using System.Text;
using Textkernel.Tx.Models.Job;

namespace Textkernel.Tx.Models.API.MatchV2.Request
{
    public class AddVacancyRequest : MatchRequestBase
    {
        public ParsedJob JobData { get; set; }
    }
}

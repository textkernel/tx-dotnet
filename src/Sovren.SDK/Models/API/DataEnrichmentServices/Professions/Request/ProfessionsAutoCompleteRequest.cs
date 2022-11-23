// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.API.DataEnrichmentServices.Professions.Request
{
    public class ProfessionsAutoCompleteRequest
    {
        public string Prefix { get; set; }
        public int Limit { get; set; } = 10;
        public List<string> Categories { get; set; }
        public List<string> Languages { get; set; }
        public string OutputLanguage { get; set; } = "en";
    }
}

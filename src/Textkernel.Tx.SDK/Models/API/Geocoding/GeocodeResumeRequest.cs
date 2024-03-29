﻿// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.Resume;

namespace Textkernel.Tx.Models.API.Geocoding
{
    /// <inheritdoc/>
    public class GeocodeResumeRequest : GeocodeOptionsBase
    {
        /// <summary>
        /// The resume you wish to be geocoded
        /// </summary>
        public ParsedResume ResumeData { get; set; }
    }
}

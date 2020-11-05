// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;
using System.Collections.Generic;
using System.Text;

namespace Sovren.Models.Resume
{
    /// <summary>
    /// All training info found in a resume
    /// </summary>
    public class TrainingHistory
    {
        /// <summary>
        /// The full text where we found all training history
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Information about each training history we found
        /// </summary>
        public List<TrainingDetails> Trainings { get; set; }
    }

    /// <summary>
    /// A training history found on a resume
    /// </summary>
    public class TrainingDetails
    {
        /// <summary>
        /// The text that was found on the resume
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The name of the school or company where the training occurred.
        /// </summary>
        public string Entity { get; set; }

        /// <summary>
        /// Any text within the <see cref="Text"/> that is recognized as a qualification (such as DDS),
        /// degree (such as B.S.), or a certification (such as PMP). Each qualification is listed separately.
        /// </summary>
        public List<string> Qualifications { get; set; }

        /// <summary>
        /// The date the training started
        /// </summary>
        public SovrenDate StartDate { get; set; }

        /// <summary>
        /// The date the training ended
        /// </summary>
        public SovrenDate EndDate { get; set; }
    }
}

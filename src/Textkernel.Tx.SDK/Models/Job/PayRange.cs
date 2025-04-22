// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.Job
{
    /// <summary>
    /// An object containing details about a job position's pay.
    /// </summary>
    public class PayRange
    {
        /// <summary>
        /// The normalized minimum yearly salary
        /// </summary>
        public TxPrimitive<int> Minimum { get; set; }

        /// <summary>
        /// The normalized maximum yearly salary
        /// </summary>
        public TxPrimitive<int> Maximum { get; set; }

        /// <summary>
        /// The raw, un-normalized, minimum value. This is returned as is in the text, so there is no guarantee that it will evaluate to a valid number and not a string.
        /// </summary>
        public string RawMinimum { get; set; }

        /// <summary>
        /// The raw, un-normalized, maximum value. This is returned as is in the text, so there is no guarantee that it will evaluate to a valid number and not a string.
        /// </summary>
        public string RawMaximum { get; set; }

        /// <summary>
        /// Currency code (ISO 4217) applied to the <see cref="Minimum"/> and <see cref="Maximum"/>
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Time scale applied to the raw values to get the minimum and maximum annual salary. Possible values are:
        /// <br/> - Hourly
        /// <br/> - Daily
        /// <br/> - Weekly
        /// <br/> - Monthly
        /// <br/> - Annually
        /// <br/>
        /// If no lexical cues are available from the job, the time scale is guessed based on predefined salary ranges.
        /// Here are some rough salary ranges (note that country-specific conditions may apply):
        /// <br/>- 1 or 2 digits salary (9, 12): hourly
        /// <br/>- 3 or 4 digits salary (3800, 5000): monthly
        /// <br/>- 5 digit salary (38000, 50000): yearly
        /// <br/>
        /// If a monthly salary is extracted, to get the annual salary it is multiplied by 14 (if country = AT) or 12 (all other countries).
        /// </summary>
        public string TimeScale { get; set; }
    }
}

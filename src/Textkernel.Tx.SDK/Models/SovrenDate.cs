// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System;

namespace Sovren.Models
{
    /// <summary>
    /// Sovren's custom Date type that represents dates found in resumes/jobs. The following are common examples:
    /// <br/> - Current, as in "July 2018 - current". See <see cref="IsCurrentDate"/>
    /// <br/> - Year only, as in "2018 - 2020". <see cref="FoundYear"/> will be <see langword="true"/>, 
    /// <see cref="FoundMonth"/> and <see cref="FoundDay"/> will be <see langword="false"/>
    /// <br/> - Year and month, as in "2018/06 - 2020/07". <see cref="FoundYear"/> and <see cref="FoundMonth"/> will be <see langword="true"/>, 
    /// <see cref="FoundDay"/> will be <see langword="false"/>
    /// <br/> - Year/month/day, as in "5/4/2018 - 7/2/2020". <see cref="FoundYear"/>, <see cref="FoundMonth"/>, 
    /// and <see cref="FoundDay"/> will be <see langword="true"/>
    /// </summary>
    public class SovrenDate
    {
        /// <summary>
        /// The ISO 8601 (yyyy-MM-dd) date, if the day and/or month could not be found, they will be 01
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// <see langword="true"/> if this date represents '- current' and not an actual date
        /// </summary>
        public bool IsCurrentDate { get; set; }

        /// <summary>
        /// <see langword="true"/> if the year was found in the text, otherwise <see langword="false"/>
        /// </summary>
        public bool FoundYear { get; set; }

        /// <summary>
        /// <see langword="true"/> if the month was found in the text (eg: June 2020), otherwise <see langword="false"/> (eg: 2020)
        /// </summary>
        public bool FoundMonth { get; set; }

        /// <summary>
        /// <see langword="true"/> if the day was found in the text (eg: June 7, 2020), otherwise <see langword="false"/> (eg: June 2020)
        /// </summary>
        public bool FoundDay { get; set; }
    }
}

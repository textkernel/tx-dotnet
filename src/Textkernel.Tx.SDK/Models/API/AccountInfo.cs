// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.API
{
    /// <summary>
    /// Contains information about the account making the API call
    /// </summary>
    public class AccountInfo
    {
        /// <summary>
        /// The AccountId for the account
        /// </summary>
        public string AccountId { get; set; }
        
        /// <summary>
        /// The customer name on the account
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The client IP Address where the API call originated
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// The region for the account, also known as the 'Data Center'
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// The number of credits remaining to be used by the account
        /// </summary>
        public decimal CreditsRemaining { get; set; }

        /// <summary>
        /// The number of credits used by the account
        /// </summary>
        public decimal CreditsUsed { get; set; }

        /// <summary>
        /// The number of requests that can be made at one time
        /// </summary>
        public int MaximumConcurrentRequests { get; set; }

        /// <summary>
        /// The date that the current credits expire
        /// </summary>
        public string ExpirationDate { get; set; }
    }
}

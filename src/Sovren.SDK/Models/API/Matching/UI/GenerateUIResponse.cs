// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using System.Text.Json.Serialization;

namespace Sovren.Models.API.Matching.UI
{
    /// <summary>
    /// The response body from a request to generate the Matching UI
    /// </summary>
    public class GenerateUIResponse
    {
        /// <summary>
        /// The URL to navigate to (or set as the src for an iFrame) that will
        /// display the Matching UI. This URL will be valid for at least 24 hours.
        /// <br/><b>NOTE: for security purposes, you should redirect a user to the session
        /// immediately after generating the session for that user</b>
        /// <br/>For more info, see <see href="https://sovren.com/technical-specs/latest/rest-api/matching-ui/overview/#authentication">here</see>
        /// </summary>
        [JsonPropertyName("url")]
        public string URL { get; set; }

        /// <summary>
        /// The number of seconds until the URL auto-authentication expires. If you try
        /// to use this URL after that period of time, the user will be asked to
        /// login (if not already authenticated)
        /// </summary>
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }
    }
}

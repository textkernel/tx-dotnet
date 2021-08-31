// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Models.API.Matching.Request;
using System.Collections.Generic;

namespace Sovren.Models.API.Matching.UI.Hooks
{
    /// <summary>
    /// A collection of Matching UI User Action Hooks
    /// </summary>
    public class UserActionHookCollection
    {
        /// <summary>
        /// The client-side <see href="https://sovren.com/technical-specs/latest/rest-api/matching-ui/overview/#ui-match-hooks">User Action Hooks</see> 
        /// for a Matching UI session. These can be used to do some client-side action (opening a tab/window, running some javascript)
        /// when a user clicks a button on a particular match result.
        /// </summary>
        public List<ClientSideHook> Client { get; set; }

        /// <summary>
        /// The server-side (HTTP POST)
        /// <see href="https://sovren.com/technical-specs/latest/rest-api/matching-ui/overview/#ui-match-hooks">User Action Hooks</see> 
        /// for a Matching UI session. These can be used to do some server-side action (performs an HTTP POST to your server) 
        /// when a user clicks a button on a particular match result.
        /// </summary>
        public List<ServerSideHook> Server { get; set; }

        /// <summary>
        /// The server-side (HTTP POST)
        /// <see href="https://sovren.com/technical-specs/latest/rest-api/matching-ui/overview/#ui-match-hooks">User Action Hooks</see> 
        /// for 'Sovren Sourcing' results during a Matching UI session. These can be used to do some server-side action (performs an HTTP POST to your server) 
        /// when a user clicks a button on a particular 'Sovren Sourcing' result.
        /// </summary>
        public List<SourcingHook> Sourcing { get; set; }

        /// <summary>
        /// <see href="https://sovren.com/technical-specs/latest/rest-api/matching-ui/overview/#ui-match-hooks">User Action Hooks</see>
        /// that are executed when a user modifies the <see cref="SearchMatchRequestBase.FilterCriteria"/> 
        /// or <see cref="MatchRequest.PreferredCategoryWeights"/> and re-runs the query. The modified weights/filters are sent in the POST body
        /// so that you can save/use them in future sessions for this user.
        /// </summary>
        public OnUpdateHooks OnUpdate { get; set; }
    }
}

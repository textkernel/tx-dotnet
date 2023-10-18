// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.API.Matching.Request;
using System.Collections.Generic;

namespace Textkernel.Tx.Models.API.Matching.UI.Hooks
{
    /// <summary>
    /// A collection of Matching UI User Action Hooks
    /// </summary>
    public class UserActionHookCollection
    {
        /// <summary>
        /// The client-side <see href="https://developer.textkernel.com/Sovren/v10/matching-ui/overview/#ui-match-hooks">User Action Hooks</see> 
        /// for a Matching UI session. These can be used to do some client-side action (opening a tab/window, running some javascript)
        /// when a user clicks a button on a particular match result.
        /// </summary>
        public List<ClientSideHook> Client { get; set; }

        /// <summary>
        /// The server-side (HTTP POST)
        /// <see href="https://developer.textkernel.com/Sovren/v10/matching-ui/overview/#ui-match-hooks">User Action Hooks</see> 
        /// for a Matching UI session. These can be used to do some server-side action (performs an HTTP POST to your server) 
        /// when a user clicks a button on a particular match result.
        /// </summary>
        public List<ServerSideHook> Server { get; set; }

        /// <summary>
        /// The server-side (HTTP POST)
        /// <see href="https://developer.textkernel.com/Sovren/v10/matching-ui/overview/#ui-match-hooks">User Action Hooks</see> 
        /// for 'Sourcing' results during a Matching UI session. These can be used to do some server-side action (performs an HTTP POST to your server) 
        /// when a user clicks a button on a particular 'Sourcing' result.
        /// </summary>
        public List<SourcingHook> Sourcing { get; set; }

        /// <summary>
        /// <see href="https://developer.textkernel.com/Sovren/v10/matching-ui/overview/#ui-match-hooks">User Action Hooks</see>
        /// that are executed when a user modifies the <see cref="SearchMatchRequestBase.FilterCriteria"/> 
        /// or <see cref="MatchRequest.PreferredCategoryWeights"/> and re-runs the query. The modified weights/filters are sent in the POST body
        /// so that you can save/use them in future sessions for this user.
        /// </summary>
        public OnUpdateHooks OnUpdate { get; set; }
    }
}

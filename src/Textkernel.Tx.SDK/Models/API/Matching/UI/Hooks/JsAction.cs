// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

namespace Textkernel.Tx.Models.API.Matching.UI.Hooks
{
    /// <summary>
    /// An action performed in Javascript
    /// </summary>
    public class JsAction
    {
        /// <summary>
        /// Any data you want to be sent (in addition to document information) in the 'message'
        /// parameter for the window.postMessage() call. For more information see 
        /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/Window/postMessage">here</see>.
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// The 'targetOrigin' parameter for the window.postMessage() call. For more information see
        /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/Window/postMessage">here</see>.
        /// <br/>NOTE: while this is optional, it is recommended for security purposes in the window.postMessage() protocol
        /// </summary>
        public string TargetOrigin { get; set; }

        /// <summary>
        /// One of "parent" or "opener", depending if you use an iFrame to show the Matching UI 
        /// in your system, or if you open it in a separate tab/window.
        /// </summary>
        public string Target { get; set; }
    }
}

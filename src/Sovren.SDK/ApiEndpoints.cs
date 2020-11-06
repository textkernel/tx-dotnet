// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Rest;
using System;

namespace Sovren
{
    internal class ApiEndpoints
    {
        private static readonly string _matchUIPrefix = "/ui";
        private DataCenter _dataCenter;

        internal ApiEndpoints(DataCenter dataCenter)
        {
            _dataCenter = dataCenter;
        }

        private string Prefix(bool isMatchUI = false)
        {
            if (isMatchUI && !_dataCenter.IsSovrenSaaS)
            {
                throw new NotSupportedException("Cannot call Matching UI on a self-hosted installation.");
            }

            return $"{(isMatchUI ? _matchUIPrefix : "")}/{_dataCenter.Version}";
        }

        internal RestRequest ParseResume() => new RestRequest($"{Prefix()}/parser/resume", RestMethod.POST);
        internal RestRequest ParseJobOrder() => new RestRequest($"{Prefix()}/parser/joborder", RestMethod.POST);
        internal RestRequest GetAccountInfo() => new RestRequest($"{Prefix()}/account", RestMethod.GET);
        
        internal RestRequest CreateIndex(string id) => new RestRequest($"{Prefix()}/index/{id}", RestMethod.POST);
        internal RestRequest GetIndex(string id) => new RestRequest($"{Prefix()}/index/{id}", RestMethod.GET);
        internal RestRequest GetIndexDocumentCount(string id) => new RestRequest($"{Prefix()}/index/{id}/count", RestMethod.GET);
        internal RestRequest DeleteIndex(string id) => new RestRequest($"{Prefix()}/index/{id}", RestMethod.DELETE);
        internal RestRequest GetAllIndexes() => new RestRequest($"{Prefix()}/index", RestMethod.GET);
        
        internal RestRequest IndexResume(string indexId, string documentId) => new RestRequest($"{Prefix()}/index/{indexId}/resume/{documentId}", RestMethod.POST);
        internal RestRequest IndexJob(string indexId, string documentId) => new RestRequest($"{Prefix()}/index/{indexId}/job/{documentId}", RestMethod.POST);
        internal RestRequest DeleteDocument(string indexId, string documentId) => new RestRequest($"{Prefix()}/index/{indexId}/documents/{documentId}", RestMethod.DELETE);
        internal RestRequest GetResume(string indexId, string documentId) => new RestRequest($"{Prefix()}/index/{indexId}/resume/{documentId}", RestMethod.GET);
        internal RestRequest GetJob(string indexId, string documentId) => new RestRequest($"{Prefix()}/index/{indexId}/job/{documentId}", RestMethod.GET);
        
        internal RestRequest MatchResume(bool isMatchUI) => new RestRequest($"{Prefix(isMatchUI)}/matcher/resume", RestMethod.POST);
        internal RestRequest MatchByDocumentId(string indexId, string documentId, bool isMatchUI) => new RestRequest($"{Prefix(isMatchUI)}/matcher/indexes/{indexId}/documents/{documentId}", RestMethod.POST);
        internal RestRequest MatchJob(bool isMatchUI) => new RestRequest($"{Prefix(isMatchUI)}/matcher/job", RestMethod.POST);
        internal RestRequest Search(bool isMatchUI) => new RestRequest($"{Prefix(isMatchUI)}/searcher", RestMethod.POST);
        
        internal RestRequest BimetricScoreResume(bool isMatchUI) => new RestRequest($"{Prefix(isMatchUI)}/scorer/bimetric/resume", RestMethod.POST);
        internal RestRequest BimetricScoreJob(bool isMatchUI) => new RestRequest($"{Prefix(isMatchUI)}/scorer/bimetric/joborder", RestMethod.POST);
    }
}

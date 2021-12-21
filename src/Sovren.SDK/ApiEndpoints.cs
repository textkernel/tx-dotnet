// Copyright © 2020 Sovren Group, Inc. All rights reserved.
// This file is provided for use by, or on behalf of, Sovren licensees
// within the terms of their license of Sovren products or Sovren customers
// within the Terms of Service pertaining to the Sovren SaaS products.

using Sovren.Rest;
using System;
using System.Web;

namespace Sovren
{
    internal class ApiEndpoints
    {
        private static readonly string _matchUIPrefix = "/ui";
        private readonly DataCenter _dataCenter;

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

            String versionSuffix = "";
            if (!string.IsNullOrWhiteSpace(_dataCenter.Version))
            {
                versionSuffix = "/" + _dataCenter.Version;
            }

            return $"{(isMatchUI ? _matchUIPrefix : "")}{versionSuffix}";
        }

        private string Sanitize(string indexOrDocId)
        {
            if (string.IsNullOrEmpty(indexOrDocId))
            {
                throw new ArgumentException("Index or document id is null or empty");
            }

            foreach (char c in indexOrDocId)
            {
                //if its not a letter, digit, dash, or underscore, invalid
                if (!(char.IsLetterOrDigit(c) || c == '-' || c == '_'))
                {
                    string charName = char.IsWhiteSpace(c) ? "whitespace" : c.ToString();
                    throw new ArgumentException("Index or document id contains an invalid character: " + charName);
                }
            }

            return indexOrDocId;
        }

        internal RestRequest ParseResume() => new RestRequest($"{Prefix()}/parser/resume", RestMethod.POST);
        internal RestRequest ParseJobOrder() => new RestRequest($"{Prefix()}/parser/joborder", RestMethod.POST);
        internal RestRequest GetAccountInfo() => new RestRequest($"{Prefix()}/account", RestMethod.GET);

        internal RestRequest FormatResume() => new RestRequest($"{Prefix()}/formatter/resume", RestMethod.POST);
        
        internal RestRequest CreateIndex(string id) => new RestRequest($"{Prefix()}/index/{Sanitize(id)}", RestMethod.POST);
        internal RestRequest GetIndexDocumentCount(string id) => new RestRequest($"{Prefix()}/index/{Sanitize(id)}/count", RestMethod.GET);
        internal RestRequest DeleteIndex(string id) => new RestRequest($"{Prefix()}/index/{Sanitize(id)}", RestMethod.DELETE);
        internal RestRequest GetAllIndexes() => new RestRequest($"{Prefix()}/index", RestMethod.GET);
        
        internal RestRequest IndexResume(string indexId, string documentId) => new RestRequest($"{Prefix()}/index/{Sanitize(indexId)}/resume/{Sanitize(documentId)}", RestMethod.POST);
        internal RestRequest IndexJob(string indexId, string documentId) => new RestRequest($"{Prefix()}/index/{Sanitize(indexId)}/joborder/{Sanitize(documentId)}", RestMethod.POST);
        internal RestRequest IndexMultipleResumes(string indexId) => new RestRequest($"{Prefix()}/index/{Sanitize(indexId)}/resumes", RestMethod.POST);
        internal RestRequest IndexMultipleJobs(string indexId) => new RestRequest($"{Prefix()}/index/{Sanitize(indexId)}/joborders", RestMethod.POST);
        internal RestRequest DeleteDocument(string indexId, string documentId) => new RestRequest($"{Prefix()}/index/{Sanitize(indexId)}/documents/{Sanitize(documentId)}", RestMethod.DELETE);
        internal RestRequest DeleteMultipleDocuments(string indexId) => new RestRequest($"{Prefix()}/index/{Sanitize(indexId)}/documents/delete", RestMethod.POST);
        internal RestRequest GetResume(string indexId, string documentId) => new RestRequest($"{Prefix()}/index/{Sanitize(indexId)}/resume/{Sanitize(documentId)}", RestMethod.GET);
        internal RestRequest GetJob(string indexId, string documentId) => new RestRequest($"{Prefix()}/index/{Sanitize(indexId)}/joborder/{Sanitize(documentId)}", RestMethod.GET);
        internal RestRequest UpdateResumeUserDefinedTags(string indexId, string documentId) => new RestRequest($"{Prefix()}/index/{Sanitize(indexId)}/resume/{Sanitize(documentId)}", RestMethod.PATCH);
        internal RestRequest UpdateJobUserDefinedTags(string indexId, string documentId) => new RestRequest($"{Prefix()}/index/{Sanitize(indexId)}/joborder/{Sanitize(documentId)}", RestMethod.PATCH);


        internal RestRequest MatchResume(bool isMatchUI) => new RestRequest($"{Prefix(isMatchUI)}/matcher/resume", RestMethod.POST);
        internal RestRequest MatchByDocumentId(string indexId, string documentId, bool isMatchUI) => new RestRequest($"{Prefix(isMatchUI)}/matcher/indexes/{Sanitize(indexId)}/documents/{Sanitize(documentId)}", RestMethod.POST);
        internal RestRequest MatchJob(bool isMatchUI) => new RestRequest($"{Prefix(isMatchUI)}/matcher/joborder", RestMethod.POST);
        internal RestRequest Search(bool isMatchUI) => new RestRequest($"{Prefix(isMatchUI)}/searcher", RestMethod.POST);
        
        internal RestRequest BimetricScoreResume(bool isMatchUI) => new RestRequest($"{Prefix(isMatchUI)}/scorer/bimetric/resume", RestMethod.POST);
        internal RestRequest BimetricScoreJob(bool isMatchUI) => new RestRequest($"{Prefix(isMatchUI)}/scorer/bimetric/joborder", RestMethod.POST);

        internal RestRequest GeocodeResume() => new RestRequest($"{Prefix()}/geocoder/resume", RestMethod.POST);
        internal RestRequest GeocodeJob() => new RestRequest($"{Prefix()}/geocoder/joborder", RestMethod.POST);
        internal RestRequest GeocodeAndIndexResume() => new RestRequest($"{Prefix()}/geocodeAndIndex/resume", RestMethod.POST);
        internal RestRequest GeocodeAndIndexJob() => new RestRequest($"{Prefix()}/geocodeAndIndex/joborder", RestMethod.POST);

        internal RestRequest ViewDetailsResume() => new RestRequest($"{Prefix(true)}/details/resume", RestMethod.POST);
        internal RestRequest ViewDetailsJob() => new RestRequest($"{Prefix(true)}/details/job", RestMethod.POST);
        internal RestRequest ViewDetailsIndexed() => new RestRequest($"{Prefix(true)}/details", RestMethod.POST);
    }
}

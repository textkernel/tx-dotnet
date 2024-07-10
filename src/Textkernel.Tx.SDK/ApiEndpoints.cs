// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.API.DataEnrichment;
using System;
using System.Net.Http;

namespace Textkernel.Tx
{
    internal class ApiEndpoints
    {
        private static readonly string _matchUIPrefix = "ui/";
        private readonly DataCenter _dataCenter;

        internal ApiEndpoints(DataCenter dataCenter)
        {
            _dataCenter = dataCenter;
        }

        private string Prefix(bool isMatchUI = false)
        {
            if (isMatchUI && !_dataCenter.IsSaaS)
            {
                throw new NotSupportedException("Cannot call Matching UI on a self-hosted installation.");
            }

            String versionSuffix = "";
            if (!string.IsNullOrWhiteSpace(_dataCenter.Version))
            {
                versionSuffix = _dataCenter.Version + "/";
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

        internal HttpRequestMessage ParseResume() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}parser/resume");
        internal HttpRequestMessage ParseJobOrder() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}parser/joborder");
        internal HttpRequestMessage GetAccountInfo() => new HttpRequestMessage(HttpMethod.Get, $"{Prefix()}account");

        internal HttpRequestMessage FormatResume() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}formatter/resume");
        internal HttpRequestMessage FormatResumeWithTemplate() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}formatter/resume/template");

        internal HttpRequestMessage CreateIndex(string id) => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}index/{Sanitize(id)}");
        internal HttpRequestMessage GetIndexDocumentCount(string id) => new HttpRequestMessage(HttpMethod.Get, $"{Prefix()}index/{Sanitize(id)}/count");
        internal HttpRequestMessage DeleteIndex(string id) => new HttpRequestMessage(HttpMethod.Delete, $"{Prefix()}index/{Sanitize(id)}");
        internal HttpRequestMessage GetAllIndexes() => new HttpRequestMessage(HttpMethod.Get, $"{Prefix()}index");
        
        internal HttpRequestMessage IndexResume(string indexId, string documentId) => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}index/{Sanitize(indexId)}/resume/{Sanitize(documentId)}");
        internal HttpRequestMessage IndexJob(string indexId, string documentId) => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}index/{Sanitize(indexId)}/joborder/{Sanitize(documentId)}");
        internal HttpRequestMessage IndexMultipleResumes(string indexId) => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}index/{Sanitize(indexId)}/resumes");
        internal HttpRequestMessage IndexMultipleJobs(string indexId) => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}index/{Sanitize(indexId)}/joborders");
        internal HttpRequestMessage DeleteDocument(string indexId, string documentId) => new HttpRequestMessage(HttpMethod.Delete, $"{Prefix()}index/{Sanitize(indexId)}/documents/{Sanitize(documentId)}");
        internal HttpRequestMessage DeleteMultipleDocuments(string indexId) => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}index/{Sanitize(indexId)}/documents/delete");
        internal HttpRequestMessage GetResume(string indexId, string documentId) => new HttpRequestMessage(HttpMethod.Get, $"{Prefix()}index/{Sanitize(indexId)}/resume/{Sanitize(documentId)}");
        internal HttpRequestMessage GetJob(string indexId, string documentId) => new HttpRequestMessage(HttpMethod.Get, $"{Prefix()}index/{Sanitize(indexId)}/joborder/{Sanitize(documentId)}");
        internal HttpRequestMessage UpdateResumeUserDefinedTags(string indexId, string documentId) => new HttpRequestMessage(new HttpMethod("PATCH"), $"{Prefix()}index/{Sanitize(indexId)}/resume/{Sanitize(documentId)}");
        internal HttpRequestMessage UpdateJobUserDefinedTags(string indexId, string documentId) => new HttpRequestMessage(new HttpMethod("PATCH"), $"{Prefix()}index/{Sanitize(indexId)}/joborder/{Sanitize(documentId)}");


        internal HttpRequestMessage MatchResume(bool isMatchUI) => new HttpRequestMessage(HttpMethod.Post, $"{Prefix(isMatchUI)}matcher/resume");
        internal HttpRequestMessage MatchByDocumentId(string indexId, string documentId, bool isMatchUI) => new HttpRequestMessage(HttpMethod.Post, $"{Prefix(isMatchUI)}matcher/indexes/{Sanitize(indexId)}/documents/{Sanitize(documentId)}");
        internal HttpRequestMessage MatchJob(bool isMatchUI) => new HttpRequestMessage(HttpMethod.Post, $"{Prefix(isMatchUI)}matcher/joborder");
        internal HttpRequestMessage Search(bool isMatchUI) => new HttpRequestMessage(HttpMethod.Post, $"{Prefix(isMatchUI)}searcher");
        
        internal HttpRequestMessage BimetricScoreResume(bool isMatchUI) => new HttpRequestMessage(HttpMethod.Post, $"{Prefix(isMatchUI)}scorer/bimetric/resume");
        internal HttpRequestMessage BimetricScoreJob(bool isMatchUI) => new HttpRequestMessage(HttpMethod.Post, $"{Prefix(isMatchUI)}scorer/bimetric/joborder");

        internal HttpRequestMessage GeocodeResume() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}geocoder/resume");
        internal HttpRequestMessage GeocodeJob() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}geocoder/joborder");
        internal HttpRequestMessage GeocodeAndIndexResume() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}geocodeAndIndex/resume");
        internal HttpRequestMessage GeocodeAndIndexJob() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}geocodeAndIndex/joborder");

        internal HttpRequestMessage ViewDetailsResume() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix(true)}details/resume");
        internal HttpRequestMessage ViewDetailsJob() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix(true)}details/job");
        internal HttpRequestMessage ViewDetailsIndexed() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix(true)}details");

        internal HttpRequestMessage DESSkillsGetTaxonomy(TaxonomyFormat format) => new HttpRequestMessage(HttpMethod.Get, $"{Prefix()}skills/Taxonomy?format={format}");
        internal HttpRequestMessage DESGetSkillsMetadata() => new HttpRequestMessage(HttpMethod.Get, $"{Prefix()}skills/Metadata");
        internal HttpRequestMessage DESSkillsNormalize() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}skills/Normalize");
        internal HttpRequestMessage DESSkillsExtract() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}skills/Extract");
        internal HttpRequestMessage DESSkillsLookup() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}skills/Lookup");
        internal HttpRequestMessage DESSkillsAutoComplete() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}skills/AutoComplete");
        internal HttpRequestMessage DESProfessionsGetTaxonomy(TaxonomyFormat format, string language) => new HttpRequestMessage(HttpMethod.Get, $"{Prefix()}professions/Taxonomy?format={format}&language={language}");
        internal HttpRequestMessage DESGetProfessionsMetadata() => new HttpRequestMessage(HttpMethod.Get, $"{Prefix()}professions/Metadata");
        internal HttpRequestMessage DESProfessionsNormalize() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}professions/Normalize");
        internal HttpRequestMessage DESProfessionsLookup() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}professions/Lookup");
        internal HttpRequestMessage DESProfessionsAutoComplete() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}professions/AutoComplete");
        internal HttpRequestMessage DESOntologySuggestSkillsFromProfessions() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}ontology/suggest-skills-from-professions");
        internal HttpRequestMessage DESOntologySuggestSkillsFromSkills() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}ontology/suggest-skills-from-skills");
        internal HttpRequestMessage DESOntologyCompareProfessions() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}ontology/compare-professions");
        internal HttpRequestMessage DESOntologySuggestProfessions() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}ontology/suggest-professions");
        internal HttpRequestMessage DESOntologyCompareSkillsToProfessions() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}ontology/compare-skills-to-profession");
        internal HttpRequestMessage DESOntologySkillsSimilarityScore() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}ontology/skills-similarity-score");

        internal HttpRequestMessage JobDescriptionGenerate() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}job-description/generate");
        internal HttpRequestMessage JobDescriptionSuggestSkills() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}job-description/suggest-skills");
    }
}

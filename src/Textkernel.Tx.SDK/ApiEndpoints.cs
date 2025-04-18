// Copyright © 2023 Textkernel BV. All rights reserved.
// This file is provided for use by, or on behalf of, Textkernel licensees
// within the terms of their license of Textkernel products or Textkernel customers
// within the Terms of Service pertaining to the Textkernel SaaS products.

using Textkernel.Tx.Models.API.DataEnrichment;
using System;
using System.Net.Http;
using System.Collections.Generic;

namespace Textkernel.Tx
{
    internal static class ApiEndpoints
    {
        private static string Sanitize(string indexOrDocId)
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

        internal static HttpRequestMessage ParseResume() => new HttpRequestMessage(HttpMethod.Post, $"parser/resume");
        internal static HttpRequestMessage ParseJobOrder() => new HttpRequestMessage(HttpMethod.Post, $"parser/joborder");
        internal static HttpRequestMessage GetAccountInfo() => new HttpRequestMessage(HttpMethod.Get, $"account");
                  
        internal static HttpRequestMessage FormatResume() => new HttpRequestMessage(HttpMethod.Post, $"formatter/resume");
        internal static HttpRequestMessage FormatResumeWithTemplate() => new HttpRequestMessage(HttpMethod.Post, $"formatter/resume/template");
                  
        internal static HttpRequestMessage CreateIndex(string id) => new HttpRequestMessage(HttpMethod.Post, $"index/{Sanitize(id)}");
        internal static HttpRequestMessage GetIndexDocumentCount(string id) => new HttpRequestMessage(HttpMethod.Get, $"index/{Sanitize(id)}/count");
        internal static HttpRequestMessage DeleteIndex(string id) => new HttpRequestMessage(HttpMethod.Delete, $"index/{Sanitize(id)}");
        internal static HttpRequestMessage GetAllIndexes() => new HttpRequestMessage(HttpMethod.Get, $"index");
                  
        internal static HttpRequestMessage IndexResume(string indexId, string documentId) => new HttpRequestMessage(HttpMethod.Post, $"index/{Sanitize(indexId)}/resume/{Sanitize(documentId)}");
        internal static HttpRequestMessage IndexJob(string indexId, string documentId) => new HttpRequestMessage(HttpMethod.Post, $"index/{Sanitize(indexId)}/joborder/{Sanitize(documentId)}");
        internal static HttpRequestMessage IndexMultipleResumes(string indexId) => new HttpRequestMessage(HttpMethod.Post, $"index/{Sanitize(indexId)}/resumes");
        internal static HttpRequestMessage IndexMultipleJobs(string indexId) => new HttpRequestMessage(HttpMethod.Post, $"index/{Sanitize(indexId)}/joborders");
        internal static HttpRequestMessage DeleteDocument(string indexId, string documentId) => new HttpRequestMessage(HttpMethod.Delete, $"index/{Sanitize(indexId)}/documents/{Sanitize(documentId)}");
        internal static HttpRequestMessage DeleteMultipleDocuments(string indexId) => new HttpRequestMessage(HttpMethod.Post, $"index/{Sanitize(indexId)}/documents/delete");
        internal static HttpRequestMessage GetResume(string indexId, string documentId) => new HttpRequestMessage(HttpMethod.Get, $"index/{Sanitize(indexId)}/resume/{Sanitize(documentId)}");
        internal static HttpRequestMessage GetJob(string indexId, string documentId) => new HttpRequestMessage(HttpMethod.Get, $"index/{Sanitize(indexId)}/joborder/{Sanitize(documentId)}");
        internal static HttpRequestMessage UpdateResumeUserDefinedTags(string indexId, string documentId) => new HttpRequestMessage(new HttpMethod("PATCH"), $"index/{Sanitize(indexId)}/resume/{Sanitize(documentId)}");
        internal static HttpRequestMessage UpdateJobUserDefinedTags(string indexId, string documentId) => new HttpRequestMessage(new HttpMethod("PATCH"), $"index/{Sanitize(indexId)}/joborder/{Sanitize(documentId)}");
                  
                  
        internal static HttpRequestMessage MatchResume() => new HttpRequestMessage(HttpMethod.Post, $"matcher/resume");
        internal static HttpRequestMessage MatchByDocumentId(string indexId, string documentId) => new HttpRequestMessage(HttpMethod.Post, $"matcher/indexes/{Sanitize(indexId)}/documents/{Sanitize(documentId)}");
        internal static HttpRequestMessage MatchJob() => new HttpRequestMessage(HttpMethod.Post, $"matcher/joborder");
        internal static HttpRequestMessage Search() => new HttpRequestMessage(HttpMethod.Post, $"searcher");
                  
        internal static HttpRequestMessage BimetricScoreResume() => new HttpRequestMessage(HttpMethod.Post, $"scorer/bimetric/resume");
        internal static HttpRequestMessage BimetricScoreJob() => new HttpRequestMessage(HttpMethod.Post, $"scorer/bimetric/joborder");
                  
        internal static HttpRequestMessage GeocodeResume() => new HttpRequestMessage(HttpMethod.Post, $"geocoder/resume");
        internal static HttpRequestMessage GeocodeJob() => new HttpRequestMessage(HttpMethod.Post, $"geocoder/joborder");
        internal static HttpRequestMessage GeocodeAndIndexResume() => new HttpRequestMessage(HttpMethod.Post, $"geocodeAndIndex/resume");
        internal static HttpRequestMessage GeocodeAndIndexJob() => new HttpRequestMessage(HttpMethod.Post, $"geocodeAndIndex/joborder");

        private static string DESVersion(bool includeCerts) => includeCerts ? "/v2" : "";
        internal static HttpRequestMessage DESSkillsGetTaxonomy(bool includeCerts, TaxonomyFormat format) => new HttpRequestMessage(HttpMethod.Get, $"skills{DESVersion(includeCerts)}/Taxonomy?format={format}");
        internal static HttpRequestMessage DESGetSkillsMetadata(bool includeCerts) => new HttpRequestMessage(HttpMethod.Get, $"skills{DESVersion(includeCerts)}/Metadata");
        internal static HttpRequestMessage DESSkillsNormalize(bool includeCerts) => new HttpRequestMessage(HttpMethod.Post, $"skills{DESVersion(includeCerts)}/Normalize");
        internal static HttpRequestMessage DESSkillsExtract(bool includeCerts) => new HttpRequestMessage(HttpMethod.Post, $"skills{DESVersion(includeCerts)}/Extract");
        internal static HttpRequestMessage DESSkillsLookup(bool includeCerts) => new HttpRequestMessage(HttpMethod.Post, $"skills{DESVersion(includeCerts)}/Lookup");
        internal static HttpRequestMessage DESSkillsAutoComplete(bool includeCerts) => new HttpRequestMessage(HttpMethod.Post, $"skills{DESVersion(includeCerts)}/AutoComplete");
        internal static HttpRequestMessage DESProfessionsGetTaxonomy(TaxonomyFormat format, string language) => new HttpRequestMessage(HttpMethod.Get, $"professions/Taxonomy?format={format}&language={language}");
        internal static HttpRequestMessage DESGetProfessionsMetadata() => new HttpRequestMessage(HttpMethod.Get, $"professions/Metadata");
        internal static HttpRequestMessage DESProfessionsNormalize() => new HttpRequestMessage(HttpMethod.Post, $"professions/Normalize");
        internal static HttpRequestMessage DESProfessionsLookup() => new HttpRequestMessage(HttpMethod.Post, $"professions/Lookup");
        internal static HttpRequestMessage DESProfessionsAutoComplete() => new HttpRequestMessage(HttpMethod.Post, $"professions/AutoComplete");
        internal static HttpRequestMessage DESOntologySuggestSkillsFromProfessions(bool includeCerts) => new HttpRequestMessage(HttpMethod.Post, $"ontology{DESVersion(includeCerts)}/suggest-skills-from-professions");
        internal static HttpRequestMessage DESOntologySuggestSkillsFromSkills(bool includeCerts) => new HttpRequestMessage(HttpMethod.Post, $"ontology{DESVersion(includeCerts)}/suggest-skills-from-skills");
        internal static HttpRequestMessage DESOntologyCompareProfessions(bool includeCerts) => new HttpRequestMessage(HttpMethod.Post, $"ontology{DESVersion(includeCerts)}/compare-professions");
        internal static HttpRequestMessage DESOntologySuggestProfessions(bool includeCerts) => new HttpRequestMessage(HttpMethod.Post, $"ontology{DESVersion(includeCerts)}/suggest-professions");
        internal static HttpRequestMessage DESOntologyCompareSkillsToProfessions(bool includeCerts) => new HttpRequestMessage(HttpMethod.Post, $"ontology{DESVersion(includeCerts)}/compare-skills-to-profession");
        internal static HttpRequestMessage DESOntologySkillsSimilarityScore(bool includeCerts) => new HttpRequestMessage(HttpMethod.Post, $"ontology{DESVersion(includeCerts)}/skills-similarity-score");

        internal static HttpRequestMessage JobDescriptionGenerate() => new HttpRequestMessage(HttpMethod.Post, $"job-description/generate");
        internal static HttpRequestMessage JobDescriptionSuggestSkills() => new HttpRequestMessage(HttpMethod.Post, $"job-description/suggest-skills");

        internal static HttpRequestMessage MatchV2CandidatesAddDocument(string documentId) => new HttpRequestMessage(HttpMethod.Post, $"matchv2/candidates/{documentId}");
        internal static HttpRequestMessage MatchV2CandidatesDeleteDocuments(IEnumerable<string> documentIds, string env) => new HttpRequestMessage(HttpMethod.Delete, $"matchv2/candidates?ids={string.Join(",", documentIds)}&SearchAndMatchEnvironment={env}");
        internal static HttpRequestMessage MatchV2CandidatesSearch() => new HttpRequestMessage(HttpMethod.Post, $"matchv2/candidates/search");
        internal static HttpRequestMessage MatchV2CandidatesMatchDocument(string documentId) => new HttpRequestMessage(HttpMethod.Post, $"matchv2/candidates/match/{documentId}");
        internal static HttpRequestMessage MatchV2CandidatesAutocomplete() => new HttpRequestMessage(HttpMethod.Post, $"matchv2/candidates/autocomplete");
        internal static HttpRequestMessage MatchV2JobsAddDocument(string documentId) => new HttpRequestMessage(HttpMethod.Post, $"matchv2/vacancies/{documentId}");
        internal static HttpRequestMessage MatchV2JobsDeleteDocuments(IEnumerable<string> documentIds, string env) => new HttpRequestMessage(HttpMethod.Delete, $"matchv2/vacancies?ids={string.Join(",", documentIds)}&SearchAndMatchEnvironment={env}");
        internal static HttpRequestMessage MatchV2JobsSearch() => new HttpRequestMessage(HttpMethod.Post, $"matchv2/vacancies/search");
        internal static HttpRequestMessage MatchV2JobsMatchDocument(string documentId) => new HttpRequestMessage(HttpMethod.Post, $"matchv2/vacancies/match/{documentId}");
        internal static HttpRequestMessage MatchV2JobsAutocomplete() => new HttpRequestMessage(HttpMethod.Post, $"matchv2/vacancies/autocomplete");
    }
}

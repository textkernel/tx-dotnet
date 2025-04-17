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
                  
        internal static HttpRequestMessage DESSkillsGetTaxonomy(TaxonomyFormat format) => new HttpRequestMessage(HttpMethod.Get, $"skills/Taxonomy?format={format}");
        internal static HttpRequestMessage DESGetSkillsMetadata() => new HttpRequestMessage(HttpMethod.Get, $"skills/Metadata");
        internal static HttpRequestMessage DESSkillsNormalize() => new HttpRequestMessage(HttpMethod.Post, $"skills/Normalize");
        internal static HttpRequestMessage DESSkillsExtract() => new HttpRequestMessage(HttpMethod.Post, $"skills/Extract");
        internal static HttpRequestMessage DESSkillsLookup() => new HttpRequestMessage(HttpMethod.Post, $"skills/Lookup");
        internal static HttpRequestMessage DESSkillsAutoComplete() => new HttpRequestMessage(HttpMethod.Post, $"skills/AutoComplete");
        internal static HttpRequestMessage DESProfessionsGetTaxonomy(TaxonomyFormat format, string language) => new HttpRequestMessage(HttpMethod.Get, $"professions/Taxonomy?format={format}&language={language}");
        internal static HttpRequestMessage DESGetProfessionsMetadata() => new HttpRequestMessage(HttpMethod.Get, $"professions/Metadata");
        internal static HttpRequestMessage DESProfessionsNormalize() => new HttpRequestMessage(HttpMethod.Post, $"professions/Normalize");
        internal static HttpRequestMessage DESProfessionsLookup() => new HttpRequestMessage(HttpMethod.Post, $"professions/Lookup");
        internal static HttpRequestMessage DESProfessionsAutoComplete() => new HttpRequestMessage(HttpMethod.Post, $"professions/AutoComplete");
        internal static HttpRequestMessage DESOntologySuggestSkillsFromProfessions() => new HttpRequestMessage(HttpMethod.Post, $"ontology/suggest-skills-from-professions");
        internal static HttpRequestMessage DESOntologySuggestSkillsFromSkills() => new HttpRequestMessage(HttpMethod.Post, $"ontology/suggest-skills-from-skills");
        internal static HttpRequestMessage DESOntologyCompareProfessions() => new HttpRequestMessage(HttpMethod.Post, $"ontology/compare-professions");
        internal static HttpRequestMessage DESOntologySuggestProfessions() => new HttpRequestMessage(HttpMethod.Post, $"ontology/suggest-professions");
        internal static HttpRequestMessage DESOntologyCompareSkillsToProfessions() => new HttpRequestMessage(HttpMethod.Post, $"ontology/compare-skills-to-profession");
        internal static HttpRequestMessage DESOntologySkillsSimilarityScore() => new HttpRequestMessage(HttpMethod.Post, $"ontology/skills-similarity-score");
                  
        internal static HttpRequestMessage JobDescriptionGenerate() => new HttpRequestMessage(HttpMethod.Post, $"job-description/generate");
        internal static HttpRequestMessage JobDescriptionSuggestSkills() => new HttpRequestMessage(HttpMethod.Post, $"job-description/suggest-skills");

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
        internal HttpRequestMessage DESSkillsGetTaxonomyV2(TaxonomyFormat format) => new HttpRequestMessage(HttpMethod.Get, $"{Prefix()}skills/v2/Taxonomy?format={format}");
        internal HttpRequestMessage DESGetSkillsMetadataV2() => new HttpRequestMessage(HttpMethod.Get, $"{Prefix()}skills/v2/Metadata");
        internal HttpRequestMessage DESSkillsNormalizeV2() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}skills/v2/Normalize");
        internal HttpRequestMessage DESSkillsExtractV2() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}skills/v2/Extract");
        internal HttpRequestMessage DESSkillsLookupV2() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}skills/v2/Lookup");
        internal HttpRequestMessage DESSkillsAutoCompleteV2() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}skills/v2/AutoComplete");
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
        internal HttpRequestMessage DESOntologySuggestSkillsFromProfessionsV2() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}ontology/v2/suggest-skills-from-professions");
        internal HttpRequestMessage DESOntologySuggestSkillsFromSkillsV2() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}ontology/v2/suggest-skills-from-skills");
        internal HttpRequestMessage DESOntologyCompareProfessionsV2() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}ontology/v2/compare-professions");
        internal HttpRequestMessage DESOntologySuggestProfessionsV2() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}ontology/v2/suggest-professions");
        internal HttpRequestMessage DESOntologyCompareSkillsToProfessionsV2() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}ontology/v2/compare-skills-to-profession");
        internal HttpRequestMessage DESOntologySkillsSimilarityScoreV2() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}ontology/v2/skills-similarity-score");

        internal HttpRequestMessage JobDescriptionGenerate() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}job-description/generate");
        internal HttpRequestMessage JobDescriptionSuggestSkills() => new HttpRequestMessage(HttpMethod.Post, $"{Prefix()}job-description/suggest-skills");
        internal static HttpRequestMessage MatchV2CandidatesAddDocument(string documentId) => new HttpRequestMessage(HttpMethod.Post, $"match-v2/candidates/{documentId}");
        internal static HttpRequestMessage MatchV2CandidatesDeleteDocuments(IEnumerable<string> documentIds) => new HttpRequestMessage(HttpMethod.Delete, $"match-v2/candidates?ids={string.Join(",", documentIds)}");
        internal static HttpRequestMessage MatchV2CandidatesSearch() => new HttpRequestMessage(HttpMethod.Post, $"match-v2/candidates/search");
        internal static HttpRequestMessage MatchV2CandidatesMatchDocument(string documentId) => new HttpRequestMessage(HttpMethod.Post, $"match-v2/candidates/match/{documentId}");
        internal static HttpRequestMessage MatchV2VacanciesAddDocument(string documentId) => new HttpRequestMessage(HttpMethod.Post, $"match-v2/vacancies/{documentId}");
        internal static HttpRequestMessage MatchV2VacanciesDeleteDocuments(IEnumerable<string> documentIds) => new HttpRequestMessage(HttpMethod.Delete, $"match-v2/vacancies?ids={string.Join(",", documentIds)}");
        internal static HttpRequestMessage MatchV2VacanciesSearch() => new HttpRequestMessage(HttpMethod.Post, $"match-v2/vacancies/search");
        internal static HttpRequestMessage MatchV2VacanciesMatchDocument(string documentId) => new HttpRequestMessage(HttpMethod.Post, $"match-v2/vacancies/match/{documentId}");
    }
}

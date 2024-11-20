using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Textkernel.Tx.Models.API.DataEnrichment.Ontology.Request;
using Textkernel.Tx.Models.API.DataEnrichment.Ontology.Response;
using Textkernel.Tx.Models.API.DataEnrichment.Professions.Request;
using Textkernel.Tx.Models.API.DataEnrichment.Professions.Response;
using Textkernel.Tx.Models.API.DataEnrichment.Skills.Request;
using Textkernel.Tx.Models.API.DataEnrichment.Skills.Response;
using Textkernel.Tx.Models.API.DataEnrichment;
using Textkernel.Tx.Models.API.JobDescription;
using Textkernel.Tx.Models.Job;
using Textkernel.Tx.Models.Resume;

namespace Textkernel.Tx.Clients
{
    /// <summary>
    /// Use <see cref="TxClient.SkillsIntelligence"/>
    /// </summary>
    public class SkillsIntelligenceClient : ClientBase, ISkillsIntelligenceClient
    {
        internal SkillsIntelligenceClient(HttpClient httpClient) : base(httpClient) { }



        #region DES - Skills

        /// <inheritdoc />
        public async Task<GetSkillsTaxonomyResponse> GetSkillsTaxonomy(TaxonomyFormat format = TaxonomyFormat.json)
        {
            HttpRequestMessage apiRequest = ApiEndpoints.DESSkillsGetTaxonomy(format);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<GetSkillsTaxonomyResponse>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<GetMetadataResponse> GetSkillsTaxonomyMetadata()
        {
            HttpRequestMessage apiRequest = ApiEndpoints.DESGetProfessionsMetadata();
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<GetMetadataResponse>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<AutoCompleteSkillsResponse> AutocompleteSkill(string prefix, IEnumerable<string> languages = null,
            string outputLanguage = null, IEnumerable<string> types = null, int limit = 10)
        {
            HttpRequestMessage apiRequest = ApiEndpoints.DESSkillsAutoComplete();
            apiRequest.AddJsonBody(new SkillsAutoCompleteRequest
            {
                Prefix = prefix,
                Languages = languages?.ToList(),
                OutputLanguage = outputLanguage,
                Types = types?.ToList(),
                Limit = limit
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<AutoCompleteSkillsResponse>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<LookupSkillCodesResponse> LookupSkills(IEnumerable<string> skillIds, string outputLanguage = null)
        {
            HttpRequestMessage apiRequest = ApiEndpoints.DESSkillsLookup();
            apiRequest.AddJsonBody(new LookupSkillsRequest
            {
                SkillIds = skillIds.ToList(),
                OutputLanguage = outputLanguage
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<LookupSkillCodesResponse>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<NormalizeSkillsResponse> NormalizeSkills(IEnumerable<string> skills, string language = "en", string outputLanguage = null)
        {
            HttpRequestMessage apiRequest = ApiEndpoints.DESSkillsNormalize();
            apiRequest.AddJsonBody(new NormalizeSkillsRequest
            {
                Skills = skills.ToList(),
                Language = language,
                OutputLanguage = outputLanguage
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<NormalizeSkillsResponse>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<ExtractSkillsResponse> ExtractSkills(string text, string language = "en", string outputLanguage = null, float threshold = 0.5f)
        {
            HttpRequestMessage apiRequest = ApiEndpoints.DESSkillsExtract();
            apiRequest.AddJsonBody(new ExtractSkillsRequest
            {
                Text = text,
                Language = language,
                OutputLanguage = outputLanguage,
                Threshold = threshold
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<ExtractSkillsResponse>(response, apiRequest);
        }

        #endregion

        #region DES - Professions

        /// <inheritdoc />
        public async Task<AutoCompleteProfessionsResponse> AutocompleteProfession(string prefix, IEnumerable<string> languages = null, string outputLanguage = null, int limit = 10)
        {
            HttpRequestMessage apiRequest = ApiEndpoints.DESProfessionsAutoComplete();
            apiRequest.AddJsonBody(new AutocompleteRequest
            {
                Prefix = prefix,
                Languages = languages?.ToList(),
                OutputLanguage = outputLanguage,
                Limit = limit
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<AutoCompleteProfessionsResponse>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<GetProfessionsTaxonomyResponse> GetProfessionsTaxonomy(string language = null, TaxonomyFormat format = TaxonomyFormat.json)
        {
            HttpRequestMessage apiRequest = ApiEndpoints.DESProfessionsGetTaxonomy(format, language);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<GetProfessionsTaxonomyResponse>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<GetMetadataResponse> GetProfessionsTaxonomyMetadata()
        {
            HttpRequestMessage apiRequest = ApiEndpoints.DESGetProfessionsMetadata();
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<GetMetadataResponse>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<LookupProfessionCodesResponse> LookupProfessions(IEnumerable<int> codeIds, string outputLanguage = null)
        {
            HttpRequestMessage apiRequest = ApiEndpoints.DESProfessionsLookup();
            apiRequest.AddJsonBody(new LookupProfessionCodesRequest
            {
                CodeIds = codeIds.ToList(),
                OutputLanguage = outputLanguage
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<LookupProfessionCodesResponse>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<NormalizeProfessionsResponse> NormalizeProfessions(IEnumerable<string> jobTitles, string language = null, string outputLanguage = null)
        {
            HttpRequestMessage apiRequest = ApiEndpoints.DESProfessionsNormalize();
            apiRequest.AddJsonBody(new NormalizeProfessionsRequest
            {
                JobTitles = jobTitles.ToList(),
                Language = language,
                OutputLanguage = outputLanguage
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<NormalizeProfessionsResponse>(response, apiRequest);
        }

        #endregion

        #region DES - Ontology

        /// <inheritdoc />
        public async Task<CompareProfessionsResponse> CompareProfessions(int profession1, int profession2, string outputLanguage = null)
        {
            HttpRequestMessage apiRequest = ApiEndpoints.DESOntologyCompareProfessions();
            apiRequest.AddJsonBody(new CompareProfessionsRequest
            {
                ProfessionACodeId = profession1,
                ProfessionBCodeId = profession2,
                OutputLanguage = outputLanguage
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<CompareProfessionsResponse>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<CompareSkillsToProfessionResponse> CompareSkillsToProfession(int professionCodeId, string outputLanguage = null, params SkillScore[] skills)
        {
            HttpRequestMessage apiRequest = ApiEndpoints.DESOntologyCompareSkillsToProfessions();
            apiRequest.AddJsonBody(new CompareSkillsToProfessionRequest
            {
                ProfessionCodeId = professionCodeId,
                Skills = new List<SkillScore>(skills),
                OutputLanguage = outputLanguage
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<CompareSkillsToProfessionResponse>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<CompareSkillsToProfessionResponse> CompareSkillsToProfession(
            ParsedResume resume,
            int professionCodeId,
            string outputLanguage = null,
            bool weightSkillsByExperience = true)
        {
            if (!(resume?.Skills?.Normalized?.Any() ?? false))
            {
                throw new ArgumentException("The resume must be parsed with V2 skills selected, and with skills normalization enabled", nameof(resume));
            }

            return await CompareSkillsToProfession(professionCodeId, outputLanguage, GetNormalizedSkillsFromResume(resume, weightSkillsByExperience).ToArray());
        }

        private IEnumerable<SkillScore> GetNormalizedSkillsFromResume(ParsedResume resume, bool weightSkillsByExperience)
        {
            if (!(resume?.Skills?.Normalized?.Any() ?? false))
            {
                throw new ArgumentException("The resume must be parsed with V2 skills selected, and with skills normalization enabled", nameof(resume));
            }

            float maxExperience = resume.Skills.Normalized.Max(s => s.MonthsExperience?.Value ?? 0);
            return resume.Skills.Normalized
                .OrderByDescending(s => s.MonthsExperience?.Value ?? 0)
                .Take(50)
                .Select(s => new SkillScore
                {
                    Id = s.Id,
                    Score = (weightSkillsByExperience && maxExperience > 0) ? ((s.MonthsExperience?.Value ?? 0) / maxExperience) : 1
                });
        }

        /// <inheritdoc />
        public async Task<SuggestSkillsResponse> SuggestSkillsFromProfessions(ParsedResume resume, int limit = 10, string outputLanguage = null)
        {
            if (!(resume?.EmploymentHistory?.Positions?.Any(p => p.NormalizedProfession?.Profession?.CodeId != null) ?? false))
            {
                throw new ArgumentException("No professions were found in the resume, or the resume was parsed without professions normalization enabled", nameof(resume));
            }

            List<int> normalizedProfs = new List<int>();
            foreach (var position in resume.EmploymentHistory.Positions)
            {
                if (position?.NormalizedProfession?.Profession?.CodeId != null)
                {
                    normalizedProfs.Add(position.NormalizedProfession.Profession.CodeId);
                }
            }

            return await SuggestSkillsFromProfessions(normalizedProfs, limit, outputLanguage);
        }

        /// <inheritdoc />
        public async Task<SuggestSkillsResponse> SuggestSkillsFromProfessions(ParsedJob job, int limit = 10, string outputLanguage = null)
        {
            if (job?.JobTitles?.NormalizedProfession?.Profession?.CodeId == null)
            {
                throw new ArgumentException("No professions were found in the job, or the job was parsed without professions normalization enabled", nameof(job));
            }

            return await SuggestSkillsFromProfessions(new int[] { job.JobTitles.NormalizedProfession.Profession.CodeId }, limit, outputLanguage);
        }

        /// <inheritdoc />
        public async Task<SuggestSkillsResponse> SuggestSkillsFromProfessions(IEnumerable<int> professionCodeIDs, int limit = 10, string outputLanguage = null)
        {
            HttpRequestMessage apiRequest = ApiEndpoints.DESOntologySuggestSkillsFromProfessions();
            apiRequest.AddJsonBody(new SuggestSkillsFromProfessionsRequest
            {
                Limit = limit,
                ProfessionCodeIds = professionCodeIDs.ToList(),
                OutputLanguage = outputLanguage
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<SuggestSkillsResponse>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<SuggestProfessionsResponse> SuggestProfessionsFromSkills(
            ParsedResume resume,
            int limit = 10,
            bool returnMissingSkills = false,
            string outputLanguage = null,
            bool weightSkillsByExperience = true)
        {
            if (!(resume?.Skills?.Normalized?.Any() ?? false))
            {
                throw new ArgumentException("The resume must be parsed with V2 skills selected, and with skills normalization enabled", nameof(resume));
            }

            return await SuggestProfessionsFromSkills(GetNormalizedSkillsFromResume(resume, weightSkillsByExperience), limit, returnMissingSkills, outputLanguage);
        }

        /// <inheritdoc />
        public async Task<SuggestProfessionsResponse> SuggestProfessionsFromSkills(ParsedJob job, int limit = 10, bool returnMissingSkills = false, string outputLanguage = null)
        {
            if (!(job?.Skills?.Normalized?.Any() ?? false))
            {
                throw new ArgumentException("The job must be parsed with V2 skills selected, and with skills normalization enabled", nameof(job));
            }

            return await SuggestProfessionsFromSkills(job.Skills.Normalized.Take(50).Select(s => new SkillScore { Id = s.Id }), limit, returnMissingSkills, outputLanguage);
        }

        /// <inheritdoc />
        public async Task<SuggestProfessionsResponse> SuggestProfessionsFromSkills(
            IEnumerable<SkillScore> skills,
            int limit = 10,
            bool returnMissingSkills = false,
            string outputLanguage = null)
        {
            HttpRequestMessage apiRequest = ApiEndpoints.DESOntologySuggestProfessions();
            apiRequest.AddJsonBody(new SuggestProfessionsRequest
            {
                Skills = skills.ToList(),
                Limit = limit,
                ReturnMissingSkills = returnMissingSkills,
                OutputLanguage = outputLanguage
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<SuggestProfessionsResponse>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<SuggestSkillsResponse> SuggestSkillsFromSkills(ParsedResume resume, int limit = 10, string outputLanguage = null, bool weightSkillsByExperience = true)
        {
            if (!(resume?.Skills?.Normalized?.Any() ?? false))
            {
                throw new ArgumentException("The resume must be parsed with V2 skills selected, and with skills normalization enabled", nameof(resume));
            }

            return await SuggestSkillsFromSkills(GetNormalizedSkillsFromResume(resume, weightSkillsByExperience), limit, outputLanguage);
        }

        /// <inheritdoc />
        public async Task<SuggestSkillsResponse> SuggestSkillsFromSkills(ParsedJob job, int limit = 10, string outputLanguage = null)
        {
            if (!(job?.Skills?.Normalized?.Any() ?? false))
            {
                throw new ArgumentException("The job must be parsed with V2 skills selected, and with skills normalization enabled", nameof(job));
            }

            return await SuggestSkillsFromSkills(job.Skills.Normalized.Take(50).Select(s => new SkillScore { Id = s.Id }), limit, outputLanguage);
        }

        /// <inheritdoc />
        public async Task<SuggestSkillsResponse> SuggestSkillsFromSkills(IEnumerable<SkillScore> skills, int limit = 25, string outputLanguage = null)
        {
            HttpRequestMessage apiRequest = ApiEndpoints.DESOntologySuggestSkillsFromSkills();
            apiRequest.AddJsonBody(new SuggestSkillsFromSkillsRequest
            {
                Limit = limit,
                Skills = skills.ToList(),
                OutputLanguage = outputLanguage
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<SuggestSkillsResponse>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<SkillsSimilarityScoreResponse> SkillsSimilarityScore(IEnumerable<SkillScore> skillSetA, IEnumerable<SkillScore> skillSetB)
        {
            HttpRequestMessage apiRequest = ApiEndpoints.DESOntologySkillsSimilarityScore();
            apiRequest.AddJsonBody(new SkillsSimilarityScoreRequest
            {
                SkillsA = skillSetA.ToList(),
                SkillsB = skillSetB.ToList()
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<SkillsSimilarityScoreResponse>(response, apiRequest);
        }

        #endregion

        #region Job Description API

        /// <inheritdoc />
        public async Task<GenerateJobResponse> GenerateJobDescription(GenerateJobRequest request)
        {
            HttpRequestMessage apiRequest = ApiEndpoints.JobDescriptionGenerate();
            apiRequest.AddJsonBody(request);
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<GenerateJobResponse>(response, apiRequest);
        }

        /// <inheritdoc />
        public async Task<SuggestSkillsFromJobTitleResponse> SuggestSkillsFromJobTitle(string jobTitle, string language = "en", int? limit = null)
        {
            HttpRequestMessage apiRequest = ApiEndpoints.JobDescriptionSuggestSkills();
            apiRequest.AddJsonBody(new SuggestSkillsFromJobTitleRequest
            {
                JobTitle = jobTitle,
                Language = language,
                Limit = limit
            });
            HttpResponseMessage response = await _httpClient.SendAsync(apiRequest);
            return await ProcessResponse<SuggestSkillsFromJobTitleResponse>(response, apiRequest);
        }

        #endregion
    }
}

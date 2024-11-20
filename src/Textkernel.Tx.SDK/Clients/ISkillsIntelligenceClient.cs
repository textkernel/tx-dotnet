using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Textkernel.Tx.Models.API.DataEnrichment.Ontology.Response;
using Textkernel.Tx.Models.API.DataEnrichment.Professions.Response;
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
    public interface ISkillsIntelligenceClient
    {

        #region Data Enrichment Services

        /// <summary>
        /// Get all skills in the taxonomy with associated IDs and descriptions in all supported languages.
        /// </summary>
        /// <param name="format">
        /// The format of the returned taxonomy.
        /// <br/>NOTE: if you set this to <see cref="TaxonomyFormat.csv"/>, only the <see cref="Taxonomy.CsvOutput"/> will be populated.
        /// </param>
        /// <returns>The full structure of the Skills Taxonomy.</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<GetSkillsTaxonomyResponse> GetSkillsTaxonomy(TaxonomyFormat format = TaxonomyFormat.json);

        /// <summary>
        /// Get metadata about the skills taxonomy/service.
        /// </summary>
        /// <returns>The skills taxonomy metadata</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<GetMetadataResponse> GetSkillsTaxonomyMetadata();

        /// <summary>
        /// Returns normalized skills that begin with a given prefix, based on the chosen language(s).
        /// Each profession is associated with multiple descriptions. If any of the descriptions are a good
        /// completion of the given prefix, the profession is included in the results.
        /// </summary>
        /// <param name="prefix">The skill prefix to be completed. Must contain at least 1 character.</param>
        /// <param name="languages">
        /// The language(s) used to search for matching skills (the language of the provided prefix).
        /// A maximum of 5 languages can be provided. Must be one of the supported 
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#skills-languages">ISO codes</see>.
        /// <br/>Default is 'en' only.
        /// </param>
        /// <param name="outputLanguage">
        /// The language to ouput the found skills in (default is 'en'). Must be one of the supported
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#skills-languages">ISO codes</see>.
        /// </param>
        /// <param name="types">
        /// If specified, only these types of skills will be returned. The following values are acceptable: Professional, IT, Language, Soft, All.
        /// </param>
        /// <param name="limit">The maximum number of returned skills. The default is 10 and the maximum is 100.</param>
        /// <returns>A list of skills that match the given prefix.</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<AutoCompleteSkillsResponse> AutocompleteSkill(string prefix, IEnumerable<string> languages = null,
            string outputLanguage = null, IEnumerable<string> types = null, int limit = 10);

        /// <summary>
        /// Get the details associated with given skills in the taxonomy.
        /// </summary>
        /// <param name="skillIds"></param>
        /// <param name="outputLanguage">
        /// The language to use for the output skill descriptions. Must be one of the supported
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// <br/>Default is 'en'.
        /// </param>
        /// <returns>An array of skills objects.</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<LookupSkillCodesResponse> LookupSkills(IEnumerable<string> skillIds, string outputLanguage = null);

        /// <summary>
        /// Normalize the given skills to the most closely-related skills in the taxonomy.
        /// </summary>
        /// <param name="skills">The list of skills to normalize (up to 50 skills, each skill may not exceed 100 characters).</param>
        /// <param name="language">
        /// The language of the given skills. Must be one of the supported
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// <br/>Default is 'en'.
        /// </param>
        /// <param name="outputLanguage">
        /// The language to use for the output skill descriptions. Must be one of the supported
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// <br/>Defaults to whatever is used for the 'language' parameter.
        /// </param>
        /// <returns>An array of skills objects.</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<NormalizeSkillsResponse> NormalizeSkills(IEnumerable<string> skills, string language = "en", string outputLanguage = null);

        /// <summary>
        /// Extracts known skills from the given text.
        /// </summary>
        /// <param name="text">The text to extract skills from. There is a 24,000 character limit.</param>
        /// <param name="language">
        /// The language of the input text. Must be one of the supported
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// <br/>Default is 'en'.
        /// </param>
        /// <param name="outputLanguage">
        /// The language to use for the output skill descriptions. If not provided, defaults to the input language. Must be one of the supported
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// </param>
        /// <param name="threshold">
        /// A value from [0 - 1] for the minimum confidence threshold for extracted skills. Lower values will return more skills,
        /// but also increase the likelihood of ambiguity-related errors. The recommended and default value is 0.5.
        /// </param>
        /// <returns>A list of extracted skills.</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<ExtractSkillsResponse> ExtractSkills(string text, string language = "en", string outputLanguage = null, float threshold = 0.5f);

        /// <summary>
        /// Get all professions in the taxonomy with associated IDs and descriptions in all supported languages.
        /// </summary>
        /// <param name="language">
        /// The language parameter returns the taxonomy with descriptions only in that specified language. 
        /// If not specified, descriptions in all languages are returned. Must be specified as one of the supported 
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// </param>
        /// <param name="format">
        /// The format of the returned taxonomy.
        /// <br/>NOTE: if you set this to <see cref="TaxonomyFormat.csv"/>, only the <see cref="Taxonomy.CsvOutput"/> will be populated.
        /// </param>
        /// <returns>The full structure of the Professions Taxonomy</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<GetProfessionsTaxonomyResponse> GetProfessionsTaxonomy(string language = null, TaxonomyFormat format = TaxonomyFormat.json);

        /// <summary>
        /// Get metadata about the professions taxonomy/service.
        /// </summary>
        /// <returns>Metadata related to the professions taxonomy.</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<GetMetadataResponse> GetProfessionsTaxonomyMetadata();

        /// <summary>
        /// Returns normalized professions that begin with a given prefix, based on the chosen language(s).
        /// Each profession is associated with multiple descriptions. If any of the descriptions are a good
        /// completion of the given prefix, the profession is included in the results.
        /// </summary>
        /// <param name="prefix">The job title prefix to be completed. Must contain at least 1 character.</param>
        /// <param name="languages">
        /// The language(s) used to search for matching professions (the language of the provided prefix).
        /// A maximum of 5 languages can be provided. Must be one of the supported 
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// <br/>Default is 'en' only.
        /// </param>
        /// <param name="outputLanguage">
        /// The language to ouput the found professions in (default is 'en'). Must be one of the supported
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// </param>
        /// <param name="limit">The maximum number of returned professions. The default is 10 and the maximum is 100.</param>
        /// <returns>A list of professions that match the given prefix.</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<AutoCompleteProfessionsResponse> AutocompleteProfession(string prefix, IEnumerable<string> languages = null, string outputLanguage = null, int limit = 10);

        /// <summary>
        /// Get details for the given professions in the taxonomy.
        /// </summary>
        /// <param name="codeIds">
        /// The profession code IDs to get details about from the
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-taxonomies">Professions Taxonomy</see>.
        /// </param>
        /// <param name="outputLanguage">
        /// The language to use for professions descriptions (default is en). Must be an allowed
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO code</see>.
        /// <br/>Default is 'en'.
        /// </param>
        /// <returns>A list of returned professions.</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<LookupProfessionCodesResponse> LookupProfessions(IEnumerable<int> codeIds, string outputLanguage = null);

        /// <summary>
        /// Normalize the given job titles to the most closely-related professions in the taxonomy.
        /// </summary>
        /// <param name="jobTitles">The list of job titles to normalize (up to 10 job titles, each job title may not exceed 400 characters).</param>
        /// <param name="language">
        /// The language of the input job titles. Must be one of the supported
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// <br/>Default is 'en'.
        /// </param>
        /// <param name="outputLanguage">
        /// The language to use for descriptions of the returned normalized professions. Must be one of the supported
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// <br/>Defaults to whatever is used for the 'language' parameter.
        /// </param>
        /// <returns>A list of returned professions.</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<NormalizeProfessionsResponse> NormalizeProfessions(IEnumerable<string> jobTitles, string language = null, string outputLanguage = null);

        /// <summary>
        /// Suggests skills related to a resume based on the recent professions in the resume.
        /// </summary>
        /// <param name="resume">The resume to suggest skills for (based on the professions in the resume)</param>
        /// <param name="limit">The maximum amount of suggested skills returned. The default is 10.</param>
        /// <param name="outputLanguage">
        /// The language to use for the returned descriptions. If not provided, no descriptions are returned. Must be one of the supported 
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// </param>
        /// <returns>A list of suggested skills.</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<SuggestSkillsResponse> SuggestSkillsFromProfessions(ParsedResume resume, int limit = 10, string outputLanguage = null);

        /// <summary>
        /// Suggests skills related to a job based on the profession title in the job.
        /// </summary>
        /// <param name="job">The job to suggest skills for (based on the profession in the job)</param>
        /// <param name="limit">The maximum amount of suggested skills returned. The default is 10.</param>
        /// <param name="outputLanguage">
        /// The language to use for the returned descriptions. If not provided, no descriptions are returned. Must be one of the supported 
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// </param>
        /// <returns>A list of suggested skills.</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<SuggestSkillsResponse> SuggestSkillsFromProfessions(ParsedJob job, int limit = 10, string outputLanguage = null);

        /// <summary>
        /// Suggests skills related to given professions. The service returns salient skills that are strongly associated with the professions.
        /// </summary>
        /// <param name="professionCodeIDs">The code IDs of the professions to suggest skills for.</param>
        /// <param name="limit">The maximum amount of suggested skills returned. The default is 10.</param>
        /// <param name="outputLanguage">
        /// The language to use for the returned descriptions. If not provided, no descriptions are returned. Must be one of the supported 
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// </param>
        /// <returns>A list of suggested skills.</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<SuggestSkillsResponse> SuggestSkillsFromProfessions(IEnumerable<int> professionCodeIDs, int limit = 10, string outputLanguage = null);

        /// <summary>
        /// Suggest professions based on the <b>skills</b> within a given resume.
        /// </summary>
        /// <param name="resume">The professions are suggested based on the <b>skills</b> within this resume.</param>
        /// <param name="limit">The maximum amount of professions returned. If not specified this parameter defaults to 10.</param>
        /// <param name="returnMissingSkills">Flag to enable returning a list of missing skills per suggested profession.</param>
        /// <param name="outputLanguage">
        /// The language to use for the returned descriptions. If not provided, no descriptions are returned. Must be one of the supported 
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// </param>
        /// <param name="weightSkillsByExperience">Whether or not to give a higher weight to skills that the candidate has more experience with. Default is true.</param>
        /// <returns>A list of professions most relevant to the given resume, based on skills.</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<SuggestProfessionsResponse> SuggestProfessionsFromSkills(ParsedResume resume, int limit = 10, bool returnMissingSkills = false, string outputLanguage = null, bool weightSkillsByExperience = true);

        /// <summary>
        /// Suggest professions based on the <b>skills</b> within a given job.
        /// </summary>
        /// <param name="job">The professions are suggested based on the <b>skills</b> within this job.</param>
        /// <param name="limit">The maximum amount of professions returned. If not specified this parameter defaults to 10.</param>
        /// <param name="returnMissingSkills">Flag to enable returning a list of missing skills per suggested profession.</param>
        /// <param name="outputLanguage">
        /// The language to use for the returned descriptions. If not provided, no descriptions are returned. Must be one of the supported 
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// </param>
        /// <returns>A list of professions most relevant to the given job, based on skills.</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<SuggestProfessionsResponse> SuggestProfessionsFromSkills(ParsedJob job, int limit = 10, bool returnMissingSkills = false, string outputLanguage = null);


        /// <summary>
        /// Suggest professions based on a given set of skill IDs.
        /// </summary>
        /// <param name="skills">The skill IDs (and optionally, scores) used to return the most relevant professions. The list can contain up to 50 skill IDs.</param>
        /// <param name="limit">The maximum amount of professions returned. If not specified this parameter defaults to 10.</param>
        /// <param name="returnMissingSkills">Flag to enable returning a list of missing skills per suggested profession.</param>
        /// <param name="outputLanguage">
        /// The language to use for the returned descriptions. If not provided, no descriptions are returned. Must be one of the supported 
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// </param>
        /// <returns>A list of professions most relevant to the given skills.</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<SuggestProfessionsResponse> SuggestProfessionsFromSkills(IEnumerable<SkillScore> skills, int limit = 10, bool returnMissingSkills = false, string outputLanguage = null);

        /// <summary>
        /// Compare two professions based on the skills associated with each.
        /// </summary>
        /// <param name="profession1">
        /// A profession code ID from the
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-taxonomies">Professions Taxonomy</see>
        /// to compare.
        /// </param>
        /// <param name="profession2">
        /// A profession code ID from the
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-taxonomies">Professions Taxonomy</see>
        /// to compare.
        /// </param>
        /// <param name="outputLanguage">
        /// The language to use for the returned descriptions. If not provided, no descriptions are returned. Must be one of the supported 
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// </param>
        /// <returns>Common skills and exclusive skills between the two professions.</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<CompareProfessionsResponse> CompareProfessions(int profession1, int profession2, string outputLanguage = null);

        /// <summary>
        /// Compare a given set of skills to the skills related to a given profession.
        /// </summary>
        /// <param name="professionCodeId">
        /// The profession code ID from the
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-taxonomies">Professions Taxonomy</see>
        /// to compare the skill set to.
        /// </param>
        /// <param name="skills">The skill IDs (and optionally, scores) which should be compared against the given profession. The list can contain up to 50 skills.</param>
        /// <param name="outputLanguage">
        /// The language to use for the returned descriptions. If not provided, no descriptions are returned. Must be one of the supported 
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// </param>
        /// <returns>Common skills and skills not in the profession.</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<CompareSkillsToProfessionResponse> CompareSkillsToProfession(int professionCodeId, string outputLanguage = null, params SkillScore[] skills);

        /// <summary>
        /// Compare the skills of a candidate to the skills related to a job using the Ontology API.
        /// </summary>
        /// <param name="resume">The resume containing the skills of the candidate</param>
        /// <param name="professionCodeId">The code ID of the profession to compare the skills of the candidate to</param>
        /// <param name="outputLanguage">
        /// The language to use for the returned descriptions. If not provided, no descriptions are returned. Must be one of the supported 
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// </param>
        /// <param name="weightSkillsByExperience">Whether or not to give a higher weight to skills that the candidate has more experience with. Default is true.</param>
        /// <returns>Skills that are common between the candidate and the job, as well as what skills are missing</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<CompareSkillsToProfessionResponse> CompareSkillsToProfession(ParsedResume resume, int professionCodeId, string outputLanguage = null, bool weightSkillsByExperience = true);

        /// <summary>
        /// Suggests skills related to a resume (but not in the resume) based on the skills in the resume. The service
        /// returns closely related skills in a sense that knowing the provided skills either implies knowledge about
        /// the returned related skills, or should make it considerably easier to acquire knowledge about them.
        /// </summary>
        /// <param name="resume">The resume to suggest skills for (based on the skills in the resume)</param>
        /// <param name="limit">The maximum amount of suggested skills returned. The maximum and default is 10.</param>
        /// <param name="outputLanguage">
        /// The language to use for the returned descriptions. If not provided, no descriptions are returned. Must be one of the supported 
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// </param>
        /// <param name="weightSkillsByExperience">Whether or not to give a higher weight to skills that the candidate has more experience with. Default is true.</param>
        /// <returns>A list of suggested skills.</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<SuggestSkillsResponse> SuggestSkillsFromSkills(ParsedResume resume, int limit = 10, string outputLanguage = null, bool weightSkillsByExperience = true);

        /// <summary>
        /// Suggests skills related to a job (but not in the job) based on the skills in the job. The service returns
        /// closely related skills in a sense that knowing the provided skills either implies knowledge about the returned related skills,
        /// or should make it considerably easier to acquire knowledge about them.
        /// </summary>
        /// <param name="job">The job to suggest skills for (based on the skills in the job)</param>
        /// <param name="limit">The maximum amount of suggested skills returned. The maximum and default is 25.</param>
        /// <param name="outputLanguage">
        /// The language to use for the returned descriptions. If not provided, no descriptions are returned. Must be one of the supported 
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// </param>
        /// <returns>A list of suggested skills.</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<SuggestSkillsResponse> SuggestSkillsFromSkills(ParsedJob job, int limit = 10, string outputLanguage = null);

        /// <summary>
        /// Returns skills related to a given skill or set of skills. The service returns closely related skills
        /// in a sense that knowing the provided skills either implies knowledge about the returned related skills,
        /// or should make it considerably easier to acquire knowledge about them.
        /// </summary>
        /// <param name="skills">
        /// The skills (and optionally, scores) for which the service should return related skills.
        /// The list can contain up to 50 skills.
        /// </param>
        /// <param name="limit">The maximum amount of suggested skills returned. The maximum and default is 25.</param>
        /// <param name="outputLanguage">
        /// The language to use for the returned descriptions. If not provided, no descriptions are returned. Must be one of the supported 
        /// <see href="https://developer.textkernel.com/tx-platform/v10/data-enrichment/overview/#professions-languages">ISO codes</see>.
        /// </param>
        /// <returns>A list of suggested skills.</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<SuggestSkillsResponse> SuggestSkillsFromSkills(IEnumerable<SkillScore> skills, int limit = 25, string outputLanguage = null);

        /// <summary>
        /// Determines how closely related one set of skills is to another. The service defines closely related skills
        /// such that knowing a skill either implies knowledge about another skill, or should make it considerably
        /// easier to acquire knowledge about that skill.
        /// </summary>
        /// <param name="skillSetA">
        /// A set of skills (and optionally, scores) to score against the other set of skills.
        /// The list can contain up to 50 skills.
        /// </param>
        /// <param name="skillSetB">
        /// A set of skills (and optionally, scores) to score against the other set of skills.
        /// The list can contain up to 50 skills.
        /// </param>
        /// <returns>A score from [0 - 1] representing how closely related skill set A and skill set B are, based on the relations between skills.</returns>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<SkillsSimilarityScoreResponse> SkillsSimilarityScore(IEnumerable<SkillScore> skillSetA, IEnumerable<SkillScore> skillSetB);

        #endregion

        #region Job Description API

        /// <summary>
        /// Generates a job description based on specified parameters.
        /// </summary>
        /// <param name="request">The request body</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<GenerateJobResponse> GenerateJobDescription(GenerateJobRequest request);

        /// <summary>
        /// Takes a job title and suggests relevant skills. 
        /// </summary>
        /// <param name="jobTitle">The title of the job for which skills are being suggested.</param>
        /// <param name="language">Language of the suggested skills in ISO 639-1 code format.</param>
        /// <param name="limit">Maximum number of skills to suggest. If not specified this parameter defaults to 10. This value cannot exceed 50.</param>
        /// <exception cref="TxException">Thrown when an API error occurred</exception>
        Task<SuggestSkillsFromJobTitleResponse> SuggestSkillsFromJobTitle(string jobTitle, string language = "en", int? limit = null);

        #endregion

    }
}

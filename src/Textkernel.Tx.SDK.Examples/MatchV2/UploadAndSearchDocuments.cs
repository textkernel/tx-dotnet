using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Textkernel.Tx.Models;
using Textkernel.Tx.Models.API.Indexes;
using Textkernel.Tx.Models.API.Parsing;

namespace Textkernel.Tx.SDK.Examples.MatchV2
{
    internal class UploadAndSearchDocuments : IExample
    {
        public async Task Run(TxClient client)
        {
            //A Document is an unparsed File (PDF, Word Doc, etc)
            Document doc = new Document("resume.docx");

            //when you create a ParseRequest, you can specify many configuration settings
            //in the ParseOptions. See https://developer.textkernel.com/tx-platform/v10/resume-parser/api/
            ParseRequest request = new ParseRequest(doc, new ParseOptions()
            {
                ProfessionsSettings = new ProfessionsSettings
                {
                    Normalize = true,
                },
                SkillsSettings = new SkillsSettings
                {
                    Normalize = true,
                    TaxonomyVersion = "V2"
                },
                //method #1: index a document during the parse request
                IndexingOptions = new IndexingOptionsGeneric(Services.MatchV2Environment.PROD, "my-first-document")
            });

            try
            {
                ParseResumeResponse response = await client.Parser.ParseResume(request);
                
                //method #2, index an existing candidate/job without parsing (we just use the same ResumeData but it could come from anywhere)
                await client.SearchMatchV2.AddCandidate("my-second-document", response.Value.ResumeData);

                await Task.Delay(5_000);//give the env a few seconds to get the document available to search/match

                //search for candidates with the first job title, should show both documents in the response
                var searchResponse = await client.SearchMatchV2.SearchCandidates(new Models.API.MatchV2.Request.SearchQuery
                {
                    QueryString = response.Value.ResumeData.EmploymentHistory.Positions.First().JobTitle.Raw//search by the name of the first job title
                }, new Models.API.MatchV2.Request.Options());

                var opts = TxJsonSerialization.CreateDefaultOptions();
                opts.WriteIndented = true;
                Console.WriteLine(JsonSerializer.Serialize(searchResponse.Value, opts));
            }
            catch (TxException e)
            {
                //the document could not be parsed, always try/catch for TxExceptions when using TxClient
                Console.WriteLine($"Error: {e.TxErrorCode}, Message: {e.Message}");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Textkernel.Tx.Batches;
using Textkernel.Tx.Models.API.Parsing;

namespace Textkernel.Tx.SDK.Examples.Parsing
{
    internal class BatchParsing : IExample
    {
        public async Task Run(TxClient client)
        {
            //you can specify many configuration settings in the ParseOptions.
            //See https://developer.textkernel.com/tx-platform/v10/resume-parser/api/
            ParseOptions parseOptions = new ParseOptions();

            //only allow users to parse N at a time (otherwise, a single user could use up all the credits)
            // you can also override some of the defaults w/ the other arguments
            BatchParsingRules rules = new BatchParsingRules(50);

            try
            {
                await BatchParser.ParseResumes(client, parseOptions, rules, @"C:\resumes",
                    SearchOption.AllDirectories, OnSuccess, OnPartialSuccess, OnError, GetDocId);
            }
            catch (Exception e)
            {
                //The directory did not exist, was too large, contained no valid files, etc.
                // You will want to handle this case gracefully if your users are in control of the directory
                Console.WriteLine(e.Message);
            }
        }

        static string GetDocId(string fileName)
        {
            //Here you might want to assign an ID to the filename and store than in your database.
            //This method is most useful if you are indexing while parsing using the batch parser,
            // since the DocumentIds used to index the documents after parsing can be controlled by you.

            //in this example, we'll simply generate a random GUID for each file
            return Guid.NewGuid().ToString();
        }

        static async Task OnSuccess(ResumeBatchSuccessResult result)
        {
            //here you would store the parsed document in your database, or use the extracted information however you like
            await WriteResultToDisk(result);
        }

        static async Task OnPartialSuccess(ResumeBatchPartialSuccessResult result)
        {
            // a partial success means _something_ went wrong, but the resume data might still be usable
            // so you would want to verify the data you care about exists before processing this result
            await WriteResultToDisk(result);
        }

        static async Task OnError(BatchErrorResult result)
        {
            //this was a failure
            Console.WriteLine();
            Console.WriteLine("**** ERROR ****");
            Console.WriteLine($"File: {result.File}");
            Console.WriteLine($"Error: {result.Error.TxErrorCode}");
            Console.WriteLine($"Message: {result.Error.Message}");

            await Task.CompletedTask;
        }

        static async Task WriteResultToDisk(ResumeBatchSuccessResult result)
        {
            string outputFileName = $"{result.File}.{result.DocumentId}.json";

            //write the non-redacted json to the file with pretty-printing
            result.Response.Value.ResumeData.ToFile(outputFileName, true);

            await Task.CompletedTask;
        }
    }
}

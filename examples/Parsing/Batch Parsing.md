# Batch Parsing Example

```c#
public static async Task Main(string[] args)
{
    SovrenClient client = new SovrenClient("12345678", "abcdefghijklmnopqrstuvwxyz", DataCenter.US);

    //you can specify many configuration settings in the ParseOptions.
    //See https://sovren.com/technical-specs/latest/rest-api/resume-parser/api/
    ParseOptions parseOptions = new ParseOptions();

    //only allow users to parse N at a time (otherwise, a single user could use up all the credits)
    // you can also override some of the defaults Sovren provides w/ the other arguments
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
    WriteResultToDisk(result);
}

static async Task OnPartialSuccess(ResumeBatchPartialSuccessResult result)
{
    // a partial success means _something_ went wrong, but the resume data might still be usable
    // so you would want to verify the data you care about exists before processing this result
    WriteResultToDisk(result);
}

static async Task OnError(BatchErrorResult result)
{
    //this was a failure
    Console.WriteLine();
    Console.WriteLine("**** ERROR ****");
    Console.WriteLine($"File: {result.File}");
    Console.WriteLine($"Error: {result.Error.SovrenErrorCode}");
    Console.WriteLine($"Message: {result.Error.Message}");
}

static void WriteResultToDisk(ResumeBatchSuccessResult result)
{
    string outputFileName = $"{result.File}.{result.DocumentId}.json";

    //write the non-redacted json to the file with pretty-printing
    result.Response.EasyAccess().SaveResumeJsonToFile(outputFileName, true, false);
}
```
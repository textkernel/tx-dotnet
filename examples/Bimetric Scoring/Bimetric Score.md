# Basic Bimetric Score Example

```c#
//https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines
static readonly HttpClient httpClient = new HttpClient();

public static async Task Main(string[] args)
{
    TxClient client = new TxClient(httpClient, new TxClientSettings
    {
        AccountId = "12345678",
        ServiceKey = "abcdefghijklmnopqrstuvwxyz",
        DataCenter = DataCenter.US
    });

    ParsedJob parsedJob = ...;//output from Job Parser
    ParsedResume parsedResume1 = ...;//output from Resume Parser
    ParsedResume parsedResume2 = ...;//output from Resume Parser

    ParsedJobWithId sourceJob = new ParsedJobWithId
    {
        Id = "my-job",
        JobData = parsedJob
    };

    List<ParsedResumeWithId> targetResumes = new List<ParsedResumeWithId>
    {
        new ParsedResumeWithId
        {
            Id = "my-resume-1",
            ResumeData = parsedResume1
        },
        new ParsedResumeWithId
        {
            Id = "my-resume-2",
            ResumeData = parsedResume2
        }
    };

    try
    {
        BimetricScoreResponse response = await client.BimetricScore(sourceJob, targetResumes);

        foreach (BimetricScoreResult match in response.Value.Matches)
        {
            Console.WriteLine($"{match.Id}: {match.SovScore}");
        }
    }
    catch (TxException e)
    {
        //this was an outright failure, always try/catch for TxExceptions when using TxClient
        Console.WriteLine($"Error: {e.TxErrorCode}, Message: {e.Message}");
    }
    
    Console.ReadKey();
}
```
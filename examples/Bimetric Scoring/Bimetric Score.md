# Basic Bimetric Score Example

```c#
//https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines
static readonly HttpClient httpClient = new HttpClient();

public static async Task Main(string[] args)
{
    SovrenClient client = new SovrenClient(httpClient, new SovrenClientSettings
    {
        AccountId = "12345678",
        ServiceKey = "abcdefghijklmnopqrstuvwxyz",
        DataCenter = DataCenter.US
    });

    ParsedJob parsedJob = ...;//output from Sovren Job Parser
    ParsedResume parsedResume1 = ...;//output from Sovren Resume Parser
    ParsedResume parsedResume2 = ...;//output from Sovren Resume Parser

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
    catch (SovrenException e)
    {
        //this was an outright failure, always try/catch for SovrenExceptions when using SovrenClient
        Console.WriteLine($"Error: {e.SovrenErrorCode}, Message: {e.Message}");
    }
    
    Console.ReadKey();
}
```
# Create an index and add a document

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

    ParsedResume parsedResume1 = ...;//output from Tx Resume Parser
    ParsedResume parsedResume2 = ...;//output from Tx Resume Parser

    string indexId = "myResumes";

    try
    {
        await client.CreateIndex(IndexType.Resume, indexId);
        await client.IndexDocument(parsedResume1, indexId, "resume-1");
        await client.IndexDocument(parsedResume2, indexId, "resume-2");

        Console.WriteLine("Success!");
    }
    catch (TxException e)
    {
        //this was an outright failure, always try/catch for TxExceptions when using TxClient
        Console.WriteLine($"Error: {e.TxErrorCode}, Message: {e.Message}");
    }

    Console.ReadKey();
}
```
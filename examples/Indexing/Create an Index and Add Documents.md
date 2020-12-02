# Create an index and add a document

```c#
public static async Task Main(string[] args)
{
    SovrenClient client = new SovrenClient("12345678", "abcdefghijklmnopqrstuvwxyz", DataCenter.US);

    ParsedResume parsedResume1 = ...;//output from Sovren Resume Parser
    ParsedResume parsedResume2 = ...;//output from Sovren Resume Parser

    string indexId = "myResumes";

    try
    {
        await client.CreateIndex(IndexType.Resume, indexId);
        await client.IndexDocument(parsedResume1, indexId, "resume-1");
        await client.IndexDocument(parsedResume2, indexId, "resume-2");

        Console.WriteLine("Success!");
    }
    catch (SovrenException e)
    {
        //this was an outright failure, always try/catch for SovrenExceptions when using SovrenClient
        Console.WriteLine($"Error: {e.SovrenErrorCode}, Message: {e.Message}");
    }

    Console.ReadKey();
}
```
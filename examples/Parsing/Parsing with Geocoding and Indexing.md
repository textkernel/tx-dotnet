# Parsing with Geocoding and Indexing Example

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
    
    //setup some basic geocoding and indexing options
    ParseOptions options = new ParseOptions
    {
        GeocodeOptions = new GeocodeOptions
        {
            IncludeGeocoding = true,
            Provider = GeocodeProvider.Google
        },
        IndexingOptions = new IndexSingleDocumentInfo
        {
            IndexId = "myResumes",
            DocumentId = "abc-123"
        }
    };

    //A Document is an unparsed File (PDF, Word Doc, etc)
    Document doc = new Document("resume.docx");

    //create the request to send
    ParseRequest request = new ParseRequest(doc, options);

    try
    {
        ParseResumeResponse response = await client.ParseResume(request);
        //if we get here, it was 200-OK and all operations succeeded

        Console.WriteLine("Success!");
    }
    catch (SovrenUsableResumeException e)
    {
        //this indicates an error occurred when geocoding or indexing, but the parsed resume
        //may still be usable
        
        //do something with e.Response.Value.ResumeData if it has good data
    }
    catch (SovrenException e)
    {
        //the document could not be parsed, always try/catch for SovrenExceptions when using SovrenClient
        Console.WriteLine($"Error: {e.SovrenErrorCode}, Message: {e.Message}");
    }

    Console.ReadKey();
}
```
# Parsing with Geocoding and Indexing Example

```c#
public static async Task Main(string[] args)
{
    SovrenClient client = new SovrenClient("12345678", "abcdefghijklmnopqrstuvwxyz", DataCenter.US);
    
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
    ParsingService parsingSvc = new ParsingService(client, options);

    //A Document is an unparsed File (PDF, Word Doc, etc)
    Document doc = new Document("resume.docx");

    try
    {
        ParseResumeResponseValue response = await parsingSvc.ParseResume(doc);
        //if we get here, it was 200-OK and all operations succeeded

        Console.WriteLine("Success!");
    }
    catch (SovrenUsableResumeException e)
    {
        //this indicates an error occurred when geocoding or indexing, but the parsed resume
        //may still be useable
        
        //do something with e.Response.ResumeData if it has good data
    }
    catch (SovrenException e)
    {
        //this was an outright failure, always try/catch for SovrenExceptions when using
        // the ParsingService
        Console.WriteLine($"Error: {e.SovrenErrorCode}, Message: {e.Message}");
    }

    Console.ReadKey();
}
```
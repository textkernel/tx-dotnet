using Textkernel.Tx;
using Textkernel.Tx.Models;
using Textkernel.Tx.Models.API.Parsing;
using Textkernel.Tx.Models.Resume.ContactInfo;
using Textkernel.Tx.SDK.Examples;
using Textkernel.Tx.SDK.Examples.MatchV2;
using Textkernel.Tx.SDK.Examples.Parsing;

public class Program
{
    //https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines
    static readonly HttpClient httpClient = new HttpClient();

    public static async Task Main(string[] args)
    {
        TxClient client = new TxClient(httpClient, new TxClientSettings
        {
            AccountId = "12345678",
            ServiceKey = "abcdefghijklmnopqrstuvwxyz",
            DataCenter = DataCenter.US,
            MatchV2Environment = Textkernel.Tx.Services.MatchV2Environment.ACC,
            SkillsIntelligenceIncludeCertifications = true
        });

        IExample example = new BasicParsing();
        //IExample example = new BatchParsing();
        //IExample example = new UploadAndSearchDocuments();
        await example.Run(client);

        Console.ReadKey();
    }

    
}
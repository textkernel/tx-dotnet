# Textkernel Tx .NET SDK Documentation
This is the auto-generated (by [DocFX](https://dotnet.github.io/docfx/)) documentation for the SDK codebase.
If you were looking for best-practices, transaction costs, or other API documentation, you
can find that [here](https://developer.textkernel.com/tx-platform/v10/overview/).

You can also go back to the [GitHub project page](https://github.com/textkernel/tx-dotnet) for more usage information and examples.

## Basics
 - @"Textkernel.Tx.TxClient" - the core class of the SDK. It is used to make all of the API calls.
 - @"Textkernel.Tx.DataCenter" - used to point the @"Textkernel.Tx.TxClient" at the correct data center for your account.
 - @"Textkernel.Tx.TxException" - the @"Textkernel.Tx.TxClient" will throw these during normal operations, for example, when you try to parse a resume that is a scanned image. See the GitHub project page for more information and examples.

## Core Models
 - @"Textkernel.Tx.Models.Document" - represents an unparsed resume/job, such as a file on the file system or a byte[] in a database. This is what you pass to the @"Textkernel.Tx.TxClient" for parsing.
 - @"Textkernel.Tx.Models.Resume.ParsedResume" - contains all of the properties/information that is extracted when a resume/cv is parsed.
 - @"Textkernel.Tx.Models.Job.ParsedJob" - contains all of the properties/information that is extracted when a job description is parsed.
 - @"Textkernel.Tx.Models.API.ApiResponseInfo" - returned with every API call, this contains information about the transaction (error messages, cost, duration, account information, etc).

## Parsing
 - @"Textkernel.Tx.Models.API.Parsing.ParseRequest" - used to make a parse API call. Also note the properties inherited from @"Textkernel.Tx.Models.API.Parsing.ParseOptions" and @"Textkernel.Tx.Models.API.Parsing.BasicParseOptions".
 - @"Textkernel.Tx.Models.API.Parsing.ParseResumeResponseValue" & @"Textkernel.Tx.Models.API.Parsing.ParseJobResponseValue" - the <code>Value</code> returned for a parse API call. Note the properties inherited from @"Textkernel.Tx.Models.API.Parsing.BaseParseResponseValue".
 - view all relevant classes: @"Textkernel.Tx.Models.API.Parsing"

## Matching/Searching/Bimetric Scoring
 - @"Textkernel.Tx.Models.API.Matching.Request.MatchRequest" & @"Textkernel.Tx.Models.API.Matching.SearchRequest" - used to make Matching/Searching API calls. Most notably the @"Textkernel.Tx.Models.API.Matching.Request.FilterCriteria" for filtering results.
 - @"Textkernel.Tx.Models.API.BimetricScoring.BimetricScoreRequest" - used to perform a Bimetric Score API call to score any combination of resumes/jobs.
 - Matching/Searching/Bimetric Scoring all return lists of results. Each result is one of the following, depending on which API call you are using:
   - @"Textkernel.Tx.Models.API.Matching.Response.IBimetricScoredResult" - all Match/Bimetric Score results implement this interface.
   - @"Textkernel.Tx.Models.API.Matching.Response.SearchResult" - search results are not scored. Each result simply represents a document in the searched index(es) that matched the @"Textkernel.Tx.Models.API.Matching.Request.FilterCriteria".
 - view all relevant classes:
    - @"Textkernel.Tx.Models.API.Matching"
      - @"Textkernel.Tx.Models.API.Matching.Request"
      - @"Textkernel.Tx.Models.API.Matching.Response"
    - @"Textkernel.Tx.Models.API.BimetricScoring"

# Sovren .NET SDK Documentation
This is the auto-generated (by [DocFX](https://dotnet.github.io/docfx/)) documentation for the SDK codebase.
If you were looking for best-practices, transaction costs, or other API documentation, you
can find that [here](https://sovren.com/technical-specs/latest/rest-api/overview/).

You can also go back to the [GitHub project page](https://github.com/sovren/sovren-dotnet) for more usage information and examples.

## Basics
 - @"Sovren.SovrenClient" - the core class of the SDK. It is used to make all of the API calls.
 - @"Sovren.DataCenter" - used to point the @"Sovren.SovrenClient" at the correct data center for your Sovren account.
 - @"Sovren.SovrenException" - the @"Sovren.SovrenClient" will throw these during normal operations, for example, when you try to parse a resume that is a scanned image. See the GitHub project page for more information and examples.

## Core Models
 - @"Sovren.Models.Document" - represents an unparsed resume/job, such as a file on the file system or a byte[] in a database. This is what you pass to the @"Sovren.SovrenClient" for parsing.
 - @"Sovren.Models.Resume.ParsedResume" - contains all of the properties/information that is extracted when a resume/cv is parsed.
 - @"Sovren.Models.Job.ParsedJob" - contains all of the properties/information that is extracted when a job description is parsed.
 - @"Sovren.Models.API.ApiResponseInfo" - returned with every API call, this contains information about the transaction (error messages, cost, duration, account information, etc).

## Parsing
 - @"Sovren.Models.API.Parsing.ParseRequest" - used to make a parse API call. Also note the properties inherited from @"Sovren.Models.API.Parsing.ParseOptions" and @"Sovren.Models.API.Parsing.BasicParseOptions".
 - @"Sovren.Models.API.Parsing.ParseResumeResponseValue" & @"Sovren.Models.API.Parsing.ParseJobResponseValue" - the <code>Value</code> returned for a parse API call. Note the properties inherited from @"Sovren.Models.API.Parsing.BaseParseResponseValue".
 - view all relevant classes: @"Sovren.Models.API.Parsing"

## Matching/Searching/Bimetric Scoring
 - @"Sovren.Models.API.Matching.Request.MatchRequest" & @"Sovren.Models.API.Matching.SearchRequest" - used to make Matching/Searching API calls. Most notably the @"Sovren.Models.API.Matching.Request.FilterCriteria" for filtering results.
 - @"Sovren.Models.API.BimetricScoring.BimetricScoreRequest" - used to perform a Bimetric Score API call to score any combination of resumes/jobs.
 - Matching/Searching/Bimetric Scoring all return lists of results. Each result is one of the following, depending on which API call you are using:
   - @"Sovren.Models.API.Matching.Response.IBimetricScoredResult" - all Match/Bimetric Score results implement this interface.
   - @"Sovren.Models.API.Matching.Response.SearchResult" - search results are not scored. Each result simply represents a document in the searched index(es) that matched the @"Sovren.Models.API.Matching.Request.FilterCriteria".
 - view all relevant classes:
    - @"Sovren.Models.API.Matching"
      - @"Sovren.Models.API.Matching.Request"
      - @"Sovren.Models.API.Matching.Response"
    - @"Sovren.Models.API.BimetricScoring"

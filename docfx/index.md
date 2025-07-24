# Textkernel Tx .NET SDK Documentation
This is the auto-generated (by [DocFX](https://dotnet.github.io/docfx/)) documentation for the SDK codebase.
If you were looking for best-practices, transaction costs, or other API documentation, you
can find that [here](https://developer.textkernel.com/tx-platform/v10/overview/).

You can also go back to the [GitHub project page](https://github.com/textkernel/tx-dotnet) for more usage information and examples.

## Basics
 - @"Textkernel.Tx.TxClient" - the core class of the SDK. It is used to make all of the API calls.
 - @"Textkernel.Tx.DataCenter" - used to point the @"Textkernel.Tx.TxClient" at the correct data center for your account.
 - @"Textkernel.Tx.TxException" - the @"Textkernel.Tx.TxClient" will throw these during normal operations, for example, when you try to parse a resume that is a scanned image. See the GitHub project page for more information and examples.

## Services
- @"Textkernel.Tx.TxClient.Parser" - provides all parsing functionality.
- @"Textkernel.Tx.TxClient.Geocoder" - determines geocoordinates based on addresses.
- @"Textkernel.Tx.TxClient.Formatter" - transforms a parsed resume into a standard/templated format.
- @"Textkernel.Tx.TxClient.SkillsIntelligence" - provides all Skills Intelligence functionality.
- @"Textkernel.Tx.TxClient.SearchMatchV1" - provides all Search &amp; Match V1 functionality.
- @"Textkernel.Tx.TxClient.SearchMatchV2" - provides all Search &amp; Match V2 functionality.

## Core Models
 - @"Textkernel.Tx.Models.Document" - represents an unparsed resume/job, such as a file on the file system or a byte[] in a database. This is what you pass to the @"Textkernel.Tx.TxClient" for parsing.
 - @"Textkernel.Tx.Models.Resume.ParsedResume" - contains all of the properties/information that is extracted when a resume/cv is parsed.
 - @"Textkernel.Tx.Models.Job.ParsedJob" - contains all of the properties/information that is extracted when a job description is parsed.
 - @"Textkernel.Tx.Models.API.ApiResponseInfo" - returned with every API call, this contains information about the transaction (error messages, cost, duration, account information, etc).





[gh-url]: https://github.com/textkernel/tx-dotnet/

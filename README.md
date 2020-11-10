# sovren-dotnet
An SDK written in C# to integrate with the Sovren v10 API. Supports .NET Framework 4.6.1+ and .NET Core 2.0+.

## Installation

From within Visual Studio:

1. Open the Solution Explorer.
2. Right-click on a project within your solution.
3. Click on *Manage NuGet Packages...*
4. Click on the *Browse* tab and search for "Sovren.SDK".
5. Click on the Sovren.SDK package, select the appropriate version in the right-tab and click *Install*.

Using the [.NET Core command-line interface (CLI) tools][dotnet-core-cli-tools]:

```sh
dotnet add package Sovren.SDK
```

Using the [NuGet Command Line Interface (CLI)][nuget-cli]:

```sh
nuget install Sovren.SDK
```

Using the [Package Manager Console][package-manager-console]:

```powershell
Install-Package Sovren.SDK
```

## Documentation

For the full API documentation, information about best practices, FAQs, etc. check out our [docs site][api-docs].

## Usage

### Creating a `SovrenClient`
This is the object that you will pass to a service to perform API calls. You create it with your account credentials and the `SovrenClient` makes the raw API calls for you. These credentials can be found in the [Sovren Portal][portal]. Be sure to select the correct `DataCenter` for your account.
```c#
SovrenClient client = new SovrenClient("12345678", "abcdefghijklmnopqrstuvwxyz", DataCenter.US);
```

### The Services
There are services that focus on different areas of the Sovren API:
* `ParsingService` - for parsing resumes and jobs
* `BimetricScoringService` - for Bimetric Scoring resumes and jobs
* `IndexService` - for creating/deleting indexes and resumes/jobs within those indexes
* `AIMatchingService` - for AI searching/matching over the resumes/jobs within indexes
* `GeocodingService` - for adding geocoordinates to resumes/jobs for use in AI searching/matching (radius or location filtering)

Note that even though there is a service for parsing and geocoding, it is sometimes more convenient (and efficient) to perform geocoding within a parse request. There is some overlap between the above services, so review their methods carefully when choosing which best fits your needs. For example, the majority of customers would not need the `GeocodingService` since they could geocode documents as they parse them with the `ParsingService`.

### Handling Errors and the `SovrenException`
Every call to any of the methods on the services should be wrapped in a `try/catch` block. Any 4xx/5xx level errors will cause a `SovrenException` to be thrown. Sometimes these are a normal and expected part of the Sovren API. For example, if you have a website where users upload resumes, sometimes a user will upload a scanned image as their resume. Sovren does not process these, and will return a `422 - Unprocessable Entity` response which will throw a `SovrenException`. Your job is to handle any `SovrenException` in a way that makes sense in your application.
```c#
```

[portal]: https://portal.sovren.com
[api-docs]: https://docs.sovren.com
[dotnet-core-cli-tools]: https://docs.microsoft.com/en-us/dotnet/core/tools/
[nuget-cli]: https://docs.microsoft.com/en-us/nuget/tools/nuget-exe-cli-reference
[package-manager-console]: https://docs.microsoft.com/en-us/nuget/tools/package-manager-console
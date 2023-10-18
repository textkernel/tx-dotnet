# sovren-dotnet
![Nuget](https://img.shields.io/nuget/dt/Sovren.SDK?color=0575aa)
![GitHub](https://img.shields.io/github/license/sovren/sovren-dotnet?color=0575aa)
![Nuget](https://img.shields.io/nuget/v/Sovren.SDK?color=0575aa)
[![build](https://github.com/sovren/sovren-dotnet/actions/workflows/build.yml/badge.svg)](https://github.com/sovren/sovren-dotnet/actions/workflows/build.yml)

The official C# SDK for the Sovren v10 API for resume/CV and job parsing, searching, and matching. Supports .NET Framework 4.6.1+ and .NET Core 2.0+.

## Installation

From within Visual Studio:

1. Open the Solution Explorer.
2. Right-click on a project within your solution.
3. Click on *Manage NuGet Packages...*
4. Click on the *Browse* tab and search for "Sovren.SDK" (ensure the *Package source* dropdown is set to `nuget.org`).
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
To view MSDN-style documentation for this SDK, check out our [DocFX-generated docs][docfx-docs].
For the full REST API documentation, information about best practices, FAQs, etc. check out our [API docs][api-docs].

## Examples
For full code examples, see [here][examples].

## Basic Usage

### Creating a `SovrenClient`
This is the object that you will use to perform API calls. You create it with your account credentials and the `SovrenClient` makes the raw API calls for you. These credentials can be found in the [Sovren Portal][portal]. Be sure to select the correct `DataCenter` for your account.
#### Without using dependency injection:
```c#
SovrenClient client = new SovrenClient(httpClient, new SovrenClientSettings
{
    AccountId = "12345678",
    ServiceKey = "abcdefghijklmnopqrstuvwxyz",
    DataCenter = DataCenter.US
});
```
**Note that the SovrenClient uses [HttpClient][http-client] and you should follow Microsoft's recommendations [here][http-client-guidelines].**
#### Using dependency injection:
Install the [Microsoft.Extensions.Http][http-extensions] package from nuget. Where you register your services, add the following code:
```c#
builder.Services.AddSingleton(_ => new SovrenClientSettings
{
    AccountId = "12345678",
    ServiceKey = "abcdefghijklmnopqrstuvwxyz",
    DataCenter = DataCenter.US
};
builder.Services.AddHttpClient<ISovrenClient, SovrenClient>();
```
After injecting your SovrenClient, you will be able to get it from the service provider:
```c#
ISovrenClient client = serviceProvider.GetRequiredService<ISovrenClient>();
```

For self-hosted customers, you can create a `DataCenter` object with your custom URL using the constructor provided on that class.

### Handling errors and the `SovrenException`
Every call to any of the methods in the `SovrenClient` should be wrapped in a `try/catch` block. Any 4xx/5xx level errors will cause a `SovrenException` to be thrown. Sometimes these are a normal and expected part of the Sovren API. For example, if you have a website where users upload resumes, sometimes a user will upload a scanned image as their resume. Sovren does not process these, and will return a `422 Unprocessable Entity` response which will throw a `SovrenException`. You should handle any `SovrenException` in a way that makes sense in your application.

Additionally, there are `SovrenUsableResumeException` and `SovrenUsableJobException` which are thrown when some error/issue occurs in the API, but the response still contains a usable resume/job. For example, if you are geocoding while parsing and there is a geocoding error (which happens after parsing is done), the `ParsedResume` might still be usable in your application.

### How to create a Matching UI session
You may be wondering, "where are the Matching UI endpoints/methods?". We have made the difference between a normal API call (such as `Search`) and its equivalent Matching UI call extremely trivial. See the following example:

```c#
SovrenClient client = new SovrenClient(httpClient, new SovrenClientSettings
{
    AccountId = "12345678",
    ServiceKey = "abcdefghijklmnopqrstuvwxyz",
    DataCenter = DataCenter.US
});
List<string> indexesToSearch = ...;
FilterCriteria searchQuery = ...;

SearchResponse searchResponse = await client.Search(indexesToSearch, searchQuery);
```
To generate a Matching UI session with the above Search query, you simply need to call the `UI(...)` extension method on the `SovrenClient` object, pass in any UI settings, and then make the same call as above:
```c#
MatchUISettings uiSettings = ...;
GenerateUIResponse uiResponse = await client.UI(uiSettings).Search(indexesToSearch, searchQuery);
```
For every relevant method in the `SovrenClient`, you can create a Matching UI session for that query by doing the same as above.

[examples]: https://github.com/sovren/sovren-dotnet/tree/master/examples
[portal]: https://portal.sovren.com
[api-docs]: https://developer.textkernel.com/Sovren/v10/overview/
[dotnet-core-cli-tools]: https://docs.microsoft.com/en-us/dotnet/core/tools/
[nuget-cli]: https://docs.microsoft.com/en-us/nuget/tools/nuget-exe-cli-reference
[package-manager-console]: https://docs.microsoft.com/en-us/nuget/tools/package-manager-console
[docfx-docs]: https://sovren.github.io/sovren-dotnet/sdk/
[http-client]: https://learn.microsoft.com/en-us/dotnet/api/system.net.http.httpclient
[http-client-guidelines]: https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines
[http-extensions]: https://www.nuget.org/packages/Microsoft.Extensions.Http
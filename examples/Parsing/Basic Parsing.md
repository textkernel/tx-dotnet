# Basic Resume Parsing Example

```c#
public static async Task Main(string[] args)
{
    SovrenClient client = new SovrenClient("12345678", "abcdefghijklmnopqrstuvwxyz", DataCenter.US);
    
    //A Document is an unparsed File (PDF, Word Doc, etc)
    Document doc = new Document("resume.docx");

    //when you create a ParseRequest, you can specify many configuration settings
    //in the ParseOptions. See https://sovren.com/technical-specs/latest/rest-api/resume-parser/api/
    ParseRequest request = new ParseRequest(doc, new ParseOptions());

    try
    {
        ParseResumeResponse response = await client.ParseResume(request);
        //if we get here, it was 200-OK and all operations succeeded

        //now we can use the response from Sovren to output some of the data from the resume
        PrintBasicResumeInfo(response);
    }
    catch (SovrenException e)
    {
        //the document could not be parsed, always try/catch for SovrenExceptions when using SovrenClient
        Console.WriteLine($"Error: {e.SovrenErrorCode}, Message: {e.Message}");
    }

    Console.ReadKey();
}

static void PrintBasicResumeInfo(ParseResumeResponse response)
{
    PrintContactInfo(response);
    PrintPersonalInfo(response);
    PrintWorkHistory(response.Value);
    PrintEducation(response.Value);
}

static void PrintHeader(string headerName)
{
    Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}--------------- {headerName} ---------------");
}

static void PrintContactInfo(ParseResumeResponse response)
{
    //general contact information (only some examples listed here, there are many others)
    PrintHeader("CONTACT INFORMATION");
    Console.WriteLine("Name: " + response.EasyAccess().GetCandidateName()?.FormattedName);
    Console.WriteLine("Email: " + response.EasyAccess().GetEmailAddresses()?.FirstOrDefault());
    Console.WriteLine("Phone: " + response.EasyAccess().GetPhoneNumbers()?.FirstOrDefault());
    Console.WriteLine("City: " + response.EasyAccess().GetContactInfo()?.Location?.Municipality);
    Console.WriteLine("Region: " + response.EasyAccess().GetContactInfo()?.Location?.Regions?.FirstOrDefault());
    Console.WriteLine("Country: " + response.EasyAccess().GetContactInfo()?.Location?.CountryCode);
    Console.WriteLine("LinkedIn: " + response.EasyAccess().GetWebAddress(WebAddressType.LinkedIn));
}

static void PrintPersonalInfo(ParseResumeResponse response)
{
    //personal information (only some examples listed here, there are many others)
    PrintHeader("PERSONAL INFORMATION");
    Console.WriteLine("Date of Birth: " + response.EasyAccess().GetDateOfBirth()?.Date.ToShortDateString());
    Console.WriteLine("Driving License: " + response.EasyAccess().GetDrivingLicense());
    Console.WriteLine("Nationality: " + response.EasyAccess().GetNationality());
    Console.WriteLine("Visa Status: " + response.EasyAccess().GetVisaStatus());
}

static void PrintWorkHistory(ParseResumeResponseValue response)
{
    //basic work history display
    PrintHeader("EXPERIENCE SUMMARY");
    Console.WriteLine("Years Experience: " + Math.Round((response.ResumeData?.EmploymentHistory?.ExperienceSummary?.MonthsOfWorkExperience ?? 0) / 12.0, 1));
    Console.WriteLine("Avg Years Per Employer: " + Math.Round((response.ResumeData?.EmploymentHistory?.ExperienceSummary?.AverageMonthsPerEmployer ?? 0) / 12.0, 1));
    Console.WriteLine("Years Management Experience: " + Math.Round((response.ResumeData?.EmploymentHistory?.ExperienceSummary?.MonthsOfManagementExperience ?? 0) / 12.0, 1));

    response.ResumeData?.EmploymentHistory?.Positions?.ForEach(position =>
    {
        Console.WriteLine($"{Environment.NewLine}POSITION '{position.Id}'");
        Console.WriteLine($"Employer: {position.Employer?.Name?.Normalized}");
        Console.WriteLine($"Title: {position.JobTitle?.Normalized}");
        Console.WriteLine($"Date Range: {GetSovrenDateAsString(position.StartDate)} - {GetSovrenDateAsString(position.EndDate)}");
    });
}

static void PrintEducation(ParseResumeResponseValue response)
{
    //basic education display
    PrintHeader("EDUCATION SUMMARY");
    Console.WriteLine($"Highest Degree: {response.ResumeData?.Education?.HighestDegree?.Name?.Normalized}");

    response.ResumeData?.Education?.EducationDetails?.ForEach(edu =>
    {
        Console.WriteLine($"{Environment.NewLine}EDUCATION '{edu.Id}'");
        Console.WriteLine($"School: {edu.SchoolName?.Normalized}");
        Console.WriteLine($"Degree: {edu.Degree?.Name?.Normalized}");
        if (edu.Majors != null)
            Console.WriteLine($"Focus: {string.Join(", ", edu.Majors)}");
        if (edu.GPA != null)
            Console.WriteLine($"GPA: {edu.GPA?.NormalizedScore}/1.0 ({edu.GPA?.Score}/{edu.GPA?.MaxScore})");
        string endDateRepresents = edu.Graduated?.HasValue ?? false ? "Graduated" : "Last Attended";
        Console.WriteLine($"{endDateRepresents}: {GetSovrenDateAsString(edu.LastEducationDate)}");
    });
}

static string GetSovrenDateAsString(SovrenDate date)
{
    //a SovrenDate represents a date found on a resume, so it can either be 
    //'current', as in "July 2018 - current"
    //a year, as in "2018 - 2020"
    //a year and month, as in "2018/06 - 2020/07"
    //a year/month/day, as in "5/4/2018 - 7/2/2020"

    if (date == null) return "";
    if (date.IsCurrentDate) return "current";

    string format = "yyyy";

    if (date.FoundMonth)
    {
        format += "-MM";//only print the month if it was actually found on the resume/job

        if (date.FoundDay) format += "-dd";//only print the day if it was actually found
    }

    return date.Date.ToString(format);
}
```
//////////////////////////////////////////////////////////////////////
// TOOLS
//////////////////////////////////////////////////////////////////////

#tool "nuget:?package=GitVersion.CommandLine"
#tool "nuget:?package=MSBuild.SonarQube.Runner.Tool"

//////////////////////////////////////////////////////////////////////
// ADDINS
//////////////////////////////////////////////////////////////////////

#addin nuget:?package=Cake.Coverlet
#addin "nuget:?package=Cake.Sonar"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var projects = "./src/**/*.csproj";
var testProjects = "./test/**/*.csproj";
var allProjectFiles = GetFiles(projects) + GetFiles(testProjects);
var packFiles = "./src/*/*.csproj";
var buildArtifacts = "./artifacts";

BuildParameters.Initialize(Context, BuildSystem);

Setup(context =>
{   
    Information($"Building {BuildParameters.Title} v{BuildParameters.Version} from branch {BuildParameters.BranchName} with configuration {BuildParameters.Configuration}");
});

Task("__Clean")
    .Does(() => 
    {
        CleanDirectories(buildArtifacts);
    });

Task("__SonarBegin")
  .WithCriteria(() => BuildParameters.ShouldAnalyze)
  .Does(() => {
     SonarBegin(new SonarBeginSettings{
        Branch = BuildParameters.BranchName,
        Url = BuildParameters.SonarUrl,
        Login = BuildParameters.SonarKey,
        Organization = "checkout",
        Key = "checkout-sdk-net",
        OpenCoverReportsPath = "./artifacts/CheckoutSDK.Tests.Coverage.xml",
        Silent = true
     });
  });

Task("__Build")
    .Does(() =>
    {
        var settings = new DotNetCoreBuildSettings
        {
            Configuration = BuildParameters.Configuration,
            ArgumentCustomization = args => args.Append($"/p:Version={BuildParameters.Version}")
        };
        
        foreach (var projectFile in allProjectFiles)
        {
            DotNetCoreBuild(projectFile.ToString(), settings);
        }
    });

Task("__Test")
    .Does(() =>
    {
        var testSettings = new DotNetCoreTestSettings();
        
        foreach (var projectFile in GetFiles(testProjects))
        {                      
            var coveletSettings = new CoverletSettings {
                CollectCoverage = true,
                CoverletOutputFormat = CoverletOutputFormat.opencover,
                CoverletOutputDirectory = Directory(buildArtifacts),
                CoverletOutputName = projectFile.GetFilenameWithoutExtension() + ".Coverage.xml"
            };
            
            DotNetCoreTest(projectFile.ToString(), testSettings, coveletSettings);
        }
    });

Task("__SonarEnd")
  .WithCriteria(() => BuildParameters.ShouldAnalyze)
  .Does(() => {
     SonarEnd(new SonarEndSettings{
        Login = BuildParameters.SonarKey
     });
  });

Task("__Pack")
    .Does(() => 
    {
        var settings = new DotNetCorePackSettings 
        {
            Configuration = BuildParameters.Configuration,
            OutputDirectory = buildArtifacts,
            NoBuild = true,
            ArgumentCustomization = args => args.Append($"/p:Version={BuildParameters.Version}")
        };
        
        foreach (var projectFile in GetFiles(packFiles))
        {
            DotNetCorePack(projectFile.ToString(), settings); 
        }
    });

Task("__PublishMyGet")
    .WithCriteria(() => BuildParameters.ShouldPublishMyGet)
    .Does(() => 
    {
        if (BuildParameters.CanPublishMyGet) 
        {
            PublishPackages(BuildParameters.MyGetSource, BuildParameters.MyGetApiKey);
        }
        else 
        {
            Warning("Unable to publish to MyGet, as necessary credentials are not available");
        }
    });

Task("__PublishNuGet")
    .WithCriteria(() => BuildParameters.ShouldPublishNuGet)
    .Does(() => 
    {
        if (BuildParameters.CanPublishNuget)
        {
            PublishPackages(BuildParameters.NuGetSource, BuildParameters.NuGetApiKey);
        }
        else 
        {
            Warning("Unable to publish to NuGet, as necessary credentials are not available");
        }
    });

private void PublishPackages(string source, string apiKey)
{
    foreach(var package in GetFiles("./artifacts/*.nupkg"))
    {
        // Push the package.
        NuGetPush(package.ToString(), new NuGetPushSettings {
            Source = source,
            ApiKey = apiKey
        });
    }
}

Task("Build")
    .IsDependentOn("__Clean")
    .IsDependentOn("__SonarBegin")
    .IsDependentOn("__Build")
    .IsDependentOn("__Test")
    .IsDependentOn("__SonarEnd")
    .IsDependentOn("__Pack");

Task("Default")
    .IsDependentOn("Build");

Task("Deploy")
    .IsDependentOn("Build")
    .IsDependentOn("__PublishMyGet")
    .IsDependentOn("__PublishNuget");

RunTarget(BuildParameters.Target);


public static class BuildParameters
{
    public static string Title => "Checkout SDK";
    public static string Target { get; private set; }
    public static string Version { get; private set; }
    public static string Configuration { get; private set; }
    public static string MyGetSource { get; private set; }
    public static string MyGetApiKey {get; private set; }
    public static string NuGetSource { get; private set; }
    public static string NuGetApiKey { get; private set; }
    public static bool IsMasterBranch { get; private set; }
    public static bool IsPullRequest { get; private set; }
    public static bool IsTagged { get; private set; }
    public static string BranchName { get; private set; }
    public static string SonarKey { get; private set; }
    public static string SonarUrl { get; private set; }
    
    public static void Initialize(ICakeContext context, BuildSystem buildSystem)
    {
        context.Information("running");
        Target = context.Argument("target", "Default");

        Configuration = context.Argument("configuration", "Release");

        var gitVersion = context.GitVersion(new GitVersionSettings {
            OutputType = GitVersionOutput.Json
        });

        Version = gitVersion.NuGetVersion;

        BranchName = gitVersion.BranchName;
        IsMasterBranch = StringComparer.OrdinalIgnoreCase.Equals("master", gitVersion.BranchName);
        IsPullRequest = buildSystem.AppVeyor.Environment.PullRequest.IsPullRequest;
        IsTagged = (
            buildSystem.AppVeyor.Environment.Repository.Tag.IsTag &&
            !string.IsNullOrWhiteSpace(buildSystem.AppVeyor.Environment.Repository.Tag.Name)
        );

        MyGetApiKey = context.EnvironmentVariable("MYGET_API_KEY");
        MyGetSource = context.EnvironmentVariable("MYGET_SOURCE");
        NuGetApiKey = context.EnvironmentVariable("NUGET_API_KEY");
        NuGetSource = context.EnvironmentVariable("NUGET_SOURCE");
        SonarKey = context.EnvironmentVariable("SONAR_KEY");
        SonarUrl = context.EnvironmentVariable("SONAR_URL");
    }

    public static bool ShouldPublishMyGet 
        => !IsMasterBranch && !IsPullRequest;

    public static bool CanPublishMyGet 
        => !string.IsNullOrEmpty(BuildParameters.MyGetApiKey) && !string.IsNullOrEmpty(BuildParameters.MyGetSource);

    public static bool ShouldPublishNuGet 
        => IsMasterBranch && IsTagged;

    public static bool CanPublishNuget 
        => !string.IsNullOrEmpty(BuildParameters.NuGetApiKey) && !string.IsNullOrEmpty(BuildParameters.NuGetSource);

    public static bool ShouldAnalyze 
        => !string.IsNullOrEmpty(BuildParameters.SonarUrl) && !string.IsNullOrEmpty(BuildParameters.SonarKey);
}
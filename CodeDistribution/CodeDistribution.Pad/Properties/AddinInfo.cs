using Mono.Addins;
using Mono.Addins.Description;

[assembly: Addin(
    "CodeDistribution.Pad",
    Namespace = "CodeDistribution.Pad",
    Version = "1.1"
)]

[assembly: AddinName("Code Distribution")]
[assembly: AddinCategory("IDE extensions")]
[assembly: AddinDescription("A Visual Studio for Mac extension that gives a count of the lines of code in each of a solution's projects and then calculate those as a percentage of the entire solution.\n\nby Arthur Demanuele")]
[assembly: AddinAuthor("Arthur Demanuele")]

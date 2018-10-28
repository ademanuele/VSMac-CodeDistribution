using CommandLine;
using CommandLine.Text;

namespace CodeDistribution.Console
{
    public class CommandLineOptions
    {
        [Option('d', "directory", Required = true, HelpText = "Root directory for C# solution.")]
        public string SolutionRoot { get; set; }

        [OptionArray('i', "include", Required = false, HelpText = "A list of file extensions to INCLUDE in the distribution.")]
        public string[] Include { get; set; }

        [OptionArray('e', "exclude", Required = false, HelpText = "A list of file extensions to EXCLUDE in the distribution")]
        public string[] Exclude { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}

namespace CodeDistribution.Console
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var options = new CommandLineOptions();

            if (!CommandLine.Parser.Default.ParseArguments(args, options))
                return;

            PrintDistributionAnalysis(options);
        }

        static void PrintDistributionAnalysis(CommandLineOptions options) {
			try
			{
                var distribution = new Common.CodeDistribution(options.SolutionRoot);

                if (options.Include != null)
                    distribution.IncludeExtensions.AddRange(options.Include);
                if(options.Exclude != null)
                    distribution.ExcludeExtensions.AddRange(options.Exclude);

                var printer = new DistributionConsolePrinter(distribution);
				printer.Print();
			}
            catch(System.IO.FileNotFoundException) {
                System.Console.WriteLine("Directory not found: " + options.SolutionRoot);
            }
			catch (System.Exception e)
			{
				System.Console.WriteLine("Fatal Error!");
			}
        }
    }
}

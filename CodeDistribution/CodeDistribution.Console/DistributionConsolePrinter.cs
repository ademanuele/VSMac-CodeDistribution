using System.Collections.Generic;
using CodeDistribution.Common;
namespace CodeDistribution.Console
{
    public class DistributionConsolePrinter
    {
        Common.CodeDistribution Distribution { get; }

        public DistributionConsolePrinter(Common.CodeDistribution distribution)
        {
            Distribution = distribution;
        }

        public void Print() {
            PrintAnalysisStart();
            var result = Distribution.Analyse();

            PrintHeader(result.DirectoryResults);
            PrintDirectoryBreakdown(result.DirectoryResults);
            PrintFooter(result);
        }

        void PrintAnalysisStart() {
            System.Console.WriteLine("Analyzing: " + Distribution.RootDirectory);
        }

        void PrintHeader(List<DirectoryAnalysisResult> results) {
            System.Console.WriteLine("-------------------------------------\n");
        }

        void PrintDirectoryBreakdown(List<DirectoryAnalysisResult> results) {
            var table = new ConsoleTable(new string[] { "Directory", "Lines Of Code", "Percentage of Solution" });

            foreach(var result in results)
                table.AddRow(new object[]{ result.Name, result.LinesOfCode, result.PercentageOfSolution });

            table.Write();
        }

        void PrintFooter(AnalysisResult result) {
            System.Console.WriteLine("-------------------------------------");
            System.Console.WriteLine("Projects: " + result.DirectoryResults.Count + " Lines: " + result.TotalLines);
            System.Console.WriteLine("-------------------------------------");
        }
    }
}

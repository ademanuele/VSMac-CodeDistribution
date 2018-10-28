using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;

namespace CodeDistribution.Common
{
    public class CodeDistribution
    {
        public string RootDirectory { get; }
        public List<string> IncludeExtensions { get; }
        public List<string> ExcludeExtensions { get; }
        public List<string> IncludeDirectories { get; }
        public List<string> ExcludeDirectories { get; }

        public CodeDistribution(string directory)
        {
            if (!Directory.Exists(directory))
                throw new FileNotFoundException("Root directory not found for code distribution analysis.");

            RootDirectory = directory;
            IncludeExtensions = new List<string>();
            ExcludeExtensions = new List<string>();
            IncludeDirectories = new List<string>();
            ExcludeDirectories = new List<string>();
        }

        public AnalysisResult Analyse()
        {
            var directories = new List<string>(Directory.GetDirectories(RootDirectory));
            var projectDirectories = FilterProjectDirectories(directories);

            var directoryResults = AnalyseFilesInDirectories(projectDirectories);
            return new AnalysisResult(directoryResults);
        }

        List<string> FilterProjectDirectories(List<string> directories)
        {
            return directories.Where((d) => IncludeDirectories.Contains(d) && !ExcludeDirectories.Contains(d)).ToList();
        }

        List<DirectoryAnalysisResult> AnalyseFilesInDirectories(List<string> directories)
        {
            var results = new List<DirectoryAnalysisResult>();
            foreach (var directory in directories)
                results.Add(AnalyseFilesInDirectory(directory));

            return results;
        }

        DirectoryAnalysisResult AnalyseFilesInDirectory(string directory)
        {
            var result = new DirectoryAnalysisResult(directory);
            var files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);
            var filtered = FilterFiles(files);

            foreach (var file in filtered)
            {
                int lines = CountLinesInFile(file);
                result.LinesOfCode += lines;
            }

            return result;
        }

        string[] FilterFiles(string[] files)
        {
            return files.Where(p =>
            {
                var extension = GetFullFileExtension(Path.GetFileName(p));
                return IncludeExtensions.Contains(extension) && !ExcludeExtensions.Contains(extension);
            }).ToArray();
        }

        string GetFullFileExtension(string file)
        {
            return file.Substring(file.IndexOf('.') + 1);
        }

        int CountLinesInFile(string filePath)
        {
            using (StreamReader r = new StreamReader(new FileStream(filePath, FileMode.Open)))
            {
                int i = 0;
                while (r.ReadLine() != null) { i++; }
                return i;
            }
        }
    }

    public class AnalysisResult
    {
        public List<DirectoryAnalysisResult> DirectoryResults { get; }
        public int TotalLines { get; private set; }

        public AnalysisResult(List<DirectoryAnalysisResult> results)
        {
            DirectoryResults = results;
            CalculateTotalLines();
            CalculatePercentages();
        }

        void CalculateTotalLines()
        {
            foreach (var result in DirectoryResults)
                TotalLines += result.LinesOfCode;
        }

        void CalculatePercentages()
        {
            foreach (var result in DirectoryResults)
                result.PercentageOfSolution = Math.Round((double)result.LinesOfCode / TotalLines * 100, 2);
        }
    }

    public class DirectoryAnalysisResult
    {
        public string Name { get; }
        public int LinesOfCode { get; internal set; }
        public double PercentageOfSolution { get; internal set; }

        public DirectoryAnalysisResult(string path)
        {
            Name = Path.GetFileName(path);
        }
    }
}

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CodeDistribution.Common
{
    public class CodeDistribution
    {
        public List<string> IgnoredDirectories { get; set; } = new List<string>();
        public List<string> IncludedExtensions { get; set; } = new List<string>();
        public List<string> IgnoredExtensions { get; set; } = new List<string>();

        public List<DirectoryAnalysis> ProcessDirectories( List<string> paths )
        {
            var results = new List<DirectoryAnalysis>();

            foreach (var path in paths)
                results.Add(ProcessDirectory(path));

            return results;
        }

        internal DirectoryAnalysis ProcessDirectory( string path, List<FileAnalysis> accumulatedResult = null )
        {
            var result = accumulatedResult ?? new List<FileAnalysis>();

            if (ShouldIgnoreDirectory(path))
                return new DirectoryAnalysis(path, result);

            var files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);
            ProcessFiles(files, result);

            ProcessSubDirectories(path, result);

            return new DirectoryAnalysis(path, result);
        }

        bool ShouldIgnoreDirectory( string path )
        {
            return IgnoredDirectories.Contains(path) || IgnoredDirectories.Contains(Path.GetFileName(path));
        }

        void ProcessSubDirectories( string rootDirectory, List<FileAnalysis> accumulatedResult )
        {
            foreach (var path in Directory.GetDirectories(rootDirectory))
                ProcessDirectory(path, accumulatedResult);
        }

        void ProcessFiles( string[] files, List<FileAnalysis> result )
        {
            foreach (var file in files)
            {
                if (!ShouldProcessFile(file))
                    continue;
                result.Add(Process(file));
            }
        }

        bool ShouldProcessFile( string file )
        {
            var extension = Helpers.File.Extension(file);
            var included = IncludedExtensions == null || IncludedExtensions.Count == 0 || IncludedExtensions.Any(e => extension.EndsWith(e, System.StringComparison.OrdinalIgnoreCase));
            var exclude = IgnoredExtensions.Contains(extension);
            return included && !exclude;
        }

        FileAnalysis Process( string filePath )
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var lineCount = CountLines(fileStream);
                return new FileAnalysis(filePath, lineCount);
            }
        }

        internal int CountLines( Stream fileStream )
        {
            using (StreamReader file = new StreamReader(fileStream))
            {
                int lines = 0;
                while (!file.EndOfStream)
                    if (!string.IsNullOrEmpty(file.ReadLine())) lines++;

                return lines;
            }
        }
    }
}

using System.Collections.Generic;
using CodeDistribution.Common.Helpers;

namespace CodeDistribution.Common
{
    public class DirectoryAnalysis
    {
        public string Path { get; }
        public List<FileAnalysis> Files { get; internal set; }
        public Dictionary<string, int> LinesByExtension { get; internal set; }
        public int TotalLines { get; private set; }

        public DirectoryAnalysis(string path, List<FileAnalysis> files = null)
        {
            Path = path;
            Files = files ?? new List<FileAnalysis>();
            AnalyseResults();
        }

        void AnalyseResults()
        {
            LinesByExtension = new Dictionary<string, int>();
            Files.Each((f, i) =>
            {
                TotalLines += f.Lines;
                AnalyseFileExtension(f);
            });
        }

        void AnalyseFileExtension(FileAnalysis f)
        {
            if (!LinesByExtension.ContainsKey(f.Extension))
                LinesByExtension[f.Extension] = 0;
            LinesByExtension[f.Extension] += f.Lines;
        }
    }

    public class FileAnalysis
    {
        public int Lines { get; }
        public string Path { get; }
        public string Extension { get; }

        public FileAnalysis(string path, int lines)
        {
            Path = path;
            Lines = lines;
            Extension = File.Extension(Path);
        }
    }
}

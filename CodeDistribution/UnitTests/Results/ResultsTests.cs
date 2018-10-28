using NUnit.Framework;
using CodeDistribution.Common;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    public class ResultsTests
    {
        [Test]
        public void CorrectFileAnalysisData_GivenInitialData()
        {
            var expectedPath = "/test/file/path.test.extension";
            var expectedExtension = "test.extension";
            var expectedLines = 10;

            var result = new FileAnalysis(expectedPath, expectedLines);

            Assert.AreEqual(expectedPath, result.Path);
            Assert.AreEqual(expectedLines, result.Lines);
            Assert.AreEqual(expectedExtension, result.Extension);
        }

        [Test]
        public void DirectoryResultHasCorrectTotalLines_GivenFileResults()
        {
            var resultLines = 10;
            var fileResults = MakeTestResults(10, "test/path", resultLines);
            var expectedLines = resultLines * fileResults.Count;

            var result = new DirectoryAnalysis("test/directory", fileResults);

            Assert.AreEqual(expectedLines, result.TotalLines);
        }

        [Test]
        public void DirectoryResultHasCorrectFileTypeCalculations_GivenFileResults()
        {
            var fileResults = new List<FileAnalysis>();
            fileResults.AddRange(MakeTestResults(10, "test/path.extension1", 10));
            fileResults.AddRange(MakeTestResults(10, "test/path.extension2", 10));

            var result = new DirectoryAnalysis("test/directory", fileResults);

            var expectedLines = new Dictionary<string, int>
            {
                { "extension1", 100 },
                { "extension2", 100 }
            };
            Assert.AreEqual(expectedLines, result.LinesByExtension);
        }

        List<FileAnalysis> MakeTestResults(int count, string path, int lines)
        {
            var results = new List<FileAnalysis>();
            foreach (var i in Enumerable.Range(0, count))
                results.Add(new FileAnalysis(path, lines));

            return results;
        }
    }
}

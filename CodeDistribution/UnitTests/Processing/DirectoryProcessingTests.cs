using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using CodeDistribution.Common.Helpers;
using System.Linq;

namespace UnitTests
{
    public class DirectoryProcessingTests
    {
        CodeDistribution.Common.CodeDistribution processor;

        [SetUp]
        public void Setup()
        {
            processor = new CodeDistribution.Common.CodeDistribution();
        }

        [Test]
        public void IncludeFiles_GivenExtensionsToInclude( [Values("cs", "g.cs")] string includedExtension )
        {
            string directory = "Data";
            var totalFileCount = FileCountForEmbeddedDirectory(directory);
            var expectedFileCount = FileCountForEmbeddedDirectory(directory, includedExtension, ExtensionComparison.EndsWith);

            processor.IncludedExtensions = new List<string> { includedExtension };
            var results = processor.ProcessDirectory(PathForEmbeddedDirectory(directory));

            Assert.AreEqual(expectedFileCount, results.Files.Count);
        }

        [Test]
        public void IgnoreFiles_GivenExtensionsToIgnore( [Values("cs", "g.cs")] string ignoredExtension )
        {
            string directory = "Data";
            var totalFileCount = FileCountForEmbeddedDirectory(directory);
            var ignoredFileCount = FileCountForEmbeddedDirectory(directory, ignoredExtension, ExtensionComparison.FullExtension);
            var expectedFileCount = totalFileCount - ignoredFileCount;

            processor.IgnoredExtensions = new List<string> { ignoredExtension };
            var results = processor.ProcessDirectory(PathForEmbeddedDirectory(directory));

            Assert.AreEqual(expectedFileCount, results.Files.Count);
        }


        [Test]
        public void DirectoriesIgnored_GivenIgnoredDirectories()
        {
            string directory = "Data", ignoredDirectory = "IgnoredDirectory";
            var directoryPath = PathForEmbeddedDirectory(directory);
            var totalFileCount = FileCountForEmbeddedDirectory(directory);
            var ignoredPath = PathForEmbeddedDirectory(Path.Combine(directory, ignoredDirectory));
            var ignoredFileCount = FileCountForEmbeddedDirectory(ignoredPath);
            var expectedFileCount = totalFileCount - ignoredFileCount;

            processor.IgnoredDirectories = new List<string> { ignoredDirectory };
            var results = processor.ProcessDirectory(directoryPath);

            Assert.AreEqual(expectedFileCount, results.Files.Count);
        }

        [Test]
        public void AllFilesProcessed_GivenMultipleDirectories() =>
            AssertAllFilesProcessed_GivenDirectories(new[] { "Data", "Data2" });

        void AssertAllFilesProcessed_GivenDirectories( string[] directories )
        {
            var directoryPaths = PathsForEmbeddedDirectories(directories);
            var fileCount = FileCountForEmbeddedDirectories(directories);

            var results = processor.ProcessDirectories(directoryPaths.ToList());
            var resultFileCount = 0;
            results.ForEach(( r ) => resultFileCount += r.Files.Count);

            Assert.AreEqual(fileCount, resultFileCount);
        }

        string[] PathsForEmbeddedDirectories( string[] directories )
        {
            string[] paths = new string[directories.Length];
            directories.Each(( d, i ) => paths[i] = PathForEmbeddedDirectory(d));
            return paths;
        }

        int FileCountForEmbeddedDirectories( string[] directories )
        {
            int count = 0;
            directories.Each(( d, i ) => count += FileCountForEmbeddedDirectory(d));
            return count;
        }

        int FileCountForEmbeddedDirectory( string directory, string extension, ExtensionComparison comp )
        {
            int count = 0;
            var files = Directory.GetFiles(PathForEmbeddedDirectory(directory), "*.*", SearchOption.AllDirectories);
            foreach (var file in files)
                if (comp == ExtensionComparison.EndsWith && Extension(file).EndsWith(extension, System.StringComparison.InvariantCultureIgnoreCase)) count++;
                else if (comp == ExtensionComparison.FullExtension && Extension(file).Equals(extension, System.StringComparison.InvariantCultureIgnoreCase)) count++;
            return count;
        }

        enum ExtensionComparison { FullExtension, EndsWith }

        string Extension( string filePath )
        {
            var fileName = Path.GetFileName(filePath);
            return fileName.Substring(fileName.IndexOf('.') + 1);
        }

        int FileCountForEmbeddedDirectory( string directory ) =>
            Directory.GetFiles(PathForEmbeddedDirectory(directory), "*.*", SearchOption.AllDirectories).Length;

        string PathForEmbeddedDirectory( string directory ) =>
            Path.Combine(OutputDirectory, directory);

        string OutputDirectory =>
            Path.GetDirectoryName(typeof(LineCountTests).Assembly.Location);
    }
}

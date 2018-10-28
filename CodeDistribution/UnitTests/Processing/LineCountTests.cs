using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using CodeDistribution.Common;

namespace UnitTests
{
    public class LineCountTests
    {
        [Test]
        public void IgnoreEmptyLines() =>
            AssertForEmbeddedResource("UnitTests.Data.ClassWithEmptyLines.cs", 10);

        [Test]
        public void Files_GiveExpectedOutput([ValueSource("TestFiles")]FileCountPair data) =>
            AssertForEmbeddedResource(data.Resource, data.ExpectedLines);

        void AssertForEmbeddedResource(string resource, int count)
        {
            var fileStream = typeof(LineCountTests).Assembly.GetManifestResourceStream(resource);

            var lineCount = new CodeDistribution.Common.CodeDistribution().CountLines(fileStream);

            Assert.AreEqual(count, lineCount);
        }

        public static IEnumerable TestFiles =>
            new List<FileCountPair>
            {
                new FileCountPair("UnitTests.Data.EmptyFile.cs", 0),
                new FileCountPair("UnitTests.Data.SmallClass.cs", 10),
                new FileCountPair("UnitTests.Data.MediumClass.cs", 107),
            };

        public struct FileCountPair
        {
            public string Resource { get; }
            public int ExpectedLines { get; }

            public FileCountPair(string resource, int expectedLines)
            {
                Resource = resource;
                ExpectedLines = expectedLines;
            }

            public override string ToString()
            {
                return string.Format("[Resource={0}, ExpectedLines={1}]", Resource, ExpectedLines);
            }
        }
    }
}

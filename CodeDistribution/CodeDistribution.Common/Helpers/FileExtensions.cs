using System.IO;

namespace CodeDistribution.Common.Helpers
{
    public static class File
    {
        public static string Extension(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            return fileName.Substring(fileName.IndexOf('.') + 1);
        }
    }
}

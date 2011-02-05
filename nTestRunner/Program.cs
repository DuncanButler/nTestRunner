using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace nTestRunner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new Configuration(args);
        }
    }

    public class Configuration
    {
        public Configuration(string[] args)
        {
            SetDefaultValues();

            SetArgumentValues(args);
        }

        void SetDefaultValues()
        {
            SolutionPath = SetWatchFilePath();
            TestRunner = string.Empty;
            ResultDisplay = string.Empty;
        }

        void SetArgumentValues(string[] args)
        {
            if (args.Length == 0)
                return;

            for (int i = 0; i < args.Length -1; i+=2)
            {
                string key = args[i];
                string value = args[i + 1];

                if (key.ToLower() == "-path" || key.ToLower() == "-p")
                    SolutionPath = value;

                if (key.ToLower() == "-test")
                    TestRunner = value;

                if (key.ToLower() == "-display")
                    ResultDisplay = value;
            }
        }

        string SetWatchFilePath()
        {
            DirectoryInfo searchDirectory = GetSearchDirectory();

            var files = GetSolutionOrProjectFiles(searchDirectory);

            return GetWatchFilePath(files);
        }

        DirectoryInfo GetSearchDirectory()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;

            return Directory.GetParent(path).Parent;
        }

        IEnumerable<FileInfo> GetSolutionOrProjectFiles(DirectoryInfo searchDirectory)
        {
            var files = GetFilesWithPattern(searchDirectory, "*.sln");

            if (FoundFiles(files))
                return files;

            return GetFilesWithPattern(searchDirectory, "*.csproj");
        }

        FileInfo[] GetFilesWithPattern(DirectoryInfo searchDirectory, string searchPattern)
        {
            return searchDirectory.GetFiles(searchPattern, SearchOption.TopDirectoryOnly);
        }

        bool FoundFiles(FileInfo[] files)
        {
            return files.Length > 0;
        }

        string GetWatchFilePath(IEnumerable<FileInfo> files)
        {
            return files.FirstOrDefault().FullName;
        }

        public string TestRunner { get; private set; }
        public string SolutionPath { get; private set; }
        public string ResultDisplay { get; private set; }
    }
}
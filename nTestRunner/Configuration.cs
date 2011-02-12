using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace nTestRunner
{
    public class Configuration
    {
        public Configuration(string[] args)
        {
            SetDefaults();

            if (args == null)
                return;

            if (string.IsNullOrWhiteSpace(args[0]))
                return;

            ParseArguments(args);
        }

        public string SolutionPath { get; private set; }

        public IList<string> RunnerDisplay { get; private set; }

        public IList<string> TestRunner { get; set; }

        void SetDefaults()
        {
            SolutionPath = GetSolutionFilePath(Directory.GetCurrentDirectory());
            RunnerDisplay = new List<string>();
            TestRunner = new List<string>();
        }

        void ParseArguments(string[] args)
        {
            try
            {
                for (int i = 0; i < args.Length; i += 2)
                {                    
                    string key = args[i].ToLower();
                    string value = args[i + 1];

                    if (key == "-p" || key == "-path")
                        SolutionPath = value;

                    if (key == "-d" || key == "-display")
                        RunnerDisplay.Add(value);

                    if (key == "-t" || key == "-test")
                        TestRunner.Add(value);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Arguments invalid", ex);
            }
        }

        static string GetSolutionFilePath(string currentDirectory)
        {
            string[] files = Directory.GetFiles(currentDirectory);

            IEnumerable<string> found = from file in files where file.Contains(".sln") select file;

            if (found.Count() > 0)
                return found.First();

            DirectoryInfo parent = Directory.GetParent(currentDirectory);

            return GetSolutionFilePath(parent.FullName);
        }
    }
}
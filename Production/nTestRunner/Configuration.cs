using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace nTestRunner
{
    public class Configuration
    {
        public void LoadConfigurationFrom(string[] args)
        {
            RunnerDisplay = new List<string>();
            TestRunner = new List<string>();

            if (args != null)
                if (args.Length > 1 && string.IsNullOrWhiteSpace(args[1]) != true)
                    ParseArguments(args);

            SetDefaults();
        }

        public string SolutionPath { get; private set; }

        public IList<string> RunnerDisplay { get; private set; }

        public IList<string> TestRunner { get; private set; }

        public string Builder { get; private set; }

        void SetDefaults()
        {
            if(string.IsNullOrWhiteSpace(SolutionPath))
                SolutionPath = GetSolutionFilePath(Directory.GetCurrentDirectory());

            if(RunnerDisplay.Count == 0)
                RunnerDisplay.Add(ConfigurationManager.AppSettings["RunnerDisplay"]);

            if(TestRunner.Count == 0)
            {
                string[] testRunners = ConfigurationManager.AppSettings["TestRunner"].Split(',');

                foreach (var runner in testRunners)
                    TestRunner.Add(runner);
            }

            if(string.IsNullOrWhiteSpace(Builder))
                Builder = ConfigurationManager.AppSettings["Builder"];
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

                    if (key == "-b" || key == "-builder")
                        Builder = value;
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

        public Solution LoadSolution()
        {
            var solution = new Solution();
            solution.Load(SolutionPath);

            return solution;
        }
    }
}
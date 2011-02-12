using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace nTestRunner
{
    public class Runner
    {
        public Runner(string[] args, IConsole console)
        {
            var configuration = new Configuration(args);

            string solutionFilePath = configuration.SolutionPath;

            Solution solution = BuildSolution(solutionFilePath);

            WriteTitle(solution, console);

            WriteSolutionDetails(solution, console);

            WriteTestRunnerDetails(configuration, console);
        }

        Solution BuildSolution(string solutionFilePath)
        {
            var solution = new Solution();
            solution.Load(solutionFilePath);
            return solution;
        }

        void WriteTitle(Solution solution, IConsole console)
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            console.Write(string.Format(TitleVersion, version.Major, version.Minor));
        }

        void WriteSolutionDetails(Solution solution, IConsole console)
        {
            console.Write(WatchingFiles);
            console.Write(solution.Name);

            foreach (var project in solution.Projects)
                console.Write(project.ProjectName);
        }

        void WriteTestRunnerDetails(Configuration configuration, IConsole console)
        {
            if (configuration.TestRunner.Count == 0)
                return;

            var builder = new StringBuilder();
            builder.Append("Running tests with ");
            bool moreThanOneRunner = false;
            foreach (var runners in configuration.TestRunner)
            {
                if(moreThanOneRunner)
                    builder.Append(", ");

                builder.Append(runners);

                moreThanOneRunner = true;
            }

            console.Write(builder.ToString());
        }

        string WatchingFiles { get { return Resource.ResourceManager.GetString("WatchingFiles"); } }
        string TitleVersion { get { return Resource.ResourceManager.GetString("TitleVersion"); } }
    }
}
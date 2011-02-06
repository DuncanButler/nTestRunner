using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace nTestRunner
{
    public class Runner
    {
        public Runner(string[] args, IConsole console)
        {   
            if(args.Length > 0)
            {
                foreach (var arg in args)
                {
                    
                }
            }
            string solutionFilePath = GetSolutionFilePath();

            Solution solution = BuildSolution(solutionFilePath);

            WriteTitle(solution, console);
        }

        Solution BuildSolution(string solutionFilePath)
        {
            var solution = new Solution();
            solution.Load(solutionFilePath);
            return solution;
        }

        string GetSolutionFilePath()
        {
            string currentPath = Directory.GetCurrentDirectory();

            return FindSolutionFile(currentPath);
        }

        void WriteTitle(Solution solution, IConsole console)
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            console.Write(string.Format(TitleVersion, version.Major, version.Minor));
            console.Write(WatchingFiles);
            console.Write(solution.Name);

            foreach (var project in solution.Projects)
                console.Write(project.ProjectName);
        }

        string FindSolutionFile(string currentPath)
        {
            string[] files = Directory.GetFiles(currentPath);

            IEnumerable<string> found = from file in files where file.Contains(".sln") select file;

            if (found.Count() > 0)
                return found.First();

            DirectoryInfo parent = Directory.GetParent(currentPath);

            return FindSolutionFile(parent.FullName);
        }

        string WatchingFiles { get { return Resource.ResourceManager.GetString("WatchingFiles"); } }
        string TitleVersion { get { return Resource.ResourceManager.GetString("TitleVersion"); } }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using nTestRunner.Interfaces;
using nTestRunner.Interfaces.ResultsSerialization;

namespace nTestRunner.TestRunners
{
    public abstract class BaseTestCaller : ITestCaller
    {       
        public virtual TestResults RunTests(IEnumerable<IProject> projects, string baseDirectory, string buildEngine,
                                             IConsole console)
        {
            BaseDirectory = baseDirectory;
            BuildEngineVersion = buildEngine;

            PackagesDirectory = FindPackagesDirectory(baseDirectory);

            var process = new Process();
            process.StartInfo.FileName = TestRunnerFullPath();
            process.StartInfo.Arguments = TestArguments(projects);
            process.StartInfo.RedirectStandardOutput = false;
            process.StartInfo.RedirectStandardError = false;
            process.StartInfo.UseShellExecute = false;
            process.EnableRaisingEvents = false;

            process.Start();
            process.WaitForExit();
            process.Close();

            return CompileResults();
        }

        protected string BuildEngineVersion
        {
            get;
            set;
        }

        protected string BaseDirectory { get; private set; }

        protected string PackagesDirectory { get; private set; }

        protected string GenerateTestAssembliyPaths(IEnumerable<IProject> projects)
        {
            var builder = new StringBuilder();
            foreach (var project in projects)
            {
                if (builder.Length > 0)
                    builder.Append(" ");

                string pathBase = Path.Combine(project.ProjectPath, @"bin\debug");
                string fileName = string.Format("{0}.dll", Path.GetFileNameWithoutExtension(project.ProjectName));

                builder.Append(BuildTestAssemblyPath(pathBase, fileName));
            }

            return builder.ToString();
        }


        string FindPackagesDirectory(string searchPath)
        {
            string[] directories = Directory.GetDirectories(searchPath);

            string packagesPath =
                (from d in directories where Path.GetDirectoryName(d).Contains("packages") select d).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(packagesPath) == false)
                return packagesPath;

            foreach (var directory in directories)
            {
                string potential = FindPackagesDirectory(directory);

                if (string.IsNullOrWhiteSpace(potential) == false)
                    return potential;
            }

            return string.Empty;
        }

        protected abstract TestResults CompileResults();

        protected abstract string TestArguments(IEnumerable<IProject> projects);

        protected abstract string TestRunnerFullPath();
        protected abstract string BuildTestAssemblyPath(string pathBase, string fileName);
    }
}
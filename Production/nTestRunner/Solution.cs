using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using BuildEngine2;
using BuildEngine35;
using BuildEngine4;
using MSpecCaller;
using nTestRunner.Interfaces;
using nTestRunner.Interfaces.ResultsSerialization;

namespace nTestRunner
{
    public class Solution
    {
        readonly List<IProject> _projects;
        string _solutionPath;

        public Solution()
        {
            _projects = new List<IProject>();
        }

        string ProjectExtractRegex
        {
            get { return Resource.ResourceManager.GetString("ExtractProjectRegEx"); }
        }

        public IEnumerable<IProject> Projects
        {
            get { return _projects; }
        }

        public string Name
        {
            get { return Path.GetFileName(_solutionPath); }
        }

        public IEnumerable<IProject> NUnitProjects
        {
            get { return from project in Projects where project.TestFramework == "nunit.framework" select project; }
        }

        public IEnumerable<IProject> MSpecProjects
        {
            get { return from project in Projects where project.TestFramework == "Machine.Specifications" select project; }
        }

        public IEnumerable<IProject> MSTestProjects
        {
            get
            {
                return from project in Projects
                       where project.TestFramework == "Microsoft.VisualStudio.QualityTools.UnitTestFramework"
                       select project;
            }
        }

        public void Load(string solutionPath)
        {
            StoreSolutionPath(solutionPath);

            string solutionText = File.OpenText(_solutionPath).ReadToEnd();

            MatchCollection projectMatches = ExtractProjectMatches(solutionText);

            LoadProjectsFromMatches(projectMatches);
        }

        void LoadProjectsFromMatches(MatchCollection projectMatches)
        {
            foreach (Match projectMatch in projectMatches)
            {
                string projectPath = BuildFullProjectPathFromMatch(projectMatch);

                AddNewProjectToProjectsList(projectPath);
            }
        }

        void AddNewProjectToProjectsList(string projectPath)
        {
            var project = new Project();
            project.Load(projectPath);

            _projects.Add(project);
        }

        string BuildFullProjectPathFromMatch(Match projectMatch)
        {
            return Path.Combine(Path.GetDirectoryName(_solutionPath), projectMatch.Value);
        }

        MatchCollection ExtractProjectMatches(string solutionText)
        {
            var regex = new Regex(ProjectExtractRegex, RegexOptions.IgnoreCase);

            return regex.Matches(solutionText);
        }

        void StoreSolutionPath(string solutionPath)
        {
            _solutionPath = solutionPath;
        }

        public bool Build(Configuration configuration)
        {
            IRunnerBuildEngine engine = null;

            if (configuration.Builder == "MSBuild4")
                engine = new MSBuild4Engine();

            if (configuration.Builder == "MSBuild35")
                engine = new MSBuild35Engine();

            if (configuration.Builder == "MSBuild2")
                engine = new MSBuild2Engine();

            if (engine == null)
                throw new ConfigurationException("no build engine defined");

            return engine.BuildProjects(_projects);
        }

        public TestResults RunTests(Configuration configuration, IConsole console)
        {
            ITestCaller caller = null;
            List<TestResults> results = new List<TestResults>();
            
            if (configuration.TestRunner.Contains("MSpec"))
            {
                if(configuration.Builder == "MSBuild4")
                    caller = new MSpecTestCaller();

                IEnumerable<IProject> projects = MSpecProjects;

                if(projects.Count() > 0)
                    results.Add(caller.RunTests(MSpecProjects, Path.GetDirectoryName(_solutionPath), configuration.Builder, console));
            }

            //if (configuration.TestRunner.Contains("NUnit"))
            //{
            //    runner = new NUnitTestRunner();
            //    projects = NUnitProjects;

            //    if (projects.Count() > 0)
            //        nunitResults =  runner.RunTests(projects, Path.GetDirectoryName(_solutionPath), configuration.Builder,
            //                                        console);

            //}

            //if (configuration.TestRunner.Contains("MSTest"))
            //{
            //    runner = new MSTestRunner();
            //    projects = MSTestProjects;

            //    if (projects.Count() > 0)
            //        msTestResults = runner.RunTests(projects, Path.GetDirectoryName(_solutionPath), configuration.Builder,
            //                                        console);
            //}

            return CombineResultFiles(results);
        }

        TestResults CombineResultFiles(List<TestResults> results)
        {
            if (results.Count == 1)
                return results[0];

            


            return null;
        }
    }
}
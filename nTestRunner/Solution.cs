using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Resources;

namespace nTestRunner
{
    public class Solution
    {
        List<IProject> _projects;

        public Solution()
        {
            _projects = new List<IProject>();
        }

        string ProjectExtractRegex
        {
            get { return Resource.ResourceManager.GetString("ExtractProjectRegEx"); }
        }

        public void Load(string solutionPath)
        {
            string solutionText = File.OpenText(solutionPath).ReadToEnd();
            
            Name = Path.GetFileName(solutionPath);

            var regex = new Regex(ProjectExtractRegex, RegexOptions.IgnoreCase);

            MatchCollection matches = regex.Matches(solutionText);

            foreach (var match in matches)
            {
                var project = new Project();
                project.ProjectPath = match.ToString();
                _projects.Add(project);
            }
        }

        public IEnumerable<IProject> Projects
        {
            get { return _projects; }
        }

        public string Name { get; set; }
    }
}
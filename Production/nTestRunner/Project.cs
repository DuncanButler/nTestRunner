using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using nTestRunner.Interfaces;

namespace nTestRunner
{
    public class Project : IProject
    {
        readonly IList<string> _references;
        string _fullProjectPath;

        public Project()
        {
            _references = new List<String>();
        }

        public string ProjectName
        {
            get { return Path.GetFileName(_fullProjectPath); }
        }

        public string ProjectPath
        {
            get { return Path.GetDirectoryName(_fullProjectPath); }
        }

        public string TestFramework
        {
            get
            {
                return
                    _references.Where(
                        reference =>
                        reference == "Machine.Specifications" || reference == "nunit.framework" ||
                        reference == "Microsoft.VisualStudio.QualityTools.UnitTestFramework").FirstOrDefault();
            }
        }

        public void Load(string fullProjectPath)
        {
            StoreFullProjectPath(fullProjectPath);
        
            var projectXml = LoadProjectXml();

            var matches = ExtractReferenceMatches(projectXml);

            StoreReferences(matches);
        }

        void StoreFullProjectPath(string fullProjectPath)
        {
            _fullProjectPath = fullProjectPath;
        }

        string LoadProjectXml()
        {
            TextReader reader = new StreamReader(_fullProjectPath);

            string projectXml = reader.ReadToEnd();

            return projectXml;
        }

        static MatchCollection ExtractReferenceMatches(string projectXml)
        {
            var referencesMatch = new Regex("(?<=<Reference Include=\")[a-zA-Z.]*(?=\\,|\")", RegexOptions.IgnoreCase);

            return referencesMatch.Matches(projectXml);
        }

        void StoreReferences(MatchCollection matches)
        {
            foreach (Match match in matches)
                _references.Add(match.Value);
        }
    }
}
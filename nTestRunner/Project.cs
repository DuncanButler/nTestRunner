using System.IO;

namespace nTestRunner
{
    public class Project : IProject
    {
        public string ProjectPath { get; set; }
        public string ProjectName {get { return Path.GetFileName(ProjectPath); }}
    }
}
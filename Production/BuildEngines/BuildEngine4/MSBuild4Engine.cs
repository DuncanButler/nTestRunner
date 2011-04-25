using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Evaluation;
using nTestRunner.Interfaces;

namespace BuildEngine4
{
    public class MSBuild4Engine : IRunnerBuildEngine
    {
        public bool BuildProjects(List<IProject> projects)
        {
            var engine = new ProjectCollection();

            return
                projects.Select(project => Path.Combine(project.ProjectPath, project.ProjectName)).Select(
                    fullProjectPath => engine.LoadProject(fullProjectPath).Build()).All(success => success);
        }
    }
}
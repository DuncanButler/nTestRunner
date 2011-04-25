﻿using System.Collections.Generic;
using System.IO;
using Microsoft.Build.BuildEngine;
using nTestRunner.Interfaces;

namespace BuildEngine2
{
    public class MSBuild2Engine : IRunnerBuildEngine
    {
        public bool BuildProjects(List<IProject> projects)
        {
            var engine = new Engine();

            foreach (IProject project in projects)
            {
                string fullProjectPath = Path.Combine(project.ProjectPath, project.ProjectName);

                bool success = engine.BuildProjectFile(fullProjectPath);

                if (success == false)
                    return false;
            }

            return true;
        }
    }
}
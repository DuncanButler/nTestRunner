using System.Collections.Generic;

namespace nTestRunner.Interfaces
{
    public interface IRunnerBuildEngine
    {
        bool BuildProjects(List<IProject> projects);
    }
}
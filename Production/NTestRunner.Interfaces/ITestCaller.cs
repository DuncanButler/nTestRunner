using System.Collections.Generic;
using nTestRunner.Interfaces.ResultsSerialization;

namespace nTestRunner.Interfaces
{
    public interface ITestCaller
    {
        TestResults RunTests(IEnumerable<IProject> projects, string baseDirectory, string buildEngine, IConsole console);
    }
}
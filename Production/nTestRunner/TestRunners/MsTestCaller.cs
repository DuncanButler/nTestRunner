using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using nTestRunner.Interfaces;
using nTestRunner.Interfaces.ResultsSerialization;

namespace nTestRunner.TestRunners
{
    public class MsTestCaller : BaseTestCaller
    {
        protected override TestResults CompileResults()
        {
            // TODO: convert test results to nunit format

            return new TestResults();
        }

        protected override string TestArguments(IEnumerable<IProject> projects)
        {
            return GenerateTestAssembliyPaths(projects);
        }

        protected override string TestRunnerFullPath()
        {
            return @"C:\Program Files (x86)\Microsoft Visual Studio 9.0\Common7\IDE\mstest.exe";
        }

        protected override string BuildTestAssemblyPath(string pathBase, string fileName)
        {
            return string.Format("/testcontainer:\"{0}\"", Path.Combine(pathBase, fileName));
        }
    }
}
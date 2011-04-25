using System.Collections.Generic;
using System.IO;
using System.Linq;
using SerializationHelpers;
using nTestRunner.Interfaces;
using nTestRunner.Interfaces.ResultsSerialization;

namespace nTestRunner.TestRunners
{
    public class NUnitTestCaller : BaseTestCaller
    {
        const string ResultFileName = "TestResults.xml";

        protected override TestResults CompileResults()
        {            
            var serializer = new TheSerializer();

            using (FileStream stream = File.OpenRead(Path.Combine(BaseDirectory, ResultFileName)))
            {
                return serializer.ToTypeof<TestResults>(stream);    
            }            
        }

        protected override string TestArguments(IEnumerable<IProject> projects)
        {
            string testAssemblies = GenerateTestAssembliyPaths(projects);

            return string.Format("/xml\"{0}\"\"{1}\"", ResultFileName, testAssemblies);
        }

        protected override string BuildTestAssemblyPath(string pathBase, string fileName)
        {
            return Path.Combine(pathBase, fileName);
        }

        protected override string TestRunnerFullPath()
        {
            string[] directories = Directory.GetDirectories(PackagesDirectory);

            return (from d in directories where d.Contains("NUnit") select d).First();
        }
    }
}
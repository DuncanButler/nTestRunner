using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using MSpecCaller.Serialization;
using SerializationHelpers;
using nTestRunner.Interfaces;
using nTestRunner.Interfaces.ResultsSerialization;

namespace MSpecCaller
{
    public class MSpecTestCaller : ITestCaller
    {
        readonly string _resultsFile;
        string _currentdirectory;

        public MSpecTestCaller()
        {
            _currentdirectory = AppDomain.CurrentDomain.BaseDirectory;

            _resultsFile = string.Format("--xml \"{0}\"", Path.Combine(_currentdirectory, "mspecresults.xml"));
        }

        public TestResults RunTests(IEnumerable<IProject> projects, string baseDirectory, string buildEngine, IConsole console)
        {
            try
            {
                string processName = string.Format("{0}", Path.Combine(_currentdirectory, "mspec-clr4.exe"));

                var process = new Process
                                  {
                                      StartInfo =
                                          {
                                              FileName = processName,
                                              Arguments = BuildArguments(projects),
                                              RedirectStandardOutput = false,
                                              RedirectStandardError = true,
                                              UseShellExecute = false
                                          },

                                      EnableRaisingEvents = false
                                  };

                process.Start();
                process.WaitForExit();
                process.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return CompileResults();
        }

        TestResults CompileResults()
        {
            var mspecResults = LoadMSpecResults();

            if (mspecResults == null)
                return InconclusiveResults();

            var testRunTime = FormatTestRunTime(mspecResults);

            var results = new TestResults
                              {
                                  Errors = GetTotalFailures(mspecResults),
                                  Failures = 0,
                                  Inconclusive = GetTotalNotImplemented(mspecResults),
                                  Total = GetTotalSpecificationCount(mspecResults),
                                  Time = FormatCurrentTime(),
                                  Date = FormatCurrentDate(),
                                  Enviroument = BuildEnviroument(),
                                  CultureInfo = BuildCultureInformation()
                              };
            
            BuildTestSuiteProjectNodes(testRunTime, results, mspecResults);

            return results;
        }

        TestResults InconclusiveResults()
        {
            var results = new TestResults
                              {
                                  Errors = 0,
                                  Failures = 0,
                                  Inconclusive = 1,
                                  Total = 1,
                                  Time = FormatCurrentTime(),
                                  Date = FormatCurrentDate(),
                                  Enviroument = BuildEnviroument(),
                                  CultureInfo = BuildCultureInformation()
                              };

            return results;
        }

        static CultureInformation BuildCultureInformation()
        {
            return new CultureInformation()
                       {
                           CurrentCulture = Thread.CurrentThread.CurrentCulture.Name,
                           CurrentUiCulture = Thread.CurrentThread.CurrentUICulture.Name
                       };
        }

        static TestEnvironment BuildEnviroument()
        {
            return new TestEnvironment()
                       {
                           MachineName = Environment.MachineName,
                           OSVersion = Environment.OSVersion.VersionString,
                           Platform = Environment.OSVersion.Platform.ToString(),
                           UserDomain = Environment.UserDomainName,
                           UserName = WindowsIdentity.GetCurrent().Name,
                           ClrVersion = "4.0.30319",
                           WorkingDirectory = Directory.GetCurrentDirectory(),
                           NunitVersion = "2.5.9.10348"
                       };
        }

        static string FormatCurrentDate()
        {
            return string.Format("{0}-{1}-{2}", DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
        }

        static string FormatCurrentTime()
        {
            return string.Format("{0}:{1}:{2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        }

        static string FormatTestRunTime(MSpec mspecResults)
        {
            int milliseconds;
            int.TryParse(mspecResults.Run.Time, out milliseconds);
            var ts = new TimeSpan(0,0,0,0,milliseconds);
            string testRunTime = string.Format("{0}.{1}", ts.Seconds,ts.Milliseconds);

            return testRunTime;
        }

        MSpec LoadMSpecResults()
        {
            try
            {
                FileStream stream = File.OpenRead(Path.Combine(_currentdirectory, "mspecresults.xml"));
                var serializer = new TheSerializer();
                var mspecResults = serializer.ToTypeof<MSpec>(stream);
                return mspecResults;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        void BuildTestSuiteProjectNodes(string testRunTime, TestResults results, MSpec mspecResults)
        {
            foreach (var assembly in mspecResults.Assembly)
            {
                var assemblySuite = new TestSuite();
                assemblySuite.TestSuiteType = "Project";
                assemblySuite.Executed = true;
                assemblySuite.Time = testRunTime;

                assemblySuite.Asserts = GetAssemblySpecificationCount(assembly);

                assemblySuite.Name = Path.Combine(Path.GetDirectoryName(mspecResults.Assembly[0].Location), mspecResults.Assembly[0].Name);

                if (GetAssemblySpecificationFailures(assembly) == 0 )
                {
                    assemblySuite.Result = "Success";
                    assemblySuite.Success = true;
                }
                else
                {
                    assemblySuite.Result = "Failure";
                    assemblySuite.Success = false;
                }

                var assemblyResults = new Results();
                assemblySuite.Results = assemblyResults;

                BuildTestSuiteNameSpaceNodes(testRunTime, assemblyResults, assembly);

                results.TestSuite = assemblySuite;
            }
        }

        void BuildTestSuiteNameSpaceNodes(string testRunTime, Results assemblyResults, Assembly assembly)
        {
            foreach (var concern in assembly.Concern)
            {

                var concernSuite = new TestSuite {TestSuiteType = "NameSpace", Name = concern.Name, Executed = true};

                if (GetConsernSpecificationFailures(concern) == 0)
                {
                    concernSuite.Result = "Success";
                    concernSuite.Success = true;
                }
                else
                {
                    concernSuite.Result = "Failure";
                    concernSuite.Success = false;                        
                }

                concernSuite.Time = testRunTime;

                assemblyResults.TestSuite = concernSuite;

                var concernResults = new Results();
                concernSuite.Results = concernResults;

                BuildTestSuiteTestFixtureNodes(testRunTime, concernResults, concern);
            }
        }

        void BuildTestSuiteTestFixtureNodes(string testRunTime, Results concernResults, Concern concern)
        {
            foreach (var context in concern.Context)
            {
                var contextSuite = new TestSuite {TestSuiteType = "TestFixture", Name = concern.Name, Executed = true};

                if (GetContextSpecificationFailures(context) == 0)
                {
                    contextSuite.Result = "Success";
                    contextSuite.Success = true;
                }
                else
                {
                    contextSuite.Result = "Failure";
                    contextSuite.Success = false;
                }

                contextSuite.Time = testRunTime;

                var contextResults = new Results();
                contextSuite.Results = contextResults;
                concernResults.TestSuite = contextSuite;

                BuildTestCaseNodes(testRunTime, contextResults, context);
            }
        }

        static void BuildTestCaseNodes(string testRunTime, Results contextResults, Context context)
        {
            foreach (var specification in context.Specification)
            {
                var specResult = new TestCase {Name = specification.Name, Executed = true, Time = testRunTime};


                if(specification.Status == "passed")
                {
                    specResult.Result = "Success";
                    specResult.Success = true;
                    specResult.Asserts = 1;
                }
                else
                {
                    specResult.Result = "Failure";
                    specResult.Success = false;
                    specResult.Asserts = 0;
                }

                contextResults.TestCases.Add(specResult);
            }
        }

        int GetTotalFailures(MSpec mspecResults)
        {
            return mspecResults.Assembly.Sum(
                assembly => assembly.Concern.Sum(concern => GetConsernSpecificationFailures(concern)));
        }

        int GetConsernSpecificationFailures(Concern concern)
        {
            return concern.Context.Sum(context => GetContextSpecificationFailures(context));
        }

        int GetContextSpecificationFailures(Context context)
        {
            return (from spec in context.Specification where spec.Status == "failed" select spec).Count();
        }

        int GetAssemblySpecificationFailures(Assembly assembly)
        {
            return assembly.Concern.Sum(concern => GetConsernSpecificationFailures(concern));
        }

        int GetAssemblySpecificationCount(Assembly assembly)
        {
            return assembly.Concern.SelectMany(concern => concern.Context).Sum(
                context => (from spec in context.Specification select spec).Count());
        }

        int GetTotalSpecificationCount(MSpec mspecResults)
        {
            return
                mspecResults.Assembly.Sum(
                    assemblies =>
                    assemblies.Concern.Sum(
                        concerns =>
                        concerns.Context.Sum(
                            context =>
                            (from spec in context.Specification select spec).Count())));
        }


        int GetTotalNotImplemented(MSpec mspecResults)
        {
            return mspecResults.Assembly.Sum(
                assemblies =>
                assemblies.Concern.Sum(
                    concerns =>
                    concerns.Context.Sum(
                        context =>
                        (from spec in context.Specification where spec.Status == "not-implemented" select spec).Count())));
        }

        string BuildArguments(IEnumerable<IProject> projects)
        {
            var builder = new StringBuilder();
            builder.Append(_resultsFile);

            foreach (var project in projects)
            {
                string debugDirectory = Path.Combine(project.ProjectPath, @"bin\debug");
                string fileName = string.Format("{0}.dll", Path.GetFileNameWithoutExtension(project.ProjectName));

                builder.Append(" ");

                builder.Append(string.Format("\"{0}\"", Path.Combine(debugDirectory, fileName)));
            }

            return builder.ToString();
        }
    }
}
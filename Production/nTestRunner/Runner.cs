using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading;
using GrowlDisplay;
using SerializationHelpers;
using nTestRunner.Interfaces;
using nTestRunner.Interfaces.ResultsSerialization;

namespace nTestRunner
{
    public class Runner
    {
        readonly IConsole _console;
        readonly Configuration _configuration;
        FileSystemWatcher _watcher;
        bool _running = false;

        public Runner(string[] args, IConsole console, Configuration configuration)
        {
            _console = console;
            _configuration = configuration;

            _configuration.LoadConfigurationFrom(args);

            DisplayProgramConfiguration();

            WriteDummyResultsFile();

            SetupFileWatcher();
        }

        void DisplayProgramConfiguration()
        {
            WriteTitleAndVersionDetails();
            WriteSolutionDetails();
            WriteBuilderDetails();
            WriteTestRunnerDetails();
            WriteDisplayDetails();
        }

        void WriteDummyResultsFile()
        {
            string solutionPath = Path.GetDirectoryName(_configuration.SolutionPath);

            string testFilePath = Path.Combine(solutionPath, "TestResult.xml");

            FileStream fileStream = File.Create(testFilePath);
            fileStream.Close();
        }

        void SetupFileWatcher()
        {
            _running = true;
            _watcher = new FileSystemWatcher(Path.GetDirectoryName(_configuration.SolutionPath));

            _watcher.Changed += FileChanged;
            _watcher.Deleted += FileChanged;
            _watcher.Created += FileChanged;

            _watcher.IncludeSubdirectories = true;
            _watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.CreationTime | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            _watcher.EnableRaisingEvents = false;
        }

        void StartWatching()
        {
            _running = false;
            _watcher.EnableRaisingEvents = true;
            
            _console.WriteLine(WatcherIsOn);
        }

        void WriteTitleAndVersionDetails()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            _console.WriteLine(string.Format(TitleVersion, version.Major, version.Minor));
        }

        void WriteSolutionDetails()
        {
            var solution = _configuration.LoadSolution();

            _console.WriteLine(WatchingFiles);
            _console.WriteLine(solution.Name);

            foreach (IProject project in solution.Projects)
                _console.WriteLine(project.ProjectName);
        }

        void WriteTestRunnerDetails()
        {
            if (_configuration.TestRunner.Count == 0)
                return;

            var builder = new StringBuilder();
            builder.Append(AvailableTestRunners);
            var moreThanOneRunner = false;

            foreach (var runners in _configuration.TestRunner)
            {
                if(moreThanOneRunner)
                    builder.Append(", ");

                builder.Append(runners);

                moreThanOneRunner = true;
            }

            _console.WriteLine(builder.ToString());
        }

        void WriteDisplayDetails()
        {
            if (_configuration.RunnerDisplay.Count == 0)
                return;

            var builder = new StringBuilder();
            builder.Append(AvailableDisplays);
            var moreThanOneRunner = false;

            foreach (var display in _configuration.RunnerDisplay)
            {
                if (moreThanOneRunner)
                    builder.Append(", ");

                builder.Append(display);
                moreThanOneRunner = true;
            }

            _console.WriteLine(builder.ToString());
        }

        void WriteBuilderDetails()
        {
            _console.WriteLine(string.Format("{0} {1}",AvailableBuilders,_configuration.Builder));
        }

        void FileChanged(object sender, FileSystemEventArgs e)
        {
            if (_running)
                return;

            StopWatching();

            var solution = GenerateSolution();

            bool built = BuildSolution(solution);

            TestResults testResults = null;
            
            if (built)
                testResults = RunTests(solution);
            
            if(testResults == null)
                testResults = BuildFailedResult();

            Display(testResults, _configuration);

            StartWatching();            
        }

        TestResults BuildFailedResult()
        {
            var results = new TestResults();
            results.Errors = 0;
            results.Failures = 0;
            results.Inconclusive = 1;
            results.Total = 1;
            results.Time = FormatCurrentTime();
            results.Date = FormatCurrentDate();
            results.Enviroument = BuildEnviroument();
            results.CultureInfo = BuildCultureInformation();

            return results;
        }

        static string FormatCurrentDate()
        {
            return string.Format("{0}-{1}-{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        }

        static string FormatCurrentTime()
        {
            return string.Format("{0}:{1}:{2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
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

        bool BuildSolution(Solution solution)
        {
            _console.WriteLine(string.Format("{0} {1}", Building, _configuration.Builder));

            return solution.Build(_configuration);
        }

        Solution GenerateSolution()
        {
            return _configuration.LoadSolution();
        }

        TestResults RunTests(Solution solution)
        {
            string testRunners = GetTestRunners(_configuration.TestRunner);
            _console.WriteLine(string.Format("{0} {1}",TestRunner,testRunners));

            var results = solution.RunTests(_configuration, _console);

            return results;
        }

        string GetTestRunners(IEnumerable<string> testRunner)
        {
            var builder = new StringBuilder();

            foreach (var runner in testRunner)
            {
                if (builder.Length > 0)
                    builder.Append(", ");

                builder.Append(runner);
            }

            return builder.ToString();
        }

        void StopWatching()
        {
            _watcher.EnableRaisingEvents = false;
            _running = true;
        }

        void Display(TestResults results, Configuration configuration)
        {
            if (configuration.RunnerDisplay.Contains("File") || configuration.RunnerDisplay.Contains("Beacons"))
                SerializeResultsToFile(results);

            if (configuration.RunnerDisplay.Contains("Growl"))
            {
                IGrowlWrapper growl = new GrowlWrapper();
                var growlDisplay = new GrowlDisplay.GrowlDisplay(growl);

                growlDisplay.DisplayNotification(results);
            }
        }

        void SerializeResultsToFile(TestResults results)
        {
            var serializer = new TheSerializer();

            string xml = serializer.ToXml(results);

            string solutionPath = Path.GetDirectoryName(_configuration.SolutionPath);

            string testFilePath = Path.Combine(solutionPath, "TestResult.xml");

            File.WriteAllText(testFilePath, xml);

        }

        static string WatchingFiles { get { return Resource.ResourceManager.GetString("WatchingFiles"); } }
        static string TitleVersion { get { return Resource.ResourceManager.GetString("TitleVersion"); } }
        static string TestRunner {get { return Resource.ResourceManager.GetString("TestRunner");}}
        static string DisplayRunner {get { return Resource.ResourceManager.GetString("DisplayRunner");}}
        static string WatcherIsOn { get { return Resource.ResourceManager.GetString("WatcherIsOn"); } }
        static string Building { get { return Resource.ResourceManager.GetString("Building"); } }
        static string AvailableTestRunners { get { return Resource.ResourceManager.GetString("AvailableTestRunners"); } }
        static string AvailableDisplays { get { return Resource.ResourceManager.GetString("AvailableDisplays"); } }
        static string AvailableBuilders { get { return Resource.ResourceManager.GetString("AvailableBuilders"); } }

        public void Start()
        {
            StartWatching();
        }
    }
}
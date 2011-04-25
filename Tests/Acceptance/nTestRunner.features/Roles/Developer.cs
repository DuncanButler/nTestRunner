using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using nTestRunner.Interfaces;
using SpecSalad;

namespace nTestRunner.features.Roles
{
    public class Developer : ApplicationRole
    {
        Runner _runner;
        IConsole _testConsole;
        Configuration _configuration;

        string GetDirectory
        {
            get { return Directory.GetCurrentDirectory(); }
        }

        public void start_the_test_runner(string args)
        {
            string[] argArray = args.Split(',');

            for (int i = 0; i < argArray.Length; i++)
            {
                if (argArray[i] == "-Path")
                {
                    argArray[i + 1] = Path.Combine(GetDirectory, @"TestData\TestSolution.sln");
                    break;
                }
            }

            _testConsole = new TestConsole();

            _configuration = new Configuration();

            _runner = new Runner(argArray, _testConsole, _configuration);            
        }

        public void run_the_test_runner()
        {
            string solutionPath = Path.Combine(GetDirectory, @"TestData\TestRunning.sln");

            var args = new string[]{"-Path",solutionPath};

            _testConsole = new TestConsole();
            _configuration = new Configuration();

            _runner = new Runner(args, _testConsole, _configuration);
            _runner.Start();
        }

        public IEnumerable<String> look_at_console()
        {
            return ((TestConsole)_testConsole).Messages;
        }

        public string Check_for_file(string fileName)
        {
            string lookingForFile = Path.Combine(Path.GetDirectoryName(_configuration.SolutionPath), fileName);

            if (File.Exists(lookingForFile))
                return Path.GetFileName(lookingForFile);
            else
                return string.Format("File not found {0}", lookingForFile);
        }

        public void Edit_and_save_a_source_file()
        {           
            string fullFilePath = Path.Combine(Path.Combine(Path.GetDirectoryName(_configuration.SolutionPath), "SpecProject"),"TestFile.txt");

            FileStream stream;
            if (File.Exists(fullFilePath) == false)
            {
                stream = File.Create(fullFilePath);
            }
            else
            {
                stream = File.OpenWrite(fullFilePath);
            }
            
            var  encoding=new UTF8Encoding();
            byte[] buffer = encoding.GetBytes(string.Format("Hello World {0}",DateTime.Now));
            
            stream.Write(buffer,0, buffer.Length);
            stream.Flush();
            stream.Close();
        }
    }

    public class TestConsole : IConsole
    {
        List<String> messages = new List<string>();

        public void WriteLine(string message)
        {
            messages.Add(message);
        }

        public void Write(string message)
        {
            messages.Add(message);
        }

        public IEnumerable<string> Messages { get { return messages; } }
    }
}
using System;
using nTestRunner.Interfaces;

namespace nTestRunner
{
    public class Program
    {
        static void Main(string[] args)
        {
            IConsole console = new DisplayConsole();
            var configuration = new Configuration();

            var runner = new Runner(args,console,configuration);

            runner.Start();
            while (true)
            {                
            }
        }
    }

    public class DisplayConsole : IConsole
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void Write(string message)
        {
            Console.Write(message);
        }
    }
}
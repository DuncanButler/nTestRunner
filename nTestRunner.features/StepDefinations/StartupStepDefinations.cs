using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace nTestRunner.features.StepDefinations
{
    [Binding]
    public class StartupStepDefinations
    {
        TestConsole _console;
        Runner _runner;

        [Given(@"that the program is not running")]
        public void GivenThatTheProgramIsNotRunning()
        {
            _runner = null;
        }

        [When(@"the program is run with arguments '(.*)'")]
        public void WhenTheProgramIsRunWith(string arguments)
        {
            var args = arguments.Split(' ');

            _console = new TestConsole();

            _runner = new Runner(args, _console);
        }

        [Then(@"the user sees text containing '(.*)'")]
        public void ThenTheUserSeesTextContaining(string expectedText)
        {
            Assert.Contains(expectedText, _console.Output);
        }
    }

    public class TestConsole : IConsole
    {
        List<string> _messages;

        public TestConsole()
        {
            _messages = new List<string>();
        }

        public ICollection Output { get { return _messages; }}

        public void Write(string message)
        {
            _messages.Add(message);
        }
    }
}
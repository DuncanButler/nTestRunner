using System;
using SpecSalad;

namespace nTestRunner.features.Tasks
{
    public class SeeTheResultsFile : ApplicationTask
    {
        public override object Perform_Task()
        {
            return Role.Check_for_file("TestResult.xml");
        }
    }
}
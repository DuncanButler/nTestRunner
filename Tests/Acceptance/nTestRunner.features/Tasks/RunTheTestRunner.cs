using System;
using System.IO;
using SpecSalad;

namespace nTestRunner.features.Tasks
{
    public class RunTheTestRunner : ApplicationTask
    {
        public override object Perform_Task()
        {
            Role.run_the_test_runner();

            return null;
        }
    }
}
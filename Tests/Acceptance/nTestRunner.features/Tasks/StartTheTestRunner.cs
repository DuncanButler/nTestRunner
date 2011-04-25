using System;
using SpecSalad;

namespace nTestRunner.features.Tasks
{
    public class StartTheTestRunner : ApplicationTask
    {
        public override object Perform_Task()
        {
            string args = Details.Value_Of("with_arguments");
            
            Role.start_the_test_runner(args);

            return null;
        }
    }
}
using System;
using SpecSalad;

namespace nTestRunner.features.Tasks
{
    public class DisplayText : ApplicationTask
    {
        public override object Perform_Task()
        {
            return Role.look_at_console();
        }
    }
}
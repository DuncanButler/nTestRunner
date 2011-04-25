using System;
using SpecSalad;

namespace nTestRunner.features.Tasks
{
    public class ChangeAFile : ApplicationTask
    {
        public override object Perform_Task()
        {
            Role.Edit_and_save_a_source_file();

            return null;
        }
    }
}
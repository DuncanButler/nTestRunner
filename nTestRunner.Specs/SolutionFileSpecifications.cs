using Machine.Specifications;

namespace nTestRunner.Specs
{
    [Subject("$Subject of Specification$")]
    public class Loads_the_solution_file_into_an_object
    {

        It loads_the_solution_file_successfully =
            () =>
                {                    
                    _solutionFile.ShouldNotBeNull();
                };

        static SolutionFile _solutionFile;
    }
}
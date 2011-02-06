using System.Linq;
using Machine.Specifications;
using System.IO;

namespace nTestRunner.Spec
{
    [Subject("Solution File")]
    public class when_the_solution_file_is_loaded
    {
        Establish context =
            () =>
                {
                    string currentDirectory = Directory.GetCurrentDirectory();
                    _solutionPath = Path.Combine(currentDirectory, "TestSolution.sln");

                    _solution = new Solution();
                };

        Because of =
            () =>
                {
                    _solution.Load(_solutionPath);              
                };

        It contains_the_name_of_the_solution =
            () => _solution.Name.ShouldEqual("TestSolution.sln");

        It contains_two_project_files =
            () => _solution.Projects.Count().ShouldEqual(2);

        It first_project_file_is_TestProject1 =
            () => (from p in _solution.Projects where p.ProjectPath == "TestProject1.csproj" select p).FirstOrDefault().ShouldNotBeNull();

        It second_project_file_is_TestProject2 =
            () => (from p in _solution.Projects where p.ProjectPath == "TestProject2.csproj" select p).FirstOrDefault().ShouldNotBeNull();

        It first_project_file_name_is_TestProject1 =
            () => (from p in _solution.Projects where p.ProjectName == "TestProject1.csproj" select p).FirstOrDefault().ShouldNotBeNull();

        It second_project_file_name_is_TestProject2 =
            () => (from p in _solution.Projects where p.ProjectName == "TestProject2.csproj" select p).FirstOrDefault().ShouldNotBeNull();

        static Solution _solution;
        static string _solutionPath;
    }
}
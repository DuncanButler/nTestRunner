using System;
using System.Linq;
using Machine.Specifications;
using System.IO;
using nTestRunner.Interfaces;

namespace nTestRunner.Spec
{
    [Subject("Solution File")]
    public class when_the_solution_file_is_loaded : SolutionFileContext
    {
        Establish context =
            () =>
                {
                    _solutionPath = Path.Combine(_currentDirectory, @"TestSolutions\TestSolution.sln");                    
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
            () => (from p in _solution.Projects where p.ProjectPath.Contains(@"TestSolutions\TestProjects") && p.ProjectName == "TestProject1.csproj" select p).FirstOrDefault().ShouldNotBeNull();

        It second_project_file_is_TestProject2 =
            () => (from p in _solution.Projects where p.ProjectPath.Contains(@"TestSolutions\TestProjects") && p.ProjectName == "TestProject2.csproj" select p).FirstOrDefault().ShouldNotBeNull();

        It first_project_file_name_is_TestProject1 =
            () => (from p in _solution.Projects where p.ProjectName == "TestProject1.csproj" select p).FirstOrDefault().ShouldNotBeNull();

        It second_project_file_name_is_TestProject2 =
            () => (from p in _solution.Projects where p.ProjectName == "TestProject2.csproj" select p).FirstOrDefault().ShouldNotBeNull();
    }

    [Subject("Solution")]
    public class given_the_solution_has_no_test_projects : SolutionFileContext
    {
        Establish context =
            () =>
                {
                    _solutionPath = Path.Combine(_currentDirectory, @"TestSolutions\TestSolution.sln");                    
                };

        Because of =
            () =>
                {
                    _solution.Load(_solutionPath);
                };

        It the_nunit_projects_will_be_empty =
            () => _solution.NUnitProjects.Count().ShouldEqual(0);

        It the_mspec_projects_will_be_empty = 
            () => _solution.MSpecProjects.Count().ShouldEqual(0);

        It the_mstest_projects_will_be_empty =
            () => _solution.MSTestProjects.Count().ShouldEqual(0);
    }

    [Subject("Solution")]
    public class given_the_solution_has_machine_specfication_projects : SolutionFileContext
    {
        Establish context =
            () =>
                {
                    _solutionPath = Path.Combine(_currentDirectory, @"TestSolutions\TestSolution_MSpecProj.sln");
                };

        Because of =
            () =>
            {
                _solution.Load(_solutionPath);
            };

        It the_mspec_projects_will_have_a_project =
            () => _solution.MSpecProjects.Count().ShouldEqual(1);

        It the_nunit_projects_will_be_empty =
            () => _solution.NUnitProjects.Count().ShouldEqual(0);

        It the_mstest_projects_will_be_empty =
            () => _solution.MSTestProjects.Count().ShouldEqual(0);
    }

    //[Subject("Solution")]
    //public class given_the_solution_has_nunit_test_projects : SolutionFileContext
    //{
    //    Establish context =
    //        () =>
    //            {
    //                _solutionPath = Path.Combine(_currentDirectory, @"TestSolutions\TestSolution_NUnitProj.sln");
    //            };

    //    Because of =
    //        () =>
    //            {
    //                _solution.Load(_solutionPath);
    //            };

    //    It the_nunit_projects_will_have_a_project =
    //        () => _solution.NUnitProjects.Count().ShouldEqual(1);

    //    It the_mspec_projects_will_be_empty =
    //        ()=> _solution.MSpecProjects.Count().ShouldEqual(0);

    //    It the_mstest_projects_will_be_empty =
    //        () => _solution.MSTestProjects.Count().ShouldEqual(0);
    //}

    //[Subject("Solution")]
    //public class given_the_solution_has_mstest_test_projects : SolutionFileContext
    //{
    //    Establish context =
    //        () =>
    //            {
    //                _solutionPath = Path.Combine(_currentDirectory, @"TestSolutions\TestSolution_MSTestProj.sln");
    //            };

    //    Because of =
    //        () =>
    //            {
    //                _solution.Load(_solutionPath);
    //            };

    //    It the_mstest_projects_will_have_a_project =
    //        () => _solution.MSTestProjects.Count().ShouldEqual(1);

    //    It the_nunit_projects_will_be_empty =
    //        () => _solution.NUnitProjects.Count().ShouldEqual(0);

    //    It the_mspec_projects_will_be_empty =
    //        () => _solution.MSpecProjects.Count().ShouldEqual(0);
    //}

    public class SolutionFileContext
    {
        Establish context =
            () =>
                {
                    _currentDirectory = Directory.GetCurrentDirectory();
                    _solution = new Solution();
                };
        
        protected static Solution _solution;
        protected static string _solutionPath;
        protected static string _currentDirectory;
    }
}
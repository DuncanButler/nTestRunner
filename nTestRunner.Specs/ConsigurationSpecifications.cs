using Machine.Specifications;

namespace nTestRunner.Specs
{
    [Subject("nTestRunner Configuration")]
    public class Creating_configuration_with_default_values
    {
        Because of =
            () =>
                {
                    _configuration = new Configuration(new string[]{});
                };

        It sets_the_specification_path_to_two_up_from_where_it_is_run =
            () => _configuration.SolutionPath.ShouldContain(@"\nTestRunner\nTestRunner.Specs\nTestRunner.Spec.csproj");

        It sets_the_default_test_runner_to_blank =
            () => _configuration.TestRunner.ShouldBeEmpty();

        It sets_the_display_to_use_to_empty =
            () => _configuration.ResultDisplay.ShouldBeEmpty();
	
	
        static Configuration _configuration;
    }

    [Subject("nTestRunner Configuration")]
    public class Creating_configuration_with_user_values_long_version
    {
        Establish context =
            () =>
                {
                    _args = new string[] {"-Path","\"C:\\TestFilePath\\testSolution.sln\"", "-Test", "MSpec", "-Display", "Growl"};
                };

        Because of =
            () =>
                {
                    _configuration = new Configuration(_args);
                };

        It sets_the_specification_path_to_the_users_entered_path =
            () => _configuration.SolutionPath.ShouldContain(@"C:\TestFilePath\testSolution.sln");

        It sets_the_test_runner_to_the_users_test_runner =
            () => _configuration.TestRunner.ShouldEqual("MSpec");

        It sets_the_display_to_the_users_selected_display =
            () => _configuration.ResultDisplay.ShouldEqual("Growl");

        static Configuration _configuration;
        static string[] _args;
    }

    [Subject("nTestRunner Configuration")]
    public class Creating_configuration_with_user_values_short_values
    {
        Establish context =
            () =>
                {
                    _args = new string[] { "-P", "\"C:\\TestFilePath\\testSolution.sln\"", "-T", "MSpec", "-D", "Growl" };
                };

        Because of =
            () =>
                {
                    _configuration = new Configuration(_args);
                };
        
        It sets_the_specification_path_to_the_users_entered_path =
            () => _configuration.SolutionPath.ShouldContain(@"C:\TestFilePath\testSolution.sln");

        It sets_the_test_runner_to_the_users_entered_test_runner =
            () => _configuration.TestRunner.ShouldEqual("MSpec");

        It sets_the_display_to_the_users_entered_runner_display =
            () => _configuration.ResultDisplay.ShouldEqual("Growl");

        static string[] _args;
        static Configuration _configuration;
    }
}
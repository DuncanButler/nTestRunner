using System;
using System.Linq;
using Machine.Specifications;

namespace nTestRunner.Spec
{
    [Subject("Configuration")]
    public class Given_a_default_configuration : ConfigurationContext
    {
        Establish context =
            () =>
                {
                    _args = null;                    
                };

        Because of =
            () =>
                {
                    _configuration.LoadConfigurationFrom(_args);
                };

        It sets_the_solution_path_to_the_found_path =
            () => _configuration.SolutionPath.ShouldContain(@"\nTestRunner\nTestRunner.sln");

        It sets_a_single_default_runner_display =
            () => _configuration.RunnerDisplay.Count.ShouldEqual(1);

        It sets_the_display_mode_to_the_windows_form =
            () => _configuration.RunnerDisplay[0].ShouldEqual("Form");

        It sets_multiple_test_runners =
            () => _configuration.TestRunner.Count.ShouldEqual(3);

        It sets_the_test_runner_to_all_available_test_runners_including_mspec =
            () => _configuration.TestRunner.ShouldContain(new string[] {"MSpec"});

        It sets_the_test_runner_to_all_available_test_runners_including_nunit =
            () => _configuration.TestRunner.ShouldContain(new string[] {"NUnit"});

        It sets_the_test_runner_to_all_available_test_runners_including_mstest =
            () => _configuration.TestRunner.ShouldContain(new string[] {"MSTest"});

        It sets_the_default_builder =
            () => _configuration.Builder.ShouldEqual("MSBuild4");
    }
    
    [Subject("Configuration")]
    public class Given_an_empty_argument_assignments : ConfigurationContext
    {
        Establish context =
            () =>
                {
                    _args = new string[]{""};
                };

        Because of =
            () =>
                {
                    _configuration.LoadConfigurationFrom(_args);
                };

        It sets_the_solution_path_to_the_found_path =
            () =>
            _configuration.SolutionPath.ShouldContain(
                @"\nTestRunner\nTestRunner.sln");

        It sets_a_single_runner_display =
            () => _configuration.RunnerDisplay.Count.ShouldEqual(1);

        It sets_the_runner_display_to_form =
            () => _configuration.RunnerDisplay[0].ShouldEqual("Form");

        It sets_the_test_runner_to_all_available_test_runners =
            () => _configuration.TestRunner.Count.ShouldEqual(3);

        It sets_the_test_runner_to_all_available_test_runners_including_mspec =
            () => _configuration.TestRunner.ShouldContain(new string[] { "MSpec" });

        It sets_the_test_runner_to_all_available_test_runners_including_nunit =
            () => _configuration.TestRunner.ShouldContain(new string[] { "NUnit" });

        It sets_the_test_runner_to_all_available_test_runners_including_mstest =
            () => _configuration.TestRunner.ShouldContain(new string[] { "MSTest" });

        It sets_the_default_builder =
            () => _configuration.Builder.ShouldEqual("MSBuild4");
    }

    [Subject("Configuration")]
    public class Given_the_short_version_of_the_arguments_assignments : ConfigurationContext
    {   
        Establish context =
            () =>
                {
                    _args = new string[]{"-P",@"C:\Users\Duncan\Documents\My Dropbox\Dropbox\nTestRunner\nTestRunner.sln","-D", "Growl", "-T", "MSpec"};
                };

        Because of =
            () =>
            {
                _configuration.LoadConfigurationFrom(_args);
            };

        It sets_the_solution_path_to_the_passed_value = 
            () => _configuration.SolutionPath.ShouldEqual(@"C:\Users\Duncan\Documents\My Dropbox\Dropbox\nTestRunner\nTestRunner.sln");

        It sets_the_display_to_the_requirested_single_value =
            () => _configuration.RunnerDisplay.Count.ShouldEqual(1);

        It sets_the_display_mode_to_the_passed_value =
            () => _configuration.RunnerDisplay.First().ShouldEqual("Growl");

        It sets_the_test_runner_to_the_required_single_value =
            () => _configuration.TestRunner.Count.ShouldEqual(1);
    
        It sets_the_test_runner_to_the_passed_value =
            () => _configuration.TestRunner.First().ShouldContain("MSpec");
    }

    [Subject("Configuration")]
    public class Given_the_long_version_of_the_arguments_assignments : ConfigurationContext
    {
        Establish context =
            () =>
            {
                _args = new string[] { "-Path", @"C:\Users\Duncan\Documents\My Dropbox\Dropbox\nTestRunner\nTestRunner.sln", "-Display", "Growl", "-Test", "MSpec" };
            };

        Because of =
            () =>
            {
                _configuration.LoadConfigurationFrom(_args);
            };

        It sets_the_solution_path_to_the_passed_value =
            () => _configuration.SolutionPath.ShouldEqual(@"C:\Users\Duncan\Documents\My Dropbox\Dropbox\nTestRunner\nTestRunner.sln");

        It sets_the_display_mode_to_the_passed_value =
            () => _configuration.RunnerDisplay.ShouldContain("Growl");

        It sets_the_test_runner_to_the_passed_value =
            () => _configuration.TestRunner.ShouldContain("MSpec");
    }

    [Subject("Configuration")]
    public class Given_an_assignment_to_more_than_one_value : ConfigurationContext
    {
        Establish context =
            () =>
            {
                _args = new string[] { "-Display", "Growl", "-Test", "MSpec", "-Display", "DefaultForm","-Test", "NUnit", "-Test", "MSTest" };
            };

        Because of =
            () =>
            {
                _configuration.LoadConfigurationFrom(_args);
            };

        It sets_the_display_mode_to_entered_values_values =
            () => _configuration.RunnerDisplay.ToList().Count.ShouldEqual(2);

        It the_display_items_include_the_user_entered_growl =
            () => _configuration.RunnerDisplay.ShouldContain("Growl");

        It the_display_items_include_the_user_entered_default_form =
            () => _configuration.RunnerDisplay.ShouldContain("DefaultForm");

        It sets_the_test_runner_to_the_passed_values =
            () => _configuration.TestRunner.ToList().Count.ShouldEqual(3);

        It the_test_runner_includes_user_entered_item_mspec =
            () => _configuration.TestRunner.ShouldContain("MSpec");

        It the_test_runner_includes_user_entered_items_nunit =
            () => _configuration.TestRunner.ShouldContain("NUnit");

        It the_test_runner_includes_user_entered_items_mstest =
            () => _configuration.TestRunner.ShouldContain("MSTest");
    }

    [Subject("Configuration")]
    public class Given_arguments_with_both_long_and_short_members : ConfigurationContext
    {
        Establish context =
            () =>
            {
                _args = new string[] { "-T", "MSpec", "-Test", "NUnit", "-T", "MSTest" };
            };

        Because of =
            () =>
            {
                _configuration.LoadConfigurationFrom(_args);
            };

        It sets_the_test_runner_to_the_passed_values =
            () => _configuration.TestRunner.ToList().Count.ShouldEqual(3);

        It the_test_runner_includes_user_entered_item_mspec =
            () => _configuration.TestRunner.ShouldContain("MSpec");

        It the_test_runner_includes_user_entered_items_nunit =
            () => _configuration.TestRunner.ShouldContain("NUnit");

        It the_test_runner_includes_user_entered_items_mstest =
            () => _configuration.TestRunner.ShouldContain("MSTest");        
    }

    [Subject("Configuration")]
    public class Invalid_arguments_throw_exception : ConfigurationContext
    {
        Establish context =
            () =>
            {
                _args = new string[] { "-T", "MSpec", "-Test", "-T", "MSTest" };
            };

        Because of =
            () =>
            {
                _exception = Catch.Exception(() => _configuration.LoadConfigurationFrom(_args));
            };

        It exception_should_be_of_type_argument_exception =
            () => _exception.ShouldBeOfType<ArgumentException>();

        It exception_message_should_be_Arguments_Invlid =
            () => _exception.Message.ShouldEqual("Arguments invalid");
    }

    public class ConfigurationContext
    {
        Establish context =
            () =>
                {
                    _configuration = new Configuration();
                };

        protected static Configuration _configuration;
        protected static string[] _args;
        protected static Exception _exception;
    }
}
using System;
using Machine.Specifications;
using SerializationHelpers;
using nTestRunner.Interfaces.ResultsSerialization;

namespace nTestRunner.Spec
{
    [Subject("Serialize Test Results")]
    public class Creating_xml_from_results_object_test_results_node : SerialiseResultsContext
    {
        Because of =
            () =>
                {
                    _createdXml = _testSerializer.ToXml(_testResults);
                };

        It contains_the_test_node =
            () => _createdXml.ShouldContain("<test-results");

        It contains_the_name_attribute =
            () => _createdXml.ShouldContain("name=\"c:\\nTestRunner.features.nunit\\\"");

        It contains_the_total_attribute =
            () => _createdXml.ShouldContain("total=\"8\"");

        It contains_the_errors_attribute =
            () => _createdXml.ShouldContain("errors=\"0\"");

        It contains_the_failures_attribute =
            () => _createdXml.ShouldContain("failures=\"1\"");

        It contains_the_not_run_attribute =
            () => _createdXml.ShouldContain("not-run=\"0\"");

        It contains_the_inconclsive_attribute =
            () => _createdXml.ShouldContain("inconclusive=\"3\"");

        It contains_the_ignored_attribute =
            () => _createdXml.ShouldContain("ignored=\"0\"");

        It contains_the_skipped_attribute =
            () => _createdXml.ShouldContain("skipped=\"0\"");

        It contains_the_invalid_attribute =
            () => _createdXml.ShouldContain("invalid=\"0\"");

        It contains_the_date_attribute =
            () => _createdXml.ShouldContain(string.Format("date=\"{0}\"", _testDate));

        It contains_the_time_attribute =
            () => _createdXml.ShouldContain(string.Format("time=\"{0}\"", _testTime));

        It contains_the_environment_node =
            () => _createdXml.ShouldContain("<environment ");

        It contains_the_culture_infomation_node =
            () => _createdXml.ShouldContain("<culture-info ");

        It contains_the_test_suite_node =
            () => _createdXml.ShouldContain("<test-suite");
    }

    [Subject("Serialize Environment")]
    public class Creating_xml_from_results_object_enviroument_node : SerialiseResultsContext
    {
        Because of =
            () =>
                {
                    _createdXml = _testSerializer.ToXml(_enviroument);
                };

        It contains_the_nunit_version =
            () => _createdXml.ShouldContain("nunit-version=\"2\"");

        It contains_the_clr_version =
            () => _createdXml.ShouldContain("clr-version=\"4\"");

        It contains_the_os_version =
            () => _createdXml.ShouldContain("os-version=\"Windows\"");

        It contains_the_platform_name =
            () => _createdXml.ShouldContain("platform=\"Win32NT\"");

        It contains_the_working_directory_name =
            () => _createdXml.ShouldContain("cwd=\"C:\\Here\"");

        It contains_the_machine_name =
            () => _createdXml.ShouldContain("machine-name=\"Test-Machine\"");

        It contains_the_user_name =
            () => _createdXml.ShouldContain("user=\"Duncan\"");

        It contains_the_user_domain_name =
            () => _createdXml.ShouldContain("user-domain=\"Tests\"");
    }

    [Subject("Serialize Culture Information")]
    public class Creating_xml_from_results_object_culture_information_node : SerialiseResultsContext
    {
        Because of =
            () =>
                {
                    _createdXml = _testSerializer.ToXml(_cultureInformation);
                };

        It contains_the_current_culture =
            () => _createdXml.ShouldContain("current-culture=\"en-GB\"");

        It contains_the_current_ui_culture =
            () => _createdXml.ShouldContain("current-uiculture=\"en-US\"");
    }

    [Subject("Serialize Test Suite")]
    public class Creating_xml_from_results_object_test_suite_node : SerialiseResultsContext
    {
        Because of =
            () =>
                {
                    _createdXml = _testSerializer.ToXml(_testSuite);
                };

        It contains_the_test_suite_type =
            () => _createdXml.ShouldContain("type=\"Project\"");

        It contains_the_test_suite_name =
            () => _createdXml.ShouldContain("name=\"C:\\here\\name\"");

        It contains_the_executed_state_of_the_test_suite =
            () => _createdXml.ShouldContain("executed=\"true\"");

        It contains_the_result_state_of_the_test_suite =
            () => _createdXml.ShouldContain("result=\"success\"");

        It contains_the_success_state_of_the_test_suite =
            () => _createdXml.ShouldContain("success=\"true\"");

        It contains_the_time_taken_for_the_test_suite =
            () => _createdXml.ShouldContain(string.Format("time=\"{0}\"", _testTime.ToString()));

        It contains_the_number_of_asserts_within_the_test_suite =
            () => _createdXml.ShouldContain("asserts=\"1\"");

        It contains_the_results_node =
            () => _createdXml.ShouldContain("<results>");
    }

    [Subject("Serialize Results")]
    public class Creating_xml_from_results_object_results_node : SerialiseResultsContext
    {
        Because of =
            () =>
                {
                    _createdXml = _testSerializer.ToXml(_results);
                };

        It contains_a_test_suite_node =
            () => _createdXml.ShouldContain("<test-suite ");

        It contains_a_test_case_node =
            () => _createdXml.ShouldContain("<test-case ");
    }

    [Subject("Serialize Test Case")]
    public class Creating_xml_from_results_object_test_case_node : SerialiseResultsContext
    {
        Because of =
            () =>
                {
                    _createdXml = _testSerializer.ToXml(_testCase);
                };

        It contains_the_name_of_the_test_case =
            () => _createdXml.ShouldContain("name=\"testcase.tests\"");

        It contains_the_execuation_state_of_the_test_case =
            () => _createdXml.ShouldContain("executed=\"true\"");

        It contains_the_result_state_of_the_test_case =
            () => _createdXml.ShouldContain("result=\"success\"");

        It contains_the_success_flag =
            () => _createdXml.ShouldContain("success=\"true\"");

        It contains_the_time_taken_for_the_test_case =
            () => _createdXml.ShouldContain(string.Format("time=\"{0}\"", _testTime));

        It contains_the_number_of_asserts_within_the_test_case =
            () => _createdXml.ShouldContain("asserts=\"1\"");
    }

    public class SerialiseResultsContext
    {
        Establish context =
            () =>
                {
                    _testDate = DateTime.Now.Date;
                    _testTime = DateTime.Now.TimeOfDay;

                    _testSerializer = new TheSerializer();

                    _enviroument = new TestEnvironment
                                       {
                                           NunitVersion = "2",
                                           ClrVersion = "4",
                                           OSVersion = "Windows",
                                           Platform = "Win32NT",
                                           WorkingDirectory = "C:\\Here",
                                           MachineName = "Test-Machine",
                                           UserName = "Duncan",
                                           UserDomain = "Tests"
                                       };

                    _cultureInformation = new CultureInformation
                                              {
                                                  CurrentCulture = "en-GB",
                                                  CurrentUiCulture = "en-US"
                                              };

                    _testCase = new TestCase
                                    {
                                        Name = "testcase.tests",
                                        Executed = true,
                                        Result = "success",
                                        Success = true,
                                        Time = _testTime.ToString(),
                                        Asserts = 1
                                    };

                    _testSuite = new TestSuite
                                     {
                                         
                                         TestSuiteType = "Project",                                         
                                         Name = "C:\\here\\name",
                                         Executed = true,
                                         Result = "success",
                                         Success = true,
                                         Time = _testTime.ToString(),
                                         Asserts = 1,                                         
                                     };

                    _testSuite.Results =_results;

                    _results = new Results
                                   {                                       
                                       TestSuite = _testSuite
                                   };
                    _results.TestCases.Add(_testCase);

                    _testResults = new TestResults
                                       {
                                           Name = @"c:\nTestRunner.features.nunit\",
                                           Total = 8,
                                           Errors = 0,
                                           Failures = 1,
                                           TestNo = 0,
                                           Inconclusive = 3,
                                           Ignored = 0,
                                           Skipped = 0,
                                           Invalid = 0,
                                           Date = _testDate.ToString(),
                                           Time = _testTime.ToString(),
                                           Enviroument = _enviroument,
                                           CultureInfo = _cultureInformation,
                                           TestSuite = _testSuite
                                       };
                };

        protected static TheSerializer _testSerializer;
        protected static string _createdXml;
        protected static TestResults _testResults;
        protected static DateTime _testDate;
        protected static TimeSpan _testTime;
        protected static TestEnvironment _enviroument;
        protected static CultureInformation _cultureInformation;
        protected static TestSuite _testSuite;
        protected static Results _results;
        protected static TestCase _testCase;
    }
}
using System;
using Machine.Specifications;

namespace nTestRunner.Spec
{
    [Subject("Serialize Results")]
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
            ()=> _createdXml.ShouldContain("skipped=\"0\"");

        It contains_the_invalid_attribute =
            () => _createdXml.ShouldContain("invalid=\"0\"");

        It contains_the_date_attribute =
            () => _createdXml.ShouldContain(string.Format("date=\"{0}\"", _testDate));

        It contains_the_time_attribute =
            () => _createdXml.ShouldContain(string.Format("time=\"{0}\"", _testTime));        
    }

    [Subject("Serialize Results")]
    public class Creating_xml_from_results_object_enviroument_node : SerialiseResultsContext
    {
        Because of =
            () =>
                {
                    _createdXml = _testSerializer.ToXml(_testResults);
                };

        It contains_the_environment_node =
            () => _createdXml.ShouldContain("<environment ");
    }

    public class SerialiseResultsContext
    {
        Establish context =
            () =>
            {
                _testDate = DateTime.Now.Date;
                _testTime = DateTime.Now.TimeOfDay;

                _testSerializer = new TestSerializer();
                _testResults = new TestResults();
                _testResults.Name = @"c:\nTestRunner.features.nunit\";
                _testResults.Total = 8;
                _testResults.Errors = 0;
                _testResults.Failures = 1;
                _testResults.TestNo = 0;
                _testResults.Inconclusive = 3;
                _testResults.Ignored = 0;
                _testResults.Skipped = 0;
                _testResults.Invalid = 0;
                _testResults.Date = _testDate.ToString();
                _testResults.Time = _testTime.ToString();
                _testResults.Enviroument = new Enviroument();
            };

        protected static TestSerializer _testSerializer;
        protected static string _createdXml;
        protected static TestResults _testResults;
        protected static DateTime _testDate;
        protected static TimeSpan _testTime;
    }
}

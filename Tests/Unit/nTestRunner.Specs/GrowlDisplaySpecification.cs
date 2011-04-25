using System;
using System.IO;
using System.Text;
using Growl.Connector;
using GrowlDisplay;
using Machine.Specifications;
using SerializationHelpers;
using nTestRunner.Interfaces.ResultsSerialization;

namespace nTestRunner.Spec
{
    [Subject("Growl Display")]
    public class given_a_successful_results : GrowlDisplayContext
    {
        Establish context =
            () =>
                {
                    _results = BuildResultsObject("<?xml version=\"1.0\" encoding=\"utf-8\"?><test-results xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" total=\"4\" errors=\"0\" failures=\"0\" not-run=\"0\" inconclusive=\"0\" ignored=\"0\" skipped=\"0\" invalid=\"0\" date=\"2011-4-19\" time=\"17:58:45\"><environment nunit-version=\"2.5.9.10348\" clr-version=\"4.0.30319\" os-version=\"Microsoft Windows NT 6.1.7600.0\" platform=\"Win32NT\" cwd=\"C:\\Users\\duncan.butler\\My Dropbox\\Dropbox\\nTestRunnerTests\\CalculatorKata\\packages\\nTestRunner.1.0.0.0\" machine-name=\"JS001184\" user=\"JOBSERVE\\duncan.butler\" user-domain=\"JOBSERVE\" /><culture-info current-culture=\"en-GB\" current-uiculture=\"en-US\" /><test-suite name=\"C:\\Users\\duncan.butler\\My Dropbox\\Dropbox\\nTestRunnerTests\\CalculatorKata\\CalculatorKata.Specs\\bin\\debug\\CalculatorKata.Specs\" executed=\"true\" result=\"Success\" success=\"true\" time=\"0.120\" asserts=\"4\" type=\"Project\"><results><test-suite name=\"Calculator\" executed=\"true\" result=\"Success\" success=\"true\" time=\"0.120\" asserts=\"0\" type=\"NameSpace\"><results><test-suite name=\"Calculator\" executed=\"true\" result=\"Success\" success=\"true\" time=\"0.120\" asserts=\"0\" type=\"TestFixture\"><results><test-case name=\"nothing returns zero\" executed=\"true\" result=\"Success\" success=\"true\" time=\"0.120\" asserts=\"1\" /><test-case name=\"one returns one\" executed=\"true\" result=\"Success\" success=\"true\" time=\"0.120\" asserts=\"1\" /><test-case name=\"two returns two\" executed=\"true\" result=\"Success\" success=\"true\" time=\"0.120\" asserts=\"1\" /><test-case name=\"one comma two returns 3\" executed=\"true\" result=\"Success\" success=\"true\" time=\"0.120\" asserts=\"1\" /></results></test-suite></results></test-suite></results></test-suite></test-results>");
                };

        Because of =
            () => _growl.DisplayNotification(_results);

        It registers_with_growl =
            () => _display.HasRegistered.ShouldBeTrue();

        It calls_growl_with_a_successful_notification =
            () => _display.NotificationTitle.ShouldEqual("Pass");

        It calls_growl_with_the_correct_message =
            () => _display.NotificationMessage.ShouldEqual("4 tests passed.");	
    }

    [Subject("Growl Display")]
    public class given_a_failed_results : GrowlDisplayContext
    {
        Establish context =
            () =>
                {
                    _results =
                        BuildResultsObject(
                            "<?xml version=\"1.0\" encoding=\"utf-8\"?><test-results xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" total=\"2\" errors=\"1\" failures=\"0\" not-run=\"0\" inconclusive=\"0\" ignored=\"0\" skipped=\"0\" invalid=\"0\" date=\"2011-4-20\" time=\"11:40:12\"><environment nunit-version=\"2.5.9.10348\" clr-version=\"4.0.30319\" os-version=\"Microsoft Windows NT 6.1.7600.0\" platform=\"Win32NT\" cwd=\"C:\\Users\\duncan.butler\\My Dropbox\\Dropbox\\nTestRunnerTests\\CalculatorKata\\packages\\nTestRunner.1.0.0.0\" machine-name=\"JS001184\" user=\"JOBSERVE\\duncan.butler\" user-domain=\"JOBSERVE\" /><culture-info current-culture=\"en-GB\" current-uiculture=\"en-US\" /><test-suite name=\"C:\\Users\\duncan.butler\\My Dropbox\\Dropbox\\nTestRunnerTests\\CalculatorKata\\CalculatorKata.Specs\\bin\\debug\\CalculatorKata.Specs\" executed=\"true\" result=\"Failure\" success=\"false\" time=\"0.158\" asserts=\"2\" type=\"Project\"><results><test-suite name=\"Calculator\" executed=\"true\" result=\"Failure\" success=\"false\" time=\"0.158\" asserts=\"0\" type=\"NameSpace\"><results><test-suite name=\"Calculator\" executed=\"true\" result=\"Failure\" success=\"false\" time=\"0.158\" asserts=\"0\" type=\"TestFixture\"><results><test-case name=\"nothing returns 0\" executed=\"true\" result=\"Success\" success=\"true\" time=\"0.158\" asserts=\"1\" /><test-case name=\"one returns 1\" executed=\"true\" result=\"Failure\" success=\"false\" time=\"0.158\" asserts=\"0\" /></results></test-suite></results></test-suite></results></test-suite></test-results>");
                };

        Because of =
            () =>
                {
                    _growl.DisplayNotification(_results);
                };

        It registers_with_growl =
            () => _display.HasRegistered.ShouldBeTrue();

        It calls_growl_with_a_failure_notification =
            () => _display.NotificationTitle.ShouldEqual("Fail");

        It calls_growl_with_the_correct_message =
            () => _display.NotificationMessage.ShouldEqual("1 test passed, 1 test failed");	
    }

    public class GrowlDisplayContext
    {
        Establish context =
            () =>
                {
                    _serializer = new TheSerializer();

                    _display = new MockDisplay();

                    _growl = new GrowlDisplay.GrowlDisplay(_display);
                };

        protected static TestResults BuildResultsObject(string xml)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml));

            return _serializer.ToTypeof<TestResults>(stream);
        }

        protected static MockDisplay _display;
        protected static GrowlDisplay.GrowlDisplay _growl;
        protected static TestResults _results;
        protected static TheSerializer _serializer;
    }

    public class MockDisplay : IGrowlWrapper
    {
        public bool HasRegistered { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationMessage { get; set; }

        public MockDisplay()
        {
            HasRegistered = false;
        }

        public void Register(Application application, NotificationType[] notificationTypes)
        {
            HasRegistered = true;
        }

        public void Notify(Notification notification)
        {
            NotificationTitle = notification.Title;
            NotificationMessage = notification.Text;
        }
    }
}
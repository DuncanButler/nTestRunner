using System;
using System.IO;
using Growl.Connector;
using nTestRunner.Interfaces;
using nTestRunner.Interfaces.ResultsSerialization;

namespace GrowlDisplay
{
    public class GrowlDisplay : IDisplay
    {
        const string ApplicationName = "nTestRunner";
        const string Success = "SUCCESS";
        const string Failed = "FAILED";
        const string Inconclusive = "INCONCLUSIVE";

        readonly IGrowlWrapper _growl;

        public GrowlDisplay(IGrowlWrapper growl)
        {
            string workingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string iconpath = Path.Combine(workingDirectory, "images");

            var application = new Application(ApplicationName);
            var success = new NotificationType(Success, "All Tests Pass")
                              {
                                  Icon = Path.Combine(iconpath, "Green.jpg")
                              };

            var fail = new NotificationType(Failed, "A Test Has Failed")
                           {
                               Icon = Path.Combine(iconpath, "Red.jpg")
                           };

            var inconclusive = new NotificationType(Inconclusive, "The Tests Are Inconclusive")
                                   {
                                       Icon = Path.Combine(iconpath, "Yellow.jpg")
                                   };

            _growl = growl;
            _growl.Register(application,new NotificationType[]{success,fail,inconclusive});
        }

        public void DisplayNotification(TestResults results)
        {
            Notification notification;

            if (IsSuccess(results))
            {
                notification = new Notification(ApplicationName, Success, "1", "Pass", BuildSuccessDisplayText(results))
                                   {
                                       Priority = Priority.Normal
                                   };
            }
            else if(IsFail(results))
            {
                notification = new Notification(ApplicationName, Failed, "2", "Fail", BuildFailDisplayText(results))
                                   {
                                       Priority = Priority.High
                                   };
            }
            else
            {
                notification = new Notification(ApplicationName, Inconclusive, "3", "Inconclusive", BuildInconclusiveDisplayText(results))
                                   {
                                       Priority = Priority.VeryLow
                                   };
            }
            
            _growl.Notify(notification);
        }

        bool IsSuccess(TestResults results)
        {
            return results.Errors == 0 && results.Failures == 0 && results.Inconclusive == 0;
        }

        string BuildSuccessDisplayText(TestResults results)
        {
            if (results.Total == 1)
                return "1 test passed.";

            return string.Format("{0} tests passed.",results.Total);
        }

        bool IsFail(TestResults results)
        {
            return results.Errors > 0;
        }

        string BuildFailDisplayText(TestResults results)
        {
            if (results.Errors == results.Total && results.Errors == 1)
            {
                return "1 test failed";
            }

            if(results.Errors == results.Total)
            {
                return string.Format("{0} tests failed", results.Errors);
            }

            int passedTests = results.Total - results.Errors;

            if (results.Errors == 1 && passedTests == 1)
            {                
               return string.Format("{0} test passed, {1} test failed", passedTests, results.Errors);
            }

            return string.Format("{0} tests passed, {1} tests failed", passedTests, results.Errors);
        }

        string BuildInconclusiveDisplayText(TestResults results)
        {
            if (results.Total == 1)
                return "test was inconclusive";

            return string.Format("{0} tests were inconclusive",results.Total);
        }
    }
}
using nTestRunner.Interfaces.ResultsSerialization;

namespace nTestRunner.Interfaces
{
    public interface IDisplay
    {
        void DisplayNotification(TestResults results);
    }
}
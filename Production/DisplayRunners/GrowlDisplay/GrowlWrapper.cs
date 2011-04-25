using Growl.Connector;

namespace GrowlDisplay
{
    public class GrowlWrapper :IGrowlWrapper
    {
        readonly GrowlConnector _growlConnector;

        public GrowlWrapper()
        {
            _growlConnector = new GrowlConnector();
        }

        public void Register(Application application, NotificationType[] notificationTypes)
        {
            _growlConnector.Register(application,notificationTypes);
        }

        public void Notify(Notification notification)
        {
            _growlConnector.Notify(notification);
        }
    }
}
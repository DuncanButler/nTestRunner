using Growl.Connector;

namespace GrowlDisplay
{
    public interface IGrowlWrapper
    {
        void Register(Application application, NotificationType[] notificationTypes);
        void Notify(Notification notification);
    }
}
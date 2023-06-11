

using Blog.Business.Notifications;

namespace Blog.Business.Interfaces
{
    public interface INotifier
    {

        void Add( Notification notification );
        List<Notification> GetNotifications();
        bool HasNotification();

    }
}

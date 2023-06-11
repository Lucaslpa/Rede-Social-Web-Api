

using Blog.Business.Interfaces;

namespace Blog.Business.Notifications
{
    public class Notifier : INotifier, IDisposable
    {

        private List<Notification> Notifications;
        public Notifier()
        {
            Notifications = new List<Notification>();
        }
        public void Add( Notification notification )
        {
            Notifications.Add( notification );
        }
        public List<Notification> GetNotifications()
        {
            return Notifications;
        }
        public bool HasNotification()
        {
            return Notifications.Any();
        }
        public void Dispose()
        {
            Notifications = new List<Notification>();
        }

    }
}

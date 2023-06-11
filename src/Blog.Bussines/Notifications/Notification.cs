

namespace Blog.Business.Notifications
{
    public class Notification
    {
        public Notification( String message )
        {
            Message = message;
        }

        public String Message { get; set; }
    }
}

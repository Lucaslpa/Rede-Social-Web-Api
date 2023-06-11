using Blog.Business.Interfaces;
using Blog.Business.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Blog.api.Controllers
{
    public class Controller : ControllerBase
    {
        readonly INotifier Notifier;
        public Controller( INotifier notifier )
        {
            Notifier = notifier;
        }

        protected bool HasErrors()
        {
            return Notifier.HasNotification();
        }

        protected ActionResult RequestResponse( object? Result = null )
        {

            if (HasErrors())
            {
                return BadRequest( new
                {
                    success = false ,
                    errors = Notifier.GetNotifications()
                } );

            }

            return Ok( new
            {
                success = true ,
                data = Result
            } );
        }

        protected ActionResult RequestResponse( ModelStateDictionary ModelState )
        {
            if (!ModelState.IsValid) NotifyErrors( ModelState );

            return RequestResponse();
        }

        protected void NotifyErrors( ModelStateDictionary ModelState )
        {
            foreach (var error in ModelState.Values.SelectMany( m => m.Errors ))
            {
                var errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                Notifier.Add( new Notification( errorMessage ) );
            }
        }

        protected void NotifyErrors( string message )
        {
            Notifier.Add( new Notification( message ) );
        }

    }
}

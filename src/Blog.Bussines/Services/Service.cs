using Blog.Business.Interfaces;
using Blog.Business.Models;
using Blog.Business.Notifications;
using FluentValidation;
using FluentValidation.Results;


namespace Blog.Business.Services
{
    public class Service
    {
        public readonly INotifier Notifier;
        protected Service( INotifier notifier )
        {
            Notifier = notifier;
        }

        void Notify( ValidationResult Validation )
        {
            foreach (var Error in Validation.Errors)
                Notifier.Add( new Notification( Error.ErrorMessage ) );
        }
        protected bool Validate<V, E>( V validator , E entity ) where V : AbstractValidator<E> where E : Entity
        {
            var Validation = validator.Validate( entity );

            if (!Validation.IsValid)
                Notify( Validation );

            return Validation.IsValid;
        }
    }
}

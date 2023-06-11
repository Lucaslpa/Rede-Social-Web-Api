using Blog.Business.Models;
using FluentValidation;

namespace Blog.Business.ModelValidation
{
    public class LikeValidation : AbstractValidator<Like>
    {

        public LikeValidation()
        {
            RuleFor( p => p.UserId )
                .Must( ( id ) => new Guid( id ) != Guid.Empty ).WithMessage( "O campo {PropertyName} precisa ser fornecido" )
                .NotNull().WithMessage( "O campo {PropertyName} precisa ser fornecido" );

            RuleFor( p => p.PostId )
                .Must( ( id ) => id != Guid.Empty ).WithMessage( "O campo {PropertyName} precisa ser fornecido" )
                .NotNull().WithMessage( "O campo {PropertyName} precisa ser fornecido" );

        }

    }
}

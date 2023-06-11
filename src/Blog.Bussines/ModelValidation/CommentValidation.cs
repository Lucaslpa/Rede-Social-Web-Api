
using Blog.Business.Models;
using FluentValidation;

namespace Blog.Business.ModelValidation
{
    public class CommentValidation : AbstractValidator<Comment>
    {
        public CommentValidation()
        {

            RuleFor( p => p.UserId )
                .Must( ( id ) => new Guid( id ) != Guid.Empty ).WithMessage( "O campo {PropertyName} precisa ser fornecido" )
                .NotNull().WithMessage( "O campo {PropertyName} precisa ser fornecido" );

            RuleFor( p => p.PostId )
                .Must( ( id ) => id != Guid.Empty ).WithMessage( "O campo {PropertyName} precisa ser fornecido" )
                .NotNull().WithMessage( "O campo {PropertyName} precisa ser fornecido" );

            RuleFor( p => p.Text )
              .NotEmpty().WithMessage( "O campo {PropertyName} precisa ser fornecido" )
              .MinimumLength( 1 ).WithMessage( "O campo {PropertyName} precisa ter no mínimo {MinLength} caracteres" )
              .MaximumLength( 200 ).WithMessage( "O campo {PropertyName} precisa ter no máximo {MaxLength} caracteres" );
        }
    }
}

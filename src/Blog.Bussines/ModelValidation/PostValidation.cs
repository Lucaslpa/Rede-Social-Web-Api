
using Blog.Business.Models;
using FluentValidation;

namespace Blog.Business.ModelValidation
{
    public class PostValidation : AbstractValidator<Post>
    {
        public PostValidation()
        {
            RuleFor( p => p.Text )
                .NotEmpty().WithMessage( "O campo {PropertyName} precisa ser fornecido" )
                .MinimumLength( 1 ).WithMessage( "O campo {PropertyName} precisa ter no mínimo {MinLength} caracteres" )
                .MaximumLength( 200 ).WithMessage( "O campo {PropertyName} precisa ter no máximo {MaxLength} caracteres" );
        }
    }
}

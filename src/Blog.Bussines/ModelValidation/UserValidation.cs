using Blog.Business.Models;
using FluentValidation;


namespace Blog.Business.ModelValidation
{
    public class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {

            RuleFor( p => p.Email )
                 .NotEmpty().WithMessage( "O campo {PropertyName} precisa ser fornecido" )
                 .EmailAddress().WithMessage( "O campo {PropertyName} está em um formato inválido" );
            RuleFor( p => p.UserName )
                 .NotEmpty().WithMessage( "O campo {PropertyName} precisa ser fornecido" )
                 .MinimumLength( 10 ).WithMessage( "O campo {PropertyName} precisa ter no mínimo {MinLength} caracteres" );
            RuleFor( p => p.DataCadastro )
                 .NotEmpty().WithMessage( "O campo {PropertyName} precisa ser fornecido" );
            RuleFor( p => p.BirthDay )
                .NotEmpty().WithMessage( "O campo {PropertyName} precisa ser fornecido" )
                .Must( BeValidDate ).WithMessage( "Data de nascimento inválida" )
                .LessThan( DateTime.Now.AddYears( -13 ) ).WithMessage( "Necessário pelo menos 13 anos de idade" );
            RuleFor( p => p.Surname )
                .NotEmpty().WithMessage( "O campo {PropertyName} precisa ser fornecido" )
                .MinimumLength( 3 ).WithMessage( "O campo {PropertyName} precisa ter no mínimo {MinLength} caracteres" );
            RuleFor( p => p.Name )
                .NotEmpty().WithMessage( "O campo {PropertyName} precisa ser fornecido" )
                .MinimumLength( 3 ).WithMessage( "O campo {PropertyName} precisa ter no mínimo {MinLength} caracteres" );
        }

        private bool BeValidDate( DateTime date )
        {
            // Verifica se o ano, mês e dia estão dentro de um intervalo válido
            if (date.Year < 1900 || date.Year > DateTime.Now.Year)
                return false;

            if (date.Month < 1 || date.Month > 12)
                return false;

            if (date.Day < 1 || date.Day > DateTime.DaysInMonth( date.Year , date.Month ))
                return false;

            return true;
        }

    }
}

using AutoMapper;
using Blog.api.Utils;
using Blog.api.ViewModels;
using Blog.Business.Interfaces;
using Blog.Business.Models;
using Blog.Business.ModelValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.api.Controllers
{
    [Route( "api/[controller]" )]
    public class AuthController : Controller
    {
        UserManager<User> UserManager;
        readonly IMapper Mapper;
        readonly IConfiguration Configuration;
        readonly SignInManager<User> SignInManager;

        public AuthController( INotifier notifier , IMapper mapper , UserManager<User> userManager , IConfiguration configuration , SignInManager<User> signInManager ) : base( notifier )
        {
            Configuration = configuration;
            SignInManager = signInManager;
            Mapper = mapper;
            UserManager = userManager;
        }

        [Route( "Registration" )]
        [HttpPost]
        public async Task<ActionResult> Registration( [FromBody] UserViewModel UserViewModel )
        {
            if (!ModelState.IsValid) return RequestResponse( ModelState );

            var user = Mapper.Map<User>( UserViewModel );

            var modelValidator = new UserValidation();

            if (!modelValidator.Validate( user ).IsValid)
            {
                foreach (var error in modelValidator.Validate( user ).Errors)
                {
                    NotifyErrors( error.ErrorMessage );
                }
                return RequestResponse( ModelState );
            }

            var RegistrationResult = await UserManager.CreateAsync( user , UserViewModel.Password );

            if (!RegistrationResult.Succeeded)
            {
                foreach (var error in RegistrationResult.Errors)
                {
                    NotifyErrors( error.Description );
                }

                return RequestResponse( UserViewModel );
            }


            return RequestResponse( await GenerateJwtToken( user.Email ) );
        }

        [Route( "Login" )]
        [HttpPost]
        public async Task<ActionResult> Login( [FromBody] LoginRequestViewModel login )
        {
            if (!ModelState.IsValid) return RequestResponse( ModelState );
            var user = await UserManager.FindByEmailAsync( login.Email );
            var LoginResult = await SignInManager.PasswordSignInAsync( user , login.Password , false , true );
            if (!LoginResult.Succeeded)
            {

                return RequestResponse( new { message = "Login falhou" } );
            }

            return RequestResponse( await GenerateJwtToken( login.Email ) );
        }

        public async Task<LoginResponseViewModel> GenerateJwtToken( string email )
        {

            var user = await UserManager.FindByEmailAsync( email );


            var jwtSettings = Configuration.GetSection( "JwtSettings" ).Get<JwtSettings>();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes( jwtSettings.SecretKey );

            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim( new Claim( ClaimTypes.Name , user.Name ) );
            claimsIdentity.AddClaim( new Claim( ClaimTypes.Surname , user.Surname ) );
            claimsIdentity.AddClaim( new Claim( ClaimTypes.Email , user.Email ) );
            claimsIdentity.AddClaim( new Claim( ClaimTypes.NameIdentifier , user.Id ) );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity ,
                Expires = DateTime.UtcNow.AddHours( jwtSettings.ExpirationTimeInHours ) ,
                Issuer = jwtSettings.Issuer ,
                Audience = jwtSettings.Issuer ,
                SigningCredentials = new SigningCredentials( new SymmetricSecurityKey( key ) , SecurityAlgorithms.HmacSha256Signature )
            };
            var token = tokenHandler.CreateToken( tokenDescriptor );
            var encodedToken = tokenHandler.WriteToken( token );

            return new LoginResponseViewModel
            {
                Token = encodedToken ,
                ExpiresInSeconds = jwtSettings.ExpirationTimeInHours * 3600 ,
            };

        }

    }
}

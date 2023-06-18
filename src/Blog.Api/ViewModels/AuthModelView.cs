using System.ComponentModel.DataAnnotations;

namespace Blog.api.ViewModels
{
    public class LoginResponseViewModel
    {
        public int ExpiresInSeconds { get; set; }
        public string Token { get; set; }

        public string UserID { get; set; }
    }

    public class LoginRequestViewModel
    {
        [Required( ErrorMessage = "O campo {0} é obrigatório" )]
        public string Email { get; set; }

        [Required( ErrorMessage = "O campo {0} é obrigatório" )]
        public string Password { get; set; }
    }
}

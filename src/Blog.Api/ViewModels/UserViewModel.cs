using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Blog.api.ViewModels.Converter;

namespace Blog.api.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            DataCadastro = DateTime.Now;
        }

        [Required( ErrorMessage = "O campo {0} é obrigatório" )]
        public string Name { get; set; }

        [Required( ErrorMessage = "O campo {0} é obrigatório" )]
        public string Surname { get; set; }

        [Required( ErrorMessage = "O campo {0} é obrigatório" )]
        public string Password { get; set; }

        [Required( ErrorMessage = "O campo {0} é obrigatório" )]
        public string Username { get; set; }

        public string? Img = "https://static.vecteezy.com/system/resources/previews/000/439/863/original/vector-users-icon.jpg";

        [EmailAddress( ErrorMessage = "O formato do email está incorreto." )]
        [Required( ErrorMessage = "O campo {0} é obrigatório" )]
        public string Email { get; set; }

        [Required( ErrorMessage = "O campo {0} é obrigatório" )]
        [JsonConverter( typeof( DateTimeConverter ) )]
        public DateTime BirthDay { get; set; }

        public DateTime DataCadastro { get; set; }

    }
}

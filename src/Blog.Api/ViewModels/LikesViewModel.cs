
using Blog.api.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Blog.Business.Models
{
    public class LikesViewModel : Entity
    {
        [Required( ErrorMessage = "O campo {0} é obrigatório" )]
        public string UserId { get; set; }

        [Required( ErrorMessage = "O campo {0} é obrigatório" )]
        public Guid PostId { get; set; }
        public UserViewModel? User { get; set; }
        public PostViewModel? Post { get; set; }

    }
}

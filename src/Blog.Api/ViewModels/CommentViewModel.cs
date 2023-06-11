using System.ComponentModel.DataAnnotations;

namespace Blog.api.ViewModels
{
    public class CommentViewModel
    {
        CommentViewModel()
        {
            Date = DateTime.Now;
        }

        [Key]
        public Guid Id { get; set; }

        [Required( ErrorMessage = "O campo {0} é obrigatório" )]
        public Guid UserId { get; set; }

        [Required( ErrorMessage = "O campo {0} é obrigatório" )]
        public Guid PostId { get; set; }

        [Required( ErrorMessage = "O campo {0} é obrigatório" )]
        [StringLength( 200 , ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres" , MinimumLength = 1 )]
        public string Text { get; set; }
        public DateTime? Date { get; set; }

        public UserViewModel? User { get; set; }
        public PostViewModel? Post { get; set; }
    }
}



using System.ComponentModel.DataAnnotations;

namespace Blog.api.ViewModels
{
    public class PostViewModel
    {

        PostViewModel()
        {
            Date = DateTime.Now;
        }

        [Key]
        public Guid Id { get; set; }
        public UserViewModel? User { get; set; }

        [Required( ErrorMessage = "O campo {0} é obrigatório" )]
        public string UserId { get; set; }

        [Required( ErrorMessage = "O campo {0} é obrigatório" )]
        [StringLength( 200 , ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres" , MinimumLength = 1 )]
        public string Text { get; set; }

        public string? Img { get; set; }

        public DateTime? Date { get; set; }

        public int? LikesCount { get; set; }

        public int? CommentsCount { get; set; }

        public IEnumerable<CommentViewModel>? Comments { get; set; }
    }
}

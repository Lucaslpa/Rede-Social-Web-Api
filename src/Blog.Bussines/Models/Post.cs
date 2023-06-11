
namespace Blog.Business.Models
{
    public class Post : Entity
    {
        public User User { get; set; }

        public string UserId { get; set; }

        public string Text { get; set; }

        public string? Img { get; set; }

        public DateTime Date { get; set; }

        public int LikesCount { get; set; }

        public int CommentsCount { get; set; }

        public IEnumerable<Comment>? Comments { get; set; }

        public IEnumerable<Like>? Likes { get; set; }

    }
}

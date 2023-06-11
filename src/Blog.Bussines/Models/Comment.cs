

namespace Blog.Business.Models
{
    public class Comment : Entity
    {
        public User User { get; set; }
        public string UserId { get; set; }
        public Guid PostId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public Post? Post { get; set; }
    }
}

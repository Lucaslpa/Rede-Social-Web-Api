
namespace Blog.Business.Models
{
    public class Like : Entity
    {

        public string UserId { get; set; }
        public Guid PostId { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }

    }
}




using Microsoft.AspNetCore.Identity;

namespace Blog.Business.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Img { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime BirthDay { get; set; }
        public IEnumerable<Post>? Posts { get; set; }
        public IEnumerable<Comment>? Comments { get; set; }
        public IEnumerable<Like>? Likes { get; set; }
    }
}

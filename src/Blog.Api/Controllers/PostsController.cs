using AutoMapper;
using Blog.api.ViewModels;
using Blog.Business.Interfaces;
using Blog.Business.Models;
using Blog.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.api.Controllers
{
    [Route( "api/[controller]" )]
    public class PostsController : Controller
    {

        readonly IPostsRepository PostRepository;
        readonly IPostsService PostService;
        readonly IMapper Mapper;

        public PostsController( IPostsRepository postRepository , IPostsService postService , INotifier notifier , IMapper mapper ) : base( notifier )
        {
            PostRepository = postRepository;
            PostService = postService;
            Mapper = mapper;
        }

        [HttpGet( "page/{page}" )]
        public async Task<ActionResult> GetAll( int page )
        {
            var posts = await PostRepository.GetAll( page );
            return RequestResponse( posts );
        }

        [HttpGet( "{id}" )]
        public async Task<ActionResult> Get( Guid id )
        {
            var post = await PostRepository.GePostCommentUser( id );

            var postViewModel = Mapper.Map<PostViewModel>( post );

            if (post == null)
                return NotFound();

            return RequestResponse( postViewModel );
        }


        [Authorize]
        [HttpPost( "{userId}" )]
        public async Task<ActionResult> Post( [FromBody] PostViewModel PostViewModel , string userId )
        {

            if (userId != PostViewModel.UserId)
            {
                NotifyErrors( "O campo UserId do body é diferente do UserId fornecido na url." );
                return RequestResponse( ModelState );
            }

            if (!ModelState.IsValid) return RequestResponse( ModelState );

            var post = Mapper.Map<Post>( PostViewModel );

            await PostService.Add( post );

            return RequestResponse( post );
        }

        [HttpGet( "User/{userId}/page/{page}" )]
        public async Task<ActionResult> GetAllCommentsByPostId( string userId , int page )
        {
            var posts = Mapper.Map<GetAllResponse<PostViewModel>>( await PostRepository.GetPostsByUserID( userId , page ) );

            return RequestResponse( posts );
        }

        [HttpPut( "{id}" )]
        public async Task<ActionResult> Put( [FromBody] PostViewModel PostViewModel , Guid id )
        {
            var existingPost = await PostRepository.GetByID( id );

            if (existingPost == null)
                return NotFound();

            existingPost.Id = id;
            existingPost.Text = PostViewModel.Text;

            var result = await PostService.Update( existingPost );

            if (!result)
            {
                return RequestResponse( ModelState );
            }
            return RequestResponse( existingPost );
        }

        [HttpDelete( "{id}" )]
        public async Task<ActionResult> Delete( string id )
        {
            var Id = new Guid( id );
            await PostRepository.Delete( Id );
            return RequestResponse();
        }
    }
}

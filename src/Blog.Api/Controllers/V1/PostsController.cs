using AutoMapper;
using Blog.api.ViewModels;
using Blog.Business.Interfaces;
using Blog.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.api.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion( "1" )]
    [Route( "api/v{version:apiVersion}/[controller]" )]
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
        public async Task<ActionResult<GetAllResponse<Post>>> GetAll( int page )
        {
            var posts = Mapper.Map<GetAllResponse<Post>>( await PostRepository.GetAll( page ) );
            return RequestResponse( posts );
        }

        [HttpGet( "{id}" )]
        public async Task<ActionResult<PostViewModel>> Get( Guid id )
        {
            var post = await PostRepository.GePostCommentUser( id );

            var postViewModel = Mapper.Map<PostViewModel>( post );

            if (post == null)
                return NotFound();

            return RequestResponse( postViewModel );
        }



        [HttpPost( "{userId}" )]
        public async Task<ActionResult<PostViewModel>> Post( [FromBody] PostViewModel PostViewModel , string userId )
        {

            if (userId != PostViewModel.UserId)
            {
                NotifyErrors( "O campo UserId do body é diferente do UserId fornecido na url." );
                return RequestResponse( ModelState );
            }

            if (!ModelState.IsValid) return RequestResponse( ModelState );

            var post = Mapper.Map<Post>( PostViewModel );

            await PostService.Add( post );

            var postViewModel = Mapper.Map<PostViewModel>( post );

            return RequestResponse( postViewModel );
        }

        [HttpGet( "User/{userId}/page/{page}" )]
        public async Task<ActionResult<GetAllResponse<PostViewModel>>> GetAllCommentsByPostId( string userId , int page )
        {
            var posts = Mapper.Map<GetAllResponse<PostViewModel>>( await PostRepository.GetPostsByUserID( userId , page ) );

            return RequestResponse( posts );
        }

        [HttpPut( "{id}" )]
        public async Task<ActionResult<PostViewModel>> Put( [FromBody] PostViewModel PostViewModel , Guid id )
        {

            string userId = User.FindFirst( ClaimTypes.NameIdentifier ).Value;
            var existingPost = await PostRepository.GetByID( id );

            if (existingPost == null)
                return NotFound();

            if (existingPost.UserId != userId)
                return RequestResponse( ModelState );

            existingPost.Id = id;
            existingPost.Text = PostViewModel.Text;

            var result = await PostService.Update( existingPost );

            if (!result)
            {
                return RequestResponse( ModelState );
            }

            var postViewModel = Mapper.Map<PostViewModel>( existingPost );

            return RequestResponse( postViewModel );
        }

        [HttpDelete( "{id}" )]
        public async Task<ActionResult> Delete( string id )
        {

            string userId = User.FindFirst( ClaimTypes.NameIdentifier ).Value;
            var adm = User.Claims.Any( c => c.Type == ClaimTypes.Role && c.Value == "Admin" );

            var post = await PostRepository.GetByID( new Guid( id ) );

            if (post.UserId == userId || adm)
            {
                var Id = new Guid( id );
                await PostRepository.Delete( Id );
            }

            return RequestResponse();
        }
    }
}

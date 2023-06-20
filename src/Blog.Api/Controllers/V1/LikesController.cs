using AutoMapper;
using Blog.api.ViewModels;
using Blog.Business.Interfaces;
using Blog.Business.Models;
using Blog.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.api.Controllers.V1
{
    [Authorize]
    [ApiController]
    [ApiVersion( "1" )]
    [Route( "api/v{version:apiVersion}/[controller]" )]
    public class LikesController : Controller
    {

        readonly ILikeRespository LikesRepository;
        readonly ILikesService LikesService;
        readonly IMapper Mapper;

        public LikesController( ILikeRespository likeRespository , ILikesService likesService , INotifier notifier , IMapper mapper ) : base( notifier )
        {
            LikesRepository = likeRespository;
            LikesService = likesService;
            Mapper = mapper;
        }

        [HttpGet( "likes/{page}" )]
        public async Task<ActionResult> GetAll( int page )
        {
            var posts = await LikesRepository.GetAll( page );
            return RequestResponse( posts );
        }

        [HttpGet( "{id}" )]
        public async Task<ActionResult> Get( Guid id )
        {
            var post = await LikesRepository.GetByID( id );

            var postViewModel = Mapper.Map<PostViewModel>( post );

            if (post == null)
                return NotFound();

            return RequestResponse( postViewModel );
        }

        [HttpGet( "Post/{postId}/page/{page}" )]
        public async Task<ActionResult> GetLikesByPostID( Guid postId , int page )
        {
            var likes = await LikesRepository.GetLikesByPostID( postId , page );

            var likesViewModel = Mapper.Map<GetAllResponse<LikesViewModel>>( likes );

            if (likes == null)
                return NotFound();

            return RequestResponse( likesViewModel );
        }

        [HttpGet( "User/{userId}/page/{page}" )]
        public async Task<ActionResult> GetUserId( Guid userId , int page )
        {
            var likes = await LikesRepository.GetLikesByUserID( userId , page );

            var likesViewModel = Mapper.Map<GetAllResponse<LikesViewModel>>( likes );

            if (likes == null)
                return NotFound();

            return RequestResponse( likesViewModel );
        }

        [HttpGet( "{userId}/{postId}" )]
        public async Task<ActionResult> GetUserIdPostId( string userId , Guid postId )
        {
            var post = await LikesRepository.getLike( userId , postId );

            var postViewModel = Mapper.Map<PostViewModel>( post );

            if (post == null)
                return NotFound();

            return RequestResponse( postViewModel );
        }

        [HttpPost( "{userId}/{postId}" )]
        public async Task<ActionResult> Post( [FromBody] LikesViewModel likesViewModel , string userId , Guid postId )
        {

            if (userId != likesViewModel.UserId || postId != likesViewModel.PostId)
            {
                NotifyErrors( "O campo UserId/PostId do body é diferente do UserId/PostId fornecido na url." );
                return RequestResponse( ModelState );
            }

            if (!ModelState.IsValid) return RequestResponse( ModelState );

            var theresLike = await LikesRepository.getLike( likesViewModel.UserId , likesViewModel.PostId );

            if (theresLike != null)
            {
                NotifyErrors( "User UserId já deu like neste PostId." );
                return RequestResponse( ModelState );
            }

            var like = Mapper.Map<Like>( likesViewModel );

            await LikesService.AddLike( like );

            return RequestResponse( like );
        }


        [HttpDelete( "{id}" )]
        public async Task<ActionResult> Delete( Guid id )
        {
            string userId = User.FindFirst( ClaimTypes.NameIdentifier ).Value;

            var like = await LikesRepository.GetByID( id );

            if (like.UserId != userId)
            {
                await LikesRepository.Delete( id );
            }

            return RequestResponse();
        }
    }
}

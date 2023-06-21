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
        public async Task<ActionResult<GetAllResponse<LikesViewModel>>> GetAll( int page )
        {
            var likes = Mapper.Map<GetAllResponse<LikesViewModel>>( await LikesRepository.GetAll( page ) );
            return RequestResponse( likes );
        }

        [HttpGet( "{id}" )]
        public async Task<ActionResult<LikesViewModel>> Get( Guid id )
        {
            var like = await LikesRepository.GetByID( id );

            var likeViewModel = Mapper.Map<LikesViewModel>( like );

            if (like == null)
                return NotFound();

            return RequestResponse( likeViewModel );
        }

        [HttpGet( "Post/{postId}/page/{page}" )]
        public async Task<ActionResult<GetAllResponse<LikesViewModel>>> GetLikesByPostID( Guid postId , int page )
        {
            var likes = await LikesRepository.GetLikesByPostID( postId , page );

            var likesViewModel = Mapper.Map<GetAllResponse<LikesViewModel>>( likes );

            if (likes == null)
                return NotFound();

            return RequestResponse( likesViewModel );
        }

        [HttpGet( "User/{userId}/page/{page}" )]
        public async Task<ActionResult<GetAllResponse<LikesViewModel>>> GetLikesByUser( Guid userId , int page )
        {
            var likes = await LikesRepository.GetLikesByUserID( userId , page );

            var likesViewModel = Mapper.Map<GetAllResponse<LikesViewModel>>( likes );

            if (likes == null)
                return NotFound();

            return RequestResponse( likesViewModel );
        }

        [HttpGet( "{userId}/{postId}" )]
        public async Task<ActionResult<LikesViewModel>> GetLikesByUserAndPost( string userId , Guid postId )
        {
            var like = await LikesRepository.getLike( userId , postId );

            var likesViewModel = Mapper.Map<LikesViewModel>( like );

            if (like == null)
                return NotFound();

            return RequestResponse( likesViewModel );
        }

        [HttpPost( "{userId}/{postId}" )]
        public async Task<ActionResult<LikesViewModel>> Post( [FromBody] LikesViewModel likesViewModel , string userId , Guid postId )
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

            return RequestResponse( likesViewModel );
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

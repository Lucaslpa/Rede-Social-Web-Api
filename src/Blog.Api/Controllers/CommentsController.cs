using AutoMapper;
using Blog.api.ViewModels;
using Blog.Business.Interfaces;
using Blog.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blog.api.Controllers
{
    [Authorize]
    [Route( "api/[controller]" )]
    public class CommentsController : Controller
    {

        readonly ICommentsRepository CommentsRespository;
        readonly ICommentsService CommentsService;
        readonly IMapper Mapper;

        public CommentsController( ICommentsRepository commentsRespository , ICommentsService commentsSerivce , INotifier notifier , IMapper mapper ) : base( notifier )
        {
            CommentsRespository = commentsRespository;
            CommentsService = commentsSerivce;
            Mapper = mapper;
        }

        [HttpGet( "page/{page}" )]
        public async Task<ActionResult> GetAll( int page )
        {
            var posts = await CommentsRespository.GetAll( page );
            return RequestResponse( posts );
        }


        [HttpGet( "Post/{postId}/page/{page}" )]
        public async Task<ActionResult> GetAllCommentsByPostId( Guid postId , int page )
        {
            var posts = Mapper.Map<GetAllResponse<CommentViewModel>>( await CommentsRespository.GetCommentsByPostID( postId , page ) );

            return RequestResponse( posts );
        }


        [HttpGet( "User/{userId}/page/{page}" )]
        public async Task<ActionResult> GetAllCommentsByUserId( Guid userId , int page )
        {
            var posts = Mapper.Map<GetAllResponse<CommentViewModel>>( await CommentsRespository.GetCommentsByUserID( userId , page ) );
            return RequestResponse( posts );
        }


        [HttpGet( "{id}" )]
        public async Task<ActionResult> Get( Guid id )
        {
            var post = await CommentsRespository.GetByID( id );

            var postViewModel = Mapper.Map<PostViewModel>( post );

            if (post == null)
                return NotFound();

            return RequestResponse( postViewModel );
        }


        [HttpPost( "{UserId}/{PostId}" )]
        public async Task<ActionResult> Post( [FromBody] CommentViewModel commentViewModel , Guid UserId , Guid PostId )
        {

            if (!ModelState.IsValid) return RequestResponse( ModelState );

            if (UserId != commentViewModel.UserId || PostId != commentViewModel.PostId)
            {
                NotifyErrors( "O Id do PostId ou UserId informado no body não é igual ao da url" );
                return RequestResponse( ModelState );
            }

            var comment = Mapper.Map<Comment>( commentViewModel );

            await CommentsService.Add( comment );

            return RequestResponse( comment );
        }


        [HttpPut( "{id}" )]
        public async Task<ActionResult> Put( [FromBody] CommentViewModel commentViewModel , Guid id )
        {



            if (!ModelState.IsValid) return RequestResponse( ModelState );

            if (id != commentViewModel.Id)
            {
                NotifyErrors( "O Id informado no body não é igual ao id na url" );
                return RequestResponse( ModelState );
            }
            string userId = User.FindFirst( ClaimTypes.NameIdentifier ).Value;
            var existingComment = await CommentsRespository.GetByID( id );

            if (existingComment.UserId != userId)
            {
                return RequestResponse( ModelState );
            }

            existingComment.Text = commentViewModel.Text;
            existingComment.Date = (DateTime)commentViewModel.Date;

            var result = await CommentsService.Update( existingComment );

            if (!result)
            {
                return RequestResponse( ModelState );
            }
            return RequestResponse( existingComment );
        }


        [HttpDelete( "{id}" )]
        public async Task<ActionResult> Delete( Guid id )
        {
            string userId = User.FindFirst( ClaimTypes.NameIdentifier ).Value;
            var adm = User.Claims.Any( c => c.Type == ClaimTypes.Role && c.Value == "Admin" );

            var comment = await CommentsRespository.GetByID( id );

            if (comment.UserId == userId || adm)
            {
                await CommentsRespository.Delete( id );
            }

            return RequestResponse();
        }
    }
}

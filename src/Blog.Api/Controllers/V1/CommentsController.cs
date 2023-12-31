﻿using AutoMapper;
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
        public async Task<ActionResult<GetAllResponse<CommentViewModel>>> GetAll( int page )
        {
            var comments = Mapper.Map<GetAllResponse<CommentViewModel>>( await CommentsRespository.GetAll( page ) );
            return RequestResponse( comments );
        }


        [HttpGet( "Post/{postId}/page/{page}" )]
        public async Task<ActionResult<GetAllResponse<CommentViewModel>>> GetAllCommentsByPostId( Guid postId , int page )
        {
            var comments = Mapper.Map<GetAllResponse<CommentViewModel>>( await CommentsRespository.GetCommentsByPostID( postId , page ) );

            return RequestResponse( comments );
        }


        [HttpGet( "User/{userId}/page/{page}" )]
        public async Task<ActionResult<GetAllResponse<CommentViewModel>>> GetAllCommentsByUserId( Guid userId , int page )
        {
            var comments = Mapper.Map<GetAllResponse<CommentViewModel>>( await CommentsRespository.GetCommentsByUserID( userId , page ) );
            return RequestResponse( comments );
        }


        [HttpGet( "{id}" )]
        public async Task<ActionResult<CommentViewModel>> Get( Guid id )
        {
            var commentViewModel = Mapper.Map<CommentViewModel>( await CommentsRespository.GetByID( id ) );

            if (commentViewModel == null)
                return NotFound();

            return RequestResponse( commentViewModel );
        }


        [HttpPost( "{UserId}/{PostId}" )]
        public async Task<ActionResult<CommentViewModel>> Post( [FromBody] CommentViewModel commentViewModel , Guid UserId , Guid PostId )
        {

            if (!ModelState.IsValid) return RequestResponse( ModelState );

            if (UserId != commentViewModel.UserId || PostId != commentViewModel.PostId)
            {
                NotifyErrors( "O Id do PostId ou UserId informado no body não é igual ao da url" );
                return RequestResponse( ModelState );
            }

            var comment = Mapper.Map<Comment>( commentViewModel );

            await CommentsService.Add( comment );

            commentViewModel = Mapper.Map<CommentViewModel>( comment );

            return RequestResponse( commentViewModel );
        }


        [HttpPut( "{id}" )]
        public async Task<ActionResult<CommentViewModel>> Put( [FromBody] CommentViewModel commentViewModel , Guid id )
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

            var commentViewModelUpdated = Mapper.Map<CommentViewModel>( existingComment );

            return RequestResponse( commentViewModelUpdated );
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

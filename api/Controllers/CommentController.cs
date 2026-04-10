using api.Dtos.Comment;
using api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {   
        private readonly ICommentRepository _commentRepository;
        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            
            var comments = await _commentRepository.GetAllAsync();

            //var commentDtos = comments.Select(c => CommentDto);

            return Ok(comments);   
        }
    }
    
}
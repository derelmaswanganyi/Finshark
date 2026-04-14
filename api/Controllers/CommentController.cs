using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {   
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            
            var comments = await _commentRepository.GetAllAsync();

            var commentDtos = comments.Select(s => s.ToCommentDto()).ToList();

            // var commentDtos = comments.Select(c => new CommentDto
            // {
            //     Id = c.Id,
            //     StockId = c.StockId,
            //     Title = c.Title,
            //     CreatedOn = c.CreatedOn,
            //     Content = c.Content
            // }).ToList();

            return Ok(commentDtos);   
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());

        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentRequestDto commentDto)
        {   
            if(! await _stockRepository.StockExistsAsync(stockId))
            {
                return NotFound($"Stock with id {stockId} not found.");
            }
            var commentModel = commentDto.ToCommentModel(stockId);
            await _commentRepository.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }
    }


}
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {   
                Id = commentModel.Id,
                StockId = commentModel.StockId,
                Title = commentModel.Title,
                CreatedOn = commentModel.CreatedOn,
                Content = commentModel.Content
            };
        }

        public static Comment ToCommentModel(this CreateCommentRequestDto createCommentRequestDto, int stockId)
        {
            return new Comment
            {   
                StockId = stockId,
                Title = createCommentRequestDto.Title, 
                Content = createCommentRequestDto.Content
            };
        }

         public static Comment ToCommentFromUpdate(this UpdateCommentRequestDto updateCommentRequestDto)
        {
            return new Comment
            {   
                Title = updateCommentRequestDto.Title, 
                Content = updateCommentRequestDto.Content
            };
        }
    }
}
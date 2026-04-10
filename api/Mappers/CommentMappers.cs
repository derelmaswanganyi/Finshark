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
                StockId = commentModel.StockId,
                Title = commentModel.Title,
                CreatedOn = commentModel.CreatedOn,
                Content = commentModel.Content
            };
        }
    }
}
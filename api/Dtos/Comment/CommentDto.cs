namespace api.Dtos.Comment
{
    public class CommentDto
    {   
        public int Id { get; set; }
       public int StockId { get; set; }  //Navigation property for the foreign key
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string Content { get; set; } = string.Empty;

    }
}
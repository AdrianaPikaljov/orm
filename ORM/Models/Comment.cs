namespace ORM.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public int ArticleId { get; set; }
        public int AuthorId { get; set; }
    }
}
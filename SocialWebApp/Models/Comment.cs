namespace SocialWebApp.Models
{
    public class Comment
    {
        public string? Content { get; set; }
        public int CommentId { get; set; }
        public int Like { get; set; }
        public Comment? Reply { get; set; }
        public int PostId { get; set; }
    }
}

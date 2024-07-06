using Microsoft.EntityFrameworkCore;

namespace SocialWebApp.Models
{
    public class Post
    {
        public string? Title { get; set; }
        public int Like { get; set; }
        public int PostId { get; set; }
        public List<Comment> Comments { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}

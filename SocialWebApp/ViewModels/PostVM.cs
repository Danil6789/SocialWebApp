using SocialWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace SocialWebApp.ViewModels
{
    public class PostVM
    {
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}

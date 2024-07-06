using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SocialWebApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialWebApp.Data
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<Post> Posts { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DbSet<Comment> Comment { get; set; }
    }
}

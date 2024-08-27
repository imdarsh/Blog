using Microsoft.EntityFrameworkCore;

namespace Blog.Models
{
    public class PostDbContext : DbContext
    {
        public PostDbContext(DbContextOptions<PostDbContext> options) : base(options) {  }

        public DbSet<PostsModel> Posts { get; set; }
    }
}

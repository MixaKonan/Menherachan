using Menherachan.Domain.Entities.DBOs;
using Microsoft.EntityFrameworkCore;

namespace Menherachan.Domain.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            
        }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }
        
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Ban> Bans { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Thread> Threads { get; set; }
        public DbSet<Token> Tokens { get; set; }
    }
}
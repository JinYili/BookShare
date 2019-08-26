using BookShare.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShare.Web.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {

        }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // primary key start from zero and increment 1
            modelBuilder.ApplyConfiguration(new BookConfiguration());
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}

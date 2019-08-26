using BookShare.Web.Data;
using Microsoft.EntityFrameworkCore; 

namespace BookShareXunitTest.API.UnitTests
{
    public static class DbContextMocker
    {
        public static BookContext GetDbContext(string dbName)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<BookContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            // Create instance of DbContext
            var dbContext = new BookContext(options);

            // Add entities in memory
            dbContext.Seed();

            return dbContext;
        }
    }
}
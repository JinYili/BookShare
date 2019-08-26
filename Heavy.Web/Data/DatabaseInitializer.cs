using System;
using System.Collections.Generic;
using System.Linq;
using BookShare.Web.Models;

namespace BookShare.Web.Data
{
    public class DatabaseInitializer
    {
        public static void Seed(BookContext context)
        {
            if (!context.Book.Any())
            {
                context.Book.AddRange
                (new List<Book>
                    {
                        new Book { Author = "Yu Hua", Title = "To Live!", CoverUrl = "https://upload.wikimedia.org/wikipedia/commons/f/f5/%E6%B4%BB%E7%9D%80%E6%97%A7%E5%B0%81%E9%9D%A2.jpg", Price = 25, ReleaseDate = new DateTime(1993, 6, 12) },
                        new Book { Author = "Yu Hua", Title = "Brothers", CoverUrl = "https://upload.wikimedia.org/wikipedia/zh/thumb/b/b7/Brother_book.jpg/200px-Brother_book.jpg", Price = 31, ReleaseDate = new DateTime(2005, 9, 19) },
                        new Book { Author = "Liu Ken", Title = "The Three-Body Problem", CoverUrl = "https://upload.wikimedia.org/wikipedia/zh/thumb/0/0f/Threebody.jpg/200px-Threebody.jpg", Price = 68, ReleaseDate = new DateTime(2008, 9, 24) },
                        new Book { Author = "Jin Chenyu ", Title = "Fan Hua", CoverUrl = "https://upload.wikimedia.org/wikipedia/zh/thumb/9/9e/The_Bookcover_of_Fanhua_%28Shanghai_Literature_and_Art_Publishing_House%2C_2013%29.jpg/200px-The_Bookcover_of_Fanhua_%28Shanghai_Literature_and_Art_Publishing_House%2C_2013%29.jpg", Price = 19, ReleaseDate = new DateTime(2013, 6, 26) }
                    });
            }
            context.SaveChanges();
        }
    }
}

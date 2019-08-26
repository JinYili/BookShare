using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShare.Web.Data;
using BookShare.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShare.Web.Services
{
    public class BookEfService : IBookService
    {
        private readonly BookContext _context;

        public BookEfService(BookContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Book.ToListAsync();
        }

        public async Task<List<Book>> GetAllInfoAsync()
        {
            var models = await _context.Book.Select(x => new { x.Id, x.Title, x.Author, x.CoverUrl, x.ReleaseDate, x.Price, Commnets = x.Comments.OrderByDescending(y => y.CreateDate).ToList() }).ToListAsync();

            // Convert into Book
            List<Book> book = new List<Book>();
            foreach (var model in models)
            {
                book.Add(new Book
                {
                    Id = model.Id,
                    Author = model.Author,
                    Title = model.Title,
                    ReleaseDate = model.ReleaseDate,
                    CoverUrl = model.CoverUrl,
                    Price = model.Price,
                    Comments = model.Commnets
                });
            }
            return book;
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            var model = await _context.Book.Select( x=>new { x.Id, x.Title, x.Author, x.CoverUrl , x.ReleaseDate, x.Price, Commnets = x.Comments.Where(y => y.bookId ==id ).OrderByDescending( y=>y.CreateDate).ToList() } ).Where(x => x.Id == id).FirstOrDefaultAsync();
            
            // Convert into Book
            Book newModel = new Book
            {
                Id = model.Id,
                Author = model.Author,
                Title = model.Title,
                ReleaseDate = model.ReleaseDate,
                CoverUrl = model.CoverUrl,
                Price = model.Price,
                Comments = model.Commnets
            };
            return newModel;
        }

        public async Task<Book> AddAsync(Book model)
        {
            _context.Book.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task UpdateAsync(Book model)
        {
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Book model)
        {
            _context.Book.Remove(model);
            await _context.SaveChangesAsync();
        }


    }
}

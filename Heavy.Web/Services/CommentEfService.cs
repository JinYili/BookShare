using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShare.Web.Data;
using BookShare.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShare.Web.Services
{
    public class CommentEfService : ICommentService
    {
        private readonly BookContext _context;
        public CommentEfService(BookContext context)
        {
            _context = context;
        }

        public async Task<Comment> AddAsync(Comment model)
        {
            _context.Comments.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<List<Comment>> GetByBookIdAsync(int id)
        {
           return await _context.Comments.Where(x => x.bookId == id).ToListAsync();
        }

        public async Task<List<Comment>> GetAllAsync()
        {
           return await _context.Comments.ToListAsync();
        }
    }
}

using BookShare.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookShare.Web.Services
{
    public interface ICommentService
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment> AddAsync(Comment model);
        Task<List<Comment>> GetByBookIdAsync(int id);
    }
}

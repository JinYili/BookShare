using System.Collections.Generic;
using System.Threading.Tasks;
using BookShare.Web.Models;

namespace BookShare.Web.Services
{
    public interface IBookService
    {
        Task<List<Book>> GetAllAsync();
        Task<List<Book>> GetAllInfoAsync();
        Task<Book> GetByIdAsync(int id);
        Task<Book> AddAsync(Book model);
        Task UpdateAsync(Book model);
        Task DeleteAsync(Book model);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookShare.Cache;
using BookShare.Web.Models;
using BookShare.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace BookShare.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICommentService _commentService;
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions _cacheEntryOption;
        private readonly IDistributedCache _distributedCache;

        public BookController(IBookService bookService, ICommentService commentService, 
                IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            _bookService = bookService;
            _commentService = commentService;
            _memoryCache = memoryCache;

            // In memory and redis cache option 
            _cacheEntryOption.SetAbsoluteExpiration(TimeSpan.FromSeconds(600)).SetSlidingExpiration(TimeSpan.FromSeconds(30));
            _cacheEntryOption.RegisterPostEvictionCallback(FillCacheCallBack, this);

            // redis cache
            _distributedCache = distributedCache;
        }

        [HttpGet("All")]
        public async Task<JsonResult> ListAll()
        {
            // No Cache
            List<Book> cachedBook = await _bookService.GetAllAsync();

            // In memory Cache
            /*
            if (!_memoryCache.TryGetValue(CacheEntryConstants.BookOfToday, out List<Book> cachedBook))
            {
                cachedBook = await _bookService.GetAllAsync();
                _memoryCache.Set(CacheEntryConstants.BookOfToday, cachedBook, _cacheEntryOption);
            }
            */
            
            //radis cache
            /*
            List<Book> cachedBook = new List<Book>();
            byte[] cachedBookbyte = _distributedCache.Get(CacheEntryConstants.BookOfToday);
            if (cachedBookbyte == null)
            {
                cachedBook = await _bookService.GetAllAsync();
                var serializedBook = JsonConvert.SerializeObject(cachedBook);
                byte[] encodedBook = Encoding.UTF8.GetBytes(serializedBook);
                await _distributedCache.SetAsync(CacheEntryConstants.BookOfToday, encodedBook);
            }
            else
            {
                string deserializedBook = Encoding.UTF8.GetString(cachedBookbyte);
                cachedBook = JsonConvert.DeserializeObject<List<Book>>(deserializedBook);
            }
            */

            return Json(cachedBook);   
        }

        private async void FillCacheCallBack(object key, object value, EvictionReason reason, object state)
        {
            List<Book> cachedBook = await _bookService.GetAllAsync();
            _memoryCache.Set(CacheEntryConstants.BookOfToday, cachedBook, _cacheEntryOption);
        }

        [HttpGet("AllInfo")]
        public async Task<JsonResult> ListAllInfo()
        {
            List<Book> book = await _bookService.GetAllInfoAsync();
            return Json(book);
        }

        [HttpGet("AllComment")]
        public async Task<JsonResult> ListComments()
        {
            List<Comment> comments = await _commentService.GetAllAsync();
            return Json(comments);
        }

        [HttpGet("View")]
        public async Task<JsonResult> View([FromQuery] int id)
        {
            var model = await _bookService.GetByIdAsync(id);
            if (model == null)
            {
                return Json(NoContent());
            }

            return Json(model);
        }

        [HttpGet("ViewComment/{id:int?}")]
        public async Task<JsonResult> ViewComment(int id)
        {
            List<Comment> comments = await _commentService.GetByBookIdAsync(id);
            return Json(comments);
        }

        [HttpPost("Add")]
        public async Task<JsonResult> Add([FromBody] Book book)
        {
            Book newModel = await _bookService.AddAsync(new Book
            {
                Author = book.Author.ToString(),
                Title = book.Title.ToString(),
                CoverUrl = book.CoverUrl.ToString(),
                Price = book.Price,
                ReleaseDate = book.ReleaseDate
            });
            return Json(newModel);
        }


        [HttpPost("AddComment")]
        public async Task<JsonResult> AddComment([FromBody] Comment comment)
        {
            Comment newModel = await _commentService.AddAsync(new Comment
            {
                Content = comment.Content,
                CreateDate = DateTime.Now,
                bookId = (int)comment.bookId  
            });
            return Json(newModel);
        }

        [HttpPut("Edit")]
        public async Task<JsonResult> Edit([FromBody]Book book)
        {
            var model = await _bookService.GetByIdAsync(book.Id);
            if (model == null)
            {
                return Json(NotFound());
            }

            await _bookService.UpdateAsync(book);

            return Json(book);
        }

        [HttpDelete("Delete/{id:int?}")]
        public async Task<JsonResult> Delete(int id)
        {
            var model = await _bookService.GetByIdAsync(id);
            if (model == null)
            {
                return null;
            }
            model.Id = id;
            await _bookService.DeleteAsync(model);

            return Json(model);
        }
    }
}
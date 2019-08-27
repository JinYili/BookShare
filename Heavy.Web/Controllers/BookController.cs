using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BookShare.Web.Models;
using BookShare.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookShare.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICommentService _CommentService;
        public BookController(IBookService bookService, ICommentService commentService)
        {
            _bookService = bookService;
            _CommentService = commentService;
        }

        [HttpGet("All")]
        public async Task<JsonResult> ListAll()
        {
            List<Book> book = await _bookService.GetAllAsync();
            return Json(book);   
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
            List<Comment> comments = await _CommentService.GetAllAsync();
            return Json(comments);
        }

        [HttpGet("View/{id:int?}")]
        public async Task<JsonResult> View(int id)
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
            List<Comment> comments = await _CommentService.GetByBookIdAsync(id);
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
            Comment newModel = await _CommentService.AddAsync(new Comment
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
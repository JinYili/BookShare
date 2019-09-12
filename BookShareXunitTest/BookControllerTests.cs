using BookShare.Web.Controllers;
using BookShare.Web.Data;
using BookShare.Web.Models;
using BookShare.Web.Services;
using BookShareXunitTest.API.UnitTests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Xunit;

namespace BookShareXunitTest
{
    public class BookControllerTests
    {
        private BookController _controller;
        private  BookEfService _bookService;
        private  CommentEfService _CommentService;

        public BookControllerTests()
        {
            BookContext dbContext = DbContextMocker.GetDbContext(nameof(BookControllerTests));
            _bookService = new BookEfService(dbContext);
            _CommentService = new CommentEfService(dbContext);
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            IOptions<MemoryDistributedCacheOptions> someOptions = Options.Create<MemoryDistributedCacheOptions>(new MemoryDistributedCacheOptions());
            MemoryDistributedCache distributedCache = new MemoryDistributedCache(someOptions);

            _controller = new BookController(_bookService, _CommentService, memoryCache, distributedCache);
        }

        [Fact]
        public async Task Should_give_Title_Of_Book_Async()
        {
            // Arrange

            // Act
            var response = await _controller.View(2) as JsonResult;
            var value = response.Value as Book;
            
            //Assert
            Assert.Equal("Brothers", value.Title);
        }

        [Fact]
        public async Task Should_give_number_Of_Book_Async()
        {
            // Arrange

            // Act
            var response = await _controller.ListAll() as JsonResult;
            var value = response.Value as List<Book>;

            //Assert
            Assert.Equal(4, value.Count);
        }

        [Fact]
        public async Task Should_give_Content_Of_Comment_Async()
        {
            // Arrange

            // Act
            var response = await _controller.ViewComment(1) as JsonResult;
            var value = response.Value as List<Comment>;

            //Assert
            Assert.Equal("Bravo", value[0].Content);
        }

        [Fact]
        public async Task Should_give_number_Of_Comments_Async()
        {
            // Arrange
            await _CommentService.AddAsync(new Comment { bookId = 1, Content = "Marvel", CreateDate = DateTime.Now });
            // Act
            var response = await _controller.ViewComment(1) as JsonResult;
            var value = response.Value as List<Comment>;

            //Assert
            Assert.Equal(2, value.Count);
        }
    }
}

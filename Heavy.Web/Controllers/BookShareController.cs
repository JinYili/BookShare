using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BookShare.Web.Data;
using BookShare.Web.Models;
using BookShare.Web.Services;
using BookShare.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookShare.Web.Controllers
{

    public class BookShareController : Controller
    {
        private readonly IBookService _bookService;
        private readonly HtmlEncoder _htmlEncoder;
        private readonly ILogger<BookShareController> _logger;

        public BookShareController(IBookService bookService,
            HtmlEncoder htmlEncoder,
            ILogger<BookShareController> logger)
        {
            _bookService = bookService;
            _htmlEncoder = htmlEncoder;
            _logger = logger;
        }

        // GET: Book
        public async Task<ActionResult> Index()
        {
            var book = await _bookService.GetAllAsync();
            return View(book);
        }

        // GET: Book/Details/5
        public async Task<ActionResult> Details(int id)
        {
            _logger.LogInformation(MyLogEventIds.BookPage, "Visiting Album {0}", id);

            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            var newModel = new BookCreateViewModel();
            return View(newModel);
        }

        // POST: Book/Create
        [HttpPost]
        public async Task<ActionResult> Create(BookCreateViewModel BookCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Model is not valid");
                return View();
            }
            try
            {
                var newModel = await _bookService.AddAsync(new Book
                {
                    Author = _htmlEncoder.Encode(BookCreateViewModel.Artist),
                    Title = BookCreateViewModel.Title,
                    CoverUrl = BookCreateViewModel.CoverUrl,
                    Price = BookCreateViewModel.Price,
                    ReleaseDate = BookCreateViewModel.ReleaseDate
                });
                return RedirectToAction(nameof(Details), new { id = newModel.Id });
            }
            catch
            {
                return View(BookCreateViewModel);
            }
        }

        // GET: Book/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var model = await _bookService.GetByIdAsync(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var editModel = new BookUpdateViewModel
            {
                Artist = model.Author,
                Title = model.Title,
                CoverUrl = model.CoverUrl,
                Price = model.Price,
                ReleaseDate = model.ReleaseDate
            };
            return View(editModel);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, BookUpdateViewModel bookUpdateViewModel)
        {
            var model = await _bookService.GetByIdAsync(id);
            if (model == null)
            {
                // return NotFound();
                return View(bookUpdateViewModel);
            }

            try
            {
                model.Author = bookUpdateViewModel.Artist;
                model.Title = bookUpdateViewModel.Title;
                model.CoverUrl = bookUpdateViewModel.CoverUrl;
                model.ReleaseDate = bookUpdateViewModel.ReleaseDate;
                model.Price = bookUpdateViewModel.Price;
                model.Id = id;
                await _bookService.UpdateAsync(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(bookUpdateViewModel);
            }
        }

        // GET: Book/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _bookService.GetByIdAsync(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // POST: Book/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            var model = await _bookService.GetByIdAsync(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                model.Id = id;
                await _bookService.DeleteAsync(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

 
    }

}

namespace test.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using test.Data;
    using test.Models;

    public class BooksController: Controller
    {
        private readonly BooksData books;

        public BooksController()
        {
            this.books = new BooksData();
        }
        
        public object All1()
        {
            return new List<BookListingModel>
            {
                new BookListingModel{ Id = 1,  Name = "Book1", Author = "Author1"},
                new BookListingModel{ Id = 2,  Name = "Book2", Author = "Author2"},
                new BookListingModel{ Id = 3,  Name = "Book2", Author = "Author3"}
            };
        }

        public IActionResult Create() //Get
        {
            this.ViewData["showError"] = "none";
            return this.View(); //"Books/Create"
        }

        [HttpPost]
        public IActionResult Create(CreateBookModel model) //post
        {
            this.ViewData["showError"] = "none";
            
            if (!ModelState.IsValid)
            {
                this.ViewData["showError"] = "block";
                this.ViewData["error"] = "write the correct data";
                return this.View();
            }

            this.books.Create(model.Title, model.Year, model.Author);

            return this.RedirectToAction(nameof(Create));
        }

        public IActionResult All()
        {
            var result = this.books
                .All()
                .Select(b => new BookListingModel
                {
                    Id = b.Id,
                    Name = b.Title,
                    Author = b.Author
                })
                .ToList();

            return this.View(result);
        }

        public IActionResult Details(int id)
        {
            var book = this.books.GetById(id);

            if (book == null)
            {
                return this.BadRequest(); //NotFound
            }

            return this.View(book);
        }

        public IActionResult Probe()
        {        
            return this.View();
        }
    }
}

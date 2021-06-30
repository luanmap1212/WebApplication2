using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        BookManagerContext context = new BookManagerContext();
        public ActionResult ListBook()
        {
            var listbook = context.Books.ToList();

            return View(listbook);
        }

        [Authorize]
        public ActionResult Buy(int id)
        {
            Book book = context.Books.SingleOrDefault(p=>p.ID == id);

            if(book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        [Authorize]
        public ActionResult CreateBook()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CreateBook([Bind(Include = "ID, Title, Author, Description, Images, Price")]Book book )
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    context.Books.Add(book);
                    context.SaveChanges();
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Error Save Data");
            }

            return RedirectToAction("ListBook","Book");
        }

        
        [Authorize]
        public ActionResult EditBook(int id)
        {
            Book book_e = context.Books.FirstOrDefault(p => p.ID == id);

            return View(book_e);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditBook(int ID, string Title, string Description, string Author, string Images, int Price )
        {
            Book book_e = context.Books.FirstOrDefault(p => p.ID == ID);
            if (book_e != null)
            {
                UpdateModel(book_e);
                context.SaveChanges();
            }

            return RedirectToAction("ListBook", "Book");
        }

        [Authorize]
        public ActionResult DeleteBook(int id)
        {
            Book book = context.Books.SingleOrDefault(p => p.ID == id);

            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteBook(Book book)
        {
            Book book_e = context.Books.FirstOrDefault(p => p.ID == book.ID);
            if (book_e != null)
            {
                context.Books.Remove(book_e);
                context.SaveChanges();
            }

            return RedirectToAction("ListBook", "Book");
        }
    }
}
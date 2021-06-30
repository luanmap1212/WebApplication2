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
        public ActionResult ListBook()
        {
            BookManagerContext context = new BookManagerContext();
            var listBook = context.Books.ToList();
            return View(listBook);
        }

        [Authorize]
        public ActionResult Buy(int id)
        {
            BookManagerContext context = new BookManagerContext();
            Book book = context.Books.SingleOrDefault(p => p.ID == id);
            if(book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        public ActionResult CreateBook()
        {
            BookManagerContext context = new BookManagerContext();
            var listBook = context.Books.ToList();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateBook(Book book, HttpPostedFileBase fileUpload)
        {
            BookManagerContext context = new BookManagerContext();
            var fileName = Path.GetFileName(fileUpload.FileName);
            var path = Path.Combine(Server.MapPath("~/Image"), fileName);
            if(System.IO.File.Exists(path))
            {
                ViewBag.ThongBao = "hinh anh da ton tai";
            }
            else
            {
                fileUpload.SaveAs(path);
            }
            book.Images = fileName;
            var b = new Book();
            context.Books.AddOrUpdate(b);
            context.SaveChanges();
            return View("ListBook", book);
        }
 
        public ActionResult EditBook()
        {
            return View();
        }
        [HttpPost, ActionName("EditBook")]
        [ValidateAntiForgeryToken]
        public ActionResult EditBook(int BookID)
        {
            BookManagerContext context = new BookManagerContext();
            Book dbUpdate = context.Books.FirstOrDefault(p => p.ID == BookID);
            var b = new Book();
            if (dbUpdate != null)
            {
                context.Books.AddOrUpdate(b); //Add or Update Book b 
                context.SaveChanges();
            }
            return View("ListBook");
        }

        public ActionResult DeleteBook()
        {
            return View();
        }

        [HttpPost, ActionName("DeleteBook")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int BookID)
        {
            BookManagerContext context = new BookManagerContext();
            Book dbDelete = context.Books.FirstOrDefault(p => p.ID == BookID);
            var b = new Book();
            if (dbDelete != null)
            {
                context.Books.Remove(b);
                context.SaveChanges();
            }
            return View("ListBook");
        }

        // GET: Book
        public ActionResult Index()
        {
            return View();
        }
    }
}

using BookStoreApp;
using BookStoreApp.DAL;
using BookStoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStoreApp.Controllers
{
    public class BookController : Controller
    {
        public BookRepository repoBook = new BookRepository();

        // GET: Book
        public ActionResult Index()
        {
            var booksList = repoBook.GetAllBooks();
            return View(booksList);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            // pass the Id of the Book
            Book book = repoBook.GetBookById(id);

            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            // This one is for show Create Form
            // generate an object of Book
            return View(new Book());
        }

        // POST: Book/Create
        [HttpPost]
        public ActionResult Create(Book newBook)
        {
            try
            {
                // I want to get access to the method in BookRepository
                // by creating an object

                repoBook.AddBook(newBook);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(newBook);
            }
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            // for edit the Book I should have id
            Book book = repoBook.GetBookById(id);

            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Book editBook)
        {
            // TODO: Add update logic here
            try
            {
                repoBook.UpdateBook(editBook);

                // the moment Book is updated I want to check in table
                // check in the index
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(editBook);
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            Book book = repoBook.GetBookById(id);

            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Book deleteBook)
        {
            try
            {
                // TODO: Add delete logic here
                repoBook.DeleteBook(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;

                // I will add here
                Book book = repoBook.GetBookById(id);

                if (book == null)
                {
                    return HttpNotFound();
                }

                return View(book);
            }
        }
    }
}

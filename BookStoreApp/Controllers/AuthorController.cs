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
    public class AuthorController : Controller
    {
        public AuthorRepository repoAuthor =new AuthorRepository();
        // GET: Author
        public ActionResult Index()
        {
            var authorsList = repoAuthor.GetAllAuthors();
            return View(authorsList);
        }

        // GET: Author/Details/5
        public ActionResult Details(int id)
        {
            // pass the Id of the Author
            Author author = repoAuthor.GetAuthorBYId(id);

            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);           
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            // This one is for show Create Form
            // generate an object of Author
            return View(new Author());
        }

        // POST: Author/Create
        [HttpPost]
        public ActionResult Create(Author newAuthor)
        {
            try
            {
                // I want to get access to the method in AuthorRepository
                // by creating an object

                repoAuthor.AddAuthor(newAuthor);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(newAuthor);
            }
        }

        // GET: Author/Edit/5
        public ActionResult Edit(int id)
        {
            // for edit the Author I should I have id
            Author author = repoAuthor.GetAuthorBYId(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Author/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Author editAuthor)
        {
            
                // TODO: Add update logic here
                try
                {
                    repoAuthor.UpdateAuthor(editAuthor);
                    // the moment Author is updated I want to check in table
                    // check in the index
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                    return View(editAuthor);
                }                
        }
        

        // GET: Author/Delete/5
        public ActionResult Delete(int id)
        {
            Author author = repoAuthor.GetAuthorBYId(id);

            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);        
        }

        // POST: Author/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Author deleteAuthor)
        {
            try
            {
                // TODO: Add delete logic here
                repoAuthor.DeleteAuthor(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            { 
                ViewBag.ErrorMessage = ex.Message;
                // I will add here
                Author author = repoAuthor.GetAuthorBYId(id);

                if (author == null)
                {
                    return HttpNotFound();
                }
                return View(author);

            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Mission06_Kasunick.Models;
using System.Diagnostics;

namespace Mission06_Kasunick.Controllers
{
    public class HomeController : Controller
    {

        private MoviesContext _context;
        public HomeController(MoviesContext temp) //Constructor
        { 
            _context = temp;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Joel()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Movies()
        {

            ViewBag.Categories = _context.Categories
                .ToList();
            
            return View(); 
        }

        [HttpPost]
        public IActionResult Movies(Movies response)
        {
            _context.Movies.Add(response); //Add record to database
            _context.SaveChanges();

            return View("Confirmation", response);
        }

        public IActionResult List()
        {
            var movies = _context.Movies
                .OrderBy(x => x.Title); 

            return View(movies);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var recordToEdit = _context.Movies
                .Single(x => x.MovieId == id);

            ViewBag.Categories = _context.Categories
                .OrderBy(x => x.CategoryName)
                .ToList(); 

            return View("Movies", recordToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Movies updatedInfo)
        {
            _context.Update(updatedInfo);
            _context.SaveChanges(); 

            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var recordToDelete = _context.Movies
                .Single(x => x.MovieId == id);

            return View(recordToDelete); 
        }

        [HttpPost]
        public IActionResult Delete(Movies application)
        {
            _context.Movies.Remove(application);
            _context.SaveChanges();

            return RedirectToAction("List");
        }
    }
}

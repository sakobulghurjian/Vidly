using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            if (User.IsInRole("CanManageMovies"))
                return View("List");

            return View("ReadOnlyList");
        }

        public ActionResult Details(int id)
        {
            var movies = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            if (movies == null)
                return HttpNotFound();

            return View(movies);
        }

        [Authorize(Roles ="CanManageMovies")]
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewmodel = new MovieFormViewModel
            {
                Genres = genres
            };
            return View("MovieForm", viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewmodel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList(),
                };

                return View("MovieForm", viewmodel);
            }


            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.First(m => m.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;

            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewmodel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", viewmodel);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using M2M.Models;

namespace M2M.Controllers
{   
    public class MoviesController : Controller
    {
		private readonly IMovieRepository movieRepository;
        private readonly ITagRepository tagRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public MoviesController() : this(new MovieRepository(), new TagRepository())
        {
        }

        public MoviesController(IMovieRepository movieRepository, ITagRepository tagRepository)
        {
			this.movieRepository = movieRepository;
            this.tagRepository = tagRepository;
        }

        //
        // GET: /Movies/

        public ViewResult Index()
        {
            return View(movieRepository.AllIncluding(movie => movie.Tags));
        }

        //
        // GET: /Movies/Details/5

        public ViewResult Details(int id)
        {
            return View(movieRepository.Find(id));
        }

        //
        // GET: /Movies/Create

        public ActionResult Create()
        {
            var m = new M2M.ViewModels.MovieCreateViewModel();
           
            m.Tags = tagRepository.All.ToList<Tag>();
            return View(m);
        } 

        //
        // POST: /Movies/Create

        [HttpPost]
        public ActionResult Create(Movie movie, FormCollection MovieForm)
        {
            if (ModelState.IsValid) {

                //how to add tags to this movie object? I am getting user selected tag ids posted back 
                //movie.Tags = new List<Tag>();
                foreach (var tag in MovieForm["Tags"].Split(','))
                {
                    var _tag = new Tag { tagID = Convert.ToInt32(tag)};
                    
                    //movie.Tags is null
                   
                    movie.Tags.Add(_tag);                   
                }
                
                movieRepository.InsertOrUpdate(movie);
                movieRepository.Save();
                return RedirectToAction("Index");
            } else {
                var m = new M2M.ViewModels.MovieCreateViewModel();

                m.Tags = tagRepository.All.ToList<Tag>();
                return View(m);
			}
        }
        
        //
        // GET: /Movies/Edit/5
 
        public ActionResult Edit(int id)
        {
            var model = new M2M.ViewModels.MovieCreateViewModel();
            var eMovie = movieRepository.Find(id);
            
            model.Movie = eMovie;
            model.Tags = eMovie.Tags.ToList<Tag>();
            return View(model);
        }

        //
        // POST: /Movies/Edit/5

        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid) {
                movieRepository.InsertOrUpdate(movie);
                movieRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Movies/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(movieRepository.Find(id));
        }

        //
        // POST: /Movies/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            movieRepository.Delete(id);
            movieRepository.Save();

            return RedirectToAction("Index");
        }
    }
}


using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Data.Objects;

namespace M2M.Models
{ 
    public class MovieRepository : IMovieRepository
    {
        M2MContext context = new M2MContext();

        public IQueryable<Movie> All
        {
            get { return context.Movies; }
        }

        public IQueryable<Movie> AllIncluding(params Expression<Func<Movie, object>>[] includeProperties)
        {
            IQueryable<Movie> query = context.Movies;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Movie Find(int id)
        {
            return context.Movies.Find(id);
        }

        public void InsertOrUpdate(Movie movie)
        {
            if (movie.MovieID == default(int)) {
                // New entity
                //to add realtion between this new movie and all existed tags in db we need to set state of 
                //each tag to Unchanged otherwise it will insert new tags into db with auto genrated Ids and 
                //other colums set to null/empty
                foreach (var tag in movie.Tags)
                {
                    context.Entry(tag).State = EntityState.Unchanged;
                }
                context.Movies.Add(movie);
            } else {
                // Existing entity
                context.Entry(movie).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var movie = context.Movies.Find(id);
            context.Movies.Remove(movie);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }

    public interface IMovieRepository
    {
        IQueryable<Movie> All { get; }
        IQueryable<Movie> AllIncluding(params Expression<Func<Movie, object>>[] includeProperties);
        Movie Find(int id);
        void InsertOrUpdate(Movie movie);
        void Delete(int id);
        void Save();
    }
}
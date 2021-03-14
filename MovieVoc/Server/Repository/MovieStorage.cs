using Microsoft.EntityFrameworkCore;
using MovieVoc.Shared.DTOs;
using MovieVoc.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieVoc.Server.Repository
{
    public class MovieStorage : IMovieStorage
    {

        private readonly ApplicationDbContext db;
        public MovieStorage(ApplicationDbContext db)
        {
            this.db = db;
        }


        public async Task<int> addMovie(Movie movie)
        {
            db.Add(movie);
            await db.SaveChangesAsync();
            return movie.Id;

        }

        public async Task<Movie> getMovie(int id)
        {
            return await db.Movies.FirstOrDefaultAsync(x => x.Id == id);
        }

       public async  Task<List<Movie>> searchMovie(string searchText)
        {
            return await db.Movies.Where(x => x.Title.Contains(searchText)).Take(5).ToListAsync();
        }
    }
}

using MovieVoc.Shared.DTOs;
using MovieVoc.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieVoc.Server.Repository
{
    public interface IMovieStorage
    {
        Task<int> addMovie(Movie movie);

        Task<Movie> getMovie(int id);

        Task<List<Movie>> searchMovie(string searchText);

    }
}

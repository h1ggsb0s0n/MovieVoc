using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieVoc.Shared.DTOs;
using MovieVoc.Shared.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MovieVoc.Server.Repository;

namespace MovieVoc.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController: ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMovieStorage movieStorage;

        public MovieController(IMapper mapper, IMovieStorage movieStorage)
        {
            this.mapper = mapper;
            this.movieStorage = movieStorage;
        }

        /// <summary>
        ///  Add Movie
        /// </summary>
        /// <param name="movieDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> AddMovie(MovieDTO movieDto)
        {
            
            Movie movieForDB = new Movie();
            movieForDB = mapper.Map(movieDto, movieForDB);
            int movieId = await movieStorage.addMovie(movieForDB);
            return Ok(movieId);

        }


        /// <summary>
        /// Get Movie
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("movie/{id}")]
        public async Task<ActionResult<Movie>> GetMovieInDb(int id)
        {
            Movie movie = await movieStorage.getMovie(id);
            if (movie == null) { return NotFound(); }

            return movie;
        }


        /// <summary>
        /// Search Movie
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        [HttpGet("search/{searchText}")]
        public async Task<ActionResult<List<MovieDTO>>> SearchMovieInDB(string searchText)
        {
            List<MovieDTO> reval = new List<MovieDTO>();
            if (!string.IsNullOrWhiteSpace(searchText))
            {

                List<Movie> dbResult = await movieStorage.searchMovie(searchText);

                foreach (Movie movie in dbResult)
                {
                    MovieDTO movieDTO = new MovieDTO();
                    reval.Add(mapper.Map(movie, movieDTO));
                }
            }

            return reval;
        }



    }
}

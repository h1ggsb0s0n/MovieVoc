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


        /* This code works:
        /// <summary>
        ///  Add Movie
        /// </summary>
        /// <param name="movieDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<int>> Post(MovieDTO movieDto)
        {
            Movie movieForDB = new Movie();
            movieForDB = mapper.Map(movieDto, movieForDB);
            db.Add(movieForDB);
            await db.SaveChangesAsync();
            return movieForDB.Id;
        }*/



        /// <summary>
        ///  Add Movie
        /// </summary>
        /// <param name="movieDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<int>> Post(MovieDTO movieDto)
        {
            Movie movieForDB = new Movie();
            movieForDB = mapper.Map(movieDto, movieForDB);
            return movieStorage.addMovie(movieForDB).Result;

        }



        /*  This code works
        /// <summary>
        /// Get Movie
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("movie/{id}")]
        public async Task<ActionResult<Movie>> GetMovieInDb(int id)
        {
            Movie movie = await db.Movies.FirstOrDefaultAsync(x => x.Id == id);

            if (movie == null) { return NotFound(); }

            return movie;
        }*/



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



        /* This code works:
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

                List<Movie> dbResult = await db.Movies.Where(x => x.Title.Contains(searchText))
                    .Take(5)
                    .ToListAsync();

                foreach (Movie movie in dbResult)
                {
                    MovieDTO movieDTO = new MovieDTO();
                    reval.Add(mapper.Map(movie, movieDTO));
                }
            }

            return reval;
        }*/


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



        /*
        [HttpPost]
        public async Task<ActionResult<int>> Post(VocabularyDTO wordsDTO)
        {
            Movie movie = db.Movies.First(mv => mv.Id == wordsDTO.MovieId);

        

            foreach (Word word in wordsDTO.ListOfWords)
            {

                //todo: check if word already exists
                //Add word intto Word Table

                db.Add(word);

                var moviesWords = new MoviesWords
                {
                    Movie = movie,
                    Word = word
                };

                db.Add(moviesWords);

            }

            await db.SaveChangesAsync();
            return movie.Id;

        }*/



    }
}

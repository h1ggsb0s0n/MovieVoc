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

namespace MovieVoc.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController: ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public MovieController(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<int>> Post(MovieDTO movieDto)
        {
            Movie movieForDB = new Movie();
            movieForDB = mapper.Map(movieDto, movieForDB);
            db.Add(movieForDB);
            await db.SaveChangesAsync();
            return movieForDB.Id;
        }


        [HttpGet("movie/{id}")]
        public async Task<ActionResult<Movie>> GetMovieInDb(int id)
        {
            Movie movie = await db.Movies.FirstOrDefaultAsync(x => x.Id == id);

            if (movie == null) { return NotFound(); }

            return movie;
        }


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

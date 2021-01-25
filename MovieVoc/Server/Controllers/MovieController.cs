using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieVoc.Shared.DTOs;
using MovieVoc.Shared.Entities;
using AutoMapper;

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
        public async Task<ActionResult<int>> Post(MovieDTO movieDto)
        {
            Movie movieForDB = new Movie();
            movieForDB = mapper.Map(movieDto, movieForDB);
            db.Add(movieForDB);
            await db.SaveChangesAsync();
            return movieForDB.Id;
        }


        [HttpPost]
        public async Task<ActionResult<int>> Post(WordsDTO wordsDTO)
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

        }



    }
}

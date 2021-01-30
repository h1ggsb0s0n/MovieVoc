using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieVoc.Shared.DTOs;
using MovieVoc.Shared.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace MovieVoc.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VocabularyController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public VocabularyController(ApplicationDbContext db)
        {
            this.db = db;
        }


        [HttpPost]
        public async Task<ActionResult<int>> Post(VocabularyDTO voc)
        {
            Movie movie = db.Movies.First(mv => mv.Id == voc.MovieId);

            foreach (Word word in voc.ListOfWords)
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


        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<MovieVocabularyDTO>> Get(int id)
        {
            throw new NotImplementedException();

        }

    }
}

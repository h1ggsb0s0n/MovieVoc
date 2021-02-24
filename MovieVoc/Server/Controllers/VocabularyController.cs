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
    public class VocabularyController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public VocabularyController(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
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

        [HttpGet("search/{searchText}")]
        public async Task<ActionResult<List<WordDTO>>> SearchWordInDB(string searchText)
        {
            List<WordDTO> reval = new List<WordDTO>();
            if (!string.IsNullOrWhiteSpace(searchText))
            {

                List<Word> dbResult = await db.Words.Where(x => x.EnglischWord.Contains(searchText))
                    .Take(5)
                    .ToListAsync();

                foreach (Word word in dbResult)
                {
                    WordDTO wordDTO = new WordDTO();
                    reval.Add(mapper.Map(word, wordDTO));
                }
            }

            return reval;
        }





        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<MovieVocabularyDTO>> Get(int id)
        {
            throw new NotImplementedException();

        }

    }
}

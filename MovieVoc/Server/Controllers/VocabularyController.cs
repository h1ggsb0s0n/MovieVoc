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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> AddVocabulary(VocabularyDTO voc)
        {
            //Movie movie = db.Movies.First(mv => mv.Id == voc.MovieId);
            Movie movie = db.Movies.Include(p => p.MoviesWords).Single(mv => mv.Id == voc.MovieId);
            int numberOfWordsAdded = 0; 
            foreach (WordDTO wordDTO in voc.ListOfWords)
            {
                Word word = db.Words.Single(w => w.Id == wordDTO.Id);

                var moviesWords = new MoviesWords
                {
                    Movie = movie,
                    Word = word
                };
                try
                {
                    //movie.MoviesWords.Add(moviesWords);
                    //movie.MoviesWords.Add(moviesWords);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
              
                numberOfWordsAdded++;

            }

            try
            {
                await db.SaveChangesAsync();
                

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            return numberOfWordsAdded;

        }

        [HttpGet("search/{searchText}")]
        [Authorize(Roles = "Admin")]
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

        [HttpGet("learn/test/{movieId}")]
        public async Task<ActionResult<List<VocWord>>> GetVocabulary(int movieId)
        {
            List<VocWord> reval = new List<VocWord>();
            if (movieId > 0)
            {
                List<Word> dbResult = await db.Words.ToListAsync();

                foreach (Word word in dbResult)
                {
                    VocWord vocWord = new VocWord();
                    reval.Add(mapper.Map(word, vocWord));
                }
            }

            return reval;
        }



        [HttpGet("learn/test/{movieId}/{difficultylevel}")]
        public async Task<ActionResult<List<VocWord>>> GetVocabulary(int movieId, int difficultylevel)
        {
            List<VocWord> reval = new List<VocWord>();
            if (movieId > 0)
            {
                List<Word> dbResult = await db.Words.Where(w => w.DifficultyLevel == difficultylevel).ToListAsync();

                foreach (Word word in dbResult)
                {
                    VocWord vocWord = new VocWord();
                    reval.Add(mapper.Map(word, vocWord));
                }
            }

            return reval;
        }



    }
}

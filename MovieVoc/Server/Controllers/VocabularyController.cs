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
    public class VocabularyController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        private readonly IMovieStorage movieStorage;

        public VocabularyController(ApplicationDbContext db, IMapper mapper, IMovieStorage movieStorage)
        {
            this.db = db;
            this.mapper = mapper;
            this.movieStorage = movieStorage;
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
                    movie.MoviesWords.Add(moviesWords);

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
                return BadRequest("Sie können keine Wörter zufügen welche schon zugefügt worden sind");
            }

            return Ok(numberOfWordsAdded);

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


        /// <summary>
        /// Todo: Diese Funktion ist nicht gut geschrieben.
        /// Eine neue Migration der Datenbank ist nötig um diese Methode sauber zu machen.
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        [HttpGet("learn/test/{movieId}")]
        public async Task<ActionResult<List<VocWord>>> GetVocabulary(int movieId)
        {
            List<VocWord> reval = new List<VocWord>();
            if (movieId > 0)
            {

                Movie movie = await db.Movies.Where(m => m.Id == movieId)
                .Include(m => m.MoviesWords).ThenInclude(mw => mw.Word)
                .FirstOrDefaultAsync();

                foreach (MoviesWords mw in movie.MoviesWords)
                {
                    VocWord vocWord = new VocWord();
                    reval.Add(mapper.Map(mw.Word, vocWord));
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

                Movie movie = await db.Movies.Where(m => m.Id == movieId)
                .Include(m => m.MoviesWords).ThenInclude(mw => mw.Word)
                .Where(m => m.MoviesWords.Any(mw => mw.Word.DifficultyLevel == difficultylevel))
                .FirstOrDefaultAsync();

                foreach (MoviesWords mw in movie.MoviesWords)
                {
                    VocWord vocWord = new VocWord();
                    reval.Add(mapper.Map(mw.Word, vocWord));
                }
            }

            return reval;

        }

    }
}

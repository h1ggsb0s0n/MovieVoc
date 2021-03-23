using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieVoc.Shared.DTOs;
using MovieVoc.Shared.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MovieVoc.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WordController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public WordController(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> AddWordsToDB(List<WordDTO> words)
        {
            int numberOfWordsAdded = 0;
            foreach (WordDTO word in words)
            {
                Word newWord = new Word();
                mapper.Map(word, newWord);
                //todo: check if word already exists
                db.Add(newWord);
            }

            await db.SaveChangesAsync();
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

        



    }
}

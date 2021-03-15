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
    [Authorize(Roles = "Admin")]
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
        public async Task<ActionResult<int>> Post(List<Word> words)
        {

            foreach (Word word in words)
            {
                //todo: check if word already exists
                //Add word intto Word Table
                db.Add(word);
            }

            await db.SaveChangesAsync();
            //return word.Id;
            return 1;

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

        



    }
}

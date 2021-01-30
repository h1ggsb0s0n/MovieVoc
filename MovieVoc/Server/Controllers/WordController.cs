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
    public class WordController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public WordController(ApplicationDbContext db)
        {
            this.db = db;
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



    }
}

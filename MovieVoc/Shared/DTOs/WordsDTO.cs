﻿using MovieVoc.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieVoc.Shared.DTOs
{
    public class WordsDTO
    {
        public int MovieId { get; set; }
        public List<Word> ListOfWords { get; set; }

    }
}
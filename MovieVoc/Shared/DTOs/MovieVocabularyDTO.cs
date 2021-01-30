using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieVoc.Shared.Entities;

namespace MovieVoc.Shared.DTOs
{
    public class MovieVocabularyDTO
    {
        public Movie movie { get; set; }

        public List<Word> vocabulary { get; set; }

    }
}

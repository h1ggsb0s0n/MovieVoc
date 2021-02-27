using MovieVoc.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieVoc.Shared.DTOs
{
    public class VocabularyDTO
    {
        public int MovieId { get; set; }
        public List<WordDTO> ListOfWords { get; set; }

    }
}

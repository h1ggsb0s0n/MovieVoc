using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieVoc.Shared.DTOs
{
    public class WordDTO
    {
        public int Id { get; set; }
        public string EnglischWord { get; set; }
        public string Translation { get; set; }
        public int DifficultyLevel { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieVoc.Shared.Entities
{
    public class Word
    {

        public int Id { get; set; }
        [Required]
        public string EnglischWord { get; set; }
        public string Translation { get; set; }
        public int DifficultyLevel { get; set; }
        public List<MoviesWords> MoviesWords { get; set; } = new List<MoviesWords>();

    }
}

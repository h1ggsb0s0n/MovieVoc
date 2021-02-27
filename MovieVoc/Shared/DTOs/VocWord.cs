using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieVoc.Shared.DTOs
{
    public class VocWord
    {

        public string EnglischWord { get; set; }
        public string Translation { get; set; }
        public int DifficultyLevel { get; set; }
        public int numberOfTries { get; set; }
        public bool correct { get; set; }

    }
}

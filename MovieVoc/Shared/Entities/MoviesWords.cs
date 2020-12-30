using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieVoc.Shared.Entities
{
    public class MoviesWords
    {

        public int MovieId { get; set; }
        public int WordId { get; set; }
        public Movie Movie { get; set; }
        public Word Word { get; set; }
    }
}

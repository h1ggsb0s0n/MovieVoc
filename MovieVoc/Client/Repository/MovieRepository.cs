using MovieVoc.Client.Helpers;
using MovieVoc.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieVoc.Client.Repository
{
    public class MovieRepository
    {
        private readonly IHttpService httpService;
        private string url = "api/movies";

        public MovieRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        

    }
}

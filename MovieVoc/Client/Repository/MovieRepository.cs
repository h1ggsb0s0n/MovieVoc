using MovieVoc.Client.Helpers;
using MovieVoc.Shared.Entities;
using MovieVoc.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieVoc.Client.Repository
{
    public class MovieRepository
    {
        private readonly IHttpService httpService;
        private string url = "api/movie";

        public MovieRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<int> CreateMovie(MovieDTO movie)
        {
            var response = await httpService.Post<MovieDTO, int>(url, movie);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }


        

    }
}

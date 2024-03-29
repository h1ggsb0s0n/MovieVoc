﻿using MovieVoc.Client.Helpers;
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

        public async Task<HttpResponseWrapper<int>> CreateMovie(MovieDTO movie)
        {
            var response = await httpService.Post<MovieDTO, int>(url, movie);
            /*
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }*/

            return response;
        }


        public async Task<MovieDTO> GetMovie(int id)
        {
            var response = await httpService.Get<MovieDTO>($"{url}/movie/{id}", includeToken: false);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }


        public async Task<List<MovieDTO>> GetMovieSuggestions(string movieName)
        {
            var response = await httpService.Get<List<MovieDTO>>($"{url}/search/{movieName}", includeToken: false);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }




    }
}

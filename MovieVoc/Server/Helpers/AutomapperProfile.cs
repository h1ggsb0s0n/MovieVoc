using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MovieVoc.Shared.Entities;
using MovieVoc.Shared.DTOs;



namespace MovieVoc.Server.Helpers
{
    public class AutomapperProfile: Profile
    {

        public AutomapperProfile()
        {
            CreateMap<MovieDTO, Movie>();
            CreateMap<Movie, MovieDTO>();
            CreateMap<Word, WordDTO>();
            CreateMap<Movie, MovieLightDTO>();
        }

    }
}

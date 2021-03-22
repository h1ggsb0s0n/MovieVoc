using MovieVoc.Client.Helpers;
using MovieVoc.Shared.DTOs;
using MovieVoc.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieVoc.Client.Repository
{
    public class WordRepository
    {

        private readonly IHttpService httpService;
        private string url = "api/word";

        public WordRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<int> AddWords(List<WordDTO> words)
        {
            var response = await httpService.Post<List<WordDTO>, int>(url, words);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task<List<WordDTO>> GetWordSuggestions(string wordName)
        {
            var response = await httpService.Get<List<WordDTO>>($"{url}/search/{wordName}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

    }
}

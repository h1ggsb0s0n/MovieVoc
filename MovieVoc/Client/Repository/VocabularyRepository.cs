using MovieVoc.Client.Helpers;
using MovieVoc.Shared.DTOs;
using MovieVoc.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieVoc.Client.Repository
{
    public class VocabularyRepository
    {

        private readonly IHttpService httpService;
        private string url = "api/vocabulary";

        public VocabularyRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<int> AddVocabularyToMovie(VocabularyDTO voc)
        {
            var response = await httpService.Post<VocabularyDTO, int>(url, voc);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }

            return response.Response;
        }

        public async Task<List<Word>> GetWordSuggestions(string name)
        {
            var response = await httpService.Get<List<Word>>($"{url}/search/{name}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

    }
}

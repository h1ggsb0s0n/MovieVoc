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

        public async Task<List<WordDTO>> GetWordSuggestions(string name)
        {
            var response = await httpService.Get<List<WordDTO>>($"{url}/search/{name}");
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<List<VocWord>> getVocFromMovie(int movieID)
        {
            var response = await httpService.Get<List<VocWord>>($"{url}/learn/test/{movieID}", includeToken: false);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

        public async Task<List<VocWord>> getVocFromMovie(int movieID, int difficultyLevel)
        {
            var response = await httpService.Get<List<VocWord>>($"{url}/learn/test/{movieID}/{difficultyLevel}", includeToken: false);
            if (!response.Success)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }

    }
}

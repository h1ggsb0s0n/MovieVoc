using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieVoc.Client.Helpers
{
    public class HttpResponseWrapper<T>
    {
        public HttpResponseWrapper(T response, bool success, HttpResponseMessage httpResponseMessage)
        {
            Success = success;
            Response = response;
            HttpResponseMessage = httpResponseMessage;
        }

        public bool Success { get; set; }
        public T Response { get; set; }
        public HttpResponseMessage HttpResponseMessage { get; set; }

        /// <summary>
        /// Gibt den Body des HTTP Rreq
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetBody()
        {
            return await HttpResponseMessage.Content.ReadAsStringAsync();
        }
    }
}

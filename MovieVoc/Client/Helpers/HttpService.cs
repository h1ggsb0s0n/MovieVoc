﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieVoc.Client.Helpers
{
    public class HttpService : IHttpService
    {
        //Identity Server 4
        private readonly HttpClientWithToken httpClientWithToken;
        private readonly HttpClientWithoutToken httpClientWithoutToken;


        private JsonSerializerOptions defaultJsonSerializerOptions =>
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        public HttpService(HttpClientWithToken httpClientWithToken, HttpClientWithoutToken httpClientWithoutToken)
        {
            this.httpClientWithToken = httpClientWithToken;
            this.httpClientWithoutToken = httpClientWithoutToken;
        }

        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T data)
        {
            var httpClient = GetHttpClient();
            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, stringContent);
            return new HttpResponseWrapper<object>(null, response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T data, bool includeToken = true)
        {

            var httpClient = GetHttpClient(includeToken);
            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, stringContent);
            if (response.IsSuccessStatusCode)
            {
                var responseDeserialized = await Deserialize<TResponse>(response, defaultJsonSerializerOptions);
                return new HttpResponseWrapper<TResponse>(responseDeserialized, true, response);
            }
            else
            {
                return new HttpResponseWrapper<TResponse>(default, false, response);
            }
        }

        public async Task<HttpResponseWrapper<T>> Get<T>(string url, bool includeToken = true)
        {
            var httpClient = GetHttpClient(includeToken);
            var responseHTTP = await httpClient.GetAsync(url);
            if (responseHTTP.IsSuccessStatusCode)
            {
                var response = await Deserialize<T>(responseHTTP, defaultJsonSerializerOptions);
                return new HttpResponseWrapper<T>(response, true, responseHTTP);
            }
            else
            {
                Console.WriteLine("no success");
                return new HttpResponseWrapper<T>(default, false, responseHTTP);
            }
        }


        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseString, options);
        }




        private HttpClient GetHttpClient(bool includeToken = true)
        {
            if (includeToken)
            {
                return httpClientWithToken.HttpClient;
            }
            else
            {
                return httpClientWithoutToken.HttpClient;
            }
        }

    }
}

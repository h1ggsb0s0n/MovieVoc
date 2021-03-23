using MatBlazor;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieVoc.Client.Helpers;
using MovieVoc.Client.Repository;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MatBlazor;

namespace MovieVoc.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient<HttpClientWithToken>(
                 client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                 .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();//Schmeisst das Token an den Header

            builder.Services.AddHttpClient<HttpClientWithoutToken>(
              client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            builder.Services.AddScoped<IHttpService, HttpService>();
            builder.Services.AddScoped<MovieRepository>();
            builder.Services.AddScoped<WordRepository>();
            builder.Services.AddScoped<VocabularyRepository>();
            builder.Services.AddMatBlazor();
            builder.Services.AddApiAuthorization();
            builder.Services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomRight;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 95;
                config.VisibleStateDuration = 6000;
            });
            await builder.Build().RunAsync();
            
        }
    }
}

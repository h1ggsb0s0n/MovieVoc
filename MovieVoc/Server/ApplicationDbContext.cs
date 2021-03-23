using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MovieVoc.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieVoc.Server
{
    public class ApplicationDbContext: ApiAuthorizationDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        /// <summary>
        /// Funktion um Composit - Primaerschluessel für die Schnittstelle einer Many-Many Relationship zu bestimmen
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MoviesWords>().HasKey(x => new { x.MovieId, x.WordId });
            base.OnModelCreating(modelBuilder);
        }



        public DbSet<Movie> Movies { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<MoviesWords> MoviesWords { get; set; }

    }
}

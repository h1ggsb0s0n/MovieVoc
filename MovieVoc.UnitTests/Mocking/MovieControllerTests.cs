using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using MovieVoc.Server.Repository;
using MovieVoc.Server.Controllers;
using AutoMapper;
using MovieVoc.Server.Helpers;
using System.Threading.Tasks;
using MovieVoc.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using MovieVoc.Shared.DTOs;
using MovieVoc.UnitTests.Helpers;

namespace MovieVoc.UnitTests.Mocking
{
    [TestFixture]
    public class MovieControllerTests
    {
        IMapper mapper;
        Mock<IMovieStorage> storage;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile(new AutomapperProfile());
            });

            mapper = config.CreateMapper();
            storage = new Mock<IMovieStorage>();
        }



        /// <summary>
        /// Beim Blazor Server wird Model Class Validation angewendet. Durch den Server wird ein Status: 400 Bad Request zurückgegeben mir dem Folgenden Test.
        /// One or more validation errors occurred.
        /// Um die Serverseitige Validierung zu prüfen wurde der Folgende Test geschrieben.
        /// </summary>
        /// <returns></returns>
        ///

        [Test]
        public void AddMovie_UnvalidMovieDTO_TitleIsNotRequired()
        {
            CheckPropertyValidation cpv = new CheckPropertyValidation();
            MovieDTO movie = new MovieDTO
            {
                Id = 0,
                ReleaseDate = null,
                Title = "N",//zu kurz
                Summary = "",//zu kurz
                Poster = null,

            };
            var errorCount = cpv.myValidation(movie).Count;
            Assert.AreEqual(2, errorCount);
        }



        [Test]
        public async Task AddMovie_WhenCalled_AddMovieToDb()
        {
            
            var controller = new MovieController(mapper, storage.Object);
            MovieDTO movie = new MovieDTO
            {
                Id = 0, 
                ReleaseDate = new DateTime(2010,10,10),
                Title = "New Title",
                Summary = "Das ist ein Titel",
                Poster = null,

            };

            var result = await controller.AddMovie(movie);

            storage.Verify(s => s.addMovie(It.IsAny<Movie>()));
        }


        public async Task addMovie_WhenCalled_TestAuthorization()
        {
            var controller = new MovieController(mapper, storage.Object);

            var mock = new Mock<ControllerContext>();
            
            mock.SetupGet(x => x.HttpContext.Request.authenticated).Returns(true);
            controller.ControllerContext = mock.Object;


        }



        [Test]
        public async Task getMovie_WhenCalled_ReturnNotFound()
        {
            Mock<IMovieStorage> movieStorage = new Mock<IMovieStorage>();
            movieStorage.Setup(s => s.getMovie(1)).ReturnsAsync((Movie)null);
            var controller = new MovieController(mapper, movieStorage.Object);

            var result = await controller.GetMovieInDb(1);
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }


        /*
        [Test]
        public async Task getMovie_WhenCalled_ReturnMovie()
        {
            /
            Mock<IMovieStorage> movieStorage = new Mock<IMovieStorage>();
            movieStorage.Setup(s => s.getMovie(1)).ReturnsAsync((Movie)null);
            var controller = new MovieController(mapper, storage.Object);

            await controller.GetMovieInDb(1);
        }*/

        [Test]
        public async Task getMovie_WhenCalled_ReturnNotFound()
        {
            Mock<IMovieStorage> movieStorage = new Mock<IMovieStorage>();
            movieStorage.Setup(s => s.getMovie(1)).ReturnsAsync((Movie)null);
            var controller = new MovieController(mapper, movieStorage.Object);

            var result = await controller.GetMovieInDb(1);
            // implicit opertator to convert to/from Value/Result
            //ActionResult<TValue> implement the interface IConvertToActionResult
            //and provide Convert method that handle both Result and Value based on their null values
            //return the non null value. Either Result or Value is set.
            Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());
        }

    }

    }

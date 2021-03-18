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

        [Test]
        public void AddMovie_WhenCalled_AddMovieToDb()
        {
            
            var controller = new MovieController(mapper, storage.Object);

            //controller.Post();

            //storage.Verify(s => s.DeleteEmployee(1));
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

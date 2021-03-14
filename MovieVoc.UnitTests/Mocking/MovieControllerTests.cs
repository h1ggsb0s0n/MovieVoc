using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using MovieVoc.Server.Repository;
using MovieVoc.Server.Controllers;

namespace MovieVoc.UnitTests.Mocking
{
    [TestFixture]
    public class MovieControllerTests
    {
        [Test]
        public void AddMovie_WhenCalled_AddMovieToDb()
        {
            var storage = new Mock<IMovieStorage>();
            //var controller = new MovieController(storage.Object);
            //var controller = new MovieController(storage.Object);

        }

    }
}

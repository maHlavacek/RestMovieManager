using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieManager.Web.ApiControllers;
using MovieManager.Web.DataTransferObjects;
using MovieManager.Web.Test.Fakes;

namespace MovieManager.Web.Test
{
    [TestClass]
    public class MoviesControllerTests
    {
        private MoviesController _moviesController;

        [TestInitialize()]
        public void InitializeTest()
        {
            _moviesController = new MoviesController(new UnitOfWorkFake());
        }

        [TestMethod]
        public void Get_WhenCall_ShouldReturnOkResult()
        {
            // Act
            var okResult = _moviesController.GetMovies();

            // Assert
            Assert.IsInstanceOfType(okResult, typeof(ActionResult<MovieDto[]>));
            Assert.IsInstanceOfType(okResult.Value, typeof(MovieDto[]));
        }

        [TestMethod]
        public void Get_WhenCall_ShouldReturnAllMovies()
        {
            // Act
            var okResult = _moviesController.GetMovies();


            // Assert
            Assert.IsNotNull(okResult);
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult?.Value, typeof(MovieDto[]));
            Assert.AreEqual(3, ((MovieDto[])okResult?.Value).Length);
        }

        [TestMethod]
        public void GetMovie_CallWithValidId_ShouldReturnCorrectMovie()
        {
            // Act
            var okResult = _moviesController.GetMovie(1);


            // Assert
            Assert.IsNotNull(okResult);
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult?.Value, typeof(MovieDto));
            Assert.AreEqual("Movie 1", okResult.Value.Title);
        }

        [TestMethod]
        public void GetMovie_CallWithInValidId_ShouldReturnNotFoundResult()
        {
            // Act
            var badResult = _moviesController.GetMovie(-1);


            // Assert
            Assert.IsNotNull(badResult);
            Assert.IsInstanceOfType(badResult.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public void AddMovie_CallWithModelValidationError_ShouldReturnBadRequestObjectResult()
        {
            // Arrange
            MovieDto movieWithMissingTitle = new MovieDto()
            {
                // Title is missing
                Duration = 130,
                Year = 1999
            };

            _moviesController.ModelState.AddModelError("Title", "Required");

            // Act
            var badResponse = _moviesController.AddMovie(movieWithMissingTitle);

            // Assert
            Assert.IsInstanceOfType(badResponse.Result, typeof(BadRequestObjectResult));
        }


        [TestMethod]
        public void AddMovie_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            string title = "Rambo";
            int duration = 120;
            int year = 1999;


            MovieDto validMovie = new MovieDto()
            {
                Title = title,
                Duration = duration,
                Year = year
            };

            // Act
            var createdResponse = _moviesController.AddMovie(validMovie);

            // Assert
            Assert.IsInstanceOfType(createdResponse.Result, typeof(CreatedAtActionResult));
        }


        [TestMethod]
        public void AddMovie_ValidObjectPassed_ReturnedResponseContainsCreatedItem()
        {
            // Arrange
            string title = "Rambo";
            int duration = 120;
            int year = 1999;


            MovieDto validMovie = new MovieDto()
            {
                Title = title,
                Duration = duration,
                Year = year
            };

            // Act
            var createdResponse = _moviesController.AddMovie(validMovie) as ActionResult<MovieDto>;
            var result = createdResponse.Result as CreatedAtActionResult;
            var movie = result?.Value as MovieDto;

            // Assert
            Assert.IsNotNull(movie);
            Assert.IsInstanceOfType(movie, typeof(MovieDto));
            Assert.AreEqual(title, movie.Title);
            Assert.AreEqual(duration, movie.Duration);
            Assert.AreEqual(year, movie.Year);
        }





        [TestMethod]
        public void GetCategoryByMovieId_CallWithNoneExistingMovieId_ShouldReturnNotFoundResult()
        {
            // Act
            var badResponse = _moviesController.GetCategoryByMovieId(-1);

            // Assert
            Assert.IsInstanceOfType(badResponse.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public void GetCategoryByMovieId_CallWithExistingMovieId_ShouldReturnTheCategory()
        {
            // Act
            var okResponse = _moviesController.GetCategoryByMovieId(1);

            // Assert
            Assert.IsNotNull(okResponse);
            Assert.IsNotNull(okResponse.Value);
            Assert.IsInstanceOfType(okResponse, typeof(ActionResult<CategoryDto>));
            Assert.IsInstanceOfType(okResponse.Value, typeof(CategoryDto));
        }

        [TestMethod]
        public void UpdateMovie_CallWithValidationError_ShouldReturnBadRequestObjectResult()
        {
            // Arrange
            MovieDto movie = new MovieDto()
            {
                // Missing CategoryId
                Title = "Movie 99",
                Duration = 120,
                Year = 1965
            };

            _moviesController.ModelState.AddModelError("CategoryId", "Required");

            // Act
            var badResponse = _moviesController.UpdateMovie(1, movie);

            // Assert
            Assert.IsInstanceOfType(badResponse, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void UpdateMovie_CallWithValidParameters_ShouldReturnNoContentResult()
        {
            // Arrange
            MovieDto movie = new MovieDto()
            {
                Title = "Movie 99",
                Duration = 120,
                Year = 1965,
                CategoryId = 1
            };

            // Act
            var updateResponse = _moviesController.UpdateMovie(1, movie);

            // Assert
            Assert.IsInstanceOfType(updateResponse, typeof(NoContentResult));
        }

        [TestMethod]
        public void DeleteMovie_CallWithNoneExistingCategoryId_ShouldReturnNotFoundResult()
        {
            // Act
            var badResponse = _moviesController.DeleteMovie(99);

            // Assert
            Assert.IsInstanceOfType(badResponse, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteMovie_CallWithValidParameters_ShouldReturnNoContentResult()
        {

            // Act
            var updateResponse = _moviesController.DeleteMovie(1);

            // Assert
            Assert.IsInstanceOfType(updateResponse, typeof(NoContentResult));
        }
    }
}

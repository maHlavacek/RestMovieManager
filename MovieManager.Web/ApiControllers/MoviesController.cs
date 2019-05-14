using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManager.Core.Contracts;
using MovieManager.Core.Entities;
using MovieManager.Web.DataTransferObjects;
using System;
using System.Linq;

namespace MovieManager.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MoviesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Liefert alle existierenden Filme
        /// </summary>
        /// <response code="200">Die Abfrage war erfolgreich.</response>
        // GET: api/Movies
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<MovieDto[]> GetMovies()
        {
            var movies = _unitOfWork.MovieRepository.GetAll();
            return movies.Select(m => new MovieDto(m)).ToArray();
        }


        /// <summary>
        /// Liert einen spezifischen Film
        /// </summary>
        /// <param name="id">Die Id des Films</param>
        /// <response code="200">Die Abfrage war erfolgreich.</response>
        /// <response code="400">Die übergebene Id konnten nicht verarbeitet werden!</response>
        /// <response code="404">Unbekannte Id!</response>
        // GET: api/Movies/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MovieDto> GetMovie(int id)
        {
            var movie = _unitOfWork.MovieRepository.GetById(id);
            if(movie == null)
            {
                return NotFound();
            }
            return new MovieDto(movie);
        }

        /// <summary>
        /// Liert die Kategorie zu einem spezifischen Film
        /// </summary>
        /// <param name="id">Die Id des Films</param>
        /// <response code="200">Die Abfrage war erfolgreich.</response>
        /// <response code="400">Die übergebene Id konnten nicht verarbeitet werden!</response>
        /// <response code="404">Unbekannte Id!</response>
        // GET: api/Movies/5/Category
        [HttpGet]
        [Route("{id}/category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CategoryDto> GetCategoryByMovieId(int id)
        {
            var category = _unitOfWork.MovieRepository.GetById(id);
            if(category == null)
            {
                return NotFound();
            }
            return new CategoryDto(category.Id,category.Title);
        }

        /// <summary>
        /// Erstellt einen neuen Film 
        /// </summary>
        /// <param name="movie">Die Informationen zum neuen Film</param>
        /// <response code="201">Der Film wurde erfolgreich erstellt.</response>
        /// <response code="400">Die übergebenen Date konnten nicht verarbeitet werden!</response>
        /// <response code="404">Unbekannte Id!</response>
        // POST: api/Movies
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<MovieDto> AddMovie(MovieDto movie)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Movie newMovie = new Movie();
            movie.CopyValuesTo(newMovie);
            _unitOfWork.MovieRepository.Add(newMovie);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetMovie), new { Title = movie.Title }, new MovieDto(newMovie));
        }

        /// <summary>
        /// Ändert einen bestehenden Film 
        /// </summary>
        /// <param name="id">Die Id des zu ändernden Films</param>
        /// <param name="movie">Die aktualisierten Informationen zum Film</param>
        /// <response code="204">Der Film wurde erfolgreich aktualisiert.</response>
        /// <response code="400">Die übergebenen Date konnten nicht verarbeitet werden!</response>
        /// <response code="404">Unbekannte Id!</response>
        // PUT: api/Movies/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateMovie(int id, MovieDto movie)
        {
            if (movie == null)
            {
                return BadRequest($"{nameof(movie)} is null");
            }
            if (movie.Id == null || movie.CategoryId == null ||string.IsNullOrEmpty(movie.Title) || movie.Duration == 0)
            {
                return BadRequest();
            }
            var updateMovie = _unitOfWork.MovieRepository.GetById(id);
            if(updateMovie == null)
            {
                return NotFound();
            }
            //updateMovie.CategoryId = movie.CategoryId;
            updateMovie.Duration = movie.Duration;
            updateMovie.Title = movie.Title;
            updateMovie.Year = movie.Year;
            _unitOfWork.Save();
            return NoContent();
        }


        /// <summary>
        /// Löscht einen bestehenden Film
        /// </summary>
        /// <param name="id">Die Id des Films</param>
        /// <response code="204">Der Film wurde erfolgreich gelöscht.</response>
        /// <response code="400">Die Id konnten nicht verarbeitet werden!</response>
        /// <response code="404">Unbekannte Id!</response>
        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteMovie(int id)
        {
            var movie = _unitOfWork.MovieRepository.GetById(id);
            if(movie == null)
            {
                return NotFound();
            }
            _unitOfWork.MovieRepository.Delete(movie);
            return NoContent();
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManager.Core.Contracts;
using MovieManager.Web.DataTransferObjects;
using System;

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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

    }
}

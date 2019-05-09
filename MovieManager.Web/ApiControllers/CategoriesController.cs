using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManager.Core.Contracts;
using MovieManager.Web.DataTransferObjects;
using System;
using System.Linq;

namespace MovieManager.Web.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Liefert alle existierenden Kategorien
        /// </summary>
        /// <response code="200">Die Abfrage war erfolgreich.</response>
        // GET: api/Categories
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CategoryDto[]> GetCategories()
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            var categories = _unitOfWork.CategoryRepository.GetAll();
            return categories.Select(c => new CategoryDto(c)).ToArray();
        }

        /// <summary>
        /// Liefert eine spezifische Kategorie
        /// </summary>
        /// <param name="id">Die Id der Kategorie</param>
        /// <response code="200">Die Abfrage war erfolgreich.</response>
        /// <response code="400">Die Id konnte nicht verarbeitet werden!</response>
        /// <response code="404">Unbekannte Id!</response>
        // GET: api/Categories/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CategoryDto> GetCategory(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetById(id);
            if(category == null)
            {
                return NotFound();
            }
            return new CategoryDto(category);
        }

        /// <summary>
        /// Liefert die Filme zu einer Kateogie
        /// </summary>
        /// <param name="id">Die Id der Kategorie</param>
        /// <response code="200">Die Abfrage war erfolgreich.</response>
        /// <response code="400">Die Id konnte nicht verarbeitet werden!</response>
        /// <response code="404">Unbekannte Id!</response>
        // GET: api/Categories/5/movies
        [HttpGet]
        [Route("{id}/movies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MovieDto[]> GetMoviesByCategory(int id)
        {
            var moviesByCategory = _unitOfWork.MovieRepository.GetAllByCatId(id);
            if (moviesByCategory == null)
            {
                return NotFound();
            }
            return moviesByCategory.Select(m => new MovieDto(m)).ToArray();
        }

        /// <summary>
        /// Erstellt eine neue Kategorie
        /// </summary>
        /// <param name="category">Die neue Kategorie</param>
        /// <response code="201">Die Kategorie wurde erfolgreich erstellt.</response>
        /// <response code="400">Die Daten der neuen Kategorie konnten nicht verarbeitet werden!</response>
        // POST: api/Categories
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CategoryDto> AddCategory(CategoryDto category)
        {

        }

        /// <summary>
        /// Ändert eine bestehende Kategorie
        /// </summary>
        /// <param name="id">Die Id der Kategorie</param>
        /// <param name="categoryName">Der neue Name der Kategorie</param>
        /// <response code="204">Die Kategorie wurde erfolgreich aktualisiert.</response>
        /// <response code="400">Die übergebenen Daten konnten nicht verarbeitet werden!</response>
        /// <response code="404">Unbekannte Id!</response>
        // PUT: api/Categories/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult UpdateCategory(int id, string categoryName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Löscht eine bestehende Kategorie
        /// </summary>
        /// <param name="id">Die Id der Kategorie</param>
        /// <response code="204">Die Kategorie wurde erfolgreich gelöscht.</response>
        /// <response code="400">Die Id konnten nicht verarbeitet werden!</response>
        /// <response code="404">Unbekannte Id!</response>
        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }
    }
}

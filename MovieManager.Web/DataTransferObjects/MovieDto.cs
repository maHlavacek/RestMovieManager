using System;
using System.ComponentModel.DataAnnotations;
using MovieManager.Core.Entities;
using MovieManager.Web.Validation;

namespace MovieManager.Web.DataTransferObjects
{
    public class MovieDto
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "The " + nameof(Title) + " field is required."),
         MinLength(3, ErrorMessage = "The length of " + nameof(Title) + " must be between 3 and 100."),
         MaxLength(100, ErrorMessage = "The length of " + nameof(Title) + " must be between 3 and 100.")]
        public string Title { get; set; }

        [Range(1900, 2099, ErrorMessage = "The field " + nameof(Year) + " must be between 1900 and 2099.")]
        public int Year { get; set; }

        [Range(0, 300)]
        [ClassicMovieMaxDuration(isClassicMovieUntilYear: 1950, maxDurationForClassicMovie: 60)]
        public int Duration { get; set; }

        [Required(ErrorMessage = "The " + nameof(MovieDto.CategoryId) + " field is required.")]
        public int? CategoryId { get; set; }

        /// <summary>
        /// Konstruktor für die JSON-Serialisierung
        /// </summary>
        public MovieDto() { }

        public MovieDto(int? id, string title, int year, int duration, int categoryId)
        {
            Id = id;
            Title = title;
            Year = year;
            Duration = duration;
            CategoryId = categoryId;
        }

        public MovieDto(Movie movie)
            : this(movie.Id, movie.Title, movie.Year, movie.Duration, movie.CategoryId)
        {
        }

        internal void CopyValuesTo(Movie newMovie)
        {
            newMovie.Title = Title;
            newMovie.Year = Year;
            newMovie.Duration = Duration;
        }
    }
}

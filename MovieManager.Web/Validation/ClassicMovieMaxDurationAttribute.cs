using MovieManager.Web.DataTransferObjects;
using System.ComponentModel.DataAnnotations;

namespace MovieManager.Web.Validation
{
    public class ClassicMovieMaxDurationAttribute : ValidationAttribute
    {
        public int IsClassicMovieUntilYear { get; }
        public int MaxDurationForClassicMovie { get; }

        public ClassicMovieMaxDurationAttribute(int isClassicMovieUntilYear, int maxDurationForClassicMovie)
        {
            IsClassicMovieUntilYear = isClassicMovieUntilYear;
            MaxDurationForClassicMovie = maxDurationForClassicMovie;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var movie = (MovieDto)validationContext.ObjectInstance;
            if (movie.Year <= IsClassicMovieUntilYear && movie.Duration > MaxDurationForClassicMovie)
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"Classical Movies (until year '{IsClassicMovieUntilYear}') may not last longer than {MaxDurationForClassicMovie} minutes!";
        }
    }
}

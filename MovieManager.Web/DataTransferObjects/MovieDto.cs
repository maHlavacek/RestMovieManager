using MovieManager.Core.Entities;

namespace MovieManager.Web.DataTransferObjects
{
    public class MovieDto
    {
        public int? Id { get; set; }


        public string Title { get; set; }


        public int Year { get; set; }

        public int Duration { get; set; }


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

    }
}

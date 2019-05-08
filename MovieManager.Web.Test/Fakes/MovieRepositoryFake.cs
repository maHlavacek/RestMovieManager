using MovieManager.Core.Contracts;
using MovieManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieManager.Web.Test.Fakes
{
    class MovieRepositoryFake : IMovieRepository
    {
        private readonly List<Movie> _movies;

        public MovieRepositoryFake()
        {
            _movies = new List<Movie>()
            {

                new Movie {Id = 1, Title = "Movie 1", CategoryId = 1, Duration = 120, Year = 2000},
                new Movie {Id = 2, Title = "Movie 2", CategoryId = 2, Duration = 121, Year = 2001},
                new Movie {Id = 3, Title = "Movie 3", CategoryId = 3, Duration = 122, Year = 2002}
            };
        }

        public int GetCount()
        {
            return _movies.Count;
        }

        public Movie[] GetAllByCatId(int id)
        {
            return _movies
                .Where(m => m.CategoryId == id)
                .ToArray();
        }

        public void Insert(Movie movie)
        {
            _movies.Add(movie);
        }

        public Movie GetById(int id)
        {
            return _movies
                .SingleOrDefault(m => m.Id == id);
        }

        public void Delete(Movie movie)
        {
            _movies.Remove(movie);
        }

        public Movie GetLongestMovie()
        {
            throw new NotImplementedException();
        }

        public void AddRange(Movie[] movies)
        {
            _movies.AddRange(movies);
        }

        public Movie[] GetAll()
        {
            return _movies
                .ToArray();
        }

        public void Add(Movie movie)
        {
            _movies.Add(movie);
        }
    }
}

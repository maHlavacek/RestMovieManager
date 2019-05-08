using MovieManager.Core.Contracts;
using MovieManager.Core.DTOs;
using MovieManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieManager.Web.Test.Fakes
{
    class CategoryRepositoryFake : ICategoryRepository
    {
        private readonly List<Category> _categories;

        public CategoryRepositoryFake()
        {
            _categories = new List<Category>()
            {

                new Category() {Id = 1, CategoryName = "Category 1"},
                new Category() {Id = 2, CategoryName = "Category 2"},
                new Category() {Id = 3, CategoryName = "Category 3"},
            };
        }

        public Category[] GetAll()
        {
            return _categories
                .ToArray();
        }

        public void Insert(Category category)
        {
            _categories
                .Add(category);
        }

        public Category GetById(int id)
        {
            return _categories
                .SingleOrDefault(c => c.Id == id);
        }

        public void Delete(Category category)
        {
            _categories
                .Remove(category);
        }

        public CategoryStatisticEntry[] GetCategoryStatistics()
        {
            throw new NotImplementedException();
        }

        public CategoryStatisticEntry GetCategoryWithMostMovies()
        {
            throw new NotImplementedException();
        }

        public (Category Category, double AverageLength)[] GetCategoriesWithAverageLengthOfMovies()
        {
            throw new NotImplementedException();
        }

        public int GetYearWithMostPublicationsForCategory(string categoryName)
        {
            throw new NotImplementedException();
        }
    }
}

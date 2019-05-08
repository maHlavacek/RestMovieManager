using MovieManager.Core.DTOs;
using MovieManager.Core.Entities;


namespace MovieManager.Core.Contracts
{
    public interface ICategoryRepository
    {
        Category[] GetAll();
        void Insert(Category category);
        Category GetById(int id);
        void Delete(Category category);
        CategoryStatisticEntry[] GetCategoryStatistics();
        CategoryStatisticEntry GetCategoryWithMostMovies();
        (Category Category, double AverageLength)[] GetCategoriesWithAverageLengthOfMovies();
        int GetYearWithMostPublicationsForCategory(string categoryName);

    }
}

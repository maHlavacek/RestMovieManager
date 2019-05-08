using MovieManager.Core.Contracts;

namespace MovieManager.Web.Test.Fakes
{
    class UnitOfWorkFake : IUnitOfWork
    {
        public IMovieRepository MovieRepository => new MovieRepositoryFake();

        public ICategoryRepository CategoryRepository => new CategoryRepositoryFake();

        public void Save()
        {

        }

        public void DeleteDatabase()
        {
        }

        public void MigrateDatabase()
        {
        }

        public void Dispose()
        {
        }
    }
}

using MovieManager.Core.Entities;

namespace MovieManager.Web.DataTransferObjects
{
    public class CategoryDto
    {
        public int? Id { get; set; }
        public string CategoryName { get; set; }


        /// <summary>
        /// Konstruktor für die JSON-Serialisierung
        /// </summary>
        public CategoryDto()
        {
        }

        public CategoryDto(int? id, string categoryName)
        {
            Id = id;
            CategoryName = categoryName;
        }

        public CategoryDto(Category category)
        {
            Id = category.Id;
            CategoryName = category.CategoryName;
        }


    }
}

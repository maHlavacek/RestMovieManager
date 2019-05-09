using MovieManager.Core.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace MovieManager.Web.DataTransferObjects
{
    public class CategoryDto
    {
        public int? Id { get; set; }
        [Required, MinLength(2, ErrorMessage = "The name must be at least two chatacters long")]
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

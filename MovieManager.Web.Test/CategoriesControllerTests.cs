using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieManager.Web.ApiControllers;
using MovieManager.Web.DataTransferObjects;
using MovieManager.Web.Test.Fakes;

namespace MovieManager.Web.Test
{
    [TestClass]
    public class CategoriesControllerTests
    {
        private CategoriesController _categoriesController;


        [TestInitialize]
        public void InitializeTest()
        {
            _categoriesController = new CategoriesController(new UnitOfWorkFake());
        }

        [TestMethod]
        public void Get_WhenCall_ShouldReturnOkResult()
        {

            // Act
            var okResult = _categoriesController.GetCategories();

            // Assert
            Assert.IsInstanceOfType(okResult, typeof(ActionResult<CategoryDto[]>));
            Assert.IsInstanceOfType(okResult.Value, typeof(CategoryDto[]));
        }

        [TestMethod]
        public void Get_WhenCall_ShouldReturnAllCategories()
        {
            // Act
            var okResult = _categoriesController.GetCategories();


            // Assert
            Assert.IsNotNull(okResult);
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult?.Value, typeof(CategoryDto[]));
            Assert.AreEqual(3, ((CategoryDto[])okResult?.Value).Length);
        }

        [TestMethod]
        public void GetCategory_CallWithValidId_ShouldReturnCorrectCategory()
        {
            // Arrange
            int categoryId = 1;

            // Act
            var okResult = _categoriesController.GetCategory(categoryId);


            // Assert
            Assert.IsNotNull(okResult);
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult?.Value, typeof(CategoryDto));
            Assert.AreEqual("Category 1", okResult.Value.CategoryName);
        }


        [TestMethod]
        public void AddCategory_CallWithModelValidationError_ShouldReturnBadRequestObjectResult()
        {
            // Arrange
            CategoryDto categoryWithMissingName = new CategoryDto(null, string.Empty);

            _categoriesController.ModelState.AddModelError("CategoryName", "Required");

            // Act
            var badResponse = _categoriesController.AddCategory(categoryWithMissingName);

            // Assert
            Assert.IsInstanceOfType(badResponse.Result, typeof(BadRequestObjectResult));
        }



        [TestMethod]
        public void AddCategory_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            string categoryName = "Category 99";
            CategoryDto validCategory = new CategoryDto(null, categoryName);


            // Act
            var createdResponse = _categoriesController.AddCategory(validCategory);

            // Assert
            Assert.IsInstanceOfType(createdResponse.Result, typeof(CreatedAtActionResult));
        }


        [TestMethod]
        public void AddCategory_ValidObjectPassed_ReturnedResponseContainsCreatedItem()
        {
            // Arrange
            string categoryName = "Category 99";
            CategoryDto validCategory = new CategoryDto(null, categoryName);

            // Act
            var createdResponse = _categoriesController.AddCategory(validCategory) as ActionResult<CategoryDto>;
            var result = createdResponse.Result as CreatedAtActionResult;
            var category = result?.Value as CategoryDto;

            // Assert
            Assert.IsNotNull(category);
            Assert.IsInstanceOfType(category, typeof(CategoryDto));
            Assert.AreEqual(categoryName, category.CategoryName);
        }

        [TestMethod]
        public void GetMoviesByCategory_InvalidCategory_ShouldReturnNotFound()
        {
            // Act
            var response = _categoriesController.GetMoviesByCategory(99);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetCategory_InvalidCategory_ShouldReturnNotFound()
        {
            // Act
            var response = _categoriesController.GetCategory(99);

            // Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response.Result, typeof(NotFoundResult));
        }


        [TestMethod]
        public void UpdateCategory_CallWithNoneExistingCategoryId_ShouldReturnNotFoundResult()
        {
            // Act
            var badResponse = _categoriesController.UpdateCategory(99, "Category 99");

            // Assert
            Assert.IsInstanceOfType(badResponse, typeof(NotFoundResult));
        }


        [TestMethod]
        public void UpdateCategory_CallWithEmptyCategoryName_ShouldReturnBadRequestObjectResult()
        {
            // Act
            var badResponse = _categoriesController.UpdateCategory(1, null);

            // Assert
            Assert.IsInstanceOfType(badResponse, typeof(BadRequestObjectResult));
        }



        [TestMethod]
        public void UpdateCategory_CallWithValidParameters_ShouldReturnNoContentResult()
        {

            // Act
            var updateResponse = _categoriesController.UpdateCategory(1, "New Category 1");

            // Assert
            Assert.IsInstanceOfType(updateResponse, typeof(NoContentResult));
        }

        [TestMethod]
        public void DeleteCategory_CallWithNoneExistingCategoryId_ShouldReturnNotFoundResult()
        {
            // Act
            var badResponse = _categoriesController.DeleteCategory(99);

            // Assert
            Assert.IsInstanceOfType(badResponse, typeof(NotFoundResult));
        }


        [TestMethod]
        public void DeleteCategory_CallWithValidParameters_ShouldReturnNoContentResult()
        {

            // Act
            var updateResponse = _categoriesController.DeleteCategory(1);

            // Assert
            Assert.IsInstanceOfType(updateResponse, typeof(NoContentResult));
        }

    }
}

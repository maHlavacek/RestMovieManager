using Microsoft.VisualStudio.TestTools.UnitTesting;
using MovieManager.Web.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace MovieManager.Web.Test
{
    [TestClass]
    public class DataValidationTests
    {
        [TestMethod]
        public void CategoryDtoValidation_MissingCategoryName_ShouldIndicateMissingCategoryName()
        {
            // Arrange
            CategoryDto category = new CategoryDto(null, null);

            // Act
            var (isValid, validationResults) = ValidateObject(category);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count(_ => _.ErrorMessage == $"The {nameof(CategoryDto.CategoryName)} field is required."));
        }

        [TestMethod]
        public void MovieDtoValidation_TooShortTitle_ShouldThrowValidationMessage()
        {
            // Arrange
            MovieDto movie = new MovieDto()
            {
                Title = new String('x', 2)
            };

            // Act
            var (isValid, validationResults) = ValidateObject(movie);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1,
                    validationResults.Count(_ =>
                        Regex.IsMatch(
                            _.ErrorMessage,
                            WildCardToRegular($"The length of {nameof(MovieDto.Title)} must be between * and *."))));
        }

        [TestMethod]
        public void MovieDtoValidation_TooLongTitle_ShouldThrowValidationMessage()
        {
            // Arrange
            MovieDto movie = new MovieDto()
            {
                Title = new String('x', 101)
            };

            // Act
            var (isValid, validationResults) = ValidateObject(movie);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1,
                validationResults.Count(_ =>
                    Regex.IsMatch(
                        _.ErrorMessage,
                        WildCardToRegular($"The length of {nameof(MovieDto.Title)} must be between * and *."))));
        }

        [TestMethod]
        public void MovieDtoValidation_MissingTitle_ShouldIndicateMissingTitle()
        {
            // Arrange
            MovieDto movie = new MovieDto();

            // Act
            var (isValid, validationResults) = ValidateObject(movie);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count(_ => _.ErrorMessage == $"The {nameof(MovieDto.Title)} field is required."));
        }

        [TestMethod]
        [DataRow(1899)]
        [DataRow(2100)]
        public void MovieDtoValidation_YearNotInRange_ShouldThrowValidationMessage(int year)
        {
            // Arrange
            MovieDto movie = new MovieDto()
            {
                Year = year
            };

            // Act

            var (isValid, validationResults) = ValidateObject(movie);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1,
                validationResults.Count(_ =>
                    Regex.IsMatch(
                        _.ErrorMessage,
                        WildCardToRegular($"The field {nameof(MovieDto.Year)} must be between * and *."))));
        }



        [TestMethod]
        public void MovieDtoValidation_MissingCategoryId_ShouldIndicateMissingCategoryId()
        {
            // Arrange
            MovieDto movie = new MovieDto();

            // Act
            var (isValid, validationResults) = ValidateObject(movie);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count(_ => _.ErrorMessage == $"The {nameof(MovieDto.CategoryId)} field is required."));
        }


        [TestMethod]
        public void MovieDtoValidation_ClassicMovieRule_ShouldThrowAValidationEror()
        {
            // Arrange
            MovieDto movie = new MovieDto()
            {
                CategoryId = 1,
                Title = "A typical classic Movie",
                Duration = 240,
                Year = 1937
            };

            // Act
            var (isValid, validationResults) = ValidateObject(movie);

            // Assert
            Assert.IsFalse(isValid);
            Assert.AreEqual(1,
                validationResults.Count(_ =>
                    Regex.IsMatch(
                        _.ErrorMessage,
                        WildCardToRegular("Classical Movies (until year '*') may not last longer than * minutes!"))));
        }


        #region Helper Methods

        private static (bool IsValid, IEnumerable<ValidationResult> ValidationResults) ValidateObject(object objectToValidate)
        {
            var context = new ValidationContext(objectToValidate, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(objectToValidate, context, results, true);

            return (isValid, results);
        }

        /// <summary>
        /// Builds a Regular Expression out of a search string with * and ? wildcards.
        /// </summary>
        private static string WildCardToRegular(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
        }


        #endregion

    }
}

using BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;

namespace BLL.Tests
{

    [TestClass()]
    public class IngredientServiceTests
    {
        DishService dishService = new();
        IngredientService ingredientService = new();
        OrderService orderService = new();

        [TestMethod()]
        public void GetByIdTest()
        {
            // Arrange
            Ingredient ingredient = ingredientService.Insert("test");
            // Act
            Ingredient? result = ingredientService.GetById(ingredient.Id);
            // Assert
            Assert.AreEqual(ingredient.Id, result?.Id);
            // Clear traces
            ingredientService.DeleteById(ingredient.Id);
        }

        [TestMethod()]
        public void UpdateByIdTest()
        {
            // Arrange
            Ingredient ingredient = ingredientService.Insert("test");
            // Act
            ingredient.Name = "test2";
            ingredientService.UpdateById(ingredient, ingredient.Id);
            // Assert
            Assert.AreEqual(ingredientService.GetById(ingredient.Id), ingredient);
            // Clear traces
            ingredientService.DeleteById(ingredient.Id);
        }

        [TestMethod()]
        public void DeleteByIdTest()
        {
            // Arrange
            Ingredient ingredient = ingredientService.Insert("test");
            // Act
            ingredientService.DeleteById(ingredient.Id);
            // Assert
            Assert.AreEqual(ingredientService.GetById(ingredient.Id), null);
        }

        [TestMethod()]
        public void LengthTest()
        {
            // Arrange
            Ingredient ingredient = ingredientService.Insert("test");
            // Act
            int length = ingredientService.Length();
            // Assert
            Assert.AreEqual(length, ingredientService.Length());
            // Clear traces
            ingredientService.DeleteById(ingredient.Id);
        }
    }
}
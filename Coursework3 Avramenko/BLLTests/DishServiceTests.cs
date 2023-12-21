using BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;

namespace BLL.Tests
{
    [TestClass()]
    public class DishServiceTests
    {
        DishService dishService = new();
        IngredientService ingredientService = new();
        OrderService orderService = new();

        [TestMethod()]
        public void InsertTest()
        {
            // Arrange
            int length = dishService.Length();
            Dish dish = new("test", 0, new(), 1);
            // Act
            dishService.Insert(dish);
            // Assert
            Assert.AreEqual(length + 1, dishService.Length());
            Assert.AreEqual(dishService[length], dish);
            // Clear traces
            dishService.Delete(length);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            // Arrange
            int length = dishService.Length();
            Dish dish = new("test", 0, new(), 1);
            dishService.Insert(dish);
            // Act
            dish.Name = "test2";
            dishService.Update(dish, length);
            // Assert
            Assert.AreEqual(dishService[length].Name, "test2");
            // Clear traces
            dishService.Delete(length);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            // Arrange
            int length = dishService.Length();
            Dish dish = new("test", 0, new(), 1);
            dishService.Insert(dish);
            // Act
            dishService.Delete(length);
            // Assert
            Assert.AreEqual(length, dishService.Length());
        }

        [TestMethod()]
        public void RemoveIngredientByIdTest()
        {
            // Arrange
            int length = dishService.Length();
            Dish dish = new("test", 0, new(), 1);
            dishService.Insert(dish);
            Ingredient ingredient = ingredientService.Insert("test");
            dish.Ingredients.Add(ingredient.Id);
            dishService.Update(dish, length);
            // Act
            dishService.RemoveIngredientById(ingredient.Id);
            // Assert
            Assert.AreEqual(0, dishService[length].Ingredients.Count);
            // Clear traces
            dishService.Delete(length);
            ingredientService.DeleteById(ingredient.Id);
        }

        [TestMethod()]
        public void FindByNameTest()
        {
            // Arrange
            int length = dishService.Length();
            Dish dish = new("test", 0, new(), 1);
            dishService.Insert(dish);
            // Act
            Dish? found = dishService.FindByName("test");
            // Assert
            Assert.AreEqual(dish.Name, found?.Name);
            // Clear traces
            dishService.Delete(length);
        }

        [TestMethod()]
        public void LengthTest()
        {
            // Arrange
            int length = dishService.Length();
            Dish dish = new("test", 0, new(), 1);
            dishService.Insert(dish);
            // Act
            // Assert
            Assert.AreEqual(length + 1, dishService.Length());
            // Clear traces
            dishService.Delete(length);
        }
    }
}
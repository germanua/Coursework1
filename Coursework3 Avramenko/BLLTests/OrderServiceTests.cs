using BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;

namespace BLL.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        DishService dishService = new();
        IngredientService ingredientService = new();
        OrderService orderService = new();

        [TestMethod()]
        public void GetByIdTest()
        {
            // Arrange
            Order order = orderService.Insert(1, "test", 1, new(), 0);
            // Act
            Order? result = orderService.GetById(order.Id);
            // Assert
            Assert.AreEqual(order.Id, result?.Id);
            // Clear traces
            orderService.DeleteById(order.Id);
        }

        [TestMethod()]
        public void GetByTableTest()
        {
            // Arrange
            Order order = orderService.Insert(1, "test", 1, new(), 0);
            // Act
            Order? result = orderService.GetById(order.Id);
            // Assert
            Assert.AreEqual(order.Id, result?.Id);
            // Clear traces
            orderService.DeleteById(order.Id);
        }

        [TestMethod()]
        public void InsertTest()
        {
            // Arrange
            int length = orderService.Length();
            // Act
            Order order = orderService.Insert(length, "test", 1, new(), 0);
            // Assert
            Assert.AreEqual(length + 1, orderService.Length());
            Assert.AreEqual(orderService.GetById(order.Id), order);
            // Clear traces
            orderService.DeleteById(order.Id);
        }

        [TestMethod()]
        public void AddDishTest()
        {
            // Arrange
            int length = orderService.Length();
            Dish dish = dishService.Insert(new("test", 0, new(), 1));
            Order order = orderService.Insert(1, "test", 1, new(), 0);
            // Act
            OrderService.AddDish(dish, order);
            // Assert
            Assert.AreEqual(orderService[length].Dishes[0], dish);
        }

        [TestMethod()]
        public void RemoveDishTest()
        {
            // Arrange
            int length = orderService.Length();
            Dish dish = dishService.Insert(new("test", 0, new(), 1));
            Order order = orderService.Insert(1, "test", 1, new(), 0);
            OrderService.AddDish(dish, order);
            // Act
            OrderService.RemoveDish(0, order);
            // Assert
            Assert.AreEqual(orderService[length].Dishes.Count, 0);
        }

        [TestMethod()]
        public void UpdateByIdTest()
        {
            // Arrange
            int length = orderService.Length();
            Order order = orderService.Insert(1, "test", 1, new(), 0);
            // Act
            order.TableNumber = 2;
            orderService.UpdateById(order, length);
            // Assert
            Assert.AreEqual(orderService.GetById(order.Id), order);
            // Clear traces
            orderService.DeleteById(order.Id);
        }

        [TestMethod()]
        public void DeleteByIdTest()
        {
            // Arrange
            int length = orderService.Length();
            Order order = orderService.Insert(1, "test", 1, new(), 0);
            // Act
            orderService.DeleteById(order.Id);
            // Assert
            Assert.AreEqual(length, orderService.Length());
        }

        [TestMethod()]
        public void LengthTest()
        {
            // Arrange
            int length = orderService.Length();
            Order order = orderService.Insert(1, "test", 1, new(), 0);
            // Act
            // Assert
            Assert.AreEqual(length + 1, orderService.Length());
            // Clear traces
            orderService.DeleteById(order.Id);
        }
    }
}
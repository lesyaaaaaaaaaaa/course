using Microsoft.VisualStudio.TestTools.UnitTesting;
using ItalianPizzaHouse.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ItalianPizzaHouse.Model;

namespace ItalianPizzaHouse.ViewModel.Tests
{
    [TestClass()]
    public class BasketClassVMTests
    {
        /// <summary>
        /// Проверяет, что при добавлении нового товара он добавляется в корзину.
        /// </summary>
        [TestMethod()]
        public void AddProduct_NewProduct_AddsToBasket()
        {
            // Arrange
            BasketClassVM.ClearBasket();
            var product = new Products { IdProduct = 1, NameProduct = "Test", Price = 10, Quantity = 1 };

            // Act
            BasketClassVM.AddProduct(product);

            // Assert
            Assert.AreEqual(1, BasketClassVM.GetBasket().Count);
            Assert.AreEqual(product, BasketClassVM.GetBasket().First());
        }
        /// <summary>
        /// Проверяет, что при удалении товара он удаляется из корзины.
        /// </summary>
        [TestMethod()]
        public void RemoveProduct_ExistingProduct_RemovesFromBasket()
        {
            // Arrange
            BasketClassVM.ClearBasket();
            var product = new Products { IdProduct = 1, NameProduct = "Test", Price = 10, Quantity = 1 };
            BasketClassVM.AddProduct(product);

            // Act
            BasketClassVM.RemoveProduct(product);

            // Assert
            Assert.AreEqual(0, BasketClassVM.GetBasket().Count);
        }
        /// <summary>
        /// Проверяет, что метод ClearBasket очищает корзину.
        /// </summary>
        [TestMethod()]
        public void ClearBasket_ClearsBasket()
        {
            // Arrange
            BasketClassVM.ClearBasket();
            BasketClassVM.AddProduct(new Products { IdProduct = 1, NameProduct = "Test1", Price = 10, Quantity = 1 });
            BasketClassVM.AddProduct(new Products { IdProduct = 2, NameProduct = "Test2", Price = 20, Quantity = 2 });

            // Act
            BasketClassVM.ClearBasket();

            // Assert
            Assert.AreEqual(0, BasketClassVM.GetBasket().Count);
        }
        /// <summary>
        /// Проверяет, что метод GetBasket возвращает корректный список товаров в корзине.
        /// </summary>
        [TestMethod()]
        public void GetBasket_ReturnsCorrectBasket()
        {
            // Arrange
            BasketClassVM.ClearBasket();
            var product1 = new Products { IdProduct = 1, NameProduct = "Test1", Price = 10, Quantity = 1 };
            var product2 = new Products { IdProduct = 2, NameProduct = "Test2", Price = 20, Quantity = 2 };
            BasketClassVM.AddProduct(product1);
            BasketClassVM.AddProduct(product2);

            // Act
            var basket = BasketClassVM.GetBasket();

            // Assert
            Assert.AreEqual(2, basket.Count);
            Assert.IsTrue(basket.Contains(product1));
            Assert.IsTrue(basket.Contains(product2));
        }
    }
}
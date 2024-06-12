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
    public class ProductsVMTests
    {
        Core db = new Core();
        /// <summary>
        /// Проверяет, что метод CreateProduct успешно добавляет новый продукт в базу данных.
        /// </summary>
        [TestMethod()]
        public void CreateProduct_AddsProduct()
        {
            var newProduct = new Products { NameProduct = "Test Product", Price = 10, Quantity = 1, CategoriesId = 1};

            // Act
            ProductsVM.CreateProduct(newProduct); 

            // Assert
            // Проверяем, что продукт добавлен
            var productInDb = db.context.Products.FirstOrDefault(p => p.NameProduct == "Test Product");
            Assert.IsNotNull(productInDb, "Продукт не найден в базе данных");
        }
        /// <summary>
        /// Проверяет, что метод DeleteProduct успешно удаляет продукт из базы данных.
        /// </summary>
        [TestMethod()]
        public void DeleteProduct_RemovesProductAndLogsEvent()
        {
            var productToDelete = new Products { NameProduct = "Test Product", Price = 10, Quantity = 1, CategoriesId = 1 };
            db.context.Products.Add(productToDelete);
            db.context.SaveChanges();

            var userId = 1;
            

            // Act
            ProductsVM.DeleteProduct(productToDelete);

            // Assert
            // Проверяем, что продукт удален
            var deletedProduct = db.context.Products.FirstOrDefault(p => p.IdProduct == productToDelete.IdProduct);
            Assert.IsNull(deletedProduct, "Продукт не был удален из базы данных");
        }
    }
}
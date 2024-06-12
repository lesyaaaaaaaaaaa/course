using ItalianPizzaHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ItalianPizzaHouse.ViewModel
{
    public class ProductsVM
    {
        static Core db = new Core();

        /// <summary>
        /// Создает новый продукт в базе данных и записывает событие в журнал.
        /// </summary>
        /// <param name="newProduct">Новый продукт для создания.</param>
        public static void CreateProduct(Products newProduct)
        {
            db.context.Products.Add(newProduct);
            int userId = UsersVM.UserId;
            DateTime currentDate = DateTime.Now;
            HistoryVM.EventHappened(currentDate, $"Создание продукта {newProduct.NameProduct}", userId);
            db.context.SaveChanges();
        }

        /// <summary>
        /// Удаляет продукт из базы данных и записывает событие в журнал.
        /// </summary>
        /// <param name="currentProduct">Продукт для удаления.</param>
        public static void DeleteProduct(Products currentProduct)
        {
            Products delProduct = db.context.Products.Where(x => x.IdProduct == currentProduct.IdProduct).FirstOrDefault();
            int userId = UsersVM.UserId;
            DateTime currentDate = DateTime.Now;
            HistoryVM.EventHappened(currentDate, $"Удаление продукта {currentProduct.NameProduct}", userId);
            db.context.Products.Remove(delProduct);
            db.context.SaveChanges();
        }
    }
}

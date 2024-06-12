using ItalianPizzaHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItalianPizzaHouse.ViewModel
{
    public class BasketClassVM
    {
        private static List<Products> products = new List<Products>();
        public static List<Products> Products
        {
            get { return products; }
        }

        /// <summary>
        /// Добавляет товар в корзину. 
        /// Если товар с таким же IdProduct уже существует, увеличивает его количество.
        /// В противном случае добавляет новый товар в корзину.
        /// </summary>
        /// <param name="product">Товар для добавления.</param>
        public static void AddProduct(Products product)
        {
            if (!products.Contains(product))
            {
                var existingProduct = products.FirstOrDefault(p => p.IdProduct == product.IdProduct);

                if (existingProduct != null)
                {
                    existingProduct.Quantity += product.Quantity;
                }
                else
                {
                    products.Add(product);
                }
            }
        }

        /// <summary>
        /// Удаляет товар из корзины.
        /// </summary>
        /// <param name="product">Товар для удаления.</param>
        public static void RemoveProduct(Products product)
        {
            if (products.Contains(product))
            {
                products.Remove(product);
            }
        }

        /// <summary>
        /// Очищает корзину, удаляя все товары.
        /// </summary>
        public static void ClearBasket()
        {
            products.Clear();
        }

        /// <summary>
        /// Возвращает список всех товаров в корзине.
        /// </summary>
        /// <returns>Список товаров в корзине.</returns>
        public static List<Products> GetBasket()
        {
            return products;
        }

    }
}

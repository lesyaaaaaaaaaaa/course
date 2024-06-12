using ItalianPizzaHouse.Model;
using ItalianPizzaHouse.ViewModel;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ItalianPizzaHouse.View
{
    /// <summary>
    /// Interaction logic for OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        Core db = new Core();

        public OrdersPage()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            // Получаем все заказы из базы данных
            var allOrders = db.context.Orders.ToList();

            // Создаем список для отображения заказов с продуктами
            var ordersToDisplay = new List<OrderViewModel>();

            // Проходим по каждому заказу
            foreach (var order in allOrders)
            {
                // Создаем новый объект OrderViewModel для текущего заказа
                var orderViewModel = new OrderViewModel
                {
                    OrderId = order.IdOrders,
                    OrderDate = order.DateTimeOrder,
                    TotalPrice = order.ResultPrice,
                    OrderedItems = new List<OrderItemViewModel>() // Инициализируем список OrderedItems
                };

                // Получаем все продукты, связанные с текущим заказом, через таблицу Baskets
                var productsInOrder = db.context.Products
                    .Where(p => db.context.Baskets.Any(b => b.OrderId == order.IdOrders && b.ProductId == p.IdProduct))
                    .ToList();

                // Добавляем каждый продукт в список OrderedItems объекта OrderViewModel
                foreach (var product in productsInOrder)
                {
                    // Находим соответствующую запись в Baskets, чтобы получить количество
                    var basketItem = db.context.Baskets.FirstOrDefault(b => b.OrderId == order.IdOrders && b.ProductId == product.IdProduct);

                    // Проверяем, что basketItem не равен null, чтобы избежать NullReferenceException
                    if (basketItem != null)
                    {
                        orderViewModel.OrderedItems.Add(new OrderItemViewModel
                        {
                            ProductName = product.NameProduct,
                            Quantity = basketItem.Quantity, // Берем количество из Baskets
                            Price = product.Price
                        });
                    }
                }

                // Добавляем сформированный OrderViewModel в список для отображения
                ordersToDisplay.Add(orderViewModel);
            }

            // Передаем список ordersToDisplay в качестве источника данных для ListView
            OrdersListView.ItemsSource = ordersToDisplay;
        }
    }

    // Вспомогательные классы-модели для отображения данных в ListView
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemViewModel> OrderedItems { get; set; }
    }

    public class OrderItemViewModel
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}

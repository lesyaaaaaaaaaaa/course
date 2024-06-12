using ItalianPizzaHouse.Model;
using ItalianPizzaHouse.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для BasketPage.xaml
    /// </summary>
    public partial class BasketPage : Page
    {
        Core db = new Core();   
        List<Products> basketItems;
        public int TotalQuantity { get; set; } 
        public decimal TotalPrice { get; set; }
        public BasketPage()
        {

            InitializeComponent();
            UpdateBasket(); 
            if(UsersVM.UserRole != 0)
            {
                AllOrdersButton.Visibility = Visibility.Visible;
            }
            else
            {
                AllOrdersButton.Visibility = Visibility.Collapsed;
            }
        }

        private void DeleteFromBasket(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Products removeProduct = button.DataContext as Products;
            basketItems.Remove(removeProduct);
            UpdateBasket();
        }

        private void UpdateBasket()
        {

            BasketListView.ItemsSource = null;
            basketItems = BasketClassVM.GetBasket();
            foreach (var product in basketItems)
            {
                if (product.ProductPhotos.Any())
                {
                    string photoPath = product.ProductPhotos.First().PhotoPath;
                    product.FullImagePath = $"../../Assets/Img/pizza/{photoPath}";
                }
                else
                {
                    product.FullImagePath = "../../Assets/Img/Картинка не найдена.png";
                }
            }
            TotalQuantity = basketItems.Sum(p => p.Quantity);
            TotalPrice = basketItems.Sum(p => p.Price * p.Quantity);

            BasketListView.ItemsSource = basketItems;
        }

        private void ClearBasketClick(object sender, RoutedEventArgs e)
        {
            basketItems.Clear();
            UpdateBasket();
        }



        private void ShowOrdersClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrdersPage());
        }

        private void CreateOrderClick(object sender, RoutedEventArgs e)
        {
            if (basketItems.Count == 0)
            {
                MessageBox.Show("Корзина пуста!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Создаем новый заказ
            var newOrder = new Orders
            {
                DateTimeOrder = DateTime.Now,
                UserId = 1,
                ResultPrice = basketItems.Sum(p => p.Price), // Считаем общую стоимость без Quantity
                StatusId = 1 // Устанавливаем начальный статус заказа (например, "Новый") 
            };

            db.context.Orders.Add(newOrder);
            db.context.SaveChanges();

            // Добавляем продукты в таблицу Baskets
            foreach (var product in basketItems)
            {
                var basketItem = new Baskets
                {
                    OrderId = newOrder.IdOrders,
                    ProductId = product.IdProduct,
                    Quantity = product.Quantity,
                };

                db.context.Baskets.Add(basketItem);
            }

            db.context.SaveChanges();

            MessageBox.Show("Заказ успешно оформлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            // Очищаем корзину после оформления заказа
            basketItems.Clear();
            UpdateBasket();
        }

    }
}

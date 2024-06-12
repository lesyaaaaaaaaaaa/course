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
using System.Data.SqlClient;
using ItalianPizzaHouse.Model;
using ItalianPizzaHouse.ViewModel; // Добавить ссылку на System.Data.SqlClient

namespace ItalianPizzaHouse.View
{
    public partial class AuthPage : Page
    {
        Core db = new Core();
        public AuthPage()
        {
            InitializeComponent();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordTextBox.Password;

            // Проверка, существует ли пользователь в базе данных
            if (ValidateUser(login, password))
            {
                // Пользователь успешно аутентифицирован
                // Перейдите на следующую страницу (например, главное меню)
                NavigationService.Navigate(new MainPage());
            }
        }

        private bool ValidateUser(string login, string password)
        {
                try
                {
                    return UsersVM.Authenticate(login, password);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка во время аутентификации: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
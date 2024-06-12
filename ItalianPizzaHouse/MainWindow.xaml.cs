using ItalianPizzaHouse.Model;
using ItalianPizzaHouse.View;
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

namespace ItalianPizzaHouse
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new MainPage());
        }

        private void AuthButtonClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AuthPage());
        }

        private void AboutUsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AboutUsPage());
        }

        private void PizzaButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProductPage(2));
        }

        private void DrinksButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProductPage(1));
            
        }

        private void SaucesButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProductPage(3));

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new BasketPage());
        }

        private void DeauthButtonClick(object sender, RoutedEventArgs e)
        {
            UsersVM.Logout();
            MainFrame.Navigate(new MainPage());
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            IsAuthed();
        }

        public void IsAuthed()
        {
            if (UsersVM.UserRole != 0)
            {
                AuthButton.Visibility = Visibility.Collapsed;
                DeauthButton.Visibility = Visibility.Visible;
                if(UsersVM.UserRole == 3) { 
                    EventsButton.Visibility = Visibility.Visible;
                }
                else
                {
                    EventsButton.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                AuthButton.Visibility = Visibility.Visible;
                DeauthButton.Visibility = Visibility.Collapsed;
            }
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new EventLogPage());
        }
    }
}

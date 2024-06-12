using ItalianPizzaHouse.Model;
using ItalianPizzaHouse.ViewModel;
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
    /// Interaction logic for CreateProductPage.xaml
    /// </summary>
    public partial class CreateProductPage : Page
    {
        Core db = new Core();
        public CreateProductPage()
        {
            InitializeComponent();
            CategoryComboBox.ItemsSource = db.context.Сategories.ToList();


        }

        private void CreateNewProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int price = Convert.ToInt32(PriceTextBox.Text);
                int discount = Convert.ToInt32(DiscountPriceTextBox.Text == string.Empty ? "0" : DiscountPriceTextBox.Text) ;
                Products product = new Products
                {
                    NameProduct = ProductNameTextBox.Text,
                    CategoriesId = (CategoryComboBox.SelectedItem as Сategories).IdCategories,
                    Price = price,
                    DiscountPrice = discount,
                    Description = DescriptionTextBox.Text,
                };
                ProductsVM.CreateProduct(product);
                this.NavigationService.Navigate(new ProductPage((CategoryComboBox.SelectedItem as Сategories).IdCategories));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

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
    /// Interaction logic for EditProductPage.xaml
    /// </summary>
    public partial class EditProductPage : Page
    {
        Core db = new Core();
        private Products _currentProduct;

        public EditProductPage(Products selectedProduct)
        {
            InitializeComponent();
            _currentProduct = selectedProduct;

            ProductNameTextBox.Text = _currentProduct.NameProduct;
            CategoryComboBox.ItemsSource = db.context.Сategories.ToList();
            CategoryComboBox.SelectedIndex = _currentProduct.CategoriesId - 1;
            CategoryComboBox.SelectedItem = db.context.Сategories.FirstOrDefault(c => c.IdCategories == _currentProduct.CategoriesId);
            PriceTextBox.Text = (Convert.ToInt32(_currentProduct.Price)).ToString();
            DiscountPriceTextBox.Text = _currentProduct.DiscountPrice.ToString();
            DescriptionTextBox.Text = _currentProduct.Description;
        }

        private void SaveEditProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _currentProduct.NameProduct = ProductNameTextBox.Text;
                _currentProduct.CategoriesId = (CategoryComboBox.SelectedItem as Сategories).IdCategories;
                _currentProduct.Price = Convert.ToInt32(PriceTextBox.Text);
                _currentProduct.DiscountPrice = Convert.ToInt32(DiscountPriceTextBox.Text == string.Empty ? "0" : DiscountPriceTextBox.Text);
                _currentProduct.Description = DescriptionTextBox.Text;


                db.context.SaveChanges();


                this.NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить этот продукт?", "Подтверждение удаления", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    ProductsVM.DeleteProduct(_currentProduct);
                    this.NavigationService.Navigate(new ProductPage(_currentProduct.CategoriesId));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}

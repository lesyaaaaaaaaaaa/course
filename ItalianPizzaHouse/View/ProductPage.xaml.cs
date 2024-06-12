using ItalianPizzaHouse.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using ItalianPizzaHouse.ViewModel;

namespace ItalianPizzaHouse.View
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        Core db = new Core();
        List<Products> allProducts;
        ObservableCollection<Products> displayedProducts;
        int currentPage = 1;
        int pageSize = 2; // Количество элементов на странице
        int maxPages; // Максимальное количество страниц


        public ProductPage(int idCategories)
        {
            InitializeComponent();

            allProducts = db.context.Products.Include("Сategories").Where(x => x.CategoriesId == idCategories).ToList();
            displayedProducts = new ObservableCollection<Products>();   
            

            // Рассчитываем максимальное количество страниц
            maxPages = (int)Math.Ceiling((double)allProducts.Count / pageSize);

            LoadProducts(currentPage);
    
            LViewProduct.ItemsSource = displayedProducts;
            
        }

        private void LoadProducts(int page)
        {
            displayedProducts.Clear();
            var pageData = allProducts.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            foreach (var product in pageData)
            {

                if (UsersVM.UserRole != 0)
                {
                    product.AdminVisibility = Visibility.Visible;
                    
                }
                else
                {
                    product.AdminVisibility = Visibility.Collapsed;
                }
               
                if (product.ProductPhotos.Any())
                {
                    string photoPath = product.ProductPhotos.First().PhotoPath;
                    product.FullImagePath = $"../../Assets/Img/pizza/{photoPath}";
                    displayedProducts.Add(product);
                }
                else
                {
                    product.FullImagePath = "../../Assets/Img/Картинка не найдена.png";
                    displayedProducts.Add(product);
                }
            }
        }

        public void IsAuthed()
        {
            if (UsersVM.UserRole != 0)
            {
                AddNewProductButton.Visibility = Visibility.Visible;
                foreach (var product in displayedProducts)
                {
                    product.AdminVisibility = Visibility.Visible;
                }
            }
            else
            {
                AddNewProductButton.Visibility = Visibility.Collapsed;
                foreach (var product in displayedProducts)
                {
                    product.AdminVisibility = Visibility.Collapsed;
                }
            }
        }


        public bool CanMovePrevious => currentPage > 0;
        public bool CanMoveNext => currentPage < maxPages - 1;
        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadProducts(currentPage);
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < maxPages)
            {
                currentPage++;
                LoadProducts(currentPage);
            }
        }

        private void BasketPage_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Products addProduct = button.DataContext as Products;
            addProduct.Quantity = 1;
            BasketClassVM.AddProduct(addProduct);
        }

        private void AddNewProductButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CreateProductPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            IsAuthed();
            LoadProducts(currentPage);
        }

        private void EditProduct_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Products editProduct = button.DataContext as Products;
            this.NavigationService.Navigate(new EditProductPage(editProduct));
        }


    }
}
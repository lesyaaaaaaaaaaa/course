using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ItalianPizzaHouse.Model
{
    public class Core
    {
        public ItalianPizzaHouseEntities context = new ItalianPizzaHouseEntities();
    }
    public partial class Products
    {
        public Visibility AdminVisibility { get; set; } = Visibility.Collapsed;
        public string FullImagePath { get; set; }
        public int Quantity { get; set; }
    }
    public partial class Orders
    {
        public List<Products> OrderedItems { get; set; } = new List<Products>();
    }

    public partial class Histories
    {
        public DateTime Date { get; set; }
    }

}

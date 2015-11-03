using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ListPopupDemo
{
    public class DataManager
    {
        public IEnumerable<Product> Load()
        {
            yield return new Product { Name = "Carrot", Color = Colors.Orange, Currency = "Euro/Kg", Value = 3.2M };
            yield return new Product { Name = "Car", Color = Colors.Black, Currency = "Euro", Value = 28559M };
            yield return new Product { Name = "Calculator", Color = Colors.Blue, Currency = "Euro", Value = 59.9M };
            yield return new Product { Name = "Table", Color = Colors.Brown, Currency = "Euro", Value = 150M };
            yield return new Product { Name = "Cherry", Color = Colors.Red, Currency = "Euro/Kg", Value = 27.5M };
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public decimal Value { get; set; }
        public string Currency { get; set; }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace UchebnayaPractica.Database
{
    public partial class Product
    {
        public Dictionary<Product, int> GetProductDetails()
        {
            var productsWithCount = GetDetails();
            productsWithCount.Add(this, 1);
            return productsWithCount;
        }

        private Dictionary<Product, int> GetDetails()
        {
            var products = new Dictionary<Product, int>();
            var childProducts = new List<Dictionary<Product, int>>();
            foreach(var a in ProductDetail)
            {
                if (products.Any(x => x.Key.Id == a.Product1.Id))
                    continue;
                products.Add(a.Product1, (int)a.Count);
                childProducts.Add(a.Product1.GetDetails());
            }
            int i = 0;
            foreach(var prod in products)
            {
                foreach(var pro in childProducts[i])
                {
                    products.Add(pro.Key, (int)pro.Value * prod.Value);
                }
                i++;
            }

            return products;
        }

        public TextBlock Passed
        {
            get
            {
                foreach(var test in Test)
                {
                    if(test.isPassed == false)
                        return new TextBlock() { Background= Brushes.Red, Text = "Не пройден"};
                }
                return new TextBlock() { Background = Brushes.LightGreen, Text = "Пройден" };
            }
        }
    }
}

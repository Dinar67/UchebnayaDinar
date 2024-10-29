
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        
    }
}

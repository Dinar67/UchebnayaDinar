using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace UchebnayaPractica.Database
{
    public partial class Order
    {
        public StatusOrder CurrentStatus
        {
            get { return StatusOrder.First(y => y.Id == StatusOrder.Max(x => x.Id)); }
        }

        public DateTime GetOrderDate
        {
            get
            {
                if (DateOrder == new DateTime())
                    return DateTime.Now.Date;
                return DateOrder.Date;
            }
        }

        public Visibility CanDelete
        {
            get
            {
                if(CurrentStatus.IdStatus == 1 && (App.currentUser.RoleId == 4 || App.currentUser.RoleId == 5))
                    return Visibility.Visible;

                return Visibility.Collapsed;
            }
        }
        public Dictionary<Material, decimal> GetMaterials()
        {
            var materialList = new Dictionary<Material, decimal>();
            var products = Product.GetProductDetails();

            foreach (var pro in products)
            {
                foreach (var mat in pro.Key.ProductMaterial)
                {
                    if (!materialList.Any(x => x.Key.Article == mat.MaterialArticle))
                        materialList.Add(mat.Material, mat.Count);
                    else
                        materialList[mat.Material] += mat.Count;
                }
            }
            return materialList;
        }

        public Dictionary<Accessories, decimal> GetAccessories()
        {
            var accessoriesList = new Dictionary<Accessories, decimal>();
            var products = Product.GetProductDetails();

            foreach (var pro in products)
            {
                foreach (var mat in pro.Key.ProductAccessories)
                {
                    if (!accessoriesList.Any(x => x.Key.Article == mat.AccessoriesArticle))
                        accessoriesList.Add(mat.Accessories, mat.Count);
                    else
                        accessoriesList[mat.Accessories] += mat.Count;
                }
            }
            return accessoriesList;
        }
    }
}

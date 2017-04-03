using System;
using System.Collections.Generic;

namespace Billing.Api.Models
{
    public class SalesByCategoryModel
    {
        public class ProductSales
        {
            public string ProductName { get; set; }
            public double ProductTotal { get; set; }
            public double CategoryPercent { get; set; }
            public double TotalPercent { get; set; }
        }

        public class CategorySales
        {
            public CategorySales()
            {
                Products = new List<ProductSales>();
            }
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public double CategoryTotal { get; set; }
            public double CategoryPercent { get; set; }
            public List<ProductSales> Products { get; set; }
        }

        public SalesByCategoryModel()
        {
            Sales = new List<CategorySales>();
        }
        public string Title { get; set; }
        public string Agent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double GrandTotal { get; set; }
        public List<CategorySales> Sales { get; set; }
    }
}
using System;
using System.Collections.Generic;

namespace Billing.Api.Models
{
    public class ProductSalesModel
    {
        public string ProductName { get; set; }
        public double ProductTotal { get; set; }
        public double CategoryPercent { get; set; }
        public double TotalPercent { get; set; }
    }

    public class CategorySalesModel
    {
        public CategorySalesModel()
        {
            Products = new List<ProductSalesModel>();
        }
        public string CategoryName { get; set; }
        public double CategoryTotal { get; set; }
        public double CategoryPercent { get; set; }
        public List<ProductSalesModel> Products { get; set; }
    }

    public class SalesByCategoryModel
    {
        public SalesByCategoryModel()
        {
            Sales = new List<CategorySalesModel>();
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double GrandTotal { get; set; }
        public List<CategorySalesModel> Sales { get; set; }
    }
}
using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Billing.Api.Reports
{
    public class SalesByCategoryReport : BaseReport
    {
        public SalesByCategoryReport(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public SalesByCategoryModel Report(RequestModel Request)
        {
            List<Item> Items = UnitOfWork.Items.Get().Where(x => x.Invoice.Date >= Request.StartDate && x.Invoice.Date <= Request.EndDate).ToList();
            SalesByCategoryModel result = new SalesByCategoryModel()
            {
                Title = "Sales by Category",
                Agent = Identity.CurrentUser.Name,
                StartDate = Request.StartDate,
                EndDate = Request.EndDate,
                GrandTotal = Items.Sum(x => x.SubTotal)
            };

            result.Sales = Items.OrderBy(x => x.Product.Category.Id).ToList()
                          .GroupBy(x => new { id = x.Product.Category.Id, name = x.Product.Category.Name })
                          .Select(x => new SalesByCategoryModel.CategorySales() { CategoryId = x.Key.id, CategoryName = x.Key.name, CategoryTotal = x.Sum(y => y.SubTotal) })
                          .ToList();
            foreach (var sale in result.Sales)
            {
                sale.CategoryPercent = Math.Round(100 * sale.CategoryTotal / result.GrandTotal, 2);
                sale.Products = Items.Where(x => x.Product.Category.Id == sale.CategoryId)
                               .GroupBy(x => x.Product.Name)
                               .Select(x => new SalesByCategoryModel.ProductSales()
                               {
                                   ProductName = x.Key,
                                   ProductTotal = x.Sum(y => y.SubTotal),
                                   CategoryPercent = Math.Round(100 * x.Sum(y => y.SubTotal) / sale.CategoryTotal, 2),
                                   TotalPercent = Math.Round(100 * x.Sum(y => y.SubTotal) / result.GrandTotal, 2)
                               }).OrderByDescending(x => x.ProductTotal).ToList();

            };
            return result;
        }

    }
}
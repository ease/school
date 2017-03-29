using Billing.Api.Models;
using Billing.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billing.Api.Reports
{
    public class ReportFactory
    {
        public MonthlySales Create(Region region, double sales)
        {
            return new MonthlySales()
            {
                Label = region.ToString(),
                Sales = sales
            };
        }

        public List<AnnualSales> Create(List<InputItem> list, int Length = 12)
        {
            List<AnnualSales> result = new List<AnnualSales>();
            AnnualSales current = new AnnualSales(Length);
            foreach (var item in list)
            {
                if (item.Label != current.Label)
                {
                    if (current.Label != null) result.Add(current);
                    current = new AnnualSales(Length);
                    current.Label = item.Label;
                }
                current.Sales[item.Index - 1] = item.Value;
            }
            if (current.Label != null) result.Add(current);
            return result;
        }

        public RegionSalesModel Create(List<Invoice> Invoices, string Region, double Sales)
        {
            double GrandTotal = Invoices.Sum(x => x.SubTotal);
            RegionSalesModel region = new RegionSalesModel()
            {
                RegionName = Region,
                RegionTotal = Sales,
                RegionPercent = Math.Round(100 * Sales / GrandTotal, 2)
            };
            region.Agents = Invoices.Where(x => x.Customer.Town.Region.ToString() == Region)
                                    .GroupBy(x => new { id = x.Agent.Id, name = x.Agent.Name })
                                    .Select(x => new AgentSalesModel()
                                    {
                                        AgentId = x.Key.id,
                                        AgentName = x.Key.name,
                                        AgentTotal = x.Sum(y => y.Total),
                                        RegionPercent = 100 * x.Sum(y => y.Total) / Sales,
                                        TotalPercent = 100 * x.Sum(y => y.Total) / GrandTotal
                                    })
                                    .ToList();
            return region;
        }

        public CategorySalesModel Create(List<Item> Items, int CategoryId, string Category, double Sales)
        {
            double GrandTotal = Items.Sum(x => x.SubTotal);
            CategorySalesModel category = new CategorySalesModel()
            {
                CategoryName = Category,
                CategoryTotal = Sales,
                CategoryPercent = Math.Round(100 * Sales / GrandTotal, 2)
            };
            category.Products = Items.Where(x => x.Product.Category.Id == CategoryId)
                                     .GroupBy(x => x.Product.Name)
                                     .Select(x => new ProductSalesModel()
                                     {
                                         ProductName = x.Key,
                                         ProductTotal = x.Sum(y => y.SubTotal),
                                         CategoryPercent = 100 * x.Sum(y => y.SubTotal) / Sales,
                                         TotalPercent = 100 * x.Sum(y => y.SubTotal) / GrandTotal
                                     }).OrderByDescending(x => x.ProductTotal)
                                    .ToList();
            return category;
        }

        public List<CustomerStatus> Customers(List<InputItem> list)
        {
            List<CustomerStatus> result = new List<CustomerStatus>();
            CustomerStatus current = new CustomerStatus();
            foreach (var item in list)
            {
                if (item.Label != current.Name)
                {
                    if (current.Name != null) result.Add(current);
                    current = new CustomerStatus();
                    current.Name = item.Label;
                }
                current.Debit += item.Value;
                if (item.Index > 3) current.Credit += item.Value;
            }
            if (current.Name != null) result.Add(current);
            return result.OrderByDescending(x => x.Debit).ToList();
        }

        public CustomerStatus Create(int Id, string Name, Status Status, double Amount) {
            return new CustomerStatus()
            {
                Id = Id,
                Name = Name,

            };
        }
    }
}
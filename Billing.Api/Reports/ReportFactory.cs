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
    }
}
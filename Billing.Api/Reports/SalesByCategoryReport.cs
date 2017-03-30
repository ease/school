using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
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
                StartDate = Request.StartDate,
                EndDate = Request.EndDate,
                GrandTotal = Items.Sum(x => x.SubTotal)
            };

            result.Sales = Items.OrderBy(x => x.Product.Category.Id).ToList()
                                   .GroupBy(x => new { id = x.Product.Category.Id, name = x.Product.Category.Name })
                                   .Select(x => Factory.Create(Items, x.Key.id, x.Key.name, x.Sum(y => y.SubTotal)))
                                   .ToList();
            return result;
        }
    }
}
using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Billing.Api.Reports
{
    public class SalesByCategoryReport
    {
        private ReportFactory Factory = new ReportFactory();
        private BillingIdentity _identity;
        private UnitOfWork _unitOfWork;
        public SalesByCategoryReport(UnitOfWork unitOfWork, BillingIdentity identity)
        {
            _unitOfWork = unitOfWork;
            _identity = identity;
        }

        public SalesByCategoryModel Report(RequestModel Request)
        {
            List<Item> Items = _unitOfWork.Items.Get().Where(x => x.Invoice.Date >= Request.StartDate && x.Invoice.Date <= Request.EndDate).ToList();
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
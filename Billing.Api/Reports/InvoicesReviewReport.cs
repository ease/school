using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
using System.Linq;

namespace Billing.Api.Reports
{
    public class InvoicesReviewReport : BaseReport
    {
        public InvoicesReviewReport(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public InvoicesReviewModel Report(RequestModel request)
        {
            Customer customer = UnitOfWork.Customers.Get(request.Id);
            InvoicesReviewModel result = new InvoicesReviewModel() {
                StartDate=request.StartDate,
                EndDate =request.EndDate,
                CustomerId = customer.Id,
                CustomerName = customer.Name,
                GrandTotal = customer.Invoices.Sum(x => x.Total)
            };

            result.Invoices = customer.Invoices.OrderBy(x => x.Date).ToList()
                                      .Select(x => new InvoicesReviewModel.InvoiceModel()
                                      {
                                          InvoiceNo = x.InvoiceNo,
                                          CustomerName = x.Customer.Name,
                                          InvoiceDate = x.Date,
                                          InvoiceStatus = x.Status.ToString(),
                                          Subtotal = x.SubTotal,
                                          VatAmount = x.VatAmount,
                                          Shipping = x.Shipping,
                                          Shipper = (x.Shipper == null) ? " " : x.Shipper.Name,
                                          ShippedOn = x.ShippedOn,
                                          Items = x.Items.Select(y => new InvoicesReviewModel.ItemModel()
                                          {
                                              ProductName = y.Product.Name,
                                              Quantity = y.Quantity,
                                              Price = y.Price,
                                              Subtotal = y.SubTotal
                                          }).ToList()
                                      }).ToList();

            return result;
        }
    }
}
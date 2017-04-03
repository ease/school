using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billing.Api.Reports
{
    public class InvoiceReport : BaseReport
    {
        public InvoiceReport(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public InvoiceReportModel Report(int id)
        {
            Invoice invoice = UnitOfWork.Invoices.Get(id);
            return new InvoiceReportModel()
            {
                InvoiceNo = invoice.InvoiceNo,
                Date = invoice.Date,
                ShippedOn = invoice.ShippedOn,
                Status = invoice.Status.ToString(),
                SubTotal = invoice.SubTotal,
                Vat = invoice.Vat,
                VatAmount = invoice.VatAmount,
                Shipping = invoice.Shipping,
                Total = invoice.Total,
                Agent = invoice.Agent.Name,
                Shipper = invoice.Shipper.Name,
                Customer = new CustomerModel()
                {
                    Name = invoice.Customer.Name,
                    Address = invoice.Customer.Address,
                    Town = invoice.Customer.Town.Zip + " " + invoice.Customer.Town.Name
                },
                Items = invoice.Items.Select(x => new InvoiceReportModel.ItemModel()
                {
                    Product = x.Product.Name,
                    Unit = x.Product.Unit,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    SubTotal = x.SubTotal
                }).ToList()
            };
        }
    }
}
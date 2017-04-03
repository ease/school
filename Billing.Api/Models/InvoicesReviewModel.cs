using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billing.Api.Models
{
    public class InvoicesReviewModel
    {
        public class ItemModel
        {
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
            public double Subtotal { get; set; }
        }

        public class InvoiceModel
        {
            public InvoiceModel()
            {
                Items = new List<ItemModel>();
            }
            public string InvoiceNo { get; set; }
            public string CustomerName { get; set; }
            public DateTime InvoiceDate { get; set; }
            public string InvoiceStatus { get; set; }
            public double Subtotal { get; set; }
            public double VatAmount { get; set; }
            public double Shipping { get; set; }
            public string Shipper { get; set; }
            public DateTime? ShippedOn { get; set; }
            public List<ItemModel> Items { get; set; }
        }

        public InvoicesReviewModel()
        {
            Invoices = new List<InvoiceModel>();
        }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double GrandTotal { get; set; }
        public List<InvoiceModel> Invoices { get; set; }
    }
}

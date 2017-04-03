using Billing.Database;
using System;
using System.Collections.Generic;

namespace Billing.Api.Models
{
    public class InvoiceReportModel
    {
        public InvoiceReportModel()
        {
            Items = new List<ItemModel>();
        }

        public class ItemModel
        {
            public string Product { get; set; }
            public int Quantity { get; set; }
            public string Unit { get; set; }
            public double Price { get; set; }
            public double SubTotal { get; set; }
        }

        public string InvoiceNo { get; set; }
        public DateTime Date { get; set; }
        public DateTime? ShippedOn { get; set; }
        public string Status { get; set; }
        public double SubTotal { get; set; }
        public double Vat { get; set; }
        public double VatAmount { get; set; }
        public double Shipping { get; set; }
        public double Total { get; set; }
        public string Agent { get; set; }
        public CustomerModel Customer { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerTown { get; set; }
        public string Shipper { get; set; }
        public List<ItemModel> Items { get; set; }
    }
}
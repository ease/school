using System;
using System.Collections.Generic;

namespace Billing.Api.Models
{
    public class CustomerSalesModel
    {
        public string CustomerName { get; set; }
        public double CustomerTurnover { get; set; }
        public double CustomerPercent { get; set; }
    }

    public class SalesByCustomerModel
    {
        public SalesByCustomerModel()
        {
            Sales = new List<CustomerSalesModel>();
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double GrandTotal { get; set; }
        public List<CustomerSalesModel> Sales { get; set; }
    }
}
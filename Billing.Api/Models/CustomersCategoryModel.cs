using Billing.Database;
using System;
using System.Collections.Generic;

namespace Billing.Api.Models
{
    public class CustomersCategoryModel
    {
        public class InputModel
        {
            public string Row { get; set; }
            public int Column { get; set; }
            public double Value { get; set; }
        }

        public class CustomerModel
        {
            public CustomerModel()
            {
                Name = " ";
                Turnover = 0;
                Sales = new Dictionary<int, double>();
            }
            public string Name { get; set; }
            public double Turnover { get; set; }
            public Dictionary<int, double> Sales { get; set; }
        }

        public CustomersCategoryModel()
        {
            Customers = new List<CustomerModel>();
        }
        public string Title { get; set; }
        public string Agent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<CustomerModel> Customers { get; set; }
    }
}
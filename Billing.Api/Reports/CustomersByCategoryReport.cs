using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Billing.Api.Reports
{
    public class CustomersByCategoryReport : BaseReport
    {
        public CustomersByCategoryReport(UnitOfWork unitOfWork) : base(unitOfWork) { }

        public CustomersCategoryModel Report(RequestModel request)
        {
            CustomersCategoryModel result = new CustomersCategoryModel()
            {
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Title = "Sales by Agents / Regions",
                Agent = Identity.CurrentUser.Name
            };

            List<int> categories = UnitOfWork.Categories.Get().Select(x => x.Id).ToList();

            List<CustomersCategoryModel.InputModel> input;
            var query = UnitOfWork.Items.Get().Where(x => (x.Invoice.Date >= request.StartDate && x.Invoice.Date <= request.EndDate)).ToList();

            input = query.GroupBy(x => x.Product.Category.Id)
                         .Select(x => new CustomersCategoryModel.InputModel() { Row = "TOTAL", Column = x.Key, Value = x.Sum(y => y.SubTotal) })
                         .ToList();
            CustomersCategoryModel.CustomerModel customer = new CustomersCategoryModel.CustomerModel();
            customer.Name = "TOTAL";
            foreach (var cat in categories) customer.Sales[cat] = 0;
            foreach (var item in input)
            {
                customer.Sales[item.Column] = item.Value;
                customer.Turnover += item.Value;
            }

            input = query.OrderBy(x => x.Invoice.Customer.Id).ThenBy(x => x.Product.Category.Id)
                         .GroupBy(x => new { customer = x.Invoice.Customer.Name, category = x.Product.Category.Id })
                         .Select(x => new CustomersCategoryModel.InputModel { Row = x.Key.customer, Column = x.Key.category, Value = x.Sum(y => y.SubTotal) })
                         .ToList();
            foreach (var item in input)
            {
                if (customer.Name != item.Row)
                {
                    result.Customers.Add(customer);
                    customer = new CustomersCategoryModel.CustomerModel() { Name = item.Row };
                    foreach (var cat in categories) customer.Sales[cat] = 0;
                }
                customer.Sales[item.Column] = item.Value;
                customer.Turnover += item.Value;
            }
            result.Customers.Add(customer);

            result.Customers = result.Customers.OrderByDescending(x => x.Turnover).ToList();
            return result;
        }
    }
}
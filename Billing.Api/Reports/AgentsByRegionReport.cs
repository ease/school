using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Billing.Api.Reports
{
    public class AgentsByRegionReport
    {
        private ReportFactory Factory = new ReportFactory();
        private BillingIdentity _identity;
        private UnitOfWork _unitOfWork;
        public AgentsByRegionReport(UnitOfWork unitOfWork, BillingIdentity identity)
        {
            _unitOfWork = unitOfWork;
            _identity = identity;
        }

        public AgentsRegionModel Report(RequestModel request)
        {
            AgentsRegionModel result = new AgentsRegionModel();
            result.StartDate = request.StartDate;
            result.EndDate = request.EndDate;

            var query = _unitOfWork.Invoices.Get().Where(x => (x.Date >= request.StartDate && x.Date <= request.EndDate)).ToList();

            result.Regions = query.OrderBy(x => x.Customer.Town.Region.ToString())
                                  .GroupBy(x => x.Customer.Town.Region.ToString())
                                  .Select(x => new RegionModel() { Region = x.Key, Total = x.Sum(y => y.SubTotal) })
                                  .ToList();
            result.GrandTotal = result.Regions.Sum(x => x.Total);
            
            List<InputModel> input = query.GroupBy(x => new { agent = x.Agent.Name, region = x.Customer.Town.Region })
                                          .Select(x => new InputModel { Row = x.Key.agent, Column = x.Key.region, Value = x.Sum(y => y.SubTotal) })
                                          .ToList();

            AgentRegionModel agent = new AgentRegionModel();
            foreach(var item in input)
            {
                if(agent.Name != item.Row)
                {
                    if (string.IsNullOrEmpty(agent.Name)) result.Agents.Add(agent);
                    agent = new AgentRegionModel();
                    agent.Name = item.Row;
                }
                agent.Sales[item.Column] = item.Value;
                agent.Turnover += item.Value;
            }
            if (string.IsNullOrEmpty(agent.Name)) result.Agents.Add(agent);
            return result;
        }
    }
}
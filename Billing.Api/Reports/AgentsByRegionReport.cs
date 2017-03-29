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

            result.GrandTotal = query.Sum(x => x.SubTotal);

            List<InputModel> input;
            input = query.GroupBy(x => new { region = x.Customer.Town.Region })
                         .Select(x => new InputModel { Row = "TOTAL", Column = x.Key.region, Value = x.Sum(y => y.SubTotal) })
                         .ToList();
            AgentRegionModel agent = new AgentRegionModel();
            agent.Name = "TOTAL";
            foreach (var item in input)
            {
                agent.Sales[item.Column] = item.Value;
                agent.Turnover += item.Value;
            }
            input = query.OrderBy(x => x.Agent.Name)
                         .GroupBy(x => new { agent = x.Agent.Name, region = x.Customer.Town.Region })
                         .Select(x => new InputModel { Row = x.Key.agent, Column = x.Key.region, Value = x.Sum(y => y.SubTotal) })
                         .ToList();
            foreach(var item in input)
            {
                if(agent.Name != item.Row)
                {
                    result.Agents.Add(agent);
                    agent = new AgentRegionModel();
                    agent.Name = item.Row;
                }
                agent.Sales[item.Column] = item.Value;
                agent.Turnover += item.Value;
            }
            result.Agents.Add(agent);
            return result;
        }
    }
}
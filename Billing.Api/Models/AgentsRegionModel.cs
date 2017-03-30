using Billing.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billing.Api.Models
{
    public class InputModel
    {
        public string Row { get; set; }
        public Region Column { get; set; }
        public double Value { get; set; }
    }

    public class AgentRegionModel
    {
        public AgentRegionModel()
        {
            Name = " ";
            Turnover = 0;
            Sales = new Dictionary<Database.Region, double>();
            foreach (Region reg in Region.GetValues(typeof(Region))) Sales[reg] = 0;
        }
        public string Name { get; set; }
        public double Turnover { get; set; }
        public Dictionary<Region, double> Sales { get; set; }
    }

    public class AgentsRegionModel
    {
        public AgentsRegionModel()
        {
            Agents = new List<AgentRegionModel>();
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<AgentRegionModel> Agents { get; set; }
    }
}
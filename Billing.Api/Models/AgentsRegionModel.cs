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

    public class RegionModel
    {
        public string Region { get; set; }
        public double Total { get; set; }
    }

    public class AgentRegionModel
    {
        public AgentRegionModel() //List<Region> regions
        {
            Sales = new Dictionary<Region, double>();
            //foreach (var region in regions)
            //{
            //    Sales[region.ToString()] = 0;
            //}
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
            Regions = new List<RegionModel>();
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double GrandTotal { get; set; }
        public List<AgentRegionModel> Agents { get; set; }
        public List<RegionModel> Regions { get; set; }
    }
}
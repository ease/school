using Billing.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billing.Api.Models
{
    public class Factory
    {
        public AgentModel Create(Agent agent)
        {
            AgentModel model = new AgentModel()
            {
                Id = agent.Id,
                Name = agent.Name
            };
            foreach (Town town in agent.Towns.Where(x => x.Customers.Count != 0).ToList())
            {
                model.Towns.Add(town.Name);
            }
            return model;
        }
    }
}
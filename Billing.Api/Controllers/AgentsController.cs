using Billing.Database;
using Billing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Billing.Api.Controllers
{
    [RoutePrefix("api/agents")]
    public class AgentsController : ApiController
    {
        public class AgentModel
        {
            public AgentModel()
            {
                Towns = new List<string>();
            }
            public int Id { get; set; }
            public string Name { get; set; }
            public List<string> Towns { get; set; }
        }
        [Route("")]
        public IHttpActionResult Get()
        {
            IBillingRepository<Agent> agents = new BillingRepository<Agent>(new BillingContext());
            List<Agent> list = agents.Get().ToList();
            List<AgentModel> result = new List<AgentModel>();
            foreach (Agent agent in list)
            {
                AgentModel agModel = new AgentModel()
                {
                    Id = agent.Id,
                    Name = agent.Name
                };
                foreach (Town town in agent.Towns.Where(x => x.Customers.Count != 0).ToList())
                {
                    agModel.Towns.Add(town.Name);
                }
                result.Add(agModel);
            }
            return Ok(result);
        }
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            IBillingRepository<Agent> agents = new BillingRepository<Agent>(new BillingContext());
            Agent agent = agents.Get(id);
            if (agent == null) return NotFound();

            AgentModel result = new AgentModel()
            {
                Id = agent.Id,
                Name = agent.Name
            };
            foreach (Town town in agent.Towns.Where(x => x.Customers.Count != 0).ToList())
            {
                result.Towns.Add(town.Name);
            }
            return Ok(result);
        }
        [Route("{name}")]
        public IHttpActionResult Get(string name)
        {
            IBillingRepository<Agent> agents = new BillingRepository<Agent>(new BillingContext());
            List<Agent> list = agents.Get().Where(x => x.Name.Contains(name)).ToList();
            List<AgentModel> result = new List<AgentModel>();
            foreach (Agent agent in list)
            {
                AgentModel agModel = new AgentModel()
                {
                    Id = agent.Id,
                    Name = agent.Name
                };
                foreach (Town town in agent.Towns.Where(x => x.Customers.Count != 0).ToList())
                {
                    agModel.Towns.Add(town.Name);
                }
                result.Add(agModel);
            }
            return Ok(result);
        }
    }
}

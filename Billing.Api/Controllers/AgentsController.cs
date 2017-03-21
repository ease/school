using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
using System;
using System.Linq;
using System.Web.Http;
using WebMatrix.WebData;

namespace Billing.Api.Controllers
{
    [RoutePrefix("api/agents")]
    public class AgentsController : BaseController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(UnitOfWork.Agents.Get().ToList().Select(x => Factory.Create(x)).ToList());
        }

        [Route("{name}")]
        public IHttpActionResult Get(string name)
        {
            return Ok(UnitOfWork.Agents.Get().Where(x => x.Name.Contains(name)).ToList().Select(a => Factory.Create(a)).ToList());
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Agent agent = UnitOfWork.Agents.Get(id);
                if (agent == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(Factory.Create(agent));
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("")]
        public IHttpActionResult Post(AgentModel model)
        {
            try
            {
                Agent agent = Factory.Create(model);
                UnitOfWork.Agents.Insert(agent);
                UnitOfWork.Agents.Commit();
                return Ok(Factory.Create(agent));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id:int}")]
        public IHttpActionResult Put(int id, AgentModel model)
        {
            try
            {
                Agent agent = Factory.Create(model);
                UnitOfWork.Agents.Update(agent, id);
                UnitOfWork.Agents.Commit();
                return Ok(Factory.Create(agent));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Agent agent = UnitOfWork.Agents.Get(id);
                if (agent == null) return NotFound();
                UnitOfWork.Agents.Delete(id);
                UnitOfWork.Agents.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("profiles")]
        [HttpGet]
        public IHttpActionResult CreateProfiles()
        {
            WebSecurity.InitializeDatabaseConnection("Billing", "UserProfile", "UserId", "UserName", autoCreateTables: true);
            foreach (var agent in UnitOfWork.Agents.Get())
            {
                string[] names = agent.Name.Split(' ');
                WebSecurity.CreateUserAndAccount(names[0], "billing", false);
            }
            return Ok("user profiles created");
        }
    }
}

using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
using System;
using System.Linq;
using System.Web.Http;

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
        public IHttpActionResult Post([FromBody]Agent agent)
        {
            try
            {
                UnitOfWork.Agents.Insert(agent);
                UnitOfWork.Commit();
                return Ok(agent);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        public IHttpActionResult Put([FromUri] int id, [FromBody]Agent agent)
        {
            try
            {
                UnitOfWork.Agents.Update(agent, id);
                UnitOfWork.Commit();
                return Ok(agent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

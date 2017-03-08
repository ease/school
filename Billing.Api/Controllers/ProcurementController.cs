using Billing.Database;
using System;
using System.Linq;
using System.Web.Http;

namespace Billing.Api.Controllers
{
    [RoutePrefix("api/procurements")]
    public class ProcurementsController : BaseController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(UnitOfWork.Procurements.Get().ToList().Select(x => Factory.Create(x)).ToList());
        }

        [Route("doc/{doc}")]
        public IHttpActionResult Get(string doc)
        {
            return Ok(UnitOfWork.Procurements.Get().Where(x => x.Document == doc).ToList().Select(x => Factory.Create(x)).ToList());
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Procurement procurement = UnitOfWork.Procurements.Get(id);
                if (procurement == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(Factory.Create(procurement));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

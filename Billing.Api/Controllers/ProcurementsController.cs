using Billing.Api.Models;
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

        [Route("")]
        public IHttpActionResult Post(ProcurementModel model)
        {
            try
            {
                Procurement procurement = Factory.Create(model);
                UnitOfWork.Procurements.Insert(procurement);
                UnitOfWork.Commit();
                return Ok(Factory.Create(procurement));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        public IHttpActionResult Put(int id, ProcurementModel model)
        {
            try
            {
                Procurement procurement = Factory.Create(model);
                UnitOfWork.Procurements.Update(procurement, id);
                UnitOfWork.Commit();
                return Ok(Factory.Create(procurement));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                UnitOfWork.Procurements.Delete(id);
                UnitOfWork.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using Billing.Api.Models;
using System;
using System.Web.Http;

namespace Billing.Api.Controllers
{
    public class StockLevelController : BaseController
    {
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(Reports.StockLevel.Report(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
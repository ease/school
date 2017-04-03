using Billing.Api.Models;
using System;
using System.Web.Http;

namespace Billing.Api.Controllers
{
    public class CustomersByCategoryController : BaseController
    {
        public IHttpActionResult Post(RequestModel request)
        {
            try
            {
                return Ok(Reports.CustomersByCategory.Report(request));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

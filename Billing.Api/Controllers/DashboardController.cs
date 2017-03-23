using Billing.Api.Helpers;
using Billing.Api.Reports;
using System;
using System.Web.Http;

namespace Billing.Api.Controllers
{
    public class DashboardController : BaseController
    {
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(Reports.Dashboard.Report());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

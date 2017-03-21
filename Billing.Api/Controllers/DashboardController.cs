using Billing.Api.Helpers;
using Billing.Api.Reports;
using System;
using System.Web.Http;

namespace Billing.Api.Controllers
{
    [TokenAuthorization]
    public class DashboardController : BaseController
    {
        private BillingIdentity identity = new BillingIdentity();

        public IHttpActionResult Get()
        {
            try
            {
                return Ok(DashboardReport.Report(UnitOfWork, identity.currentUser));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

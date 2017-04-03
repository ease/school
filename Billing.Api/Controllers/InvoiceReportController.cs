using System;
using System.Web.Http;

namespace Billing.Api.Controllers
{
    public class InvoiceReportController : BaseController
    {
        public IHttpActionResult Get(int id)
        {
            try
            {
                return Ok(Reports.Invoice.Report(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

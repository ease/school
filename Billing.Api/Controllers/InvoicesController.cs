﻿using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
using System;
using System.Linq;
using System.Web.Http;

namespace Billing.Api.Controllers
{
    [RoutePrefix("api/invoices")]
    public class InvoicesController : BaseController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(UnitOfWork.Invoices.Get().ToList().Select(x => Factory.Create(x)).ToList());
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Invoice invoice = UnitOfWork.Invoices.Get(id);
                if (invoice == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(Factory.Create(invoice));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
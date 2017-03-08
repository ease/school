﻿using Billing.Database;
using System;
using System.Linq;
using System.Web.Http;

namespace Billing.Api.Controllers
{
    [RoutePrefix("api/towns")]
    public class TownsController : BaseController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(UnitOfWork.Towns.Get().ToList().Select(x => Factory.Create(x)).ToList());
        }

        [Route("{name}")]
        public IHttpActionResult Get(string name)
        {
            return Ok(UnitOfWork.Towns.Get().Where(x => x.Name.Contains(name)).ToList().Select(a => Factory.Create(a)).ToList());
        }

        [Route("zip/{zip}")]
        public IHttpActionResult GetZip(string zip)
        {
            try
            {
                Town town = UnitOfWork.Towns.Get().FirstOrDefault(x => x.Zip == zip);
                if (town == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(Factory.Create(town));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Town town = UnitOfWork.Towns.Get(id);
                if (town == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(Factory.Create(town));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

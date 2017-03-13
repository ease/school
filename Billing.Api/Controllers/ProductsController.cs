﻿using Billing.Api.Models;
using Billing.Database;
using Billing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Billing.Api.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : BaseController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(UnitOfWork.Products.Get().ToList().Select(x => Factory.Create(x)).ToList());
        }

        [Route("{name}")]
        public IHttpActionResult Get(string name)
        {
            return Ok(UnitOfWork.Products.Get().Where(x => x.Name.Contains(name)).ToList().Select(a => Factory.Create(a)).ToList());
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            Product product = UnitOfWork.Products.Get(id);
            if (product == null) return NotFound();
            return Ok(Factory.Create(product));
        }

        [Route("")]
        public IHttpActionResult Post(ProductModel model)
        {
            try
            {
                Product product = Factory.Create(model);
                UnitOfWork.Products.Insert(product);
                UnitOfWork.Commit();
                return Ok(Factory.Create(product));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id:int}")]
        public IHttpActionResult Put(int id, ProductModel model)
        {
            try
            {
                Product product = Factory.Create(model);
                UnitOfWork.Products.Update(product, id);
                UnitOfWork.Commit();
                return Ok(Factory.Create(product));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                UnitOfWork.Products.Delete(id);
                UnitOfWork.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("stock")]
        [HttpGet]
        public IHttpActionResult Leverage()
        {
            List<Product> products = UnitOfWork.Products.Get().ToList();
            foreach (Product product in products)
            {
                product.Stock.Input = product.Procurements.Sum(x => x.Quantity);
                product.Stock.Output = product.Items.Sum(x => x.Quantity);
                UnitOfWork.Products.Update(product, product.Id);
            }
            UnitOfWork.Commit();
            return Ok("Inventory leveraged for all products.");
        }
    }
}

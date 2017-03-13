using Microsoft.VisualStudio.TestTools.UnitTesting;
using Billing.Api.Controllers;
using System.Web.Http;
using System.Threading;
using System.Net.Http;
using System.Web.Http.Routing;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using Billing.Database;
using Billing.Api.Models;
using Billing.Repository;

namespace Billing.Tests
{
    [TestClass]
    public class TestProductsController
    {
        ProductsController controller = new ProductsController();
        HttpConfiguration config = new HttpConfiguration();

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "api/products");

        [TestInitialize]
        void InitTest()
        {
            using (BillingContext context = new BillingContext())
            {
                context.Database.Delete();
                context.Database.Create();

                Category category = context.Categories.Add(new Category() { Name = "Test category number one" });
                context.SaveChanges();

                Product product = context.Products.Add(new Product() { Name = "Test product number one", Unit = "pcs", Price = 100, Category = category });
                context.SaveChanges();

                context.Stocks.Add(new Stock() { Id = product.Id, Input = 120, Output = 66 });
                context.Categories.Add(new Category() { Name = "Test category number two" });
                context.SaveChanges();
            }
        }

        void GetReady()
        {
            var route = config.Routes.MapHttpRoute("default", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "products" } });

            controller.ControllerContext = new HttpControllerContext(config, routeData, request);
            controller.Request = request;
            controller.Request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
        }

        [TestMethod]
        public void GetAllProducts()
        {
            InitTest();
            GetReady();
            var actRes = controller.Get();
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public void GetProductById()
        {
            GetReady();
            var actRes = controller.Get(1);
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsNotNull(response.Content);
        }

        [TestMethod]
        public void GetProductByWrongId()
        {
            GetReady();
            var actRes = controller.Get(999);
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsNull(response.Content);
        }

        [TestMethod]
        public void PostProductGood()
        {
            GetReady();
            var actRes = controller.Post(new ProductModel() { Name = "Brand new product", Unit="pcs", Price=100, CategoryId=1 });
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void PostProductBad()
        {
            GetReady();
            var actRes = controller.Post(new ProductModel() { Name = "Brand new product", Unit = "pcs", Price = 100, CategoryId = 999 });
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsFalse(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void ChangeName()
        {
            GetReady();
            var actRes = controller.Put(1, new ProductModel() { Id = 1, Name = "New name for old product", Unit = "pcs", Price = 100, CategoryId = 1 });
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void ChangeCategory()
        {
            GetReady();
            var actRes = controller.Put(1, new ProductModel() { Id = 1, Name = "Brand new product", Unit = "pcs", Price = 100, CategoryId = 2 });
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void DeleteById()
        {
            GetReady();
            var actRes = controller.Delete(3);
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [TestMethod]
        public void DeleteByWrongId()
        {
            GetReady();
            var actRes = controller.Delete(999);
            var response = actRes.ExecuteAsync(CancellationToken.None).Result;

            Assert.IsFalse(response.IsSuccessStatusCode);
        }
    }
}
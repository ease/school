using Billing.Api.Helpers;
using Billing.Api.Models;
using Billing.Database;
using System;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http;
using WebMatrix.WebData;

namespace Billing.Api.Controllers
{
    public class LoginController : BaseController
    {
        [BillingAuthorization]
        [Route("api/login")]
        [HttpPost]
        public IHttpActionResult Login(TokenRequestModel request)
        {
            ApiUser apiUser = UnitOfWork.ApiUsers.Get().FirstOrDefault(x => x.AppId == request.ApiKey);
            if (apiUser == null) return NotFound();
            if (Helper.Signature(apiUser.Secret, apiUser.AppId) != request.Signature) return BadRequest("Bad application signature");

            string rawTokenInfo = DateTime.Now.Ticks.ToString() + apiUser.AppId;
            byte[] rawTokenByte = Encoding.UTF8.GetBytes(rawTokenInfo);
            var authToken = new AuthToken()
            {
                Token = Convert.ToBase64String(rawTokenByte),
                Expiration = DateTime.Now.AddMinutes(20),
                ApiUser = apiUser
            };

            UnitOfWork.Tokens.Insert(authToken);
            UnitOfWork.Commit();
            return Ok(Factory.Create(authToken, new BillingIdentity(UnitOfWork).CurrentUser));
        }

        [Route("api/logout")]
        [HttpGet]
        public IHttpActionResult Logout()
        {
            if (!WebSecurity.Initialized) WebSecurity.InitializeDatabaseConnection("Billing", "UserProfile", "UserId", "UserName", autoCreateTables: true);
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                string message = $"User {Thread.CurrentPrincipal.Identity.Name} logged out";
                WebSecurity.Logout();
                return Ok(message);
            }
            else
            {
                return Ok("No user is logged in!!!");
            }

        }
    }
}
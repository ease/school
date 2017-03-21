﻿using Billing.Repository;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Billing.Api.Helpers
{
    public class TokenAuthorizationAttribute: AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string ApiKey = actionContext.Request.Headers.GetValues("ApiKey").SingleOrDefault();
            string Token = actionContext.Request.Headers.GetValues("Token").SingleOrDefault();

            UnitOfWork unitOfWork = new UnitOfWork();
            var token = unitOfWork.Tokens.Get().FirstOrDefault(x => x.Token == Token);
            if (token != null)
            {
                if (token.ApiUser.AppId == ApiKey && 
                    token.Expiration > DateTime.UtcNow)
                    return;
            }
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        }
    }
}
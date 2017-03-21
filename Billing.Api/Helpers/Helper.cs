﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace Billing.Api.Helpers
{
    public static class Helper
    {
        private static readonly log4net.ILog log = 
            log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static void Log(string Message, string Level = "ERROR")
        {
            if (HttpContext.Current != null)
            {
                Message += ": " + HttpContext.Current.Request.Url.AbsoluteUri;
            }

            if (Level == "INFO") log.Info(Message); else log.Error(Message);

            //if (!url.ToLower().Contains("localhost")) Email.Send(“admin@billing.com", “>> " + Level, Message);
        }

        internal static string Signature(string Secret, string AppId)
        {
            byte[] secret = Convert.FromBase64String(Secret);
            byte[] appId = Convert.FromBase64String(AppId);

            var provider = new System.Security.Cryptography.HMACSHA256(secret);
            string key = System.Text.Encoding.Default.GetString(appId);
            var hash = provider.ComputeHash(Encoding.UTF8.GetBytes(key));

            return Convert.ToBase64String(hash);
        }
    }
}
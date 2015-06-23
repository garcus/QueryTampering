using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http.Core;
using Microsoft.AspNet.Http.Extensions;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System.Text;
using System.Security.Cryptography;
using QueryTampering.Utils;
using Microsoft.Framework.ConfigurationModel;

namespace QueryTampering.Middleware
{
    public class QSHashMiddleware
    {
        private IConfiguration _config;
        private readonly RequestDelegate next;
        private readonly string _hashKey;

        public QSHashMiddleware(IConfiguration config, RequestDelegate next)
        {
            this.next = next;
            _config = config;
            _hashKey = _config.Get("Security:HashKey");
        }

        public Task Invoke(HttpContext context)
        {
            ValidateQueryString(context);
            return next(context);
        }

        public void ValidateQueryString(HttpContext context)
        {
            string qs = context.Request.QueryString.Value;
            if (!context.Request.QueryString.HasValue)
                return;  // Nothing to validate

            qs = qs.TrimStart(new char[] { '?' });
            string submittedHash = context.Request.Query["h"];
            if (submittedHash == null)
                throw new Exception("Querystring validation hash missing!");

            int hashPos = qs.IndexOf("&h=");
            qs = qs.Substring(0, hashPos);
            if (submittedHash != Security.ComputeHash(qs, _hashKey))
                throw new Exception("Querystring hash value mismatch");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using WebServer.Server.Common;
using WebServer.Server.Http.Contracts;

namespace WebServer.Server.Http
{
    public class HttpContext : IHttpContext
    {
        private readonly IHttpRequest request;

        public HttpContext(IHttpRequest requestString)
        {
            CoreValidator.ThrowIfNull(requestString, nameof(requestString));

            request = requestString;
        }

        public IHttpRequest Request => this.request;
    }
}

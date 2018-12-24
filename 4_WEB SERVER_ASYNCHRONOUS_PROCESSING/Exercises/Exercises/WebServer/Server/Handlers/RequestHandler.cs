using System;
using System.Collections.Generic;
using System.Text;
using WebServer.Server.Common;
using WebServer.Server.Handlers.Contracts;
using WebServer.Server.Http;
using WebServer.Server.Http.Contracts;

namespace WebServer.Server.Handlers
{
    public class RequestHandler : IRequestHandler
    {
        private readonly Func<IHttpRequest, IHttpResponse> func;

        public RequestHandler(Func<IHttpRequest, IHttpResponse> func)
        {
            CoreValidator.ThrowIfNull(func, nameof(func));

            this.func = func;
        }

        public IHttpResponse Handle(IHttpContext context)
        {
            var response = this.func(context.Request);

            if (!response.Headers.ContainsKey(HttpHeader.ContentType))
            {
                response.Headers.Add(new HttpHeader("Content-Type", "text/plain"));
            }

            return response;
        }
    }
}

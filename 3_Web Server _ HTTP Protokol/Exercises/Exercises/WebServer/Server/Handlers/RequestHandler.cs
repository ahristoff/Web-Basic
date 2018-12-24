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
            string sessionIdToSend = null;

            if (!context.Request.Cookies.ContainsKey(SessionStore.SessionCookieKey))
                //5 session
            {
                var sessionId = Guid.NewGuid().ToString();

                context.Request.Session = SessionStore.Get(sessionId);//generate new Session

                sessionIdToSend = sessionId;
            }

            var response = this.func(context.Request);

            if (sessionIdToSend != null)  //session sent to response like a header
            {
                response.Headers.Add(
                    HttpHeader.SetCookie,
                    $"{SessionStore.SessionCookieKey}={sessionIdToSend}; HttpOnly; path=/");
            }

            if (!response.Headers.ContainsKey(HttpHeader.ContentType))
            {
                response.Headers.Add(HttpHeader.ContentType, "text/plain");
            }

            foreach (var x in response.Cookies) //10 Cookies
            {
                response.Headers.Add(HttpHeader.SetCookie, x.ToString());
            }

            return response;
        }
    }
}

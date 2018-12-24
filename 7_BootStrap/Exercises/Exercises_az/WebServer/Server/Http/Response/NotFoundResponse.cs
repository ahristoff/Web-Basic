using System;
using System.Collections.Generic;
using System.Text;
using WebServer.Server.Enums;

namespace WebServer.Server.Http.Response
{
    using Common;
    using Enums;

    public class NotFoundResponse : ViewResponse
    {
        public NotFoundResponse()
            : base(HttpStatusCode.NotFound, new NotFoundView())
        {
        }
    }
}

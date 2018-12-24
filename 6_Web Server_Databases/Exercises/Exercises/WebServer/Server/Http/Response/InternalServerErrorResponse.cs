
namespace WebServer.Server.Http.Response
{
    using Enums;
    using Server.Common;
    using System;

    public class InternalServerErrorResponse : ViewResponse
    {
        public InternalServerErrorResponse(Exception ex)
            : base(HttpStatusCode.InternalserverError, new InternalServerErrorView(ex))
        {
        }
    }
}

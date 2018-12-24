using System;
using System.Collections.Generic;
using System.Text;

namespace WebServer.Server.Enums
{
    public enum HttpStatusCode
    {
        Ok = 200,
        MovedPermanently = 301,
        Found = 302,
        MovedTemporary = 303,
        BadRequest = 400,
        NotAuthorized = 401,
        NotFound = 404,
        InternalserverError = 500
    }
}

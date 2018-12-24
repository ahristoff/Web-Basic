
namespace WebServer.Server.Http.Response
{
    public class BadRequestResponse: HttpResponse
    {
        public BadRequestResponse()
        {
            this.StatusCode = Enums.HttpStatusCode.BadRequest;
        }
    }
}

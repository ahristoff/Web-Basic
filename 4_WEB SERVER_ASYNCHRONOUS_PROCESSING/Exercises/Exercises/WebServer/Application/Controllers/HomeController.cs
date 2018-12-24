
namespace WebServer.Application.Controllers
{
    using Server.Enums;
    using Server.Http.Response;
    using Server.Http.Contracts;
    using WebServer.Application.Views;

    public class HomeController
    {
        public IHttpResponse Index()
        {
            var response = new ViewResponse(HttpStatusCode.Ok, new HomeIndexView());

            return response;
        }
    }
}

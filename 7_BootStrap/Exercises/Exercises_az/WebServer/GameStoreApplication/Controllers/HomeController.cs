
namespace WebServer.GameStoreApplication.Controllers
{
    using GameStoreApplication.Infrastructure;
    using Server.Http.Contracts;

    public class HomeController: Controller
    {
        public HomeController(IHttpRequest request)
            :base(request)
        {
            //this.Request = request;
        }

        public IHttpResponse Index()
        {
            return this.FileViewResponse(@"home\index");
        }
    }
}

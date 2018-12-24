
namespace WebServer
{
    using Server;
    using Server.Routing;
    using WebServer.Application;

    public class Launcher
    {
        static void Main(string[] args)
        {
            var mainApplication = new MainApplication();

            var appRouteConfig = new AppRouteConfig();
            mainApplication.Start(appRouteConfig);
            var webServer = new WebServer(1337, appRouteConfig);

            webServer.Run();
        }
    }
}

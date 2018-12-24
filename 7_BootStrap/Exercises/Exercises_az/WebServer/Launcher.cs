
namespace WebServer
{
    using Server;
    using WebServer.Server.Contracts;
    using Server.Routing;
    using WebServer.Application;
    using WebServer.ByTheCakeApp;
    using WebServer.GameStoreApplication;

    public class Launcher:IRunable
    {
        static void Main(string[] args)
        {
            new Launcher().Run();
        }

        public void Run()
        {
            var mainApplication = new GameStoreApp();
            mainApplication.InitializeDatabase();

            var appRouteConfig = new AppRouteConfig();
            mainApplication.Start(appRouteConfig);
            var webServer = new WebServer(1337, appRouteConfig);

            webServer.Run();
        }
    }
}


namespace WebServer
{
    using Server;
    using WebServer.Server.Contracts;
    using Server.Routing;
    using WebServer.Application;
    using WebServer.ByTheCakeApp;
    using System;

    public class Launcher:IRunable
    {
        static void Main(string[] args)
        {            
            new Launcher().Run();
        }

        public void Run()
        {
            Console.WriteLine("write calculator or cake: ");
            
            while (true)
            {
                var choose = Console.ReadLine();

                if (choose == "cake")
                {
                    var mainApplication = new ByTheCakeApplication();
                    var appRouteConfig = new AppRouteConfig();
                    mainApplication.Start(appRouteConfig);
                    var webServer = new WebServer(1337, appRouteConfig);
                    webServer.Run();
                    break;
                }
                else if (choose == "calculator")
                {
                    var mainApplication = new MainApplication();
                    var appRouteConfig = new AppRouteConfig();
                    mainApplication.Start(appRouteConfig);
                    var webServer = new WebServer(1337, appRouteConfig);
                    webServer.Run();
                    break;
                }
                else
                {
                    Console.WriteLine("write calculator or cake: ");              
                }              
            }           
        }
    }
}

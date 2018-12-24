
namespace SimpleMvc.Framework
{
    using System;
    using System.Reflection;
    using WebServer;

    public static class MvcEngine
    {
        public static void Run(WebServer server)
        {
            MvcContext.Get.AssemblyName = Assembly.GetEntryAssembly().GetName().Name;

            try
            {
                server.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

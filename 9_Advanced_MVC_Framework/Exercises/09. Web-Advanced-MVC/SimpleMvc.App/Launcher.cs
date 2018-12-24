
namespace SimpleMvc.App
{
    using Framework;
    using Framework.Routers;
    using Microsoft.EntityFrameworkCore;
    using SimpleMvc.App.Data;
    using WebServer;

    public class Launcher
    {
        static void Main()
        {
            using (var db = new SimpleMvcFrameworkDbContext())
            {
                db.Database.Migrate();
            }
            MvcEngine.Run(new WebServer(1337, new ControllerRouter(), new RecourceRouter()));
        }
    }
}

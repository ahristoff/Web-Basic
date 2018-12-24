
namespace SimpleMvc.App.Controllers
{
    using SimpleMvc.App.Models;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Contracts;
    using SimpleMvc.Framework.Controllers;

    public class HomeController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

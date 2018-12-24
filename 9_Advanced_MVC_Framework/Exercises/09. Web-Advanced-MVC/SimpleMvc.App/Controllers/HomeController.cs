
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
            //this.Redirect("/home");
            return View();
        }
        // POST /home/index?id=10
        // BODY: Text=Ivan&Number=2
        [HttpPost]
        public IActionResult Index(int id, IndexModel model)
        {
            return View();
        }
    }
}

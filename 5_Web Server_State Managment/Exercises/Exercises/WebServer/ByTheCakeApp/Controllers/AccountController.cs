
namespace WebServer.ByTheCakeApp.Controllers
{
    using Server.Http.Contracts;
    using Infrastructure;
    using Server.Http.Response;
    using Server.Http;
    using ByTheCakeApp.Models;

    public class AccountController: Controller //1.2
    {
        public IHttpResponse Login()
        {
            this.ViewData["showError"] = "none";
            this.ViewData["authDisplay"] = "none";

            return this.FileViewResponse(@"Account/login");
        }

        //1.4
        public IHttpResponse Login(IHttpRequest req)
        {
            if (!req.FormData.ContainsKey("name") || (!req.FormData.ContainsKey("password")))
            {
                return new BadRequestResponse();
            }

            var name = req.FormData["name"];
            var password = req.FormData["password"];

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
            {
                this.ViewData["error"] = "You have empty fields";
                this.ViewData["showError"] = "block";

                return this.FileViewResponse(@"Account\login");        
            }
            //1.6
            req.Session.Add(SessionStore.CurrentUserKey, name);
            //CurrentUserKey = "Current_User"
            //3.4
            req.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());
            //SessionKey = "Shopping_Cart_Key"

            return new RedirectResponse("/");
        }

        public IHttpResponse Logout(IHttpRequest req)
        {
            req.Session.Clear();

            return new RedirectResponse("/login");
        }
    }
}

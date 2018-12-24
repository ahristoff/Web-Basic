
namespace WebServer.ByTheCakeApp.Controllers
{
    using Server.Http.Contracts;
    using Infrastructure;
    using Server.Http.Response;
    using Server.Http;
    using ByTheCakeApp.ViewModels;
    using ByTheCakeApp.ViewModels.Account;
    using ByTheCakeApp.Services;
    using System;

    public class AccountController: Controller //1.2
    {
        private readonly IUserService users;

        public AccountController()
        {
            this.users = new UserService();
        }
        //2
        public IHttpResponse Register()// Get
        {
            this.ViewData["showError"] = "none";
            this.ViewData["authDisplay"] = "none";

            return this.FileViewResponse(@"Account/register");
        }
        //2
        public IHttpResponse Register(IHttpRequest req, RegisterUserViewModel model)
        {
            this.ViewData["showError"] = "none";
            this.ViewData["authDisplay"] = "none";

            if (model.Username.Length < 3
                || model.Password.Length < 3
                || model.ConfirmPassword != model.Password)
            {
                this.ViewData["showError"] = "block";
                this.ViewData["error"] = "Invalid user details";

                return this.FileViewResponse(@"Account/register");
            }

            //business logic from servise
            var success = this.users.Create(model.Username, model.Password);

            if (success)
            {
                req.Session.Add(SessionStore.CurrentUserKey, model.Username);
                req.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());

                return new RedirectResponse("/");
            }
            else
            {
                this.ViewData["showError"] = "block";
                this.ViewData["error"] = "This username is taken";

                return this.FileViewResponse(@"Account/register");
            }
        }

        //3
        public IHttpResponse Login() // Get
        {
            this.ViewData["showError"] = "none";
            this.ViewData["authDisplay"] = "none";

            return this.FileViewResponse(@"Account\login");
        }

        //3
        public IHttpResponse Login(IHttpRequest req, LoginViewModel model) //Post
        {

            if (string.IsNullOrWhiteSpace(model.Username)
                || string.IsNullOrWhiteSpace(model.Password))
            {
                this.ViewData["showError"] = "block";
                this.ViewData["error"] = "You have empty fields";
                
                return this.FileViewResponse(@"Account/login");
            }

            var success = this.users.Find(model.Username, model.Password);

            if (success)
            {
                req.Session.Add(SessionStore.CurrentUserKey, model.Username);
                req.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());

                return new RedirectResponse("/");
            }
            else
            {
                this.ViewData["showError"] = "block";
                this.ViewData["error"] = "Invalid user details";

                return this.FileViewResponse(@"Account/login");
            }
        }
        //4
        public IHttpResponse Profile(IHttpRequest req)
        {
            if (!req.Session.Contains(SessionStore.CurrentUserKey))
            {
                throw new InvalidOperationException("There is no logged in user.");
            }

            var username = req.Session.Get<string>(SessionStore.CurrentUserKey);

            var profile = this.users.Profile(username);

            if (profile == null)
            {
                throw new InvalidOperationException($"The user {username} could not be found in the database.");
            }

            this.ViewData["username"] = profile.Username;
            this.ViewData["registrationDate"] = profile.RegistrationDate.ToShortDateString();
            this.ViewData["totalOrders"] = profile.TotalOrders.ToString();

            return this.FileViewResponse(@"Account/profile");
        }

        public IHttpResponse Logout(IHttpRequest req)
        {
            req.Session.Clear();

            return new RedirectResponse("/login");
        }
    }
}

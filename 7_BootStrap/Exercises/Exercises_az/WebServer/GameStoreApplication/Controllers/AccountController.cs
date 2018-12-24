
namespace WebServer.GameStoreApplication.Controllers
{

    using GameStoreApplication.Infrastructure;
    using GameStoreApplication.Services;
    using GameStoreApplication.Models.Account;
    using Server.Http;
    using Server.Http.Contracts;
    using GameStoreApplication.Services.Contracts;
    using Server.Http.Response;

    public class AccountController: Controller
    {
        private readonly IUserService users;

        public AccountController(IHttpRequest request)
            :base(request)
        {
            this.users = new UserService();
           // this.Request = request;
        }


        public IHttpResponse Register()  //get
        {
            //because method ApplyAuthentication in Controller
            //this.ViewData["anonymousDisplay"] = "flex"; 
            //this.ViewData["authDisplay"] = "none";

            return this.FileViewResponse(@"account\register");
        }

        public IHttpResponse Register(RegisterViewModel model) //post
        {
            
            if (!this.ValidateModel(model))
            {
                return this.FileViewResponse(@"account\register");
            }

            var success = this.users
                .Create(model.Email, model.FullName, model.Password);

            if (!success)
            {
                this.ShowError("E-mail is taken.");
                return this.FileViewResponse(@"account\register");
            }
            else
            {
                this.Request.Session.Add(SessionStore.CurrentUserKey, model.Email);
                return new RedirectResponse("/");
            }
        }

        public IHttpResponse Login()
        {
            //because method ApplyAuthentication in Controller
            //this.ViewData["anonymousDisplay"] = "flex"; 
            //this.ViewData["authDisplay"] = "none";

            return this.FileViewResponse(@"account\login");
        }

        public IHttpResponse Login(LoginViewModel model)
        {
            var success = this.users.Find(model.Email, model.Password);

            if (!success)
            {
                this.ShowError("Invalid user details.");

                return this.Login();
            }
            else
            {
                this.Request.Session.Add(SessionStore.CurrentUserKey, model.Email);

                return new RedirectResponse("/");
            }
        }

        public IHttpResponse Logout()
        {
            this.Request.Session.Clear();

            return new RedirectResponse("/");
        }
    }
}

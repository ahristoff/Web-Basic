
namespace WebServer.GameStoreApplication.Infrastructure
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using Server.Enums;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using GameStoreApplication.Common;
    using GameStoreApplication.Services;
    using GameStoreApplication.Services.Contracts;
    using Server.Http;

    public class Controller
    {
        private readonly IUserService users;

        public Controller(IHttpRequest request)
        {
            this.ViewData = new Dictionary<string, string>()
            {
                ["anonymousDisplay"] = "none",
                ["authDisplay"] = "flex",
                ["showError"] = "none"
            };

            this.Authentication = new Authentication(false, false);
            this.users = new UserService();
            this.Request = request;
            this.ApplyAuthentication();
            
        }

        protected Authentication Authentication { get; private set; }

        protected IDictionary<string, string> ViewData { get; set; }

        protected IHttpRequest Request { get;private set; }

        protected IHttpResponse FileViewResponse(string fileName)
        {
            var layoutHtml = File.ReadAllText($@"../../../GameStoreApplication\Resources\layout.html");

            var fileHtml = File.ReadAllText($@"../../../GameStoreApplication\Resources\{fileName}.html");

            var result = layoutHtml.Replace("{{{content}}}", fileHtml);

            if (this.ViewData.Any())
            {
                foreach (var x in ViewData)
                {
                    // escape { -> {{
                    result = result.Replace($"{{{{{{{x.Key}}}}}}}", x.Value);
                }
            }

            var response = new ViewResponse(HttpStatusCode.Ok, new FileView(result));

            return response;
        }

        protected bool ValidateModel(object model)
        {
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(model, context, results, true) == false)
            {
                foreach (var result in results)
                {
                    if (result != ValidationResult.Success)
                    {
                        //this.ShowError(result.ErrorMessage);
                        this.ViewData["showError"] = "block";
                        this.ViewData["error"] = result.ToString();
                        return false;
                    }
                }
            }

            return true;
        }
        protected void ShowError(string errorMessage)
        {
            this.ViewData["showError"] = "block";
            this.ViewData["error"] = errorMessage;
        }

        //------------------3-------------------------------------------
        private void ApplyAuthentication()
        {
            //default - i am not login
            var anonymousDisplay = "flex";
            var authDisplay = "none";
            var adminDisplay = "none";

            var authenticatedUserEmail = Request
                .Session
                .Get<string>(SessionStore.CurrentUserKey);

            //if i am login
            if (authenticatedUserEmail != null)
            {
                anonymousDisplay = "none";
                authDisplay = "flex";

                var isAdmin = this.users.IsAdmin(authenticatedUserEmail); //from Db

                //if i am admin
                if (isAdmin)
                {
                    adminDisplay = "flex";
                }

                this.Authentication = new Authentication(true, isAdmin);
            }

            this.ViewData["anonymousDisplay"] = anonymousDisplay;
            this.ViewData["authDisplay"] = authDisplay;
            this.ViewData["adminDisplay"] = adminDisplay;
        }
    }
}

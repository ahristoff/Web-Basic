
namespace SimpleMvc.Framework.Controllers
{
    using Contracts;
    using ActionResult;
    using Models;
    using ViewEngine;
    using System.Runtime.CompilerServices;
    using System.Reflection;
    using System.Linq;
    using Attributes.Validation;
    using WebServer.Http.Contracts;
    using Security;
    using WebServer.Http;

    public abstract class Controller
    {
        public Controller()
        {
            this.ViewModel = new ViewModel();
            this.User = new Authentication(); // by default is not authenticated
        }

        protected ViewModel ViewModel { get; private set; }

        protected internal IHttpRequest Request { get; internal set; }

        protected internal Authentication User { get; private set; }

        protected IViewable View([CallerMemberName]string caller = "")
        {
            var assamblyName = MvcContext.Get.AssemblyName;
            var folderName = MvcContext.Get.ViewFolders;
            var controllerName = this.GetType().Name
                .Replace(MvcContext.Get.ContrellerSuffix, string.Empty);
            var methodName = caller;

            //SimpleMvc.App.Views.Home.Index, ...
            var viewFullQuilifiedname = string.Format("{0}.{1}.{2}.{3}, {0}",
                assamblyName, folderName, controllerName, methodName);

            var view = new View(viewFullQuilifiedname, this.ViewModel.Data);

            return new ViewResult(view);
        }

        protected IRedirectable Redirect(string redirectUrl)
        {
            return new RedirectResult(redirectUrl);
        }

        protected bool IsValidModel(object model)
        {
            var properties = model.GetType().GetProperties();

            foreach (var x in properties)
            {
                var attributes = x
                    .GetCustomAttributes()
                    .Where(a => a is PropertyValidationAttribute)
                    .Cast<PropertyValidationAttribute>();

                foreach (var y in attributes)
                {
                    var propertyValue = x.GetValue(model);

                    if (!y.IsValid(propertyValue))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        protected void SignIn(string name)
        {
            this.Request.Session.Add(SessionStore.CurrentUserKey, name);
        }

        protected void SignOut()
        {
            this.Request.Session.Clear();
        }

        protected internal void InitializeController()
        {
            var user = this.Request
                .Session
                .Get<string>(SessionStore.CurrentUserKey);

            if (user != null)
            {
                this.User = new Authentication(user);
            }
        }
    }
}

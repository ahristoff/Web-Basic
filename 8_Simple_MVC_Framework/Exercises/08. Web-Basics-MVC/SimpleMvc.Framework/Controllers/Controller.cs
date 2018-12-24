
namespace SimpleMvc.Framework.Controllers
{
    using Contracts;
    using Contracts.Generic;
    using SimpleMvc.Framework.ViewEngine;
    using System.Runtime.CompilerServices;
    using ViewEngine.Generics;

    public abstract class Controller
    {
        protected IActionResult View([CallerMemberName]string caller = "")
        {
            var assamblyName = MvcContext.Get.AssemblyName;
            var folderName = MvcContext.Get.ViewFolders;
            var controllerName = this.GetType().Name
                .Replace(MvcContext.Get.ContrellerSuffix, string.Empty);
            var methodName = caller;

            //SimpleMvc.App.Views.Home.Index, ...
            var viewFullQuilifiedname = string.Format("{0}.{1}.{2}.{3}, {0}",
                assamblyName, folderName, controllerName, methodName);

            return new ActionResult(viewFullQuilifiedname);
        }

        protected IActionResult View(string controller, string action)
        {
            var assamblyName = MvcContext.Get.AssemblyName;
            var folderName = MvcContext.Get.ViewFolders;
            var controllerName = controller;
            var methodName = action;

            var viewFullQuilifiedname = string.Format("{0}.{1}.{2}.{3}, {0}",
               assamblyName, folderName, controllerName, methodName);

            return new ActionResult(viewFullQuilifiedname);
        }

        protected IActionResult<TModel> View<TModel>(TModel model, [CallerMemberName]string caller = "")
        {
            var assamblyName = MvcContext.Get.AssemblyName;
            var folderName = MvcContext.Get.ViewFolders;
            var controllerName = this.GetType().Name
                .Replace(MvcContext.Get.ContrellerSuffix, string.Empty);
            var methodName = caller;

            var viewFullQuilifiedname = string.Format("{0}.{1}.{2}.{3}, {0}",
              assamblyName, folderName, controllerName, methodName);

            return new ActionResult<TModel>(viewFullQuilifiedname, model);
        }

        protected IActionResult<TModel> View<TModel>(TModel model, string controller, string action)
        {
            var assamblyName = MvcContext.Get.AssemblyName;
            var folderName = MvcContext.Get.ViewFolders;
            var controllerName = controller;
            var methodName = action;

            var viewFullQuilifiedname = string.Format("{0}.{1}.{2}.{3}, {0}",
              assamblyName, folderName, controllerName, methodName);

            return new ActionResult<TModel>(viewFullQuilifiedname, model);
        }
    }
}

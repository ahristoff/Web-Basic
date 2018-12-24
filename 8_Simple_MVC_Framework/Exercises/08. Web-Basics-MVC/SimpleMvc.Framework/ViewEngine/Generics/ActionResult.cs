
namespace SimpleMvc.Framework.ViewEngine.Generics
{
    using System;
    using Contracts.Generic;

    public class ActionResult<TModel> : IActionResult<TModel>
    {
        public ActionResult(string viewFullQualifiedName, TModel model)
        {
            this.Action = (IRenderable<TModel>)Activator
               .CreateInstance(Type.GetType(viewFullQualifiedName));

            this.Action.Model = model;
        }

        public IRenderable<TModel> Action { get ; set ; }

        public string Invoke()
        {
            return this.Action.Render();
        }
    }
}

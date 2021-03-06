﻿
namespace SimpleMvc.Framework.ViewEngine
{
    using System;
    using SimpleMvc.Framework.Contracts;

    public class ActionResult : IActionResult
    {
        public ActionResult(string viewFullQualifiedName)
        {
            this.Action = (IRenderable)Activator
                .CreateInstance(Type.GetType(viewFullQualifiedName));
        }

        public IRenderable Action { get ; set; }

        public string Invoke()
        {
            return Action.Render();
        }
    }
}

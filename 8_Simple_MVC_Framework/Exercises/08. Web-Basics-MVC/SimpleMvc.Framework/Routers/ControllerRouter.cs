
namespace SimpleMvc.Framework.Routers
{
    using Framework.Attributes.Methods;
    using Framework.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using WebServer.Contracts;
    using WebServer.Enums;
    using WebServer.Http.Contracts;
    using WebServer.Http.Response;

    public class ControllerRouter : IHandleable
    {
        private IDictionary<string, string> getParameters;
        private IDictionary<string, string> postParameters;
        private string requestMethod;
        private object controllerInstance;
        private string controllerName;
        private string actionName;
        private object[] methodParameters;

        public IHttpResponse Handle(IHttpRequest request)
        {
            this.getParameters = new Dictionary<string, string>(request.UrlParameters);
            this.postParameters = new Dictionary<string, string>(request.FormData);
            this.requestMethod = request.Method.ToString().ToUpper();
            //------------
            var pathParts = request.Path.Split(new[] { '/', '?' }, StringSplitOptions.RemoveEmptyEntries);

            if (pathParts.Length < 2)
            {
                //BadRequestException.ThrowFromInvalidRequest(); 
                actionName = "";
            }
            else
            {
                this.actionName = $"{Capitalize(pathParts[1])}";
            }

            this.controllerName = $"{Capitalize(pathParts[0])}{MvcContext.Get.ContrellerSuffix}";

            //----retrieve method
            var methodInfo = this.GetActionForExecution();// retrieve Get or Post

            if (methodInfo == null)
            {
                return new NotFoundResponse();
            }

            this.PrepareMethodParameters(methodInfo); //retrieve the parameters of the method

            var actionResult = (IInvocable)methodInfo.Invoke(this.GetControllerInstance(), this.methodParameters);

            var content = actionResult.Invoke();

            return new ContentResponse(HttpStatusCode.Ok, content);
        }      

        public string Capitalize(string input) //controlerName&actionname
        {
            if (input == null || input.Length == 0)
            {
                return input;
            }
            var firstLetter = char.ToUpper(input[0]);
            var rest = input.Substring(1);

            return $"{firstLetter}{rest}";
        }

        //--------------------Get or Post

        private MethodInfo GetActionForExecution() // Get or Post
        {
            foreach (var method in this.GetSuitableMethods())
            {
                var httpMethodAttributes = method
                    .GetCustomAttributes()
                    .Where(a => a is HttpMethodAttribute)
                    .Cast<HttpMethodAttribute>();

                //if no available attribute
                if (!httpMethodAttributes.Any() && this.requestMethod == "GET")
                {
                    return method;
                }

                //if available attribute
                foreach (var httpMethodAttribute in httpMethodAttributes)
                {
                    if (httpMethodAttribute.IsValid(this.requestMethod))
                    {
                        return method;
                    }
                }
            }

            return null;
        }

        private IEnumerable<MethodInfo> GetSuitableMethods()
        {
            var controller = this.GetControllerInstance();

            if (controller == null) 
            {
                return new MethodInfo[0]; 
            }

            return controller // return all methods, that names match with actionName
                .GetType().GetMethods().Where(m => m.Name == actionName);

        }

        private object GetControllerInstance()// Get or Post
        {
            if (controllerInstance != null)
            {
                return controllerInstance;
            }

            var controllerFullQualifiedName = string.Format(
                "{0}.{1}.{2}, {0}",
                MvcContext.Get.AssemblyName,
                MvcContext.Get.ControllersFolder,
                this.controllerName);

            var controllerType = Type.GetType(controllerFullQualifiedName);

            if (controllerType == null)
            {
                return null;
            }

            this.controllerInstance = Activator.CreateInstance(controllerType);
            return controllerInstance;
        }

        //-------------------------parameters

        private void PrepareMethodParameters(MethodInfo method) //who is the method
        {
            var parameters = method.GetParameters();   //obtain the parameters from the method

            this.methodParameters = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)           
            {
                var parameter = parameters[i];

                if (parameter.ParameterType.IsPrimitive || 
                    parameter.ParameterType == typeof(string))
                {
                    var getParameterValue = this.getParameters[parameter.Name];

                    //cast to type of parameter
                    var value = Convert.ChangeType(getParameterValue, parameter.ParameterType);

                    this.methodParameters[i] = value; //end we get array from parameters
                }
                else
                {
                    var modelType = parameter.ParameterType;

                    var modelInstance = Activator.CreateInstance(modelType);

                    var modelProperties = modelType.GetProperties();//get all properties

                    foreach (var modelProperty in modelProperties)
                    {
                        var postParametervalue = this.postParameters[modelProperty.Name];

                        var value = Convert.ChangeType(postParametervalue, modelProperty.PropertyType);

                        modelProperty.SetValue(modelInstance, value);//set value to property                    
                    }

                    this.methodParameters[i] = Convert.ChangeType(modelInstance, modelType);
                }
            }
        }
    }
}

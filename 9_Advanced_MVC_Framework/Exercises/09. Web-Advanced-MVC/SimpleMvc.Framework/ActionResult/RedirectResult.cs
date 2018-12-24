
using SimpleMvc.Framework.Contracts;

namespace SimpleMvc.Framework.ActionResult
{
    using WebServer.Http.Contracts;
    using WebServer.Http.Response;

    public class RedirectResult : IRedirectable
    {
        public RedirectResult(string redirectUrl)
        {
            this.RedirectUrl = redirectUrl;
        }

        public string RedirectUrl { get ; set ; }

        public IHttpResponse Invoke()
        {
            return new RedirectResponse(this.RedirectUrl);
        }
    }
}

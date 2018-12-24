
namespace SimpleMvc.App.Views.User
{
    using SimpleMvc.App.Models;
    using SimpleMvc.Framework.Contracts.Generic;
    using System.Text;

    public class All : IRenderable<AllUsernamesViewModel>
    {
        public AllUsernamesViewModel Model { get ; set; }

        public string Render()
        {
            var sb = new StringBuilder();
            sb.AppendLine("<h3> All users</h3>");
            sb.AppendLine("<ul>");
            foreach (var user in Model.Users)
            {        
                sb.AppendLine($@"<li><a href = ""/user/profile?id={user.Id}"">{user.Username}</a></li>");
            }
            sb.AppendLine("</ul>");
            sb.AppendLine(@"<br/>");
            sb.AppendLine(@"<br/>");
            sb.AppendLine(@"<a href = ""/user/register"" >Register</ a >");

            return sb.ToString();
        }
    }
}

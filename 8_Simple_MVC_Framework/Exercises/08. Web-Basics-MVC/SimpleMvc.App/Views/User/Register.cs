
namespace SimpleMvc.App.Views.User
{
    using SimpleMvc.Framework.Contracts;
    using System.Text;

    public class Register : IRenderable
    {
        public string Render()
        {
            var sb = new StringBuilder();
            sb.AppendLine("<h3>Regiter new User</h3>");
            sb.AppendLine(@"<form method=""POST"" action=""register""><br/>");
            sb.AppendLine(@"Username: <input type=""text"" name=""Username"" placeholder=""username""><br/>");
            sb.AppendLine(@"Password: <input type=""text"" name=""Password"" placeholder=""password""><br/>");
            sb.AppendLine(@"<input type=""submit"" value=""Register""/>");
            sb.AppendLine(@"<form/><br/>");
            sb.AppendLine(@"<br/>");
            sb.AppendLine(@"<br/>");
            sb.AppendLine(@"<a href = ""/user/all"" > List all users</ a >");

            return sb.ToString();
        }
    }
}

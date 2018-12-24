
namespace SimpleMvc.App.Views.User
{
    using SimpleMvc.App.Models;
    using SimpleMvc.Framework.Contracts.Generic;
    using System.Text;

    public class Profile : IRenderable<UserProfileViewModel>
    {
        public UserProfileViewModel Model { get ; set ; }

        public string Render()
        {
            var sb = new StringBuilder();

            sb.AppendLine(@"<form method=""POST"" action=""profile""><br/>");
            sb.AppendLine(@"Title: <input type=""text"" name=""Title"" placeholder=""title""><br/>");
            sb.AppendLine(@"Content: <input type=""text"" name=""Content"" placeholder=""content""><br/>");
            sb.AppendLine($@"<input type=""hidden"" name=""UserId"" value=""{Model.UserId}""><br/>"); 
            sb.AppendLine(@"<input type=""submit"" value=""Add Note""/>");
            sb.AppendLine(@"<form/><br/>");
            sb.AppendLine(@"<br/>");
            sb.AppendLine(@"<br/>");
            sb.AppendLine("<h5>List of notes</h5>");
            sb.AppendLine("<ul>");
            sb.AppendLine($"Count of notes: {Model.Notes.Count}");
            sb.AppendLine(@"<br/>");
            foreach (var note in Model.Notes)
            {
                sb.AppendLine($"<li>{note.Title} - {note.Content}</li>");
            }
            sb.AppendLine("</ul>");
            sb.AppendLine(@"<br/>");
            sb.AppendLine(@"<br/>");
            sb.AppendLine(@"<li><a href = ""/user/all"">Return to all users</a></li>");

            return sb.ToString();
        }
    }
}

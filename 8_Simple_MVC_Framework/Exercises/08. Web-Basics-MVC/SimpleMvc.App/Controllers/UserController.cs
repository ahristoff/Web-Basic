
namespace SimpleMvc.App.Controllers
{
    using SimpleMvc.App.Data;
    using SimpleMvc.App.Data.Models;
    using SimpleMvc.App.Models;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Contracts;
    using SimpleMvc.Framework.Contracts.Generic;
    using SimpleMvc.Framework.Controllers;
    using System.Collections.Generic;
    using System.Linq;

    public class UserController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            var user = new User()
            {
                Username = model.Username,
                Password = model.Password
            };

            using (var db = new SimpleMvcFrameworkDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }

            return View();
        }

        public IActionResult<AllUsernamesViewModel> All()
        {
            var users = new List<User>();

            using (var db = new SimpleMvcFrameworkDbContext())
            {
                users = db.Users.ToList();
            }

            var model = new AllUsernamesViewModel()
            {
                Users = users
            };

            return View(model);
        }
        [HttpGet]
        public IActionResult<UserProfileViewModel> Profile(int id)
        {
            using (var db = new SimpleMvcFrameworkDbContext())
            {
                var user = db.Users
                    .Where(u => u.Id == id)
                    .Select(s => new User()
                    {
                       Username = s.Username,
                       Notes = s.Notes
                       .Select(p => new Note()
                       {
                           Title = p.Title,
                           Content = p.Content
                       })
                       .ToList()
                    })
                    .FirstOrDefault();

                var viewModel = new UserProfileViewModel()
                {
                    UserId = id,
                    Username = user.Username,
                    Notes = user.Notes
                    .Select(n => new Note() 
                    {
                        Title = n.Title,
                        Content = n.Content
                    })
                    .ToList()
                };

                return View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult<UserProfileViewModel> Profile(AddNoteViewModel model)
        {
            using (var db = new SimpleMvcFrameworkDbContext())
            {
                var user = db.Users.Find(model.UserId);
                //var user = db.Users.FirstOrDefault(u => u.Id == model.UserId);
                var note = new Note()
                {
                    Title = model.Title,
                    Content = model.Content
                };

                user.Notes.Add(note);
                db.SaveChanges();
            }

            return Profile(model.UserId);
        }
    }
}

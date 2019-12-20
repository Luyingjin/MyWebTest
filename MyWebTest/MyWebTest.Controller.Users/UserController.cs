using MWebTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace MyWebTest.Controllers.Users
{
    public class UserController: Controller
    {
        List<User> users = null;
       
        public ActionResult Index()
        {
            users = new List<User>();
            for (int i = 1; i <= 10; i++)
            {
                users.Add(new MWebTest.Models.User
                {
                    Id = i,
                    Age = i + 20,
                    Gender = i % 2 == 0 ? "男" : "女",
                    Name = $"Name{i}"
                });
            }
            return View(users);
        }

        
    }
}

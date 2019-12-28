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
       public UserController()
        {
            users = new List<User>();
            for (int i = 1; i <= 10; i++)
            {
                users.Add(new MWebTest.Models.User
                {
                    Id = i,
                    Age = i + 20,
                    Gender = i % 2 == 0 ? "男" : "女",
                    Name = $"Name{i}",
                    ChildList = new List<User>() { new User { Id = i + 10, Name =$"{i}_Name{i+10}"} }

                });
            }
        }
        public ActionResult Index()
        {
            return new MvcRazorToPdf.PdfActionResult(users);

        }

        public ActionResult Edit(int? Id)
        {
            
            return View(Id!=null?users.FirstOrDefault(m => m.Id == Id):new MWebTest.Models.User());
        }

        [HttpPost]
        public ActionResult Edit(User userDto)
        {
            return View(userDto);
        }
        
    }
}

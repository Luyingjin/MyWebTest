using Microsoft.AspNetCore.Mvc.ApiExplorer;
using MWebTest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MvcApiSwaggerTest.Controllers
{
    /// <summary>
    /// Api控制器
    /// </summary>
    public class ApiTestController : ApiController
    {
        List<User> users = null;
        /// <summary>
        /// Api控制器
        /// </summary>
        public ApiTestController()
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
                    ChildList = new List<User>() { new User { Id = i + 10, Name = $"{i}_Name{i + 10}" } }

                });
            }
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<User> GetList()
        {

            return users;
        }
        /// <summary>
        /// 获取排除固定用户的列表
        /// </summary>
        /// <param name="id">排除的ID</param>
        /// <returns></returns>
        [HttpGet]
        public List<User> GetList(int id)
        {
            return users.Where(m => m.Id != id).ToList();
        }
    }
}

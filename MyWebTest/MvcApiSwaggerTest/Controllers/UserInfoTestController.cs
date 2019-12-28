using DocumentFormat.OpenXml.Spreadsheet;
using MvcApiSwaggerTest.AuthAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MvcApiSwaggerTest.Controllers
{
    
    public class UserInfoTestController : ApiController
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [ApiAuthorize]
        [HttpGet]
        //[Route("api/GetUserInfo")]
        public string GetUserInfo()
        {
            var userInfo = new
            {
                UserName = "test",
                Tel = "123456789",
                Address = "testddd"
            };
            return JsonConvert.SerializeObject(userInfo);
        }
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Route("api/GetString")]
        public string GetStringArr()
        {
            return "你好";
        }

    }
}

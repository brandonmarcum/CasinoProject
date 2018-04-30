using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasinoService.Api.Models;
using Microsoft.AspNetCore.Mvc;
using CasinoService.Api.Models.ViewModels;

namespace CasinoService.Api.Controllers
{
    
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {  
        [HttpGet("{username}")]
        [ActionName("userget")]
        [Route("api/user/userget/{username}")]
        public async Task<User> UserGetAsync(string username)
        {
           //create userhelper for getting user from db, send it
           User user= UserHelper.GetUserDataAsync(username).GetAwaiter().GetResult();
            
             Console.WriteLine("Reached API UserController with user: " + username);


            return await Task.Run(() => user);
        }
         [HttpPost]
        [ActionName("userregister")]
        public async Task<User> UserRegisterAsync([FromBody]User user)
        {
            //User user = HttpContext.Session.Get<User>("user");
            Console.WriteLine("redirected to user get and registering user to db! " + "username: " + user.Username);

            UserHelper.RegisterUserDataAsync(user);

            return await Task.Run(() => user);
        }
        [HttpPost]
        [ActionName("userpost")]
        public async Task<User> UserPostAsync([FromBody] User user)
        {
            //HttpContext.Session.Set<User>("user", user);
            Console.WriteLine("Reached user post with user: " + user.Username);
            return await Task.Run(() => user);
        }
    }
}

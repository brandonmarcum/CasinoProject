using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasinoData.Db;
using CasinoData.Db.DbContexts;
using Microsoft.AspNetCore.Mvc;

namespace CasinoData.Svc.Controllers
{
    
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {  
        [HttpGet("{username}")]
        [ActionName("datauserget")]
        [Route("api/user/datauserget/{username}")]
        public async Task<Users> UserGetAsync(string username)
        {
            //Get user from DB using EF DBContext, pass it through
            //Populate the User Pocket by using the UserPocketID foreign key together with Pocket DBContext
            //Finally, get user Chips using Pocket table reference ChipsID foreign key and add it to user object 

            UserContext uc = new UserContext();
            PocketContext pc = new PocketContext();
            ChipsContext cc = new ChipsContext();
            //Users user = UserHelper.GetUserAsync().GetAwaiter().GetResult();

            Console.WriteLine("Reached Data UserController with user: " + username);

            Users user = new Users();
            
            user = uc.Users.Where( u => u.Username == username).FirstOrDefault();
            
            user.Name = "falseuser";
            //if(uc.Users.ToList().Count > 0)
            //{
                // foreach(var item in uc.Users.ToList())
                // {
                //     if(username.Equals(item.Username))
                //     {
                //         user = item;
                //     }
                //     else 
                //     {
                //         user.Name = "falseuser";
                //     }
                // }
            //}

            return await Task.Run(() => user);
        }
        [HttpGet]
        [ActionName("datauserregister")]
        public void UsersRegisterAsync()
        {
            EfData ef = new EfData();
            Users user = UserHelper.RegisterUserAsync().GetAwaiter().GetResult();
            ef.InsertChips(user.UserPocket.Chips);
            ef.InsertPocket(user.UserPocket);
            ef.InsertUser(user);
        }
    }
}

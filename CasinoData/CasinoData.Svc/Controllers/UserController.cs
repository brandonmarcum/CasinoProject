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
        public async Task<User> UserGetAsync(string username)
        {
            //Get user from DB using EF DBContext, pass it through
            //Populate the User Pocket by using the UserPocketID foreign key together with Pocket DBContext
            //Finally, get user Chips using Pocket table reference ChipsID foreign key and add it to user object 

            UserContext uc = new UserContext();
            PocketContext pc = new PocketContext();
            //ChipsContext cc = new ChipsContext();
            casinodbContext cc = new casinodbContext();
            //Users user = UserHelper.GetUserAsync().GetAwaiter().GetResult();

            Console.WriteLine("Reached Data UserController with user: " + username);

            Users user = new Users();
            
           // user = cc.Users.Where( u => u.Username == username).FirstOrDefault();
            
            //user.Name = "falseuser";
            //if(uc.Users.ToList().Count > 0)
            //{
            if(cc.Users.Count() > 0)
            {
                foreach(var item in cc.Users.ToList())
                {
                    if(username.Equals(item.Username))
                    {
                        user = item;
                    }
                    else 
                    {
                        user.Name = "falseuser";
                    }
                }
            }
            else
            {
                user.Name = "falseuser";
            }
            User verifiedUser = new User();

            verifiedUser.Username = user.Username;
            verifiedUser.Name = user.Name;
            verifiedUser.Age = user.Age;
            verifiedUser.Email = user.Email;
            verifiedUser.Password = user.Password;
            

            return await Task.Run(() => verifiedUser);
        }
        [HttpPost]
        [ActionName("datauserregister")]
        public void UsersRegisterAsync([FromBody]User user)
        {
            casinodbContext cc = new casinodbContext();
            //Users user = UserHelper.RegisterUserAsync().GetAwaiter().GetResult();
            Console.WriteLine("user reached data register as: " + user.Name + " username: " + user.Username);

            Users newUser = new Users();

            newUser.Name = user.Name;
            newUser.Age = user.Age;
            newUser.Email = user.Email;
            newUser.UserId = user.UserID;
            newUser.Username = user.Username;
            newUser.Password = user.Password;

            Pockets userPocket = new Pockets();

            userPocket.Cash = (decimal)user.UserPocket.CashInPocket;
            userPocket.Coins = user.UserPocket.CoinsInPocket;

            Chips userChips = new Chips();

            userPocket.AllChips = user.UserPocket.AllChips;

            newUser.UserPocket = userPocket;

            foreach(var item in newUser.UserPocket.AllChips)
            {
                //cc.Chips.Add(item);
            }
           // cc.Pockets.Add(newUser.UserPocket);
            cc.Users.Add(newUser);

            cc.SaveChanges();
            
            
            Console.WriteLine("user: " + newUser.Name + "with username: " + newUser.Username + "is successfully submitted to the database!");
        }
    }
}

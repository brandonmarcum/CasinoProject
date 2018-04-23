using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Casino.Client.Models;
using Casino.Library.Models;
using Casino.Library;
//my change hello
namespace Casino.Client.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            //return View();
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string password, string submitButton)
        {
            if(submitButton.Equals("register"))
            {
                return RedirectToAction("Register", "User");
            }
            //access db from api service to check user and fill user fields
            //insert into newUser

            User newUser = new User();
            List<User> users = new List<User>();
            newUser.Username = username;
            newUser.Password = password;

            //UserHelper.PostUserAsync(newUser);

        
            newUser = UserHelper.GetUser(username).GetAwaiter().GetResult();

            Console.WriteLine("successfully retrieved user from database back in client: " + newUser.Name);

            if(newUser.Name.Equals("falseuser"))
            {
                return RedirectToAction("Error", "User");
            }


            HttpContext.Session.Set<User>("currentUser", newUser);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult UserProfile()
        {
            User newUser = new User();

            try{
				 newUser = HttpContext.Session.Get<User>("currentUser");
				 newUser.Equals("check");
			 }
			 catch
			 {
				 return RedirectToAction("Login", "User");
			 }
             UserProfileViewModel uvm = new UserProfileViewModel();
             uvm.User = newUser;
             uvm.username = newUser.Username;
             uvm.Orange = uvm.User.UserPocket.AllChips[0].Amount;
             uvm.Purple = uvm.User.UserPocket.AllChips[1].Amount;
             uvm.Black = uvm.User.UserPocket.AllChips[2].Amount;
             uvm.Green = uvm.User.UserPocket.AllChips[3].Amount;
             uvm.Blue = uvm.User.UserPocket.AllChips[4].Amount;
             uvm.Red = uvm.User.UserPocket.AllChips[5].Amount;
             uvm.White = uvm.User.UserPocket.AllChips[6].Amount;

            return View(uvm);
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserProfileViewModel upvm)
        {            
            UserHelper.RegisterUserAsync(upvm.User);
            return RedirectToAction("Index", "Game");
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}

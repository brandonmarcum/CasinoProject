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
    [Produces("application/json")]   
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
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
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

            Console.WriteLine("successfully retrieved user from database back in client: " + newUser.Name 
                + "username: "+ newUser.Username + "password: " + newUser.Password + "Age: "+ newUser.Age
                + "Email: " + newUser.Email);

            if(newUser.Name.Equals("falseuser"))
            {
                return RedirectToAction("Error", "User");
            }

            HttpContext.Session.Set<User>("currentUser", newUser);
            return RedirectToAction("Index", "Home");
        }
        [Route("/user/userprofile")]
        [HttpGet]
        public IActionResult UserProfile()
        {
            User newUser = new User();
            try{
				 newUser = HttpContext.Session.Get<User>("currentUser");
				 newUser.Equals("check");
			 }
			 catch
			 {
				 return RedirectToAction("Error", "User");
			 }
            
            return Redirect("http://localhost:4200?username="+newUser.Username);
            
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
        [Route("/user/getuser")]
        public User GetUser()
        {
            User newUser = new User();
            // try{
			// 	 newUser = HttpContext.Session.Get<User>("currentUser");
			// 	 newUser.Equals("check");
			//  }
			//  catch
			//  {
			// 	 return (new User(){ Name = "falseuser" });
			//  }
            newUser = HttpContext.Session.Get<User>("currentUser");

            return newUser;
        }
    }
}

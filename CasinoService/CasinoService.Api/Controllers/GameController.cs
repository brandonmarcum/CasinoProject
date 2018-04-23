using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CasinoService.Api.Games;
using CasinoService.Api.Models;
using CasinoService.Api.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CasinoService.Api.Controllers
{
    [Produces("application/json")]   
    [Route("api/[controller]/[action]")]
    public class GameController : Controller
    {
       
        // GET api/values
        [HttpGet]
        [ActionName("getblackjack")]
        public async Task<BlackJackViewModel> GetAsync()
        {
            BlackJackViewModel model = HttpContext.Session.Get<BlackJackViewModel>("model");
            HttpContext.Session.Set<BlackJackViewModel>("model", model);
            Console.WriteLine("Successfully got model in GameController Service, model ID: " + model.RequestId);
            //model.Users[0].Name = "jermaine, oops";

            return await Task.Run(() => model);
        }
        [HttpPost]
        [ActionName("postblackjack")]
        public void Post([FromBody]BlackJackViewModel model)
        {
            HttpContext.Session.Set<BlackJackViewModel>("model", model);
            Console.WriteLine("Model Post Successful from GameController Service, model ID: " + model.RequestId);
            //Console.WriteLine(HttpContext.Session.Get<BlackJackViewModel>("model").RequestId);

			//BlackJackViewModel = JsonConvert.DeserializeObject<BlackJackViewModel>(model);
            //var request_Bodyb = new StreamReader(Request.Body).ReadToEnd();
             //BlackJackViewModel = request_Body as BlackJackViewModel;

        }


        [HttpGet]
        [ActionName("firsttestget")]
        public async Task<Test> GetFirstTestAsync()
        {
            Test test = new Test();

            return await Task.Run(() => test);
        }
        [HttpGet]
        [ActionName("testget")]
        public async Task<Test> GetTestAsync()
        {
            Test test = HttpContext.Session.Get<Test>("test");
            test.Value++;
            HttpContext.Session.Set<Test>("test", test);
            Console.WriteLine("Successfully got test in GameController Service, test value: " + test.Value);

            return await Task.Run(() => test);
        }
        [HttpPost]
        [ActionName("testpost")]
        public void TestPost([FromBody]Test test)
        {
            HttpContext.Session.Set<Test>("test", test);
            Console.WriteLine("Successfully got test: " + test.Value);
        }


    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Casino.Client.Models;
using Casino.Library.Games;
using Casino.Library.Models;
using Microsoft.AspNetCore.Http;

namespace Casino.Client.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Games(string anchor)
        {
            ViewBag.Section = anchor;
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult PlayGame()
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
            return View();
        }
        [HttpGet]
        public IActionResult UserBet(BlackJackViewModel model)
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
            return View(model);
        }
        [HttpPost]
        public IActionResult UserBet(ChipHelperViewModel chmodel)
        {
            ViewData["type"] = chmodel.Chips.Type;
            return View();
        }
        [HttpGet]
        public IActionResult BlackJack()
        {
            BlackJackViewModel model = new BlackJackViewModel();
            User newUser = new User();

            try{
				 newUser = HttpContext.Session.Get<User>("currentUser");
				 newUser.Equals("check");
			 }
			 catch
			 {
				 return RedirectToAction("Login", "User");
			 }

            ViewData["game"] = "bet";

            return View(model);
        }
        [HttpPost]
        public IActionResult BlackJack(BlackJackViewModel model, IFormCollection collection, string submitButton)
        {

            //bring user from session

            model = new BlackJackViewModel();

            ChipHelper ch = new ChipHelper();
            
            foreach(var item in (new Pocket()).AllChips)
            {
                int intThrow;
				if (Int32.TryParse(collection[item.Type], out intThrow))
				{
					model.Blackjack.GamePocket.AllChips.Add(new Chips() { Amount = Int32.Parse(collection[item.Type]), Type = item.Type });
				}
            }

            ch.pocketSubtraction(model.User.UserPocket, model.Blackjack.GamePocket);

            if(submitButton.Equals("bet"))
            {
                foreach(var item in model.Blackjack.GamePocket.AllChips)
                {
                    if(item.Amount>0)
                    {
                        model.Bet.Add(item.Type, item.Amount); 
                    }
                }
                model.User.UserPocket.AllChips[0].Amount = 237; 
                model.User.UserPocket.AllChips[1].Amount = 314;
                model.User.UserPocket.AllChips[2].Amount = 5798;
                model.User.UserPocket.AllChips[3].Amount = 221;
            }
            else{
                model.Blackjack.playerTotal = HttpContext.Session.Get<int>("playerTotal");
                model.Blackjack.dealerTotal = HttpContext.Session.Get<int>("dealerTotal");
                model.Bet = HttpContext.Session.Get<IDictionary<string, int>>("bet"); 
                
                model.User.UserPocket.AllChips = HttpContext.Session.Get<List<Chips>>("chips");
            }


            if(submitButton.Equals("hit"))
            {
                model.Blackjack.NextTurn();

                HttpContext.Session.Set<int>("playerTotal", model.Blackjack.playerTotal);
                HttpContext.Session.Set<int>("dealerTotal", model.Blackjack.dealerTotal);
            }
            if(submitButton.Equals("stand"))
            {
                model.Blackjack.PlayerStand();
                model.Blackjack.NextTurn();

                HttpContext.Session.Set<int>("playerTotal", model.Blackjack.playerTotal);
                HttpContext.Session.Set<int>("dealerTotal", model.Blackjack.dealerTotal);
            }
            if(model.Blackjack.status.Equals("win"))
            {
                ch.pocketAddition(model.User.UserPocket, model.Blackjack.GamePocket);
                ch.pocketAddition(model.User.UserPocket, model.Blackjack.GamePocket);
            }

            ViewData["game"] = model.Blackjack.status;
            HttpContext.Session.Set<IDictionary<string, int>>("bet", model.Bet);                

            if(submitButton.Equals("play"))
            {
                model.Blackjack = new Blackjack();
                HttpContext.Session.Set<int>("playerTotal", model.Blackjack.playerTotal);
                HttpContext.Session.Set<int>("dealerTotal", model.Blackjack.dealerTotal);
            }

            HttpContext.Session.Set<List<Chips>>("chips", model.User.UserPocket.AllChips);

            return View(model);
        }


        [HttpPost]
        public IActionResult RockPaperScissors(RPSViewModel model, IFormCollection collection, string submitButton)
        {
            model = new RPSViewModel();

            ChipHelper ch = new ChipHelper();

            foreach (var item in (new Pocket()).AllChips)
            {
                int intThrow;
                if (Int32.TryParse(collection[item.Type], out intThrow))
                {
                    model.rps.GamePocket.AllChips.Add(new Chips() { Amount = Int32.Parse(collection[item.Type]), Type = item.Type });
                }
            }

            ch.pocketSubtraction(model.User.UserPocket, model.rps.GamePocket);


            if (submitButton.Equals("bet"))
            {
                foreach (var item in model.rps.GamePocket.AllChips)
                {
                    if (item.Amount > 0)
                    {
                        model.Bet.Add(item.Type, item.Amount);
                    }
                }
                model.User.UserPocket.AllChips[0].Amount = 237;
                model.User.UserPocket.AllChips[1].Amount = 314;
                model.User.UserPocket.AllChips[2].Amount = 5798;
                model.User.UserPocket.AllChips[3].Amount = 221;
            }
            else
            {
                model.Bet = HttpContext.Session.Get<IDictionary<string, int>>("bet");

                model.User.UserPocket.AllChips = HttpContext.Session.Get<List<Chips>>("chips");
            }


            if (submitButton == "rock" || submitButton == "paper" || submitButton == "scissors")
            {
                model.rps.MakeChoice(submitButton);

                ViewData["game"] = model.rps.status;
                ViewData["you"] = model.rps.playerChoice;
                ViewData["they"] = model.rps.cpuChoice;
            }

            ViewData["game"] = model.rps.status;
            HttpContext.Session.Set<IDictionary<string, int>>("bet", model.Bet);

            if (submitButton.Equals("play"))
            {
                model.rps = new RockPaperScissors();
                ViewData["game"] = "bet";
                model.rps.status = "playing";
                return View(new RPSViewModel());
            }

            TempData.Put("model", model);

            HttpContext.Session.Set<List<Chips>>("chips", model.User.UserPocket.AllChips);

            return View(model);
        }

        [HttpGet]
        public IActionResult Slots()
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
            return View(new SlotsViewModel());
        }
        [HttpPost]
        public IActionResult Slots(SlotsViewModel model, IFormCollection collection, string submitButton)
        {
            model = new SlotsViewModel();

            ChipHelper ch = new ChipHelper();

            foreach (var item in (new Pocket()).AllChips)
            {
                int intThrow;
                if (Int32.TryParse(collection[item.Type], out intThrow))
                {
                    model.Slots.GamePocket.AllChips.Add(new Chips() { Amount = Int32.Parse(collection[item.Type]), Type = item.Type });
                }
            }

            ch.pocketSubtraction(model.User.UserPocket, model.Slots.GamePocket);


            if (submitButton.Equals("bet"))
            {
                foreach (var item in model.Slots.GamePocket.AllChips)
                {
                    if (item.Amount > 0)
                    {
                        model.Bet.Add(item.Type, item.Amount);
                    }
                }
                model.User.UserPocket.AllChips[0].Amount = 237;
                model.User.UserPocket.AllChips[1].Amount = 314;
                model.User.UserPocket.AllChips[2].Amount = 5798;
                model.User.UserPocket.AllChips[3].Amount = 221;
            }
            else
            {
                model.Bet = HttpContext.Session.Get<IDictionary<string, int>>("bet");

                model.User.UserPocket.AllChips = HttpContext.Session.Get<List<Chips>>("chips");
            }

            try
            {
            model = TempData.Get<SlotsViewModel>("slots");
            string k = model.Slots.status;
            }
            catch
            {
                model = new SlotsViewModel();
            }

            //ViewData["userChips"] = newUser.UserPocket.AllChips;

            ViewData["left"] = model.Slots.left;
            ViewData["middle"] = model.Slots.middle;
            ViewData["right"] = model.Slots.right;

            if(submitButton.Equals("run"))
            {
                model.Slots.SetSlots();
            }
            if(submitButton.Equals("stop"))
            {
                model.Slots.StopPlaying();
            }
            ViewData["status"] = model.Slots.status;


            ViewData["game"] = model.Slots.status;
            HttpContext.Session.Set<IDictionary<string, int>>("bet", model.Bet);
            return View(model);
        }

        
        [HttpGet]
        public IActionResult RussianRoulette()
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
            return View(new RRViewModel());
        }
        [HttpPost]
        public IActionResult RussianRoulette(RRViewModel model, IFormCollection collection, string submitButton)
        {

            model = new RRViewModel();

            ChipHelper ch = new ChipHelper();

            foreach (var item in (new Pocket()).AllChips)
            {
                int intThrow;
                if (Int32.TryParse(collection[item.Type], out intThrow))
                {
                    model.rr.GamePocket.AllChips.Add(new Chips() { Amount = Int32.Parse(collection[item.Type]), Type = item.Type });
                }
            }

            ch.pocketSubtraction(model.User.UserPocket, model.rr.GamePocket);


            if (submitButton.Equals("bet"))
            {
                foreach (var item in model.rr.GamePocket.AllChips)
                {
                    if (item.Amount > 0)
                    {
                        model.Bet.Add(item.Type, item.Amount);
                    }
                }
                model.User.UserPocket.AllChips[0].Amount = 237;
                model.User.UserPocket.AllChips[1].Amount = 314;
                model.User.UserPocket.AllChips[2].Amount = 5798;
                model.User.UserPocket.AllChips[3].Amount = 221;
            }
            else
            {
                model.Bet = HttpContext.Session.Get<IDictionary<string, int>>("bet");

                model.User.UserPocket.AllChips = HttpContext.Session.Get<List<Chips>>("chips");
            }


            if (submitButton == "fire")
            {
                model.rr.NextTurn();
                

                ViewData["game"] = model.rr.status;
                if(model.rr.PlayerGun[model.rr.turn - 1])
                    ViewData["you"] = "BANG!";
                else
                    ViewData["you"] = "*click*";

                if (model.rr.OpponentGun[model.rr.turn - 1])
                    ViewData["they"] = "BANG!";
                else
                    ViewData["they"] = "*click*";

                ViewData["turn"] = model.rr.turn.ToString();
            }
            if (submitButton == "leave")
            {
                model.rr.PlayerLeave();
            }

            ViewData["game"] = model.rr.status;
            HttpContext.Session.Set<IDictionary<string, int>>("bet", model.Bet);

            if (submitButton.Equals("play"))
            {
                model.rr = new RussianRoulette();
                ViewData["game"] = "bet";
                return View(new RRViewModel());
            }

            TempData.Put("model", model);

            HttpContext.Session.Set<List<Chips>>("chips", model.User.UserPocket.AllChips);

            return View(model);
        }
        [HttpGet]
        public IActionResult ChickenFight()
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
            return View(new CFViewModel());
        }
        [HttpPost]
        public IActionResult ChickenFight(CFViewModel model, IFormCollection collection, string submitButton)
        {

            model = new CFViewModel();

            ChipHelper ch = new ChipHelper();

            foreach (var item in (new Pocket()).AllChips)
            {
                int intThrow;
                if (Int32.TryParse(collection[item.Type], out intThrow))
                {
                    model.fight.GamePocket.AllChips.Add(new Chips() { Amount = Int32.Parse(collection[item.Type]), Type = item.Type });
                }
            }

            ch.pocketSubtraction(model.User.UserPocket, model.fight.GamePocket);


            if (submitButton.Equals("bet"))
            {
                foreach (var item in model.fight.GamePocket.AllChips)
                {
                    if (item.Amount > 0)
                    {
                        model.Bet.Add(item.Type, item.Amount);
                    }
                }
                model.User.UserPocket.AllChips[0].Amount = 237;
                model.User.UserPocket.AllChips[1].Amount = 314;
                model.User.UserPocket.AllChips[2].Amount = 5798;
                model.User.UserPocket.AllChips[3].Amount = 221;
            }
            else
            {
                model.Bet = HttpContext.Session.Get<IDictionary<string, int>>("bet");

                model.User.UserPocket.AllChips = HttpContext.Session.Get<List<Chips>>("chips");
            }


            if (submitButton == "chickenA")
                model.fight.PlaceBetA();
            if (submitButton == "chickenB")
                model.fight.PlaceBetB();
            if (submitButton == "chickenA" || submitButton == "chickenB")
            {
                model.fight.Engage();
            }

            ViewData["game"] = model.fight.status;
            HttpContext.Session.Set<IDictionary<string, int>>("bet", model.Bet);

            if (submitButton.Equals("play"))
            {
                return View(new CFViewModel());
                //ViewData["game"] = "bet";
            }

            TempData.Put("model", model);

            HttpContext.Session.Set<List<Chips>>("chips", model.User.UserPocket.AllChips);

            return View(model);
        }
        [HttpGet]
        public IActionResult Bingo()
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
            return View(new BingoViewModel());
        }
        [HttpPost]
        public IActionResult Bingo(BingoViewModel model, IFormCollection collection, string submitButton)
        {
            model = new BingoViewModel();

            ChipHelper ch = new ChipHelper();

            foreach (var item in (new Pocket()).AllChips)
            {
                int intThrow;
                if (Int32.TryParse(collection[item.Type], out intThrow))
                {
                    model.bingo.GamePocket.AllChips.Add(new Chips() { Amount = Int32.Parse(collection[item.Type]), Type = item.Type });
                }
            }

            ch.pocketSubtraction(model.User.UserPocket, model.bingo.GamePocket);


            if (submitButton.Equals("bet"))
            {
                foreach (var item in model.bingo.GamePocket.AllChips)
                {
                    if (item.Amount > 0)
                    {
                        model.Bet.Add(item.Type, item.Amount);
                    }
                }
                model.User.UserPocket.AllChips[0].Amount = 237;
                model.User.UserPocket.AllChips[1].Amount = 314;
                model.User.UserPocket.AllChips[2].Amount = 5798;
                model.User.UserPocket.AllChips[3].Amount = 221;
            }
            else
            {
                model.Bet = HttpContext.Session.Get<IDictionary<string, int>>("bet");

                model.User.UserPocket.AllChips = HttpContext.Session.Get<List<Chips>>("chips");
            }


            if (submitButton == "start")
            {
                model.bingo.CommenceGame();
            }

            ViewData["game"] = model.bingo.status;
            HttpContext.Session.Set<IDictionary<string, int>>("bet", model.Bet);

            if (submitButton.Equals("play"))
            {
                return View(new BingoViewModel());
                //ViewData["game"] = "bet";
            }

            ViewData["game"] = model.bingo.status;

            TempData.Put("model", model);

            HttpContext.Session.Set<List<Chips>>("chips", model.User.UserPocket.AllChips);

            return View(model);
        }
    }
}

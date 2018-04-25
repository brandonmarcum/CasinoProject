using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Casino.Client.Models;
using Casino.Library.Games;
using Casino.Library.Models;
using Casino.Library.Games.Bingo;
using Casino.Library.Games.ChickenFight;
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
             try{
                model.User.UserPocket.AllChips = HttpContext.Session.Get<List<Chips>>("chips");
                model.User.UserPocket.AllChips[0].Amount.Equals(0);
             }
             catch{
                 model.User.UserPocket.AllChips = (new User()).UserPocket.AllChips;
             }
            //HttpContext.Session.Set<User>("currentUser", model.User);
            ViewData["game"] = "bet";

            return View(model);
        }
        [HttpPost]
        public IActionResult BlackJack(BlackJackViewModel model, IFormCollection collection, string submitButton)
        {

            //bring user from session

            model = new BlackJackViewModel();

            ChipHelper ch = new ChipHelper();

             
             try{
                model.User.UserPocket.AllChips = HttpContext.Session.Get<List<Chips>>("chips");
                model.User.UserPocket.AllChips[0].Amount.Equals(0);
             }
             catch{
                 model.User.UserPocket.AllChips = (new User()).UserPocket.AllChips;
             }
            
            foreach(var item in (new Pocket()).AllChips)
            {
                int intThrow;
				if (Int32.TryParse(collection[item.Type], out intThrow))
				{
					model.Blackjack.GamePocket.AllChips.Add(new Chips() { Amount = Int32.Parse(collection[item.Type]), Type = item.Type });
                    try
                    {
                        HttpContext.Session.Get<List<Chips>>("gamechips").Count();
                    }
                    catch
                    {
                        HttpContext.Session.Set<List<Chips>>("gamechips", model.Blackjack.GamePocket.AllChips);
                    }
				}
            }

            model.User.UserPocket = ch.pocketSubtraction(model.User.UserPocket, model.Blackjack.GamePocket);

            if(submitButton.Equals("bet"))
            {
                foreach(var item in model.Blackjack.GamePocket.AllChips)
                {
                    if(item.Amount>0)
                    {
                        model.Bet.Add(item.Type, item.Amount); 
                    }
                }
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
                model.Blackjack.GamePocket.AllChips = HttpContext.Session.Get<List<Chips>>("gamechips");
                model.User.UserPocket = ch.pocketAddition(model.User.UserPocket, model.Blackjack.GamePocket);
                model.User.UserPocket = ch.pocketAddition(model.User.UserPocket, model.Blackjack.GamePocket);
            }

            ViewData["game"] = model.Blackjack.status;
            HttpContext.Session.Set<IDictionary<string, int>>("bet", model.Bet);                

            if(submitButton.Equals("play"))
            {
                model.Blackjack = new Blackjack();
                HttpContext.Session.Set<int>("playerTotal", model.Blackjack.playerTotal);
                HttpContext.Session.Set<int>("dealerTotal", model.Blackjack.dealerTotal);
            }
            //HttpContext.Session.Set<User>("currentUser", model.User);
            HttpContext.Session.Set<List<Chips>>("chips", model.User.UserPocket.AllChips);

            return View(model);
        }

        public IActionResult RockPaperScissors()
        {
            RPSViewModel model = new RPSViewModel();
            User newUser = new User();

            try
            {
                newUser = HttpContext.Session.Get<User>("currentUser");
                newUser.Equals("check");
            }
            catch
            {
                return RedirectToAction("Login", "User");
            }
            try{
                model.User.UserPocket.AllChips = HttpContext.Session.Get<List<Chips>>("chips");
                model.User.UserPocket.AllChips[0].Amount.Equals(0);
             }
             catch{
                 model.User.UserPocket.AllChips = (new User()).UserPocket.AllChips;
             }

            ViewData["game"] = "bet";

            return View(model);
        }

        [HttpPost]
        public IActionResult RockPaperScissors(RPSViewModel model, IFormCollection collection, string submitButton)
        {
            model = new RPSViewModel();

            ChipHelper ch = new ChipHelper();

            try{
                model.User.UserPocket.AllChips = HttpContext.Session.Get<List<Chips>>("chips");
                model.User.UserPocket.AllChips[0].Amount.Equals(0);
             }
             catch{
                 model.User.UserPocket.AllChips = (new User()).UserPocket.AllChips;
             }

            foreach (var item in (new Pocket()).AllChips)
            {
                int intThrow;
                if (Int32.TryParse(collection[item.Type], out intThrow))
                {
                    model.rps.GamePocket.AllChips.Add(new Chips() { Amount = Int32.Parse(collection[item.Type]), Type = item.Type });
                     try
                    {
                        HttpContext.Session.Get<List<Chips>>("gamechips").Count();
                    }
                    catch
                    {
                        HttpContext.Session.Set<List<Chips>>("gamechips", model.rps.GamePocket.AllChips);
                    }
                }
            }

            model.User.UserPocket = ch.pocketSubtraction(model.User.UserPocket, model.rps.GamePocket);


            if (submitButton.Equals("bet"))
            {
                foreach (var item in model.rps.GamePocket.AllChips)
                {
                    if (item.Amount > 0)
                    {
                        model.Bet.Add(item.Type, item.Amount);
                    }
                }
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
                //ViewData["game"] = "bet";
                model.rps.status = "playing";
                return View(new RPSViewModel());
            }
            if(model.rps.status.Equals("win"))
            {
                model.rps.GamePocket.AllChips = HttpContext.Session.Get<List<Chips>>("gamechips");
                model.User.UserPocket = ch.pocketAddition(model.User.UserPocket, model.rps.GamePocket);
                model.User.UserPocket = ch.pocketAddition(model.User.UserPocket, model.rps.GamePocket);
            }

            HttpContext.Session.Set<List<Chips>>("chips", model.User.UserPocket.AllChips);

            return View(model);
        }

        [HttpGet]
        public IActionResult Slots()
        {
            SlotsViewModel.Slots = new Slots();
            SlotsViewModel model = new SlotsViewModel();
            User newUser = new User();

            try
            {
                newUser = HttpContext.Session.Get<User>("currentUser");
                newUser.Equals("check");
            }
            catch
            {
                return RedirectToAction("Login", "User");
            }
            try{
                model.User.UserPocket.AllChips = HttpContext.Session.Get<List<Chips>>("chips");
                model.User.UserPocket.AllChips[0].Amount.Equals(0);
             }
             catch{
                 model.User.UserPocket.AllChips = (new User()).UserPocket.AllChips;
             }

            ViewData["game"] = "bet";

            return View(model);
        }
        [HttpPost]
        public IActionResult Slots(SlotsViewModel model, IFormCollection collection, string submitButton)
        {
            model = new SlotsViewModel();

            ChipHelper ch = new ChipHelper();
            try{
                model.User.UserPocket.AllChips = HttpContext.Session.Get<List<Chips>>("chips");
                model.User.UserPocket.AllChips[0].Amount.Equals(0);
             }
             catch{
                 model.User.UserPocket.AllChips = (new User()).UserPocket.AllChips;
             }

            foreach (var item in (new Pocket()).AllChips)
            {
                int intThrow;
                if (Int32.TryParse(collection[item.Type], out intThrow))
                {
                    SlotsViewModel.Slots.GamePocket.AllChips.Add(new Chips() { Amount = Int32.Parse(collection[item.Type]), Type = item.Type });
                     try
                    {
                        HttpContext.Session.Get<List<Chips>>("gamechips").Count();
                    }
                    catch
                    {
                        HttpContext.Session.Set<List<Chips>>("gamechips", SlotsViewModel.Slots.GamePocket.AllChips);
                    }
                }
            }

            ch.pocketSubtraction(model.User.UserPocket, SlotsViewModel.Slots.GamePocket);


            if (submitButton.Equals("bet"))
            {
                foreach (var item in SlotsViewModel.Slots.GamePocket.AllChips)
                {
                    if (item.Amount > 0)
                    {
                        model.Bet.Add(item.Type, item.Amount);
                    }
                }
            }
            else
            {
                model.Bet = HttpContext.Session.Get<IDictionary<string, int>>("bet");
                model.User.UserPocket.AllChips = HttpContext.Session.Get<List<Chips>>("chips");
            }

            try
            {
            model = TempData.Get<SlotsViewModel>("slots");
            string k = SlotsViewModel.Slots.status;
            }
            catch
            {
                model = new SlotsViewModel();
            }
            if(SlotsViewModel.Slots.status.Equals("win"))
            {
                SlotsViewModel.Slots.GamePocket.AllChips = HttpContext.Session.Get<List<Chips>>("gamechips");
                model.User.UserPocket = ch.pocketAddition(model.User.UserPocket, SlotsViewModel.Slots.GamePocket);
                model.User.UserPocket = ch.pocketAddition(model.User.UserPocket, SlotsViewModel.Slots.GamePocket);
            }

            //ViewData["userChips"] = newUser.UserPocket.AllChips;

            ViewData["left"] = SlotsViewModel.Slots.left;
            ViewData["middle"] = SlotsViewModel.Slots.middle;
            ViewData["right"] = SlotsViewModel.Slots.right;

            if(submitButton.Equals("left"))
            {
                SlotsViewModel.Slots.SetLeft();
            }
            if (submitButton.Equals("middle"))
            {
                SlotsViewModel.Slots.SetMiddle();
            }
            if (submitButton.Equals("right"))
            {
                SlotsViewModel.Slots.SetRight();
            }
            if (submitButton.Equals("play"))
            {
                SlotsViewModel.Slots = new Slots();
                model = new SlotsViewModel();
                User newUser = new User();

                try
                {
                    newUser = HttpContext.Session.Get<User>("currentUser");
                    newUser.Equals("check");
                }
                catch
                {
                    return RedirectToAction("Login", "User");
                }
                //put in chips
                

                //ViewData["game"] = "bet";

                return View(model);
            }

            if(SlotsViewModel.Slots.left != 0 && SlotsViewModel.Slots.middle != 0 && SlotsViewModel.Slots.right != 0)
                SlotsViewModel.Slots.status = SlotsViewModel.Slots.CheckForMatch();

            ViewData["status"] = SlotsViewModel.Slots.status;
            //HttpContext.Session.Set<List<Chips>>("chips", model.User.UserPocket.AllChips);

            ViewData["game"] = SlotsViewModel.Slots.status;
            return View(model);
        }

        
        [HttpGet]
        public IActionResult RussianRoulette()
        {
            RRViewModel.rr = new RussianRoulette();
            RRViewModel model = new RRViewModel();
            User newUser = new User();

            try{
				 newUser = HttpContext.Session.Get<User>("currentUser");
				 newUser.Equals("check");
			 }
			 catch
			 {
				 return RedirectToAction("Login", "User");
			 }
             try{
                model.User.UserPocket.AllChips = HttpContext.Session.Get<List<Chips>>("chips");
                model.User.UserPocket.AllChips[0].Amount.Equals(0);
             }
             catch{
                 model.User.UserPocket.AllChips = (new User()).UserPocket.AllChips;
             }

            ViewData["game"] = "bet";

            return View(model);
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
                    RRViewModel.rr.GamePocket.AllChips.Add(new Chips() { Amount = Int32.Parse(collection[item.Type]), Type = item.Type });
                     try
                    {
                        HttpContext.Session.Get<List<Chips>>("gamechips").Count();
                    }
                    catch
                    {
                        HttpContext.Session.Set<List<Chips>>("gamechips", RRViewModel.rr.GamePocket.AllChips);
                    }
                }
            }

            ch.pocketSubtraction(model.User.UserPocket, RRViewModel.rr.GamePocket);


            if (submitButton.Equals("bet"))
            {
                foreach (var item in RRViewModel.rr.GamePocket.AllChips)
                {
                    if (item.Amount > 0)
                    {
                        model.Bet.Add(item.Type, item.Amount);
                    }
                }
            }
            else
            {
                model.Bet = HttpContext.Session.Get<IDictionary<string, int>>("bet");

                model.User.UserPocket.AllChips = HttpContext.Session.Get<List<Chips>>("chips");
            }


            if (submitButton == "fire")
            {
                RRViewModel.rr.NextTurn();
                

                ViewData["game"] = RRViewModel.rr.status;
                if(RRViewModel.rr.PlayerGun[RRViewModel.rr.turn - 1])
                    ViewData["you"] = "BANG!";
                else
                    ViewData["you"] = "*click*";

                if (RRViewModel.rr.OpponentGun[RRViewModel.rr.turn - 1])
                    ViewData["they"] = "BANG!";
                else
                    ViewData["they"] = "*click*";

                ViewData["turn"] = RRViewModel.rr.turn.ToString();
            }
            if (submitButton == "leave")
            {
                RRViewModel.rr.PlayerLeave();
            }

            ViewData["game"] = RRViewModel.rr.status;
            HttpContext.Session.Set<IDictionary<string, int>>("bet", model.Bet);

            if (submitButton.Equals("play"))
            {
                RRViewModel.rr = new RussianRoulette();
                RRViewModel x = new RRViewModel();
                User newUser = new User();

                try
                {
                    newUser = HttpContext.Session.Get<User>("currentUser");
                    newUser.Equals("check");
                }
                catch
                {
                    return RedirectToAction("Login", "User");
                }

                ViewData["game"] = "bet";

                //chips
                HttpContext.Session.Set<List<Chips>>("chips", model.User.UserPocket.AllChips);


                return View(x);
            }
            if(RRViewModel.rr.status.Equals("win"))
            {
                RRViewModel.rr.GamePocket.AllChips = HttpContext.Session.Get<List<Chips>>("gamechips");
                model.User.UserPocket = ch.pocketAddition(model.User.UserPocket, RRViewModel.rr.GamePocket);
                model.User.UserPocket = ch.pocketAddition(model.User.UserPocket, RRViewModel.rr.GamePocket);
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult ChickenFight()
        {
            CFViewModel.fight = new Fight();
            CFViewModel model = new CFViewModel();
            User newUser = new User();

            try{
				 newUser = HttpContext.Session.Get<User>("currentUser");
				 newUser.Equals("check");
			 }
			 catch
			 {
				 return RedirectToAction("Login", "User");
			 }
             try{
                model.User.UserPocket.AllChips = HttpContext.Session.Get<List<Chips>>("chips");
                model.User.UserPocket.AllChips[0].Amount.Equals(0);
             }
             catch{
                 model.User.UserPocket.AllChips = (new User()).UserPocket.AllChips;
             }

            ViewData["game"] = "bet";

            return View(model);
        }
        [HttpPost]
        public IActionResult ChickenFight(CFViewModel model, IFormCollection collection, string submitButton)
        {

            model = new CFViewModel();

            ChipHelper ch = new ChipHelper();

            try{
                model.User.UserPocket.AllChips = HttpContext.Session.Get<List<Chips>>("chips");
                model.User.UserPocket.AllChips[0].Amount.Equals(0);
             }
             catch{
                 model.User.UserPocket.AllChips = (new User()).UserPocket.AllChips;
             }

            foreach (var item in (new Pocket()).AllChips)
            {
                int intThrow;
                if (Int32.TryParse(collection[item.Type], out intThrow))
                {
                    CFViewModel.fight.GamePocket.AllChips.Add(new Chips() { Amount = Int32.Parse(collection[item.Type]), Type = item.Type });
                     try
                    {
                        HttpContext.Session.Get<List<Chips>>("gamechips").Count();
                    }
                    catch
                    {
                        HttpContext.Session.Set<List<Chips>>("gamechips", CFViewModel.fight.GamePocket.AllChips);
                    }
                }
            }

            ch.pocketSubtraction(model.User.UserPocket, CFViewModel.fight.GamePocket);


            if (submitButton.Equals("bet"))
            {
                foreach (var item in CFViewModel.fight.GamePocket.AllChips)
                {
                    if (item.Amount > 0)
                    {
                        model.Bet.Add(item.Type, item.Amount);
                    }
                }

            }
            else
            {
                model.Bet = HttpContext.Session.Get<IDictionary<string, int>>("bet");

                model.User.UserPocket.AllChips = HttpContext.Session.Get<List<Chips>>("chips");
            }


            if (submitButton == "chickenA")
                CFViewModel.fight.PlaceBetA();
            if (submitButton == "chickenB")
                CFViewModel.fight.PlaceBetB();
            if (submitButton == "chickenA" || submitButton == "chickenB")
            {
                CFViewModel.fight.Engage();
            }

            ViewData["game"] = CFViewModel.fight.status;
            HttpContext.Session.Set<IDictionary<string, int>>("bet", model.Bet);

            if (submitButton.Equals("play"))
            {
                CFViewModel.fight = new Fight();
                CFViewModel x = new CFViewModel();
                User newUser = new User();

                try
                {
                    newUser = HttpContext.Session.Get<User>("currentUser");
                    newUser.Equals("check");
                }
                catch
                {
                    return RedirectToAction("Login", "User");
                }
                
                return View(x);
            }
            if(CFViewModel.fight.status.Equals("win"))
            {
                CFViewModel.fight.GamePocket.AllChips = HttpContext.Session.Get<List<Chips>>("gamechips");
                model.User.UserPocket = ch.pocketAddition(model.User.UserPocket, CFViewModel.fight.GamePocket);
                model.User.UserPocket = ch.pocketAddition(model.User.UserPocket, CFViewModel.fight.GamePocket);
            }
            //TempData.Put("model", model);

            HttpContext.Session.Set<List<Chips>>("chips", model.User.UserPocket.AllChips);

            return View(model);
        }
        [HttpGet]
        public IActionResult Bingo()
        {
            BingoViewModel.bingo = new Bingo();
            BingoViewModel model = new BingoViewModel();
            User newUser = new User();

            try{
				 newUser = HttpContext.Session.Get<User>("currentUser");
				 newUser.Equals("check");
			 }
			 catch
			 {
				 return RedirectToAction("Login", "User");
			 }
             try{
                model.User.UserPocket.AllChips = HttpContext.Session.Get<List<Chips>>("chips");
                model.User.UserPocket.AllChips[0].Amount.Equals(0);
             }
             catch{
                 model.User.UserPocket.AllChips = (new User()).UserPocket.AllChips;
             }

            ViewData["game"] = "bet";

            return View(model);
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
                    BingoViewModel.bingo.GamePocket.AllChips.Add(new Chips() { Amount = Int32.Parse(collection[item.Type]), Type = item.Type });
                     try
                    {
                        HttpContext.Session.Get<List<Chips>>("gamechips").Count();
                    }
                    catch
                    {
                        HttpContext.Session.Set<List<Chips>>("gamechips", BingoViewModel.bingo.GamePocket.AllChips);
                    }
                }
            }

            ch.pocketSubtraction(model.User.UserPocket, BingoViewModel.bingo.GamePocket);


            if (submitButton.Equals("bet"))
            {
                foreach (var item in BingoViewModel.bingo.GamePocket.AllChips)
                {
                    if (item.Amount > 0)
                    {
                        model.Bet.Add(item.Type, item.Amount);
                    }
                }
            }
            else
            {
                model.Bet = HttpContext.Session.Get<IDictionary<string, int>>("bet");

                model.User.UserPocket.AllChips = HttpContext.Session.Get<List<Chips>>("chips");
            }


            if (submitButton == "start")
            {
                BingoViewModel.bingo.CommenceGame();
            }

            ViewData["game"] = BingoViewModel.bingo.status;
            HttpContext.Session.Set<IDictionary<string, int>>("bet", model.Bet);

            if (submitButton.Equals("play"))
            {
                BingoViewModel.bingo = new Bingo();
                BingoViewModel x = new BingoViewModel();
                User newUser = new User();

                try
                {
                    newUser = HttpContext.Session.Get<User>("currentUser");
                    newUser.Equals("check");
                }
                catch
                {
                    return RedirectToAction("Login", "User");
                }
                return View(x);
            }
            if(BingoViewModel.bingo.status.Equals("win"))
            {
                BingoViewModel.bingo.GamePocket.AllChips = HttpContext.Session.Get<List<Chips>>("gamechips");
                model.User.UserPocket = ch.pocketAddition(model.User.UserPocket, BingoViewModel.bingo.GamePocket);
                model.User.UserPocket = ch.pocketAddition(model.User.UserPocket, BingoViewModel.bingo.GamePocket);
            }

            ViewData["game"] = BingoViewModel.bingo.status;
            ViewData["roll"] = BingoViewModel.bingo.numberChosen;

            TempData.Put("model", model);

            HttpContext.Session.Set<List<Chips>>("chips", model.User.UserPocket.AllChips);

            return View(model);
        }
    }
}

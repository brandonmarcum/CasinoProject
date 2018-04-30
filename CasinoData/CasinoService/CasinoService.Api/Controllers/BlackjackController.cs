using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasinoService.Api.Games;
using CasinoService.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CasinoService.Api.Controllers
{
    
    [Route("api/[controller]/[action]")]
    public class BlackjackController : Controller
    {
        [HttpGet]
        [ActionName("blackjackgameget")]
        public async Task<Blackjack> BlackjackGetAsync()
        {
            Blackjack blackjack = HttpContext.Session.Get<Blackjack>("blackjack");

            HttpContext.Session.Set<Blackjack>("blackjack", blackjack);

            return await Task.Run(() => blackjack);
        }
        [HttpPost]
        [ActionName("blackjackgamepost")]
        public void BlackjackPost([FromBody]Blackjack blackjack)
        {
            HttpContext.Session.Set<Blackjack>("blackjack", blackjack);
        }
    }
}

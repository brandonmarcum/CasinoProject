using System;
using System.Collections.Generic;
using Casino.Library;
using Casino.Library.Games.ChickenFight;
using Casino.Library.Models;
using Newtonsoft.Json;

namespace Casino.Client.Models
{
    public class CFViewModel : BetViewModel
    {
        public Fight fight { get; set; }

        public CFViewModel()
        {
            fight = new Fight();
            User = new User();
            Bet = new Dictionary<string, int>();
        }
    }
}

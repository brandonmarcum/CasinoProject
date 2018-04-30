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
        public static Fight fight { get; set; }

        public CFViewModel()
        {
            User = new User();
            Bet = new Dictionary<string, int>();
        }
    }
}

using System;
using System.Collections.Generic;
using Casino.Library;
using Casino.Library.Games;
using Casino.Library.Models;

namespace Casino.Client.Models
{
    public class RRViewModel : BetViewModel
    {
        public static RussianRoulette rr { get; set; }

        public RRViewModel()
        {
            User = new User();
            Bet = new Dictionary<string, int>();
        }
    }
}

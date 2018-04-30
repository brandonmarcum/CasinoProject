using System.Collections.Generic;
using Casino.Library.Games;
using Casino.Library.Models;

namespace Casino.Client.Models
{
    public class SlotsViewModel: BetViewModel
    {
        public static Slots Slots{ get; set; }
        
        public SlotsViewModel()
        {
            User = new User();
            Bet = new Dictionary<string, int>();
        }
    }
}
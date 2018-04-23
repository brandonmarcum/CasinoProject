using System.Collections.Generic;
using Casino.Library.Games;
using Casino.Library.Models;

namespace Casino.Client.Models
{
    public class SlotsViewModel: BetViewModel
    {
        public Slots Slots{ get; set; }
        
        public SlotsViewModel()
        {
            Slots = new Slots();
            User = new User();
            Bet = new Dictionary<string, int>();
        }
    }
}
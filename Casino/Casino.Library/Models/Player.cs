using System.Collections.Generic;

namespace Casino.Library.Models
{
    public class Player : User
    {
        public Player()
        {
        }
        public List<CasinoGame> GameHistory{ get; set; }
        public int NumberOfWins{ get; set; }
        public int NumberOfLosses{ get; set; }

        //player luck may be a way to track win/loss ratio and catch scammers or fix bugs
        public double PlayerLuck{ get; set; }
        public double GameTime{ get; set; }
        public double Earnings{ get; set; }

    }
}
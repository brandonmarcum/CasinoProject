using System;
using System.Collections.Generic;
using System.Text;
using Casino.Library.Models;

namespace Casino.Library.Games
{
    public class Blackjack
    {
        public string GameName = "Blackjack";
        public int playerTotal;
        public int dealerTotal;
        public bool playerStand;
        public bool dealerStand;
        public string status;
        public Pocket GamePocket{ get; set; }

        public Blackjack()
        {
            playerTotal = 0;
            dealerTotal = 0;
            status = "playing";
            playerStand = false;
            dealerStand = false;
            GamePocket = new Pocket();
            GamePocket.AllChips = new List<Chips>();
        }
        
        public void NextTurn()
        {
            if(!playerStand)
                status = PlayerHit();
            if(status == "playing" && !dealerStand)
                status = DealerHit();
            if (status == "playing" && dealerStand && playerStand)
                status = DecideOnWinner();

        }

        public void PlayerStand()
        {
            playerStand = true;
        }

        public string PlayerHit()
        {
            playerTotal += GenerateCard();

            return Check21("player");
        }

        public string DealerHit()
        {
            dealerTotal += GenerateCard();

            CheckForDealerStand();

            return Check21("dealer");
        }

        public void CheckForDealerStand()
        {
            if ((dealerTotal >= 16) && (dealerTotal <= 21) && (dealerTotal >= playerTotal))
                dealerStand = true;
        }

        public int GenerateCard()
        {
            Random rand = new Random();
            return rand.Next(1, 11);
        }

        public string DecideOnWinner()
        {
            if (playerTotal > dealerTotal)
                return "win";
            else
                return "lose";
        }

        public string Check21(string person)
        {
            if(person == "player")
            {
                if (playerTotal > 21)
                    return "lose";
                if (playerTotal == 21)
                    return "win";
                if (playerTotal > dealerTotal && dealerStand)
                    return "win";
            }
            if(person == "dealer")
            {
                if (dealerTotal > 21)
                    return "win";
                if (dealerTotal == 21)
                    return "lose";
                if (playerTotal <= dealerTotal && playerStand)
                    return "lose";
            }
            
            return "playing";
        }


    }
}

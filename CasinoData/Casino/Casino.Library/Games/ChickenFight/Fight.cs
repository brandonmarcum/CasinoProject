using System;
using System.Collections.Generic;
using System.Text;
using Casino.Library.Models;

namespace Casino.Library.Games.ChickenFight
{
    public class Fight
    {
        public Chicken chickenA;
        public Chicken chickenB;
        public string status;
        public Pocket GamePocket { get; set; }

        public Fight()
        {
            chickenA = new Chicken();
            chickenB = new Chicken();
            status = "choosing";
            GamePocket = new Pocket();
            GamePocket.AllChips = new List<Chips>();
        }

        public void PlaceBetA()
        {
            chickenA.Betted = true;
            status = "playing";
        }

        public void PlaceBetB()
        {
            chickenB.Betted = true;
            status = "playing";
        }

        public string Engage()
        {

            while (chickenA.Standing && chickenB.Standing)
            {
                if(chickenA.Betted)
                {
                    ChickenBTurn();
                    ChickenATurn();
                }
                else if (chickenB.Betted)
                {
                    ChickenATurn();
                    ChickenATurn();
                }
            }

            return CheckWin();
        }

        public string CheckWin()
        {
            if (chickenA.Betted && !chickenA.Standing)
                status = "lose";
            else if (chickenB.Betted && !chickenB.Standing)
                status = "lose";
            else
                status = "win";

            return status;
        }

        public void ChickenATurn()
        {
            if (!chickenB.Evade() && chickenA.Standing)
                chickenB.DecreaseHealth(chickenA.Attack);

            if(chickenB.Health <= 0)
            {
                chickenB.Standing = false;
                chickenB.Health = 0;
            }
        }

        public void ChickenBTurn()
        {
            if (!chickenA.Evade() && chickenB.Standing)
                chickenA.DecreaseHealth(chickenB.Attack);

            if (chickenA.Health <= 0)
            {
                chickenA.Standing = false;
                chickenA.Health = 0;
            }
        }
    }
}

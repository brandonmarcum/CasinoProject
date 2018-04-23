using System;
using System.Collections.Generic;
using System.Text;

namespace CasinoService.Api.Games.ChickenFight
{
    public class Fight
    {
        public Chicken chickenA;
        public Chicken chickenB;

        public Fight()
        {
            chickenA = new Chicken();
            chickenB = new Chicken();
        }

        public void PlaceBetA()
        {
            chickenA.Betted = true;
        }

        public void PlaceBetB()
        {
            chickenB.Betted = true;
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
                return "lose";
            else if (chickenB.Betted && !chickenB.Standing)
                return "lose";
            else
                return "win";
        }

        public void ChickenATurn()
        {
            if (!chickenB.Evade() && chickenA.Standing)
                chickenB.DecreaseHealth(chickenA.Attack);
        }

        public void ChickenBTurn()
        {
            if (!chickenA.Evade() && chickenB.Standing)
                chickenA.DecreaseHealth(chickenB.Attack);
        }
    }
}

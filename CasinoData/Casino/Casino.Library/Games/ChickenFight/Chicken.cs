using System;
using System.Collections.Generic;
using System.Text;

namespace Casino.Library.Games.ChickenFight
{
    public class Chicken
    {
        public int Health;
        public int Attack;
        public int Evasion;
        public int StatTotal;
        public bool Betted;
        public bool Standing;

        public Chicken()
        {
            GenerateStats();

            Standing = true;
            Betted = false;
        }

        public int DecreaseHealth(int damage)
        {
            Health -= damage;

            if (Health <= 0)
                Standing = false;

            return Health;
        }

        public bool Evade()
        {
            Random rand = new Random();

            if (Evasion < rand.Next(1, int.Parse((StatTotal * 1.5).ToString())))
                return false;
            
            return true;
        }

        public void GenerateStats()
        {
            Random rand = new Random();

            StatTotal = 400;
            int x = int.Parse((StatTotal / 1.6).ToString());
            Health = rand.Next(5, x);
            int j = int.Parse(((StatTotal - x) / 2).ToString());
            Attack = rand.Next(5, j);
            Evasion = StatTotal - (Health + Attack);
        }

    }
}

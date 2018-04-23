using System;
using System.Collections.Generic;
using System.Text;

namespace CasinoService.Api.Games
{
    public class RussianRoulette
    {
        public string GameName = "Russian Roulette";
        public List<bool> PlayerGun;
        public List<bool> OpponentGun;
        public int turn;
        public string status;

        public RussianRoulette()
        {
            PlayerGun = new List<bool>();
            OpponentGun = new List<bool>();

            LoadGuns();
            turn = 0;
            status = "playing";
        }

        public void LoadGuns()
        {
            for(int i = 0; i < 6; i++)
            {
                PlayerGun.Add(false);
                OpponentGun.Add(false);
            }

            Random random = new Random();
            PlayerGun[random.Next(0, 6)] = true;
            Random random2 = new Random();
            OpponentGun[random2.Next(0, 6)] = true;
        }

        public void NextTurn()
        {
            status = PlayerFire();
            if(status == "playing")
                status = OpponentFire();
            turn++;
        }

        public string PlayerFire()
        {
            if (PlayerGun[turn])
                return "lose";

            return "playing";
        }

        public string OpponentFire()
        {
            if (OpponentGun[turn])
                return "win";

            return "playing";
        }

    }
}

using System;
using Xunit;
using Casino.Library.Games;

namespace Casino.Test
{
    public class RussianRouletteTest
    {
        RussianRoulette rr = new RussianRoulette();

        [Fact]
        public void T01Access()
        {
            RussianRoulette rr = new RussianRoulette();
            Assert.True(rr.status == "playing");
        }

        [Fact]
        public void T02LoadGuns()
        {
            RussianRoulette rr = new RussianRoulette();

            Assert.True(rr.PlayerGun.Count == 6);
            Assert.True(rr.OpponentGun.Count == 6);

            int u = 0;
            int l = 0;
            for (int i = 0; i < rr.PlayerGun.Count; i++)
            {
                if (rr.PlayerGun[i])
                    l++;
                else
                    u++;
            }
            Assert.Equal(1, l);
            Assert.Equal(5, u);

            u = 0;
            l = 0;
            for (int i = 0; i < rr.OpponentGun.Count; i++)
            {
                if (rr.OpponentGun[i])
                    l++;
                else
                    u++;
            }
            Assert.True(l == 1);
            Assert.True(u == 5);
        }

        [Fact]
        public void T03PlayerFireLose()
        {
            RussianRoulette rr = new RussianRoulette();

            string exit = "";
            while(exit != "lose")
            {
                exit = rr.PlayerFire();
                rr.turn++;
            }
        }

        [Fact]
        public void T04OpponentFireLose()
        {
            RussianRoulette rr = new RussianRoulette();

            string exit = "";
            while (exit != "win")
            {
                exit = rr.OpponentFire();
                rr.turn++;
            }
        }

        [Fact]
        public void T05SkipOpponent()
        {
            RussianRoulette rr = new RussianRoulette();

            int bullet = 0;
            for (int i = 0; i < rr.PlayerGun.Count; i++)
            {
                if (rr.PlayerGun[i])
                    bullet = i;
            }
            int obullet = 0;
            for (int i = 0; i < rr.OpponentGun.Count; i++)
            {
                if (rr.OpponentGun[i])
                    obullet = i;
            }

            rr.turn = bullet;
            rr.status = rr.PlayerFire();
            rr.turn = obullet;
            if (rr.status == "playing")
                rr.status = rr.OpponentFire();


            Assert.True(rr.status == "lose");
        }

        [Fact]
        public void T06GoldenTest()
        {
            for(int i = 0; i > 500; i++)
            {
                RussianRoulette rr = new RussianRoulette();
                int j = 0;

                while (rr.status == "playing")
                {
                    rr.NextTurn();
                    j++;
                    Assert.Equal(j, rr.turn);
                }

                Assert.True(rr.status == "win" || rr.status == "lose");
            }
        }
    }
}
